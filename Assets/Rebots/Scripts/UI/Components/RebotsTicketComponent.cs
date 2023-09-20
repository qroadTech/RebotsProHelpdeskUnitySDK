using HelpDesk.Sdk.Common.Objects;
using System;
using UnityEngine.UIElements;

namespace Rebots.HelpDesk
{
    public class RebotsTicketComponent
    {
        const string TicketButton = "rebots-ticket-button";
        const string TicketNoLabel = "rebots-ticket-no-label";
        const string TicketCategoryLabel = "rebots-ticket-category-label";
        const string TicketPreviewLabel = "rebots-ticket-preview-label";
        const string TicketStateContainer = "rebots-ticket-state-container";
        const string TicketStateLabel = "rebots-ticket-state-label";

        HelpdeskTicket m_Ticket;
        Button m_TicketButton;
        Label m_TicketNoLabel;
        Label m_TicketCategoryLabel;
        Label m_TicketPreviewLabel;
        VisualElement m_TicketStateContainer;
        Label m_TicketStateLabel;

        public RebotsTicketComponent(HelpdeskTicket ticket)
        {
            m_Ticket = ticket;
        }

        public void SetVisualElements(TemplateContainer ticketUIElement)
        {
            if (ticketUIElement == null)
            {
                return;
            }

            m_TicketButton = ticketUIElement.Q<Button>(TicketButton);
            m_TicketNoLabel = ticketUIElement.Q<Label>(TicketNoLabel);
            m_TicketCategoryLabel = ticketUIElement.Q<Label>(TicketCategoryLabel);
            m_TicketPreviewLabel = ticketUIElement.Q<Label>(TicketPreviewLabel);
            m_TicketStateContainer = ticketUIElement.Q(TicketStateContainer);
            m_TicketStateLabel = ticketUIElement.Q<Label>(TicketStateLabel);
        }

        public void SetTicketData(TemplateContainer ticketUIElement)
        {
            if (ticketUIElement == null)
            {
                return;
            }

            m_TicketNoLabel.text = m_Ticket.ticketId;

            var routeCategories = m_Ticket.categories;
            var routeCategoryStr = "";
            foreach (var category in routeCategories)
            {
                routeCategoryStr = routeCategoryStr == "" ? category.name : category.name + " > " + routeCategoryStr;
            }
            var createDateStr = (string.Format("{0:d}", m_Ticket.created));
            m_TicketCategoryLabel.text = createDateStr;

            var ticketData = m_Ticket.data;
            var ticketPreviewStr = string.IsNullOrEmpty(ticketData.content) ? "" : ticketData.content;
            if (ticketPreviewStr.Length > 25)
            {
                ticketPreviewStr = ticketPreviewStr.Substring(0, 25);
                ticketPreviewStr += "...";
            }
            m_TicketPreviewLabel.text = ticketPreviewStr;

            if (m_Ticket.isAnswers)
            {
                m_TicketStateContainer.AddToClassList(RebotsUIStaticString.RebotsBackgroundColor_Theme);
                m_TicketStateLabel.text = "Completed";
            }
            else
            {
                m_TicketStateContainer.AddToClassList(RebotsUIStaticString.RebotsBackgroundColor_Grey);
                m_TicketStateLabel.text = "Waiting";
            }
        }

        public void RegisterCallbacks(Action<HelpdeskTicket> ticketAction)
        {
            m_TicketButton?.RegisterCallback<ClickEvent>(evt => ticketAction(m_Ticket));
        }
    }
}
