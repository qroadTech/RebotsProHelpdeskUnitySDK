using HelpDesk.Sdk.Common.Objects;
using HelpDesk.Sdk.Library.Utility;
using System;
using UnityEngine.UIElements;

namespace Rebots.HelpDesk
{
    public class RebotsFaqComponent
    {
        const string FaqButton = "rebots-faq-button";
        const string FaqCategoryRouteLabel = "rebots-category-route-label";
        const string FaqTitleLabel = "rebots-faq-title-label";
        const string FaqContentsLabel = "rebots-contents-label";
        const string FaqLikeCountLabel = "rebots-faq-like-label";

        Button m_FaqButton;
        Label m_FaqCategoryRouteLabel;
        Label m_FaqTitleLabel;
        Label m_FaqContentsLabel;
        Label m_FaqLikeCountLabel;

        private Faq faq;
        private RebotsFaqAssetType faqType;
        private string searchStr;

        public RebotsFaqComponent(Faq faq, RebotsFaqAssetType faqType, string? search)
        {
            this.faq = faq;
            this.faqType = faqType;
            if (!string.IsNullOrEmpty(search))
            {
                this.searchStr = search;
            }
        }

        public void SetVisualElements(TemplateContainer faqUIElement)
        {
            if (faqUIElement == null)
            {
                return;
            }

            m_FaqButton = faqUIElement.Q<Button>(FaqButton);
            m_FaqCategoryRouteLabel = faqUIElement.Q<Label>(FaqCategoryRouteLabel);
            m_FaqTitleLabel = faqUIElement.Q<Label>(FaqTitleLabel);
            m_FaqContentsLabel = faqUIElement.Q<Label> (FaqContentsLabel);
            m_FaqLikeCountLabel = faqUIElement.Q<Label>(FaqLikeCountLabel);
        }

        public void SetFaqData(TemplateContainer faqUIElement)
        {
            if (faqUIElement == null)
            {
                return;
            }

            var routeCategories = faq.categories;
            var routeCategoryStr = "";
            foreach (var category in routeCategories)
            {
                var categoryName = category.name;
                routeCategoryStr = routeCategoryStr == "" ? categoryName : categoryName + " > " + routeCategoryStr;
            }
            m_FaqCategoryRouteLabel.text = routeCategoryStr;

            var faqTitleStr = faq.title;

            if (faqType == RebotsFaqAssetType.Popular)
            {
                m_FaqLikeCountLabel.text = faq.likeCount.ToString();
                m_FaqTitleLabel.text = faqTitleStr;
            }

            if (faqType == RebotsFaqAssetType.Search)
            {
                var searchHtml = "<u>" + searchStr + "</u>";
                faqTitleStr = faqTitleStr.Replace(searchStr, searchHtml);
                m_FaqTitleLabel.text = faqTitleStr;
                
                var contents = HtmlParser.HtmlToRemoveTag(faq.contents.ToString());
                contents = contents.Replace(searchStr, searchHtml);
                m_FaqContentsLabel.text = contents;
            }
        }

        public void RegisterCallbacks(Action<Faq> faqAction)
        {
            m_FaqButton?.RegisterCallback<ClickEvent>(evt => faqAction(faq));
        }
    }
}
