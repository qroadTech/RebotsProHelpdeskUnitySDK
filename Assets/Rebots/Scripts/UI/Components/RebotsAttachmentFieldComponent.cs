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

        private TicketCategoryInputField csCategoryField;
        private VisualTreeAsset fileAsset;
        private List<RebotsTicketAttachment> attachments;
        private string validationComment;

        private readonly List<string> ImageType = new List<string>() { "png", "jpeg", "jpg" };

        public RebotsAttachmentFieldComponent(TicketCategoryInputField csCategoryField, VisualTreeAsset fileAsset, string[] validationComment)
        {
            this.csCategoryField = csCategoryField;
            this.fileAsset = fileAsset;
            this.attachments = new List<RebotsTicketAttachment>();
            this.validationComment = (validationComment != null) ? validationComment[0] : "";
        }

        public void SetVisualElement(TemplateContainer attachmentFieldUIElement)
        {
            if (attachmentFieldUIElement == null)
            {
                return;
            }

            this.m_FieldLabel = attachmentFieldUIElement.Q<Label>(FieldLabel);
            this.m_RequiredFieldLabel = attachmentFieldUIElement.Q<Label>(RequiredFieldLabel);
            this.m_ChooseFileButton = attachmentFieldUIElement.Q<Button>(ChooseFileButton);
            this.m_NoFileContainer = attachmentFieldUIElement.Q(NoFileContainer);
            this.m_FileList = attachmentFieldUIElement.Q(FileList);
            this.m_ValidationLabel = attachmentFieldUIElement.Q<Label>(ValidationLabel);

            this.m_NoFileContainer.style.display = DisplayStyle.Flex;
            this.m_FileList.Clear();
        }

        public void SetFieldData(TemplateContainer attachmentFieldUIElement)
        {
            if (attachmentFieldUIElement == null)
            {
                return;
            }

            this.m_FieldLabel.text = this.csCategoryField.text;
            this.m_ChooseFileButton?.RegisterCallback<ClickEvent>(evt => ClickChooseFile());

            this.m_RequiredFieldLabel.style.display = (this.csCategoryField.isRequire) ? DisplayStyle.Flex : DisplayStyle.None;

            this.m_ValidationLabel.text = this.validationComment;
            this.m_ValidationLabel.style.display = DisplayStyle.None;
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

                var originPathList = this.attachments.Select(x => x.originPath).ToList();
                if (this.attachments.Count == 0 || !originPathList.Contains(path))
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
                    this.attachments.Add(attachmentInfo);

                    TemplateContainer fileUIElement = this.fileAsset.Instantiate();
                    var m_FileNameLabel = fileUIElement.Q<Label>(FileNameLabel);
                    var m_FileSizeLabel = fileUIElement.Q<Label>(FileSizeLabel);
                    var m_FileRemoveButton = fileUIElement.Q<Button>(FileRemoveButton);

                    m_FileNameLabel.text = attachmentInfo.filename;
                    m_FileSizeLabel.text = string.Format("({0:N2}KB)", (double)attachmentInfo.fileSize / 1024);
                    m_FileRemoveButton?.RegisterCallback<ClickEvent>(evt => RemoveAttachmentFile(attachmentInfo, fileUIElement));

                    this.m_FileList.Add(fileUIElement);

                    this.m_NoFileContainer.style.display = DisplayStyle.None;

                    if (this.attachments.Count > 2 && this.m_FileList.childCount > 2)
                    {
                        this.m_ChooseFileButton.SetEnabled(false);
                    }
                }
            }
        }

        private void RemoveAttachmentFile(RebotsTicketAttachment attachmentInfo, TemplateContainer fileUIElement)
        {
            this.attachments.Remove(attachmentInfo);
            this.m_FileList.Remove(fileUIElement);

            if (this.attachments.Count < 3 && this.m_FileList.childCount < 3)
            {
                this.m_ChooseFileButton.SetEnabled(true);
            }
        }

        public bool CheckFieldValid()
        {
            if (this.csCategoryField.isRequire && (this.attachments == null || this.attachments.Count == 0))
            {
                this.m_ValidationLabel.style.display = DisplayStyle.Flex;
                return false;
            }
            else
            {
                this.m_ValidationLabel.style.display = DisplayStyle.None;
                return true;
            }
        }

        public RebotsTicketAttachment[] GetFieldValue()
        {
            return this.attachments.ToArray();
        }
    }
}
