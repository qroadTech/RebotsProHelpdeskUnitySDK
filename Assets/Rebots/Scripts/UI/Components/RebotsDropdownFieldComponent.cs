using HelpDesk.Sdk.Common.Objects;
using System;
using System.Linq;
using UnityEngine.UIElements;

namespace Rebots.HelpDesk
{
    public class RebotsDropdownFieldComponent
    {
        const string FieldLabel = "rebots-field-label";
        const string RequiredFieldLabel = "rebots-required";
        const string DropdownField = "rebots-dropdown";
        const string ValidationLabel = "rebots-validation-label";

        Label m_Label;
        Label m_RequiredFieldLabel;
        DropdownField m_DropdownField;
        Label m_ValidationLabel;

        private TicketCategoryInputField csCategoryField;
        private string parameter;
        private string validationComment;
        private string[] answers;

        public RebotsDropdownFieldComponent(TicketCategoryInputField csCategoryField, string? parameter, string[] validationComment)
        {
            this.csCategoryField = csCategoryField;
            this.answers = csCategoryField.answers;
            this.parameter = (!string.IsNullOrEmpty(parameter)) ? parameter.Trim().ToLower() : "";
            this.validationComment = (validationComment != null) ? validationComment[0] : "";
        }

        public void SetVisualElement(TemplateContainer dropdownFieldUIElement)
        {
            if (dropdownFieldUIElement == null)
            {
                return;
            }

            m_Label = dropdownFieldUIElement.Q<Label>(FieldLabel);
            m_RequiredFieldLabel = dropdownFieldUIElement.Q<Label>(RequiredFieldLabel);
            m_DropdownField = dropdownFieldUIElement.Q<DropdownField>(DropdownField);
            m_ValidationLabel = dropdownFieldUIElement.Q<Label>(ValidationLabel);

            m_DropdownField.choices = null;
        }

        public void SetFieldData(TemplateContainer dropdownFieldUIElement)
        {
            if (dropdownFieldUIElement == null)
            {
                return;
            }

            m_Label.text = csCategoryField.text;

            var choiceAnswers = answers.ToList();
            m_DropdownField.choices = choiceAnswers;

            int parameterIndex = choiceAnswers.Select(x => x.ToLower()).ToList().IndexOf(parameter);

            m_DropdownField.index = parameterIndex;

            m_RequiredFieldLabel.style.display = (csCategoryField.isRequire) ? DisplayStyle.Flex : DisplayStyle.None;

            m_ValidationLabel.text = validationComment;
            m_ValidationLabel.style.display = DisplayStyle.None;

            dropdownFieldUIElement.style.display = (csCategoryField.isHidden) ? DisplayStyle.None : DisplayStyle.Flex;
        }

        public bool CheckFieldValid()
        {
            var value = (m_DropdownField.value != null) ? m_DropdownField.value : "";
            if (csCategoryField.isRequire && string.IsNullOrEmpty(value))
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

        public string GetFieldValue()
        {
            return m_DropdownField.value;
        }
    }
}
