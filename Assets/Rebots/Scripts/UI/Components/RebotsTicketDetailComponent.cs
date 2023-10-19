using HelpDesk.Sdk.Library.Utility;
using System.Linq;
using UnityEngine.UIElements;

namespace Rebots.HelpDesk
{
    public class RebotsTicketDetailComponent
    {
        const string DetailSubLabel = "rebots-sub-label";
        const string DetailContentLabel = "rebots-content-label";

        Label m_DetailSubLabel;
        Label m_DetailContentLabel;

        private string sub;
        private string content;

        public RebotsTicketDetailComponent(string sub, string content)
        {
            this.sub = sub;
            this.content = content;
        }

        public void SetVisualElements(TemplateContainer answerUIElement)
        {
            if (answerUIElement == null)
            {
                return;
            }

            m_DetailSubLabel = answerUIElement.Q<Label>(DetailSubLabel);
            m_DetailContentLabel = answerUIElement.Q<Label>(DetailContentLabel);
        }

        public void SetTicketDetailData(TemplateContainer answerUIElement)
        {
            if (answerUIElement == null)
            {
                return;
            }

            m_DetailSubLabel.text = sub;
            m_DetailContentLabel.text = content;
        }
    }
}
