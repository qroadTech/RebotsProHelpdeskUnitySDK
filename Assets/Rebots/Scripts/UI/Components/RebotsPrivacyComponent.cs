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
        const string PrivacyLinkButton = "rebots-privacy-link-button";
        const string PrivacyCheck = "rebots-privacy-check";
        const string ValidationLabel = "rebots-validation-label";
        const string TicketSubmitButton = "rebots-submit-button";

        const string FormStringFormat = "<b>{0}</b><br>{1}<br>";

        VisualElement m_Root;
        VisualElement m_PrivacyTextContainer;
        Label m_PrivacyTextLabel;
        VisualElement m_PrivacyLinkContainer;
        Button m_PrivacyLinkButton;
        Toggle m_PrivacyCheck;
        Label m_ValidationLabel;
        Button m_TicketSubmitButton;

        private Category category = new();
        private PrivacySetting ticketPrivacySetting = new();
        private string[] transData;
        private string validationComment;
        private bool privacyValue = false;

        public RebotsPrivacyComponent(PrivacySetting ticketPrivacySetting, string[] transData, string validationComment, Category category)
        {
            this.category = category;
            this.ticketPrivacySetting = ticketPrivacySetting;
            this.transData = transData;
            this.validationComment = validationComment;
        }

        public void SetVisualElements(TemplateContainer privacyUIElement)
        {
            if (privacyUIElement == null)
            {
                return;
            }

            m_Root = privacyUIElement;
            m_PrivacyTextContainer = privacyUIElement.Q(PrivacyTextContainer);
            m_PrivacyTextLabel = privacyUIElement.Q<Label>(PrivacyTextLabel);
            m_PrivacyLinkContainer = privacyUIElement.Q(PrivacyLinkContainer);
            m_PrivacyLinkButton = privacyUIElement.Q<Button>(PrivacyLinkButton);
            m_PrivacyCheck = privacyUIElement.Q<Toggle>(PrivacyCheck);
            m_ValidationLabel = privacyUIElement.Q<Label>(ValidationLabel);
            m_TicketSubmitButton = privacyUIElement.Q<Button>(TicketSubmitButton);
        }

        public void SetPrivacyData(TemplateContainer privacyUIElement)
        {
            if (privacyUIElement == null)
            {
                return;
            }

            if (ticketPrivacySetting.usePrivacyPolicyURL)
            {
                m_PrivacyTextContainer.style.display = DisplayStyle.None;
                m_PrivacyLinkContainer.style.display = DisplayStyle.Flex;

                m_PrivacyLinkButton?.RegisterCallback<ClickEvent>(evt => Application.OpenURL(ticketPrivacySetting.privacyPolicyURL));
            }
            else
            {
                m_PrivacyTextContainer.style.display = DisplayStyle.Flex;
                m_PrivacyLinkContainer.style.display = DisplayStyle.None;

                if (ticketPrivacySetting.useFormPrivacyPolicy)
                {
                    string formStr = "";
                    
                    if(!string.IsNullOrEmpty(ticketPrivacySetting.formPurposeText.Trim()))
                    {
                        formStr += string.Format(FormStringFormat, transData[0], ticketPrivacySetting.formPurposeText.Trim());
                    }
                    if(!string.IsNullOrEmpty(ticketPrivacySetting.formItemText.Trim()))
                    {
                        formStr += string.Format(FormStringFormat, transData[1], ticketPrivacySetting.formItemText.Trim());
                    }
                    if(!string.IsNullOrEmpty(ticketPrivacySetting.formPeriodText.Trim()))
                    {
                        formStr += string.Format(FormStringFormat, transData[2], ticketPrivacySetting.formPeriodText.Trim());
                    }
                    if(!string.IsNullOrEmpty(ticketPrivacySetting.formAgencyNameText.Trim()))
                    {
                        formStr += string.Format(FormStringFormat, transData[3], ticketPrivacySetting.formAgencyNameText.Trim());
                    }
                    if(!string.IsNullOrEmpty(ticketPrivacySetting.formAgencyProivdeText.Trim()))
                    {
                        formStr += string.Format(FormStringFormat, transData[4], ticketPrivacySetting.formAgencyProivdeText.Trim());
                    }
                    if(!string.IsNullOrEmpty(ticketPrivacySetting.formAgencyPeriodText.Trim()))
                    {
                        formStr += string.Format(FormStringFormat, transData[5], ticketPrivacySetting.formAgencyPeriodText.Trim());
                    }

                    m_PrivacyTextLabel.text = formStr;
                }
                else
                {
                    m_PrivacyTextLabel.text = ticketPrivacySetting.privacyPolicyText;
                }
            }

            m_PrivacyCheck.value = false;

            m_ValidationLabel.text = validationComment;
            m_ValidationLabel.style.display = DisplayStyle.None;

            m_PrivacyCheck?.RegisterValueChangedCallback(ChangePrivacyValue);
        }

        public void RegisterCallbacks(Action<bool, Category> submitAction)
        {
            m_TicketSubmitButton?.RegisterCallback<ClickEvent>(evt => {
                CheckFieldValid();
                submitAction(privacyValue, category);
            });
        }

        void ChangePrivacyValue(ChangeEvent<bool> evt)
        {
            privacyValue = evt.newValue;
            if (privacyValue)
            {
                m_Root.RemoveFromClassList(RebotsUIStaticString.RebotsValidationStyle);
                m_ValidationLabel.style.display = DisplayStyle.None;
            }
            else
            {
            }
        }

        public void CheckFieldValid()
        {
            if (m_PrivacyCheck.value == false)
            {
                m_Root.AddToClassList(RebotsUIStaticString.RebotsValidationStyle);
                m_ValidationLabel.style.display = DisplayStyle.Flex;
            }
            else
            {
                m_Root.RemoveFromClassList(RebotsUIStaticString.RebotsValidationStyle);
                m_ValidationLabel.style.display = DisplayStyle.None;
            }
        }
    }
}
