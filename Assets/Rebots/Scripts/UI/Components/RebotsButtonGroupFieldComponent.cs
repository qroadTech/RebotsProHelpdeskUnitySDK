using HelpDesk.Sdk.Common.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

namespace Rebots.HelpDesk
{
    public class RebotsButtonGroupFieldComponent
    {
        const string k_FieldLabel = "rebots-field-label";
        const string RequiredFieldLabel = "rebots-required";
        const string k_ButtonGroupField = "rebots-button-group";
        const string k_ButtonLabel = "rebots-button-label";
        const string k_Check = "rebots-check";
        const string k_Radio = "rebots-radio";
        const string ValidationLabel = "rebots-validation-label";

        Label m_Label;
        Label m_RequiredFieldLabel;
        RadioButtonGroup m_ButtonGroupField;
        Label m_ValidationLabel;

        private TicketCategoryInputField m_csCategoryField;
        private string parameter;
        private string validationComment;
        private VisualTreeAsset m_buttonAsset;
        private string[] m_answers;
        private Dictionary<Toggle, string> m_Checkbuttons = new Dictionary<Toggle, string>();
        private Dictionary<RadioButton, string> m_Radiobuttons = new Dictionary<RadioButton, string>();

        public RebotsButtonGroupFieldComponent(TicketCategoryInputField csCategoryField, VisualTreeAsset buttonAsset, string? parameter, string[] validationComment)
        {
            this.m_csCategoryField = csCategoryField;
            this.m_buttonAsset = buttonAsset;
            this.m_answers = csCategoryField.answers;
            this.parameter = (!string.IsNullOrEmpty(parameter)) ? parameter.Trim().ToLower() : "";
            this.validationComment = (validationComment != null) ? validationComment[0] : "";
        }

        public void SetVisualElement(TemplateContainer buttonGroupFieldUIElement)
        {
            if (buttonGroupFieldUIElement == null)
            {
                return;
            }

            m_Label = buttonGroupFieldUIElement.Q<Label>(k_FieldLabel);
            m_RequiredFieldLabel = buttonGroupFieldUIElement.Q<Label>(RequiredFieldLabel);
            m_ButtonGroupField = buttonGroupFieldUIElement.Q<RadioButtonGroup>(k_ButtonGroupField);
            m_ValidationLabel = buttonGroupFieldUIElement.Q<Label>(ValidationLabel);
        }

        public void SetFieldData(TemplateContainer buttonGroupFieldUIElement)
        {
            if (buttonGroupFieldUIElement == null)
            {
                return;
            }

            m_Label.text = m_csCategoryField.text;

            for (int i = 0; i < m_answers.Count(); i++)
            {
                var item = m_answers[i];
                TemplateContainer buttonUIElement = m_buttonAsset.Instantiate();
                var m_ButtonLabel = buttonUIElement.Q<Label>(k_ButtonLabel);
                m_ButtonLabel.text = item;

                if (m_csCategoryField.fieldType == RebotsInputFieldType.Checkbox)
                {
                    var m_Check = buttonUIElement.Q<Toggle>(k_Check);
                    m_Check.value = (item.ToLower() == parameter) ? true : false;

                    m_Checkbuttons.Add(m_Check, item);
                }
                else
                {
                    var m_Radio = buttonUIElement.Q<RadioButton>(k_Radio);
                    m_Radio.value = (item.ToLower() == parameter) ? true : false;

                    m_Radiobuttons.Add(m_Radio, item);
                }
                m_ButtonGroupField.Add(buttonUIElement);
            }

            m_RequiredFieldLabel.style.display = (m_csCategoryField.isRequire) ? DisplayStyle.Flex : DisplayStyle.None;

            m_ValidationLabel.text = validationComment;
            m_ValidationLabel.style.display = DisplayStyle.None;

            buttonGroupFieldUIElement.style.display = (m_csCategoryField.isHidden) ? DisplayStyle.None : DisplayStyle.Flex;
        }

        public bool CheckFieldValid()
        {
            var value = GetFieldValue();
            if (m_csCategoryField.isRequire && string.IsNullOrEmpty(value))
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
            List<string> valueStr = new List<string>();

            if (m_csCategoryField.fieldType == RebotsInputFieldType.Checkbox)
            {
                foreach (var item in m_Checkbuttons)
                {
                    var button = item.Key as Toggle;
                    var label = button.Q<Label>(k_ButtonLabel);
                    if (button.value)
                    {
                        valueStr.Add(label.text);
                    }
                }
            }
            else
            {
                foreach (var item in m_Radiobuttons)
                {
                    var button = item.Key as RadioButton;
                    var label = button.Q<Label>(k_ButtonLabel);
                    if (button.value)
                    {
                        valueStr.Add(label.text);
                    }
                }
            }

            return string.Join(", ", valueStr);
        }
    }
}
