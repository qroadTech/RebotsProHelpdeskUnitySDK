using HelpDesk.Sdk.Common.Objects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

namespace Rebots.HelpDesk
{
    public class RebotsDropdownFieldComponent : RebotsFieldBase
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

        public RebotsDropdownFieldComponent(TicketCategoryInputField csCategoryField, string? parameter, string[] validationComment) : base(csCategoryField.isAdvice, csCategoryField.adviceText)
        {
            this.csCategoryField = csCategoryField;
            this.answers = csCategoryField.answers;
            this.parameter = (!string.IsNullOrEmpty(parameter)) ? parameter.Trim().ToLower() : "";
            this.validationComment = (validationComment != null) ? validationComment[0] : "";
        }

        public override void SetVisualElements(TemplateContainer dropdownFieldUIElement)
        {
            if (dropdownFieldUIElement == null)
            {
                return;
            }
            base.SetVisualElements(dropdownFieldUIElement);

            m_Label = dropdownFieldUIElement.Q<Label>(FieldLabel);
            m_RequiredFieldLabel = dropdownFieldUIElement.Q<Label>(RequiredFieldLabel);
            m_DropdownField = dropdownFieldUIElement.Q<DropdownField>(DropdownField);
            m_ValidationLabel = dropdownFieldUIElement.Q<Label>(ValidationLabel);

            m_DropdownField.choices = new List<string>();
        }

        public void SetFieldData(TemplateContainer dropdownFieldUIElement)
        {
            if (dropdownFieldUIElement == null)
            {
                return;
            }
            base.SetFieldData();

            m_Label.text = csCategoryField.text;

            var choiceAnswers = answers.ToList();
            m_DropdownField.choices = choiceAnswers;

            int parameterIndex = choiceAnswers.Select(x => x.ToLower()).ToList().IndexOf(parameter);
            m_DropdownField.index = parameterIndex;
            if (parameterIndex > -1)
            {
                m_DropdownField.SetEnabled(csCategoryField.isEnable);
            }

            m_DropdownField.RegisterValueChangedCallback(OnValueChanged);

            m_RequiredFieldLabel.style.display = (csCategoryField.isRequire) ? DisplayStyle.Flex : DisplayStyle.None;

            m_ValidationLabel.text = validationComment;
            m_ValidationLabel.style.display = DisplayStyle.None;

            dropdownFieldUIElement.style.display = (csCategoryField.isHidden) ? DisplayStyle.None : DisplayStyle.Flex;
        }

        public void OnValueChanged(ChangeEvent<string> evt)
        {
            var value = (evt.newValue != null) ? evt.newValue : "";
            if (csCategoryField.isRequire && !string.IsNullOrEmpty(value))
            {
                m_ValidationLabel.style.display = DisplayStyle.None;
                m_Root.RemoveFromClassList(RebotsUIStaticString.RebotsValidationStyle);
            }
        }

        public bool CheckFieldValid()
        {
            var value = (m_DropdownField.value != null) ? m_DropdownField.value : "";
            if (csCategoryField.isRequire && string.IsNullOrEmpty(value))
            {
                m_ValidationLabel.style.display = DisplayStyle.Flex;
                m_Root.AddToClassList(RebotsUIStaticString.RebotsValidationStyle);
                return false;
            }
            else 
            {
                m_ValidationLabel.style.display = DisplayStyle.None;
                m_Root.RemoveFromClassList(RebotsUIStaticString.RebotsValidationStyle);
                return true;
            }
        }

        public float GetVerticalPsition()
        {
            return m_Root.layout.y;
        }

        public string GetFieldValue()
        {
            return m_DropdownField.value;
        }
    }
}
