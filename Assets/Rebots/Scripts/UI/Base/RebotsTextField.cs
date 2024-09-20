using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Rebots.HelpDesk
{
    public class RebotsTextField
    {
        private TextField textField;

        private string m_placeholderStr = "";
        private bool m_usePlaceholder = false;
        private string m_parameter = "";
        private bool m_useParameter = false;
        private bool m_UseEnable = true;
        private Action m_FocusOutAction = null;

        public bool m_setPlaceholder { get; private set; }

        #region constructor and Initialize
        public RebotsTextField(TextField textField)
        {
            this.m_usePlaceholder = false;
            this.m_placeholderStr = "";
            this.m_useParameter = false;
            this.m_parameter = "";

            this.textField = textField;

            this.textField.RegisterCallback<FocusInEvent>(evt => OnFocusIn());
            this.textField.RegisterCallback<FocusOutEvent>(evt => OnFocusOut());

            this.textField.value = "";
            this.m_setPlaceholder = false;
        }

        public RebotsTextField UsePlaceholder(string newPlaceholder)
        {
            this.m_placeholderStr = newPlaceholder.Trim();
            this.m_usePlaceholder = !string.IsNullOrEmpty(m_placeholderStr);
            return this;
        }

        public RebotsTextField UseParameter(string newParameter)
        {
            this.m_parameter = newParameter;
            this.m_useParameter = !string.IsNullOrEmpty(newParameter);
            return this;
        }

        public RebotsTextField UseEnable(bool newUseEnable)
        {
            this.m_UseEnable = newUseEnable;
            return this;
        }

        public RebotsTextField AddFocusOut(Action action)
        {
            this.m_FocusOutAction = action;
            return this;
        }

        public RebotsTextField InitializeTextField()
        {
            if (m_useParameter)
            {
                this.textField.SetValueWithoutNotify(m_parameter);
                this.textField.SetEnabled(m_UseEnable);
            }
            else if (m_usePlaceholder)
            {
                SetPlaceholderStyle();
                return this;
            }

            UnsetPlaceholderStyle();
            return this;
        }
        #endregion

        private void OnFocusIn()
        {
            if (m_setPlaceholder)
            {
                this.textField.SetValueWithoutNotify("");
                UnsetPlaceholderStyle();
            }
        }

        private void OnFocusOut()
        {
            if (string.IsNullOrEmpty(this.textField.value) && m_usePlaceholder)
            {
                SetPlaceholderStyle();
            }

            if (m_FocusOutAction != null)
            {
                this.m_FocusOutAction();
            }
        }

        private void UnsetPlaceholderStyle()
        {
            this.textField.RemoveFromClassList(RebotsUIStaticString.RebotsFontColor_Grey);
            this.textField.AddToClassList(RebotsUIStaticString.RebotsFontColor_Black);

            m_setPlaceholder = false;
        }

        private void SetPlaceholderStyle()
        {
            this.textField.SetValueWithoutNotify(m_placeholderStr);

            this.textField.RemoveFromClassList(RebotsUIStaticString.RebotsFontColor_Black);
            this.textField.AddToClassList(RebotsUIStaticString.RebotsFontColor_Grey);

            m_setPlaceholder = true;
        }

        public string GetValue()
        {
            if (!m_setPlaceholder)
            {
                return this.textField.value;
            }
            else
            {
                return "";
            }
        }

        public void KeyDownEvent(EventCallback<KeyDownEvent> callback) 
        {
            this.textField.RegisterCallback<KeyDownEvent>(callback);
        }
    }
}
