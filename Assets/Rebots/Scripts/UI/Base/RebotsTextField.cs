using Newtonsoft.Json.Bson;
using UnityEngine.Rendering;
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
        private bool m_ReadOnly = false;

        public bool m_setPlaceholder { get; private set; }

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

        public RebotsTextField UseReadOnly(bool newReadOnly)
        {
            this.m_ReadOnly = newReadOnly;
            return this;
        }

        public RebotsTextField InitializeTextField()
        {
            if (m_useParameter)
            {
                this.textField.SetValueWithoutNotify(m_parameter);
                this.textField.isReadOnly = m_ReadOnly;
            }
            else if (m_usePlaceholder)
            {
                SetPlaceholderStyle();
                return this;
            }

            UnsetPlaceholderStyle();
            return this;
        }

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
        }

        private void UnsetPlaceholderStyle()
        {
            if (this.textField.ClassListContains(RebotsUIStaticString.RebotsFontColor_Grey))
            {
                this.textField.RemoveFromClassList(RebotsUIStaticString.RebotsFontColor_Grey);
            }
            this.textField.AddToClassList(RebotsUIStaticString.RebotsFontColor_Black);

            m_setPlaceholder = false;
        }

        private void SetPlaceholderStyle()
        {
            this.textField.SetValueWithoutNotify(m_placeholderStr);

            if (this.textField.ClassListContains(RebotsUIStaticString.RebotsFontColor_Black))
            {
                this.textField.RemoveFromClassList(RebotsUIStaticString.RebotsFontColor_Black);
            }
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
    }
}
