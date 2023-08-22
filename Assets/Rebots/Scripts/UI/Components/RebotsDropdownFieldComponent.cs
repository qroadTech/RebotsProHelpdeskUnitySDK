using HelpDesk.Sdk.Common.Objects;
using System;
using System.Linq;
using UnityEngine.UIElements;

namespace Rebots.HelpDesk
{
    public class RebotsDropdownFieldComponent
    {
        const string k_FieldLabel = "rebots-field-label";
        const string RequiredFieldLabel = "rebots-required";
        const string k_DropdownField = "rebots-dropdown";

        Label m_Label;
        Label m_RequiredFieldLabel;
        DropdownField m_DropdownField;

        private TicketCategoryInputField m_csCategoryField;
        private string parameter;
        private TicketCategoryInputFieldAnswer[] m_answers;

        public RebotsDropdownFieldComponent(TicketCategoryInputField csCategoryField, string? parameter)
        {
            this.m_csCategoryField = csCategoryField;
            this.m_answers = csCategoryField.answers;
            this.parameter = (!string.IsNullOrEmpty(parameter)) ? parameter.Trim().ToLower() : "";
        }

        public void SetVisualElement(TemplateContainer dropdownFieldUIElement)
        {
            if (dropdownFieldUIElement == null)
            {
                return;
            }

            m_Label = dropdownFieldUIElement.Q<Label>(k_FieldLabel);
            m_RequiredFieldLabel = dropdownFieldUIElement.Q<Label>(RequiredFieldLabel);
            m_DropdownField = dropdownFieldUIElement.Q<DropdownField>(k_DropdownField);

            m_DropdownField.choices = null;
        }

        public void SetFieldData(TemplateContainer dropdownFieldUIElement)
        {
            if (dropdownFieldUIElement == null)
            {
                return;
            }

            m_Label.text = m_csCategoryField.text;

            var choiceAnswers = m_answers.Select(x => x.answer).ToList();
            m_DropdownField.choices = choiceAnswers;

            var parameterLowerStr =  parameter.Replace(m_csCategoryField.name, "");
            int parameterIndex;
            if (Int32.TryParse(parameterLowerStr, out parameterIndex))
                parameterIndex = parameterIndex - 1;
            else
                parameterIndex = -1;
            
            m_DropdownField.index = parameterIndex;

            m_RequiredFieldLabel.style.display = (m_csCategoryField.isRequire) ? DisplayStyle.Flex : DisplayStyle.None;

            dropdownFieldUIElement.style.display = (m_csCategoryField.isHidden) ? DisplayStyle.None : DisplayStyle.Flex;
        }

        public string GetFieldValue()
        {
            return m_DropdownField.value;
        }
    }
}
