﻿using HelpDesk.Sdk.Common.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace Rebots.HelpDesk
{
    public class RebotsFaqComponent
    {
        const string k_FaqButton = "rebots-faq-button";
        const string k_FaqCategoryRouteLabel = "rebots-category-route-label";
        const string k_FaqTitleLabel = "rebots-faq-title-label";
        const string k_FaqContentsLabel = "rebots-faq-contents-label";
        const string k_FaqLikeCountLabel = "rebots-faq-like-label";

        Faq m_Faq;
        RebotsFaqAssetType faqType;
        string searchStr;
        Button m_FaqButton;
        Label m_FaqCategoryRouteLabel;
        Label m_FaqTitleLabel;
        Label m_FaqContentsLabel;
        Label m_FaqLikeCountLabel;

        public RebotsFaqComponent(Faq faq, RebotsFaqAssetType faqType, string? search)
        {
            this.m_Faq = faq;
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

            m_FaqButton = faqUIElement.Q<Button>(k_FaqButton);
            m_FaqCategoryRouteLabel = faqUIElement.Q<Label>(k_FaqCategoryRouteLabel);
            m_FaqTitleLabel = faqUIElement.Q<Label>(k_FaqTitleLabel);
            m_FaqContentsLabel = faqUIElement.Q<Label> (k_FaqContentsLabel);
            m_FaqLikeCountLabel = faqUIElement.Q<Label>(k_FaqLikeCountLabel);
        }

        public void SetFaqData(TemplateContainer faqUIElement)
        {
            if (faqUIElement == null)
            {
                return;
            }

            var routeCategories = m_Faq.categories;
            var routeCategoryStr = "";
            foreach (var category in routeCategories)
            {
                routeCategoryStr = routeCategoryStr == "" ? category.name : category.name + " > " + routeCategoryStr;
            }
            m_FaqCategoryRouteLabel.text = routeCategoryStr;

            var faqTitleStr = m_Faq.title;
            if (faqTitleStr.Length > 50)
            {
                faqTitleStr = faqTitleStr.Substring(0, 48);
                faqTitleStr += "...";
            }

            if (faqType == RebotsFaqAssetType.popular)
            {
                m_FaqLikeCountLabel.text = m_Faq.likeCount.ToString();
                m_FaqTitleLabel.text = faqTitleStr;
            }

            if (faqType == RebotsFaqAssetType.search)
            {
                var searchHtml = "<u>" + searchStr + "</u>";
                faqTitleStr = faqTitleStr.Replace(searchStr, searchHtml);
                m_FaqTitleLabel.text = faqTitleStr;

                var contents = m_Faq.contents.ToString();
                contents = contents.Replace("<br>", " ");
                if (contents.Length > 179)
                {
                    contents = contents.Substring(0, 179);
                    contents += "...";
                }
                contents = contents.Replace(searchStr, searchHtml);
                m_FaqContentsLabel.text = contents;
            }
        }

        public void RegisterCallbacks(Action<Faq> faqAction)
        {
            m_FaqButton?.RegisterCallback<ClickEvent>(evt => faqAction(m_Faq));
        }
    }
}
