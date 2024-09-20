using HelpDesk.Sdk.Common.Objects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

namespace Rebots.HelpDesk
{
    public class RebotsButtonGroupFieldComponent : RebotsFieldBase
    {
        const string FieldLabel = "rebots-field-label";
        const string RequiredFieldLabel = "rebots-required";
        const string ButtonGroupField = "rebots-button-group";
        const string ButtonLabel = "rebots-button-label";
        const string Check = "rebots-check";
        const string Radio = "rebots-radio";
        const string ValidationLabel = "rebots-validation-label";

        Label m_Label;
        Label m_RequiredFieldLabel;
        RadioButtonGroup m_ButtonGroupField;
        Label m_ValidationLabel;

        private TicketCategoryInputField csCategoryField;
        private string parameter;
        private string validationComment;
        private VisualTreeAsset buttonAsset;
        private string[] answers;
        private Dictionary<Toggle, string> checkbuttons = new Dictionary<Toggle, string>();
        private Dictionary<RadioButton, string> radiobuttons = new Dictionary<RadioButton, string>();

        public RebotsButtonGroupFieldComponent(TicketCategoryInputField csCategoryField, VisualTreeAsset buttonAsset, string? parameter, string[] validationComment) : base(csCategoryField.isAdvice, csCategoryField.adviceText)
        {
            this.csCategoryField = csCategoryField;
            this.buttonAsset = buttonAsset;
            this.answers = csCategoryField.answers;
            this.parameter = (!string.IsNullOrEmpty(parameter)) ? parameter.Trim().ToLower() : "";
            this.validationComment = (validationComment != null) ? validationComment[0] : "";
        }

        public override void SetVisualElements(TemplateContainer buttonGroupFieldUIElement)
        {
            if (buttonGroupFieldUIElement == null)
            {
                return;
            }
            base.SetVisualElements(buttonGroupFieldUIElement);

            m_Label = buttonGroupFieldUIElement.Q<Label>(FieldLabel);
            m_RequiredFieldLabel = buttonGroupFieldUIElement.Q<Label>(RequiredFieldLabel);
            m_ButtonGroupField = buttonGroupFieldUIElement.Q<RadioButtonGroup>(ButtonGroupField);
            m_ValidationLabel = buttonGroupFieldUIElement.Q<Label>(ValidationLabel);
        }

        public void SetFieldData(TemplateContainer buttonGroupFieldUIElement)
        {
            if (buttonGroupFieldUIElement == null)
            {
                return;
            }
            base.SetFieldData();

            m_Label.text = csCategoryField.text;
            var isEnable = csCategoryField.isEnable;

            for (int i = 0; i < answers.Count(); i++)
            {
                var item = answers[i];
                TemplateContainer buttonUIElement = buttonAsset.Instantiate();
                var m_ButtonLabel = buttonUIElement.Q<Label>(ButtonLabel);
                m_ButtonLabel.text = item;

                if (csCategoryField.fieldType == RebotsInputFieldType.Checkbox)
                {
                    var m_Check = buttonUIElement.Q<Toggle>(Check);
                    m_Check.value = (item.ToLower() == parameter) ? true : false;
                    if (parameter != "")
                    {                                                                           
                        m_Check.SetEnabled(isEnable);
                    }
                    m_Check.RegisterValueChangedCallback(OnValueChanged);

                    checkbuttons.Add(m_Check, item);
                }
                else
                {
                    var m_Radio = buttonUIElement.Q<RadioButton>(Radio);
                    m_Radio.value = (item.ToLower() == parameter) ? true : false;
                    if (parameter != "")
                    {
                        m_Radio.SetEnabled(isEnable);
                    }
                    m_Radio.RegisterValueChangedCallback(OnValueChanged);

                    radiobuttons.Add(m_Radio, item);
                }
                m_ButtonGroupField.Add(buttonUIElement);
            }

            m_RequiredFieldLabel.style.display = (csCategoryField.isRequire) ? DisplayStyle.Flex : DisplayStyle.None;

            m_ValidationLabel.text = validationComment;
            m_ValidationLabel.style.display = DisplayStyle.None;

            buttonGroupFieldUIElement.style.display = (csCategoryField.isHidden) ? DisplayStyle.None : DisplayStyle.Flex;
        }

        public void OnValueChanged(ChangeEvent<bool> evt)
        {
            var value = evt.newValue;
            if (csCategoryField.isRequire && value)
            {
                m_ValidationLabel.style.display = DisplayStyle.None;
                m_Root.RemoveFromClassList(RebotsUIStaticString.RebotsValidationStyle);
            }
        }

        public bool CheckFieldValid()
        {
            var value = GetFieldValue();
            if (csCategoryField.isRequire && string.IsNullOrEmpty(value))
            {
                m_Root.AddToClassList(RebotsUIStaticString.RebotsValidationStyle);
                m_ValidationLabel.style.display = DisplayStyle.Flex;
                return false;
            }
            else
            {
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
            List<string> valueStr = new List<string>();

            if (csCategoryField.fieldType == RebotsInputFieldType.Checkbox)
            {
                foreach (var item in checkbuttons)
                {
                    var button = item.Key as Toggle;
                    var label = button.Q<Label>(ButtonLabel);
                    if (button.value)
                    {
                        valueStr.Add(label.text);
                    }
                }
            }
            else
            {
                foreach (var item in radiobuttons)
                {
                    var button = item.Key as RadioButton;
                    var label = button.Q<Label>(ButtonLabel);
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
