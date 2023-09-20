using HelpDesk.Sdk.Common.Objects;
using HelpDesk.Sdk.Common.Protocols.Responses;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Rebots.HelpDesk
{
    public class RebotsPrivacyComponent
    {
        const string PrivacyTextContainer = "rebots-privacy-text-container";
        const string PrivacyTextLabel = "rebots-privacy-text-label";
        const string PrivacyLinkContainer = "rebots-privacy-link-container";
        const string PrivacyLinkTitleLabel = "rebots-privacy-link-title-label";
        const string PrivacyLinkButton = "rebots-privacy-link-button";
        const string PrivacyLinkLabel = "rebots-privacy-link-label";
        const string PrivacyCheck = "rebots-privacy-check";
        const string TicketSubmitButton = "rebots-submit-button";

        const string FormStringFormat = "<b>{0}</b><br>{1}<br>";

        VisualElement m_PrivacyTextContainer;
        Label m_PrivacyTextLabel;
        VisualElement m_PrivacyLinkContainer;
        Label m_PrivacyLinkTitleLabel;
        Button m_PrivacyLinkButton;
        Label m_PrivacyLinkLabel;
        Toggle m_PrivacyCheck; 
        Button m_TicketSubmitButton;

        private PrivacySetting m_ticketPrivacySetting;
        private string[] m_transData;
        private bool privacyValue = false;

        public RebotsPrivacyComponent(PrivacySetting ticketPrivacySetting, string[] transData)
        {
            m_ticketPrivacySetting = ticketPrivacySetting;
            m_transData = transData;
        }

        public void SetVisualElements(TemplateContainer privacyUIElement)
        {
            if (privacyUIElement == null)
            {
                return;
            }

            m_PrivacyTextContainer = privacyUIElement.Q(PrivacyTextContainer);
            m_PrivacyTextLabel = privacyUIElement.Q<Label>(PrivacyTextLabel);
            m_PrivacyLinkContainer = privacyUIElement.Q(PrivacyLinkContainer);
            m_PrivacyLinkTitleLabel = privacyUIElement.Q<Label>(PrivacyLinkTitleLabel);
            m_PrivacyLinkButton = privacyUIElement.Q<Button>(PrivacyLinkButton);
            m_PrivacyLinkLabel = privacyUIElement.Q<Label>(PrivacyLinkLabel);
            m_PrivacyCheck = privacyUIElement.Q<Toggle>(PrivacyCheck);
            m_TicketSubmitButton = privacyUIElement.Q<Button>(TicketSubmitButton);
        }

        public void SetPrivacyData(TemplateContainer privacyUIElement)
        {
            if (privacyUIElement == null)
            {
                return;
            }

            if (!m_ticketPrivacySetting.usePrivacyPolicyURL)
            {
                m_PrivacyTextContainer.style.display = DisplayStyle.None;
                m_PrivacyLinkContainer.style.display = DisplayStyle.Flex;

                m_PrivacyLinkButton?.RegisterCallback<ClickEvent>(evt => Application.OpenURL(m_ticketPrivacySetting.privacyPolicyURL));
            }
            else
            {
                m_PrivacyTextContainer.style.display = DisplayStyle.Flex;
                m_PrivacyLinkContainer.style.display = DisplayStyle.None;

                if (!m_ticketPrivacySetting.useFormPrivacyPolicy)
                {
                    string formStr = "";
                    
                    if(!string.IsNullOrEmpty(m_ticketPrivacySetting.formPurposeText.Trim()))
                    {
                        formStr += string.Format(FormStringFormat, m_transData[0], m_ticketPrivacySetting.formPurposeText.Trim());
                    }
                    if(!string.IsNullOrEmpty(m_ticketPrivacySetting.formItemText.Trim()))
                    {
                        formStr += string.Format(FormStringFormat, m_transData[1], m_ticketPrivacySetting.formItemText.Trim());
                    }
                    if(!string.IsNullOrEmpty(m_ticketPrivacySetting.formPeriodText.Trim()))
                    {
                        formStr += string.Format(FormStringFormat, m_transData[2], m_ticketPrivacySetting.formPeriodText.Trim());
                    }
                    if(!string.IsNullOrEmpty(m_ticketPrivacySetting.formAgencyNameText.Trim()))
                    {
                        formStr += string.Format(FormStringFormat, m_transData[3], m_ticketPrivacySetting.formAgencyNameText.Trim());
                    }
                    if(!string.IsNullOrEmpty(m_ticketPrivacySetting.formAgencyProivdeText.Trim()))
                    {
                        formStr += string.Format(FormStringFormat, m_transData[4], m_ticketPrivacySetting.formAgencyProivdeText.Trim());
                    }
                    if(!string.IsNullOrEmpty(m_ticketPrivacySetting.formAgencyPeriodText.Trim()))
                    {
                        formStr += string.Format(FormStringFormat, m_transData[5], m_ticketPrivacySetting.formAgencyPeriodText.Trim());
                    }

                    m_PrivacyTextLabel.text = formStr;
                }
                else
                {
                    m_PrivacyTextLabel.text = m_ticketPrivacySetting.privacyPolicyText;
                }
            }

            m_PrivacyCheck.value = false;

            m_PrivacyCheck?.RegisterValueChangedCallback(ChangePrivacyValue);

            m_TicketSubmitButton.style.opacity = 0.6f;
            m_TicketSubmitButton.RemoveFromClassList(RebotsUIStaticString.RebotsBackgroundColor_None);
            m_TicketSubmitButton.AddToClassList(RebotsUIStaticString.RebotsBackgroundColor_Grey);
        }

        public void RegisterCallbacks(Action<bool> submitAction)
        {
            m_TicketSubmitButton?.RegisterCallback<ClickEvent>(evt => submitAction(privacyValue));
        }

        void ChangePrivacyValue(ChangeEvent<bool> evt)
        {
            privacyValue = evt.newValue;
            if (privacyValue)
            {
                m_TicketSubmitButton.style.opacity = 1f;
                m_TicketSubmitButton.RemoveFromClassList(RebotsUIStaticString.RebotsBackgroundColor_Grey);
                m_TicketSubmitButton.AddToClassList(RebotsUIStaticString.RebotsBackgroundColor_None);
            }
            else
            {
                m_TicketSubmitButton.style.opacity = 0.6f;
                m_TicketSubmitButton.RemoveFromClassList(RebotsUIStaticString.RebotsBackgroundColor_None);
                m_TicketSubmitButton.AddToClassList(RebotsUIStaticString.RebotsBackgroundColor_Grey);
            }
        }
    }
}
