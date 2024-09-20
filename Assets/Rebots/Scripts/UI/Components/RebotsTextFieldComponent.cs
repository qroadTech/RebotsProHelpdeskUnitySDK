using HelpDesk.Sdk.Common.Objects;
using System.Text.RegularExpressions;
using UnityEngine.UIElements;

namespace Rebots.HelpDesk
{
    public class RebotsTextFieldComponent : RebotsFieldBase
    {
        const string FieldLabel = "rebots-field-label";
        const string RequiredFieldLabel = "rebots-required";
        const string TextField = "rebots-text-field";
        const string ValidationLabel = "rebots-validation-label";
        readonly Regex EmailValidation = new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");

        Label m_FieldLabel;
        Label m_RequiredFieldLabel;
        RebotsTextField m_TextField;
        Label m_ValidationLabel;

        private TicketCategoryInputField csCategoryField;
        private string parameter;
        private string validationComment;
        private string validtaionCommentEmail;

        public RebotsTextFieldComponent(TicketCategoryInputField csCategoryField, string? parameter, string[] validationComment) : base(csCategoryField.isAdvice, csCategoryField.adviceText)
        {
            this.csCategoryField = csCategoryField;
            this.parameter = (!string.IsNullOrEmpty(parameter)) ? parameter.Trim() : "";
            this.validationComment = (validationComment != null) ? validationComment[0] : "";
            this.validtaionCommentEmail = (validationComment != null && validationComment.Length > 1) ? validationComment[1] : "";
        }

        public override void SetVisualElements(TemplateContainer textFieldUIElement)
        {
            if (textFieldUIElement == null)
            {
                return;
            }
            base.SetVisualElements(textFieldUIElement);

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
            base.SetFieldData();

            m_FieldLabel.text = csCategoryField.text;

            m_TextField
                .UsePlaceholder(csCategoryField.placeholderText)
                .UseParameter(parameter)
                .UseEnable(csCategoryField.isEnable)
                .AddFocusOut(OnFocusOut)
                .InitializeTextField();

            m_RequiredFieldLabel.style.display = (csCategoryField.isRequire) ? DisplayStyle.Flex : DisplayStyle.None;

            m_ValidationLabel.text = validationComment;
            m_ValidationLabel.style.display = DisplayStyle.None;

            textFieldUIElement.style.display = (csCategoryField.isHidden) ? DisplayStyle.None : DisplayStyle.Flex;
        }

        public void OnFocusOut()
        {
            var value = m_TextField.GetValue().Trim();
            if (csCategoryField.isRequire && !string.IsNullOrEmpty(value))
            {
                m_Root.RemoveFromClassList(RebotsUIStaticString.RebotsValidationStyle);
                m_ValidationLabel.style.display = DisplayStyle.None;
            }
        }
        
        public bool CheckFieldValid()
        {
            var value = m_TextField.GetValue().Trim();
            if (csCategoryField.isRequire && string.IsNullOrEmpty(value))
            {
                m_ValidationLabel.text = validationComment;
                m_Root.AddToClassList(RebotsUIStaticString.RebotsValidationStyle);
                m_ValidationLabel.style.display = DisplayStyle.Flex;
                return false;
            }
            else
            {
                if (csCategoryField.name == "email")
                {
                    Match valid = EmailValidation.Match(value);
                    if (!valid.Success)
                    {
                        m_ValidationLabel.text = validtaionCommentEmail;
                        m_Root.AddToClassList(RebotsUIStaticString.RebotsValidationStyle);
                        m_ValidationLabel.style.display = DisplayStyle.Flex;
                        return false;
                    }
                }
                m_Root.RemoveFromClassList(RebotsUIStaticString.RebotsValidationStyle);
                m_ValidationLabel.style.display = DisplayStyle.None;
                return true;
            }
        }

        public float GetVerticalPsition()
        {
            return m_Root.layout.y;
        }
                         
        public string GetFieldValue()
        {
            var value = m_TextField.GetValue().Trim();
            return value;
        }
    }
}
