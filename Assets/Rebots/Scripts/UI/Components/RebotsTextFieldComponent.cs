using HelpDesk.Sdk.Common.Objects;
using UnityEngine.UIElements;

namespace Rebots.HelpDesk
{
    public class RebotsTextFieldComponent
    {
        const string FieldLabel = "rebots-field-label";
        const string RequiredFieldLabel = "rebots-required";
        const string TextField = "rebots-text-field";

        Label m_FieldLabel;
        Label m_RequiredFieldLabel;
        RebotsTextField m_TextField;

        private TicketCategoryInputField m_csCategoryField;
        private string parameter;

        public RebotsTextFieldComponent(TicketCategoryInputField csCategoryField, string? parameter)
        {
            this.m_csCategoryField = csCategoryField;
            this.parameter = (!string.IsNullOrEmpty(parameter)) ? parameter.Trim() : "";
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

            textFieldUIElement.style.display = (m_csCategoryField.isHidden) ? DisplayStyle.None : DisplayStyle.Flex;
        }
        
        public void CheckFieldValid()
        {

        }
                         
        public string GetFieldValue()
        {
            var value = m_TextField.GetValue();
            return value.Trim();
        }
    }
}
