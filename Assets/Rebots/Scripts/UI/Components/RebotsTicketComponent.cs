using HelpDesk.Sdk.Common.Objects;
using System;
using UnityEngine.UIElements;

namespace Rebots.HelpDesk
{
    public class RebotsTicketComponent
    {
        const string TicketButton = "rebots-ticket-button";
        const string TicketPreviewLabel = "rebots-ticket-preview-label";
        const string TicketStateContainer = "rebots-ticket-state-container";
        const string TicketStateLabel = "rebots-ticket-state-label";
        const string TicketNewAnswer = "rebots-ticket-new-answer";
        const string TicketCreateLabel = "rebots-ticket-create-label";

        Button m_TicketButton;
        Label m_TicketPreviewLabel;
        VisualElement m_TicketStateContainer;
        Label m_TicketStateLabel;
        VisualElement m_TicketNewAnswer;
        Label m_TicketCreateLabel;

        private HelpdeskTicket ticket;
        private bool isNewAnswer;
        private string[] transData;

        public RebotsTicketComponent(HelpdeskTicket ticket, bool isNewAnswer, string[] transData)
        {
            this.ticket = ticket;
            this.isNewAnswer = isNewAnswer;
            this.transData = transData;
        }

        public void SetVisualElements(TemplateContainer ticketUIElement)
        {
            if (ticketUIElement == null)
            {
                return;
            }

            m_TicketButton = ticketUIElement.Q<Button>(TicketButton);
            m_TicketPreviewLabel = ticketUIElement.Q<Label>(TicketPreviewLabel);
            m_TicketStateContainer = ticketUIElement.Q(TicketStateContainer);
            m_TicketStateLabel = ticketUIElement.Q<Label>(TicketStateLabel);
            m_TicketNewAnswer = ticketUIElement.Q(TicketNewAnswer);
            m_TicketCreateLabel = ticketUIElement.Q<Label>(TicketCreateLabel);
        }

        public void SetTicketData(TemplateContainer ticketUIElement)
        {
            if (ticketUIElement == null)
            {
                return;
            }
            var createdLocalTime = ticket.created.ToLocalTime();
            m_TicketCreateLabel.text = createdLocalTime.ToString("yyyy.MM.dd HH:mm");

            var ticketData = ticket.data;
            var ticketPreviewStr = string.IsNullOrEmpty(ticketData.content) ? "" : ticketData.content;
            m_TicketPreviewLabel.text = ticketPreviewStr;

            if (ticket.isAnswers)
            {
                m_TicketStateContainer.AddToClassList(RebotsUIStaticString.RebotsBackgroundColor_Theme);
                m_TicketStateLabel.text = transData[0];
            }
            else
            {
                m_TicketStateContainer.AddToClassList(RebotsUIStaticString.RebotsBackgroundColor_DarkGrey);
                m_TicketStateLabel.text = transData[1];
            }

            m_TicketNewAnswer.style.display = (isNewAnswer) ? DisplayStyle.Flex : DisplayStyle.None;
        }

        public void RegisterCallbacks(Action<HelpdeskTicket> ticketAction)
        {
            m_TicketButton?.RegisterCallback<ClickEvent>(evt => ticketAction(ticket));
        }
    }
}
