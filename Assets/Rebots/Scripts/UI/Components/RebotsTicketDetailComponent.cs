using HelpDesk.Sdk.Library.Utility;
using System.Linq;
using UnityEngine.UIElements;

namespace Rebots.HelpDesk
{
    public class RebotsTicketDetailComponent
    {
        const string DetailSubLabel = "rebots-sub-label";
        const string DetailContentLabel = "rebots-content-label";
        const string TicketNewAnswer = "rebots-ticket-new-answer";

        Label m_DetailSubLabel;
        Label m_DetailContentLabel;
        VisualElement m_TicketNewAnswer;

        private string sub;
        private string content;
        private bool isNewAnswer;

        public RebotsTicketDetailComponent(string sub, string content, bool isNewAnswer = false)
        {
            this.sub = sub;
            this.content = content;
            this.isNewAnswer = isNewAnswer;
        }

        public void SetVisualElements(TemplateContainer answerUIElement)
        {
            if (answerUIElement == null)
            {
                return;
            }

            m_DetailSubLabel = answerUIElement.Q<Label>(DetailSubLabel);
            m_DetailContentLabel = answerUIElement.Q<Label>(DetailContentLabel);
            m_TicketNewAnswer = answerUIElement.Q(TicketNewAnswer);
        }

        public void SetTicketDetailData(TemplateContainer answerUIElement)
        {
            if (answerUIElement == null)
            {
                return;
            }

            m_DetailSubLabel.text = sub;

            if (m_TicketNewAnswer != null)
            {
                m_TicketNewAnswer.style.display = (isNewAnswer) ? DisplayStyle.Flex : DisplayStyle.None;
            }

            if (m_DetailContentLabel != null)
            {
                if (string.IsNullOrEmpty(content.Trim()))
                {
                    m_DetailContentLabel.text = "-";
                }
                else
                {
                    m_DetailContentLabel.text = content;
                }
            }
        }
    }
}
