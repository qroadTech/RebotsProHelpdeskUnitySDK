using HelpDesk.Sdk.Common.Objects;
using System.Text.RegularExpressions;
using UnityEngine.UIElements;

namespace Rebots.HelpDesk
{
    public class RebotsTextFieldComponent
    {
        const string FieldLabel = "rebots-field-label";
        const string RequiredFieldLabel = "rebots-required";
        const string TextField = "rebots-text-field";
        const string ValidationLabel = "rebots-validation-label";
        Regex EmailValidation = new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");

        Label m_FieldLabel;
        Label m_RequiredFieldLabel;
        RebotsTextField m_TextField;
        Label m_ValidationLabel;

        private TicketCategoryInputField m_csCategoryField;
        private string parameter;
        private string validationComment;
        private string validtaionCommentEmail;

        public RebotsTextFieldComponent(TicketCategoryInputField csCategoryField, string? parameter, string[] validationComment)
        {
            this.m_csCategoryField = csCategoryField;
            this.parameter = (!string.IsNullOrEmpty(parameter)) ? parameter.Trim() : "";
            this.validationComment = (validationComment != null) ? validationComment[0] : "";
            this.validtaionCommentEmail = (validationComment != null && validationComment.Length > 1) ? validationComment[1] : "";
        }

        public void SetVisualElements(TemplateContainer textFieldUIElement)
        {
            if (textFieldUIElement == null)
            {
                return;
            }

            m_FieldLabel = textFieldUIElement.Q<Label>(FieldLabel);
            m_RequiredFieldLabel = textFieldUIElement.Q<Label>(RequiredFieldLabel);
            m_TextField = new RebotsTextField(textFieldUIElement.Q<TextField>(TextField));
            m_ValidationLabel = textFieldUIElement.Q<Label>(ValidationLabel);
        }

        public void SetFieldData(TemplateContainer textFieldUIElement)
        {
            if (textFieldUIElement == null)
            {
                return;
            }

            m_FieldLabel.text = m_csCategoryField.text;

            m_TextField
                .UsePlaceholder(m_csCategoryField.placeholderText)
                .UseParameter(parameter)
                .UseReadOnly(!m_csCategoryField.isEnable)
                .InitializeTextField();

            m_RequiredFieldLabel.style.display = (m_csCategoryField.isRequire) ? DisplayStyle.Flex : DisplayStyle.None;

            m_ValidationLabel.text = validationComment;
            m_ValidationLabel.style.display = DisplayStyle.None;

            textFieldUIElement.style.display = (m_csCategoryField.isHidden) ? DisplayStyle.None : DisplayStyle.Flex;
        }
        
        public bool CheckFieldValid()
        {
            var value = m_TextField.GetValue().Trim();
            if (m_csCategoryField.isRequire && (string.IsNullOrEmpty(value) || m_TextField.m_setPlaceholder))
            {
                m_ValidationLabel.text = validationComment;
                m_ValidationLabel.style.display = DisplayStyle.Flex;
                return false;
            }
            else
            {
                if (m_csCategoryField.name == "email")
                {
                    Match valid = EmailValidation.Match(value);
                    if (!valid.Success)
                    {
                        m_ValidationLabel.text = validtaionCommentEmail;
                        m_ValidationLabel.style.display = DisplayStyle.Flex;
                        return false;
                    }
                }
                m_ValidationLabel.style.display = DisplayStyle.None;
                return true;
            }
        }
                         
        public string GetFieldValue()
        {
            var value = m_TextField.GetValue().Trim();
            return value;
        }
    }
}
