using HelpDesk.Sdk.Common.Objects;
using HelpDesk.Sdk.Common.Objects.Enums;
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

        Label m_Label;
        Label m_RequiredFieldLabel;
        RadioButtonGroup m_ButtonGroupField;

        private TicketCategoryInputField m_csCategoryField;
        private string parameter;
        private VisualTreeAsset m_buttonAsset;
        private TicketCategoryInputFieldAnswer[] m_answers;
        private Dictionary<Toggle, string> m_Checkbuttons = new Dictionary<Toggle, string>();
        private Dictionary<RadioButton, string> m_Radiobuttons = new Dictionary<RadioButton, string>();

        public RebotsButtonGroupFieldComponent(TicketCategoryInputField csCategoryField, VisualTreeAsset buttonAsset, string? parameter)
        {
            this.m_csCategoryField = csCategoryField;
            this.m_buttonAsset = buttonAsset;
            this.m_answers = csCategoryField.answers;
            this.parameter = (!string.IsNullOrEmpty(parameter)) ? parameter.Trim().ToLower() : "";
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
        }

        public void SetFieldData(TemplateContainer buttonGroupFieldUIElement)
        {
            if (buttonGroupFieldUIElement == null)
            {
                return;
            }

            m_Label.text = m_csCategoryField.text;

            var parameterLowerStr = parameter.Replace(m_csCategoryField.name, "");
            int parameterIndex;
            if (Int32.TryParse(parameterLowerStr, out parameterIndex))
                parameterIndex = parameterIndex - 1;
            else
                parameterIndex = -1;


            for (int i = 0; i < m_answers.Count(); i++)
            {
                var item = m_answers[i];
                TemplateContainer buttonUIElement = m_buttonAsset.Instantiate();
                var m_ButtonLabel = buttonUIElement.Q<Label>(k_ButtonLabel);
                m_ButtonLabel.text = item.answer;

                if (m_csCategoryField.fieldType == RebotsInputFieldType.checkbox)
                {
                    var m_Check = buttonUIElement.Q<Toggle>(k_Check);
                    m_Check.value = (parameterIndex == i) ? true : false;

                    m_Checkbuttons.Add(m_Check, item.answer);
                }
                else
                {
                    var m_Radio = buttonUIElement.Q<RadioButton>(k_Radio);
                    m_Radio.value = (parameterIndex == i) ? true : false;

                    m_Radiobuttons.Add(m_Radio, item.answer);
                }
                m_ButtonGroupField.Add(buttonUIElement);
            }

            m_RequiredFieldLabel.style.display = (m_csCategoryField.isRequire) ? DisplayStyle.Flex : DisplayStyle.None;

            buttonGroupFieldUIElement.style.display = (m_csCategoryField.isHidden) ? DisplayStyle.None : DisplayStyle.Flex;
        }

        public string GetFieldValue()
        {
            List<string> valueStr = new List<string>();

            if (m_csCategoryField.fieldType == RebotsInputFieldType.checkbox)
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
