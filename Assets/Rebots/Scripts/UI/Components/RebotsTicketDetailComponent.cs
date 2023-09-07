using HelpDesk.Sdk.Library.Utility;
using System.Linq;
using UnityEngine.UIElements;

namespace Rebots.HelpDesk
{
    public class RebotsTicketDetailComponent
    {
        const string DetailSubLabel = "rebots-sub-label";
        const string DetailContentLabel = "rebots-content-label";

        string m_sub;
        string m_content;
        Label m_DetailSubLabel;
        VisualElement m_DetailContentContainer;
        Label m_DetailContentLabel;

        public RebotsTicketDetailComponent(string sub, string content)
        {
            m_sub = sub;
            m_content = content;
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

            m_DetailSubLabel.text = m_sub;
            m_DetailContentLabel.text = m_content;
        }
    }
}
