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

        Button m_TicketButton;
        Label m_TicketNoLabel;
        Label m_TicketCategoryLabel;
        Label m_TicketPreviewLabel;
        VisualElement m_TicketStateContainer;
        Label m_TicketStateLabel;

        private HelpdeskTicket ticket;
        private string[] transData;

        public RebotsTicketComponent(HelpdeskTicket ticket, string[] transData)
        {
            this.ticket = ticket;
            this.transData = transData;
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

            m_TicketNoLabel.text = ticket.ticketId;
 
            var routeCategories = ticket.categories;
            var routeCategoryStr = "";
            foreach (var category in routeCategories)
            {
                routeCategoryStr = routeCategoryStr == "" ? category.name : category.name + " > " + routeCategoryStr;
            }
            var createDateStr = (string.Format("{0:d}", ticket.created));
            m_TicketCategoryLabel.text = createDateStr;

            var ticketData = ticket.data;
            var ticketPreviewStr = string.IsNullOrEmpty(ticketData.content) ? "" : ticketData.content;
            if (ticketPreviewStr.Length > 25)
            {
                ticketPreviewStr = ticketPreviewStr.Substring(0, 25);
                ticketPreviewStr += "...";

            }
            m_TicketPreviewLabel.text = ticketPreviewStr;

            if (ticket.isAnswers)
            {
                m_TicketStateContainer.AddToClassList(RebotsUIStaticString.RebotsBackgroundColor_Theme);
                m_TicketStateLabel.text = transData[0];
            }
            else
            {
                m_TicketStateContainer.AddToClassList(RebotsUIStaticString.RebotsBackgroundColor_Grey);
                m_TicketStateLabel.text = transData[1];
            }
        }

        public void RegisterCallbacks(Action<HelpdeskTicket> ticketAction)
        {
            m_TicketButton?.RegisterCallback<ClickEvent>(evt => ticketAction(ticket));
        }
    }
}
