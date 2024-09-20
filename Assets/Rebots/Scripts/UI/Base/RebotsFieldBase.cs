using UnityEngine.UIElements;

namespace Rebots.HelpDesk
{
    public class RebotsFieldBase
    {
        const string TooltipButtonContainer = "rebots-tooltip-button-container";
        const string TooltipButton = "rebots-tooltip-button";
        const string TooltipContainer = "rebots-tooltip-container";
        const string TooltipLabel = "rebots-tooltip-label";

        protected VisualElement m_Root;
        VisualElement m_TooltipButtonContainer;
        Button m_TooltipButton;
        VisualElement m_TooltipContainer;
        Label m_TooltipLabel;

        private bool useTooltip;
        private string tooltipComment;

        public RebotsFieldBase(bool useTooltip, string? tooltipComment)
        {
            this.useTooltip = useTooltip;
            this.tooltipComment = tooltipComment;
        }

        public virtual void SetVisualElements(TemplateContainer fieldUIElement)
        {
            m_Root = fieldUIElement;
            m_TooltipButtonContainer = fieldUIElement.Q<VisualElement>(TooltipButtonContainer);
            m_TooltipButton = fieldUIElement.Q<Button>(TooltipButton);
            m_TooltipContainer = fieldUIElement.Q<VisualElement>(TooltipContainer);
            m_TooltipLabel = fieldUIElement.Q<Label>(TooltipLabel);
        }

        public virtual void SetFieldData()
        {
            m_TooltipLabel.text = tooltipComment;

            m_TooltipButton?.RegisterCallback<ClickEvent>(evt => ClickTooltip());

            m_TooltipButtonContainer.style.display = (useTooltip) ? DisplayStyle.Flex : DisplayStyle.None;
            m_TooltipContainer.style.display = DisplayStyle.None;
        }
        
        public void ClickTooltip()
        {
            StyleEnum<DisplayStyle> displayState = m_TooltipContainer.style.display;

            m_TooltipContainer.style.display = (displayState == DisplayStyle.None)? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}
