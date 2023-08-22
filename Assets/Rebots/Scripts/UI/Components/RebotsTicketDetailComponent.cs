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

            if (m_content.Contains("<p>"))
            {
                var answerStr = m_content.ToString();
                var startTagRemove = answerStr.Replace("<p>", "");
                var endTagReplace = startTagRemove.Replace("</p>", "<br>");

                m_DetailContentLabel.text = endTagReplace.ToString();
            }
            else
            {
                m_DetailContentLabel.text = m_content;
            }
        }
    }
}
