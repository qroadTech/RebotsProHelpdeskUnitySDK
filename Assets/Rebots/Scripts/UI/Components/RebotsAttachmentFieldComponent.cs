using HelpDesk.Sdk.Common.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Rebots.HelpDesk
{
    public class RebotsAttachmentFieldComponent
    {
        const string FieldLabel = "rebots-field-label";
        const string RequiredFieldLabel = "rebots-required";
        const string ChooseFileButton = "rebots-choose-file-button";
        const string NoFileContainer = "rebots-no-file-container";
        const string FileList = "rebots-file-list-container";
        const string ValidationLabel = "rebots-validation-label";

        const string FileNameLabel = "rebots-file-name-label";
        const string FileSizeLabel = "rebots-file-size-label";
        const string FileRemoveButton = "rebots-file-remove-button";

        Label m_FieldLabel;
        Label m_RequiredFieldLabel;
        Button m_ChooseFileButton;
        VisualElement m_NoFileContainer;
        VisualElement m_FileList;
        Label m_ValidationLabel;

        private TicketCategoryInputField m_csCategoryField;
        private VisualTreeAsset m_fileAsset;
        private List<RebotsTicketAttachment> m_attachments;
        private string validationComment;

        private List<string> ImageType = new List<string>() { "png", "jpeg", "jpg" };

        public RebotsAttachmentFieldComponent(TicketCategoryInputField csCategoryField, VisualTreeAsset fileAsset, string[] validationComment)
        {
            m_csCategoryField = csCategoryField;
            m_fileAsset = fileAsset;
            m_attachments = new List<RebotsTicketAttachment>();
            this.validationComment = (validationComment != null) ? validationComment[0] : "";
        }

        public void SetVisualElement(TemplateContainer attachmentFieldUIElement)
        {
            if (attachmentFieldUIElement == null)
            {
                return;
            }

            m_FieldLabel = attachmentFieldUIElement.Q<Label>(FieldLabel);
            m_RequiredFieldLabel = attachmentFieldUIElement.Q<Label>(RequiredFieldLabel);
            m_ChooseFileButton = attachmentFieldUIElement.Q<Button>(ChooseFileButton);
            m_NoFileContainer = attachmentFieldUIElement.Q(NoFileContainer);
            m_FileList = attachmentFieldUIElement.Q(FileList);
            m_ValidationLabel = attachmentFieldUIElement.Q<Label>(ValidationLabel);

            m_NoFileContainer.style.display = DisplayStyle.Flex;
            m_FileList.Clear();
        }

        public void SetFieldData(TemplateContainer attachmentFieldUIElement)
        {
            if (attachmentFieldUIElement == null)
            {
                return;
            }

            m_FieldLabel.text = m_csCategoryField.text;
            m_ChooseFileButton?.RegisterCallback<ClickEvent>(evt => ClickChooseFile());

            m_RequiredFieldLabel.style.display = (m_csCategoryField.isRequire) ? DisplayStyle.Flex : DisplayStyle.None;

            m_ValidationLabel.text = validationComment;
            m_ValidationLabel.style.display = DisplayStyle.None;
        }

        private void ClickChooseFile()
        {
#if UNITY_EDITOR
            string path = EditorUtility.OpenFilePanelWithFilters("", "", new string[] { "Image Files", "png, jpeg, jpg" });
            if (true)
            {
                SetAttachmentFile(path);
            }
#elif UNITY_IOS || UNITY_ANDROID
            if (NativeGallery.CanSelectMultipleMediaTypesFromGallery())
            {
                NativeGallery.Permission permission = NativeGallery.GetMixedMediaFromGallery((path) =>
                {
                    if (path != null)
                    {
                        SetAttachmentFile(path);
                    }
                }, (NativeGallery.MediaType.Image | NativeGallery.MediaType.Video), "Select an image");
            }
#endif
        }

        private void SetAttachmentFile(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                if (path.Equals("error"))
                {
                    return;
                }
                if (path.Equals("error_res"))
                {
                    return;
                }

                var originPathList = m_attachments.Select(x => x.originPath).ToList();
                if (m_attachments.Count == 0 || !originPathList.Contains(path))
                {
                    var itemType = Path.GetExtension(path).Replace(".", "");
                    itemType = ImageType.Contains(itemType) ? "image/" + itemType : "";
                    var file = new FileInfo(path);

                    FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                    var attachmentInfo = new RebotsTicketAttachment()
                    {
                        originPath = path,
                        filename = Path.GetFileName(path),
                        content = fileStream,
                        fileType = itemType,
                        fileSize = file.Length
                    };
                    m_attachments.Add(attachmentInfo);

                    TemplateContainer fileUIElement = m_fileAsset.Instantiate();
                    var m_FileNameLabel = fileUIElement.Q<Label>(FileNameLabel);
                    var m_FileSizeLabel = fileUIElement.Q<Label>(FileSizeLabel);
                    var m_FileRemoveButton = fileUIElement.Q<Button>(FileRemoveButton);

                    m_FileNameLabel.text = attachmentInfo.filename;
                    m_FileSizeLabel.text = string.Format("({0:N2}KB)", (double)attachmentInfo.fileSize / 1024);
                    m_FileRemoveButton?.RegisterCallback<ClickEvent>(evt => RemoveAttachmentFile(attachmentInfo, fileUIElement));

                    m_FileList.Add(fileUIElement);

                    m_NoFileContainer.style.display = DisplayStyle.None;

                    if (m_attachments.Count > 2 && m_FileList.childCount > 2)
                    {
                        m_ChooseFileButton.SetEnabled(false);
                    }
                }
            }
        }

        private void RemoveAttachmentFile(RebotsTicketAttachment attachmentInfo, TemplateContainer fileUIElement)
        {
            m_attachments.Remove(attachmentInfo);
            m_FileList.Remove(fileUIElement);

            if (m_attachments.Count < 3 && m_FileList.childCount < 3)
            {
                m_ChooseFileButton.SetEnabled(true);
            }
        }

        public bool CheckFieldValid()
        {
            if (m_csCategoryField.isRequire && (m_attachments == null || m_attachments.Count == 0))
            {
                m_ValidationLabel.style.display = DisplayStyle.Flex;
                return false;
            }
            else
            {
                m_ValidationLabel.style.display = DisplayStyle.None;
                return true;
            }
        }

        public List<RebotsTicketAttachment> GetFieldValue()
        {
            return m_attachments;
        }
    }
}
