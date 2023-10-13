using Assets.Rebots;
using HelpDesk.Sdk.Common.Objects;
using HelpDesk.Sdk.Common.Protocols.Responses;
using HelpDesk.Sdk.Library.Utility;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Rebots.HelpDesk
{
    public class RebotsPageUI : RebotsModalScreen
    {
        public RebotsHelpdeskScreen helpdeskScreen;

        #region - - - Page UI Element - - -
        public VisualElement m_PageConatiner;

        public VisualElement m_PageTitlleContainer;
        public Button m_PageBackButton;
        public VisualElement m_PageTitleLabelContainer;
        public Label m_PageTitleLabel;
        public VisualElement m_PageRouteContainer;

        public VisualElement m_MainContainer;
        public VisualElement m_FaqPopularContainer;
        public Label m_PopularFaqCation;
        public VisualElement m_FaqPopularList;
        public VisualElement m_FaqCategoryContainer;
        public VisualElement m_FaqCategoryList;

        public VisualElement m_FaqSearchContainer;
        public Label m_SearchResultLabel;
        public Label m_SearchStringLabel;
        public VisualElement m_FaqSearchList;

        public VisualElement m_InquiryListContainer;
        public VisualElement m_InquiryList;

        public VisualElement m_FaqListContainer;
        public Label m_ListResultLabel;
        public VisualElement m_FaqList;
        public VisualElement m_SiblingCategoryContainer;
        public ScrollView m_SiblingCategoryScrollview;
        public VisualElement m_SiblingCategoryList;
        public VisualElement m_LowerCategoryContainer;
        public ScrollView m_LowerCategoryScrollview;
        public VisualElement m_LowerCategoryList;

        public VisualElement m_FaqContainer;
        public Label m_FaqTitleLabel;
        public VisualElement m_FaqDetailContainer;
        public Label m_FaqHelpfulLabel;
        public Label m_HelpfulYesLabel;
        public Label m_HelpfulNoLabel;

        public VisualElement m_PagingContainer;

        public VisualElement m_TicketCreateContainer;
        public VisualElement m_TicketFieldList;

        public VisualElement m_TicketSuccessContainer;
        public Label m_TicketReceivedLabel;
        public Label m_TicketReplyLabel;
        public Label m_TicketThankYouLabel;
        public Button m_TicketSuccessMainButton;
        public Label m_TicketReturnMainLabel;

        public VisualElement m_TicketListContainer;
        public VisualElement m_TicketList;

        public VisualElement m_TicketContainer;
        public VisualElement m_TicketDetailList;
        public Label m_TicketContentsLabel;
        public VisualElement m_TicketAnswerList;
        #endregion

        private RebotsLocalizationManager localizationManager;

        public PrivacySetting m_TicketPrivacySetting { get; private set; }
        public string m_Theme { get; private set; }
        public bool m_ScreenPortrait { get; private set; } = true;
        public Dictionary<string, string> m_ParameterDic { get; private set; }

        #region Run in 'Awake' call
        protected override void SetVisualElements()
        {
            base.SetVisualElements();

            m_PageConatiner = m_Root.Q(RebotsUIStaticString.PageConatiner);

            m_PageTitlleContainer = m_PageConatiner.Q(RebotsUIStaticString.PageTitlleContainer);
            m_PageBackButton = m_PageTitlleContainer.Q<Button>(RebotsUIStaticString.PageBackButton);
            m_PageTitleLabelContainer = m_PageTitlleContainer.Q(RebotsUIStaticString.PageTitleLabelContainer);
            m_PageTitleLabel = m_PageTitlleContainer.Q<Label>(RebotsUIStaticString.PageTitleLabel);
            m_PageRouteContainer = m_PageTitlleContainer.Q(RebotsUIStaticString.PageRouteContainer);

            m_MainContainer = m_PageConatiner.Q(RebotsUIStaticString.MainContainer);
            m_FaqPopularContainer = m_MainContainer.Q(RebotsUIStaticString.FaqPopularContainer);
            m_PopularFaqCation = m_FaqPopularContainer.Q<Label>(RebotsUIStaticString.PopularFaqCation);
            m_FaqPopularList = m_FaqPopularContainer.Q(RebotsUIStaticString.List);
            m_FaqCategoryContainer = m_MainContainer.Q(RebotsUIStaticString.FaqCategoryContainer);
            m_FaqCategoryList = m_FaqCategoryContainer.Q(RebotsUIStaticString.List);

            m_FaqSearchContainer = m_PageConatiner.Q(RebotsUIStaticString.FaqSearchContainer);
            m_SearchResultLabel = m_FaqSearchContainer.Q<Label>(RebotsUIStaticString.SearchResultLabel);
            m_SearchStringLabel = m_FaqSearchContainer.Q<Label>(RebotsUIStaticString.SearchStringLabel);
            m_FaqSearchList = m_FaqSearchContainer.Q(RebotsUIStaticString.List);

            m_InquiryListContainer = m_PageConatiner.Q(RebotsUIStaticString.InquiryListContainer);
            m_InquiryList = m_InquiryListContainer.Q(RebotsUIStaticString.List);

            m_FaqListContainer = m_PageConatiner.Q(RebotsUIStaticString.FaqListContainer);
            m_ListResultLabel = m_FaqListContainer.Q<Label>(RebotsUIStaticString.ListResultLabel);
            m_FaqList = m_FaqListContainer.Q(RebotsUIStaticString.List);
            m_SiblingCategoryContainer = m_FaqListContainer.Q(RebotsUIStaticString.SiblingCategoryContainer);
            m_SiblingCategoryScrollview = m_SiblingCategoryContainer.Q<ScrollView>(RebotsUIStaticString.SiblingCategoryScrollview);
            m_SiblingCategoryList = m_SiblingCategoryScrollview.Q(RebotsUIStaticString.CategoryList);
            m_LowerCategoryContainer = m_FaqListContainer.Q(RebotsUIStaticString.LowerCategoryContainer);
            m_LowerCategoryScrollview = m_LowerCategoryContainer.Q<ScrollView>(RebotsUIStaticString.LowerCategoryScrollview);
            m_LowerCategoryList = m_LowerCategoryContainer.Q(RebotsUIStaticString.CategoryList);

            m_FaqContainer = m_PageConatiner.Q(RebotsUIStaticString.FaqContainer);
            m_FaqTitleLabel = m_FaqContainer.Q<Label>(RebotsUIStaticString.FaqTitleLabel);
            m_FaqDetailContainer = m_FaqContainer.Q(RebotsUIStaticString.FaqDetailContainer);
            m_FaqHelpfulLabel = m_FaqContainer.Q<Label>(RebotsUIStaticString.FaqHelpfulLabel);
            m_HelpfulYesLabel = m_FaqContainer.Q<Label>(RebotsUIStaticString.HelpfulYesLabel);
            m_HelpfulNoLabel = m_FaqContainer.Q<Label>(RebotsUIStaticString.HelpfulNoLabel);

            m_PagingContainer = m_PageConatiner.Q(RebotsUIStaticString.PagingContainer);

            m_TicketCreateContainer = m_PageConatiner.Q(RebotsUIStaticString.TicketCreateContainer);
            m_TicketFieldList = m_TicketCreateContainer.Q(RebotsUIStaticString.TicketFieldList);

            m_TicketSuccessContainer = m_PageConatiner.Q(RebotsUIStaticString.TicketSuccessContainer);
            m_TicketReceivedLabel = m_TicketSuccessContainer.Q<Label>(RebotsUIStaticString.TicketReceivedLabel);
            m_TicketReplyLabel = m_TicketSuccessContainer.Q<Label>(RebotsUIStaticString.TicketReplyLabel);
            m_TicketThankYouLabel = m_TicketSuccessContainer.Q<Label>(RebotsUIStaticString.TicketThankYouLabel);
            m_TicketSuccessMainButton = m_TicketSuccessContainer.Q<Button>(RebotsUIStaticString.MainButton);
            m_TicketReturnMainLabel = m_TicketSuccessContainer.Q<Label>(RebotsUIStaticString.TicketReturnMainLabel);

            m_TicketListContainer = m_PageConatiner.Q(RebotsUIStaticString.TicketListContainer);
            m_TicketList = m_TicketListContainer.Q(RebotsUIStaticString.List);

            m_TicketContainer = m_PageConatiner.Q(RebotsUIStaticString.TicketContainer);
            m_TicketDetailList = m_TicketContainer.Q(RebotsUIStaticString.TicketDetailList);
            m_TicketContentsLabel = m_TicketContainer.Q<Label>(RebotsUIStaticString.TicketContentsLabel);
            m_TicketAnswerList = m_TicketContainer.Q(RebotsUIStaticString.TicketAnswerList);
        }

        protected override void RegisterButtonCallbacks()
        {
            m_PageBackButton?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.ChangePage(true));
            m_TicketSuccessMainButton?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.ShowMain(false));
        }
        #endregion

        #region Set before show page
        public void SetTranslationText()
        {
            localizationManager = helpdeskScreen.rebotsSettingManager.localizationManager;

            m_PopularFaqCation.text = localizationManager.translationDic[RebotsUIStaticString.PopularFaqCation];

            m_FaqHelpfulLabel.text = localizationManager.translationDic[RebotsUIStaticString.FaqHelpfulLabel];
            m_HelpfulYesLabel.text = localizationManager.translationDic[RebotsUIStaticString.HelpfulYesLabel];
            m_HelpfulNoLabel.text = localizationManager.translationDic[RebotsUIStaticString.HelpfulNoLabel];

            m_TicketReceivedLabel.text = localizationManager.translationDic[RebotsUIStaticString.TicketReceivedLabel];
            m_TicketReplyLabel.text = localizationManager.translationDic[RebotsUIStaticString.TicketReplyLabel];
            m_TicketThankYouLabel.text = localizationManager.translationDic[RebotsUIStaticString.TicketThankYouLabel];
            m_TicketReturnMainLabel.text = localizationManager.translationDic[RebotsUIStaticString.TicketReturnMainLabel];
        }

        public void SetHelpdeskData(HelpdeskSetting helpdeskSetting)
        {
            m_Theme = helpdeskSetting.theme;

            m_ScreenPortrait = (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.AutoRotation);
        }

        public void SetPrivacyData(PrivacySetting ticketPrivacySetting)
        {
            m_TicketPrivacySetting = ticketPrivacySetting;
        }

        public void SetParameterData(RebotsParameterData parameterData)
        {
            m_ParameterDic = new Dictionary<string, string>();
            m_ParameterDic = parameterData.parameters;
        }
        #endregion

        #region Set callback data after API
        public void OnFaqRecommendUpdated(HelpdeskFaqListResponse response)
        {
            var faqs = response.faqs;

            if (faqs != null && faqs.Count() > 0)
            {
                foreach (var item in faqs)
                {
                    TemplateContainer faqUIElement = null;
                    helpdeskScreen.rebotsUICreater.CreateFaq(item, RebotsFaqAssetType.Popular, null, helpdeskScreen.ClickFaq, out faqUIElement);

                    faqUIElement.style.flexShrink = 0;
                    m_FaqPopularList.Add(faqUIElement);
                }
            }
            else
            {
                ShowVisualElement(m_FaqPopularContainer, false);
            }
        }

        public void OnFaqSearchUpdated(HelpdeskFaqSearchResponse response)
        {
            var searchResult = response;
            var faqs = searchResult.faqs;
            var search = searchResult.search;
            var currentPage = searchResult.page;
            var recordCount = searchResult.recordCount;

            var recordCountStr = "<color=" + RebotsUIStaticString.BlackColor + ">" + recordCount.ToString() + "</color>";
            var searchResultStr = localizationManager.translationDic[RebotsUIStaticString.SearchResultLabel];
            m_SearchResultLabel.text = string.Format(searchResultStr, recordCountStr);
            m_SearchStringLabel.text = search;

            if (faqs != null && faqs.Count() > 0)
            {
                foreach (var item in faqs)
                {
                    TemplateContainer faqUIElement = null;
                    helpdeskScreen.rebotsUICreater.CreateFaq(item, RebotsFaqAssetType.Search, search, helpdeskScreen.ClickFaq, out faqUIElement);

                    var contentsLabel = faqUIElement.Q<Label>(RebotsUIStaticString.ContentsLabel);
                    StartCoroutine(LabelMultilineEllipsis(contentsLabel));
                    m_FaqSearchList.Add(faqUIElement);
                }
            }

            int pageSize = helpdeskScreen.listPagingSize;
            if (recordCount != 0 && recordCount > 0)
            {
                var totalPage = ((recordCount % pageSize) == 0) ? (recordCount / pageSize) : (recordCount / pageSize) + 1;
                RebotsPagingData<string> pagingData = new RebotsPagingData<string>(search, totalPage, currentPage);

                helpdeskScreen.rebotsUICreater.CreatePaging(pagingData, helpdeskScreen.ShowSearch, out TemplateContainer pageUIElement);

                m_PagingContainer.Add(pageUIElement);
            }
        }

        public void OnFaqUpdated(HelpdeskFaqResponse response)
        {
            var faq = response;

            m_PageTitleLabel.text = "FAQ";

            Label routeLabel = null;
            var routeList = faq.categories.Reverse().Select(c => new { c.name, c.id }).ToList();
            routeList.Insert(0, new { name = "FAQ", id = 0 });
            foreach (var item in routeList)
            {
                helpdeskScreen.rebotsUICreater.CreateRouteLabel(item.name, (faq.categoryId == item.id), out routeLabel);
                m_PageRouteContainer.Add(routeLabel);
            }

            m_FaqTitleLabel.text = faq.title;

            var contentsDic = HtmlParser.HtmlToUnityTag(faq.contents.ToString());
            foreach (var item in contentsDic)
            {
                switch (item.type)
                {
                    case "text":
                        helpdeskScreen.rebotsUICreater.CreateLabel(item.value, out Label labelUIElement);
                        m_FaqDetailContainer.Add(labelUIElement);
                        break;
                    case "img":
                        var imgContents = item.value;
                        helpdeskScreen.ImageUrlToTexture2D(OnFaqImageAdded, new System.Uri(imgContents), imgContents);
                        break;
                    case "link":
                        var linkContents = item.value.Split(HtmlParser.linkSplitPoint);
                        helpdeskScreen.rebotsUICreater.CreateLinkLabel(linkContents[0], linkContents[1], out Label linkLabelUIElement);
                        m_FaqDetailContainer.Add(linkLabelUIElement);
                        break;
                    case "iframe":
                        var videoContents = item.value;
                        helpdeskScreen.rebotsUICreater.CreateLinkLabel(videoContents, null, out Label iframeLabelUIElement);
                        m_FaqDetailContainer.Add(iframeLabelUIElement);
                        break;
                }
            }
        }

        public void OnFaqImageAdded(Texture2D texture, string externalLinkUri)
        {
            if (texture != null && !string.IsNullOrEmpty(externalLinkUri))
            {
                VisualElement imgUIElement = new VisualElement();

                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2));
                imgUIElement.style.backgroundImage = new StyleBackground(sprite);
                imgUIElement.style.unityBackgroundScaleMode = ScaleMode.ScaleToFit;
                imgUIElement.style.width = Length.Percent(100);
                imgUIElement.style.minHeight = new Length(300f);
                imgUIElement.style.flexShrink = 0;
                imgUIElement.style.flexGrow = 1;

                m_FaqDetailContainer.Add(imgUIElement);
            }
        }

        public void OnFaqCategoriesUpdated(HelpdeskFaqCategoriesResponse response)
        {
            var categories = response.items;

            if (m_ScreenPortrait)
                m_FaqCategoryList.style.flexDirection = FlexDirection.Column;
            else
                m_FaqCategoryList.style.flexDirection = FlexDirection.Row;

            var faqCategories = (response.items != null) ? response.items.Where(x => x.use == 1).ToArray() : new Category[0];
            for (int i = 0; i < faqCategories.Count(); i++)
            {
                var category = faqCategories[i];

                TemplateContainer categoryUIElement = null;
                helpdeskScreen.rebotsUICreater.CreateCategory<Category>(category, RebotsCategoryAssetType.Cs, helpdeskScreen.ClickFaqCategory, out categoryUIElement);

                if (m_ScreenPortrait)
                {
                    categoryUIElement.style.width = Length.Percent(100f);
                }
                else
                {
                    if (i % 2 == 1)
                    {
                        categoryUIElement.style.width = Length.Percent(49f);
                    }
                    else
                    {
                        categoryUIElement.style.width = Length.Percent(50f);
                        categoryUIElement.style.paddingRight = 10f;
                    }
                }
                m_FaqCategoryList.Add(categoryUIElement);
            }
        }

        public void OnCsCategoriesUpdated(HelpdeskTicketCategoriesResponse response)
        {
            var categories = response.items;

            m_PageTitleLabel.text = "Inquiry";

            m_InquiryList.style.flexDirection = m_ScreenPortrait ? FlexDirection.Column : FlexDirection.Row;

            var csCategories = (categories != null) ? categories.Where(x => x.use == 1).ToArray() : new Category[0];
            
            for (int i = 0; i < csCategories.Count(); i++)
            {
                var category = csCategories[i];
                TemplateContainer categoryUIElement = null;
                helpdeskScreen.rebotsUICreater.CreateCategory<Category>(category, RebotsCategoryAssetType.Cs, helpdeskScreen.ClickCsCategory, out categoryUIElement);

                if (m_ScreenPortrait)
                {
                    categoryUIElement.style.width = Length.Percent(100f);
                }
                else
                {
                    if (i % 2 == 1)
                    {
                        categoryUIElement.style.width = Length.Percent(49f);
                    }
                    else
                    {
                        categoryUIElement.style.width = Length.Percent(50f);
                        categoryUIElement.style.paddingRight = 10f;
                    }
                }
                m_InquiryList.Add(categoryUIElement);
            }
        }

        public void OnSubCategoryUpdated(HelpdeskFaqCategoryResponse response)
        {
            var faqCategory = response;

            m_PageTitleLabel.text = "FAQ";

            Label routeLabel = null;
            List<string> routeList = faqCategory.categories.Select(c => c.name).Reverse().ToList();
            routeList.Insert(0, "FAQ");
            foreach (var item in routeList)
            {
                helpdeskScreen.rebotsUICreater.CreateRouteLabel(item, false, out routeLabel);
                m_PageRouteContainer.Add(routeLabel);
            }
            helpdeskScreen.rebotsUICreater.CreateRouteLabel(faqCategory.name, true, out routeLabel);
            m_PageRouteContainer.Add(routeLabel);

            var siblingCategories = (faqCategory.siblingCategories != null) ? faqCategory.siblingCategories.Where(x => x.use == 1).ToList() : null;
            var siblingCount = (siblingCategories != null) ? siblingCategories.Count() : 0;
            if (siblingCount > 0)
            {
                ShowVisualElement(m_SiblingCategoryContainer, true);
                var selectedIndex = -1;
                for (int i = 0; i < siblingCount; i++)
                {
                    var item = siblingCategories[i];
                    TemplateContainer categoryUIElement = null;

                    if (item.id == faqCategory.id)
                    {
                        selectedIndex = i;
                        helpdeskScreen.rebotsUICreater.CreateCategory<Category>(item, RebotsCategoryAssetType.Selected, helpdeskScreen.ClickFaqCategory, out categoryUIElement);
                    }
                    else
                    {
                        helpdeskScreen.rebotsUICreater.CreateCategory<Category>(item, RebotsCategoryAssetType.Sibling, helpdeskScreen.ClickFaqCategory, out categoryUIElement);
                    }
                    m_SiblingCategoryList.Add(categoryUIElement);
                }

                StartCoroutine(ScrollingSiblingCategory(selectedIndex));
            }
            else
            {
                ShowVisualElement(m_SiblingCategoryContainer, false);
            }

            var lowerCategories = (faqCategory.subCategories != null) ? faqCategory.subCategories.Where(x => x.use == 1) : null;
            var lowerCount = (lowerCategories != null) ? lowerCategories.Count() : 0;
            if (lowerCount > 0)
            {
                ShowVisualElement(m_LowerCategoryContainer, true);
                foreach (var item in lowerCategories)
                {
                    TemplateContainer categoryUIElement = null;
                    helpdeskScreen.rebotsUICreater.CreateCategory<Category>(item, RebotsCategoryAssetType.Lower, helpdeskScreen.ClickFaqCategory, out categoryUIElement);
                    m_LowerCategoryList.Add(categoryUIElement);
                }
            }
            else
            {
                ShowVisualElement(m_LowerCategoryContainer, false);
            }

            int displayPage = faqCategory.page;
            int pageSize = helpdeskScreen.listPagingSize;
            int recordCount = faqCategory.recordCount;
            string listResultFormat = localizationManager.translationDic[RebotsUIStaticString.ListResultLabel];
            m_ListResultLabel.text = string.Format(listResultFormat, recordCount.ToString());

            var faqs = (faqCategory.faqs != null) ? faqCategory.faqs.Where(x => x.use == 1) : null;
            int subSize = (faqs != null) ? faqs.Count() : 0;
            if (subSize > 0)
            {
                foreach (var item in faqs.Take(pageSize))
                {
                    TemplateContainer categoryUIElement = null;
                    helpdeskScreen.rebotsUICreater.CreateCategory<Faq>(item, RebotsCategoryAssetType.Faq, helpdeskScreen.ClickFaq, out categoryUIElement);
                    m_FaqList.Add(categoryUIElement);
                }
            }

            if (recordCount > 0)
            {
                var totalPage = ((recordCount % pageSize) == 0) ? (recordCount / pageSize) : (recordCount / pageSize) + 1;
                RebotsPagingData<Category> pagingData = new RebotsPagingData<Category>(faqCategory, totalPage, displayPage);

                helpdeskScreen.rebotsUICreater.CreatePaging(pagingData, helpdeskScreen.ShowFaqSubCategory, out TemplateContainer pageUIElement);

                m_PagingContainer.Add(pageUIElement);
            }
        }

        public void OnSubCategoryUpdated(HelpdeskTicketCategoryResponse response)
        {
            var csCategory = response;

            m_PageTitleLabel.text = "Inquiry";

            Label routeLabel = null;
            List<string> routeList = csCategory.categories.Select(c => c.name).Reverse().ToList();
            routeList.Insert(0, "Inquiry");
            foreach (var item in routeList)
            {
                helpdeskScreen.rebotsUICreater.CreateRouteLabel(item, false, out routeLabel);
                m_PageRouteContainer.Add(routeLabel);
            }
            helpdeskScreen.rebotsUICreater.CreateRouteLabel(csCategory.name, true, out routeLabel);
            m_PageRouteContainer.Add(routeLabel);

            m_InquiryList.style.flexDirection = m_ScreenPortrait ? FlexDirection.Column : FlexDirection.Row;

            var subCategories = (csCategory.subCategories != null) ? csCategory.subCategories.Where(x => x.use == 1).ToArray() : new Category[0];
            for (int i = 0; i < subCategories.Count(); i++)
            {
                var category = subCategories[i];
                TemplateContainer categoryUIElement = null;
                helpdeskScreen.rebotsUICreater.CreateCategory(category, RebotsCategoryAssetType.Cs, helpdeskScreen.ClickCsCategory, out categoryUIElement);
                if (m_ScreenPortrait)
                {
                    categoryUIElement.style.width = Length.Percent(100f);
                }
                else
                {
                    if (i % 2 == 1)
                    {
                        categoryUIElement.style.width = Length.Percent(49f);
                    }
                    else
                    {
                        categoryUIElement.style.width = Length.Percent(50f);
                        categoryUIElement.style.paddingRight = 10f;
                    }
                }
                m_InquiryList.Add(categoryUIElement);
            }
        }

        public void OnCsCategoryFieldsUpdated(HelpdeskTicketCategoryField response)
        {
            var csCategory = response.category;

            m_PageTitleLabel.text = "Inquiry";

            Label routeLabel = null;
            List<string> routeList = csCategory.categories.Select(c => c.name).Reverse().ToList();
            routeList.Insert(0, "Inquiry");
            foreach (var item in routeList)
            {
                helpdeskScreen.rebotsUICreater.CreateRouteLabel(item, false, out routeLabel);
                m_PageRouteContainer.Add(routeLabel);
            }
            helpdeskScreen.rebotsUICreater.CreateRouteLabel(csCategory.name, true, out routeLabel);
            m_PageRouteContainer.Add(routeLabel);

            var fields = csCategory.inputFields;
            string validationField = localizationManager.translationDic[RebotsUIStaticString.ValidRequired];
            string validationEmail = localizationManager.translationDic[RebotsUIStaticString.ValidEmail];
            if (fields != null && fields.Count() > 0)
            {
                foreach (var field in fields)
                {
                    string parameterValue = "";
                    m_ParameterDic.TryGetValue(field.name, out parameterValue);

                    TemplateContainer fieldUIElement = null;
                    object fieldUIComponent = null;
                    string[] validationComment = (field.name == "email") ? new string[] { validationField, validationEmail } : new string[] { validationField };


                    helpdeskScreen.rebotsUICreater.CreateCsCategoryField(field, parameterValue, validationComment, out fieldUIElement, out fieldUIComponent);

                    var m_RequiredLabel = fieldUIElement.Q<Label>(RebotsUIStaticString.RequiredLabel);
                    m_RequiredLabel.text = localizationManager.translationDic[RebotsUIStaticString.RequiredLabel];

                    if (field.fieldType == RebotsInputFieldType.File)
                    {
                        var m_ChooseFileButtonLabel = fieldUIElement.Q<Label>(RebotsUIStaticString.ChooseFileButtonLabel);
                        m_ChooseFileButtonLabel.text = localizationManager.translationDic[RebotsUIStaticString.ChooseFileButtonLabel];
                        var m_NoFileLabel = fieldUIElement.Q<Label>(RebotsUIStaticString.NoFileLabel);
                        m_NoFileLabel.text = localizationManager.translationDic[RebotsUIStaticString.NoFileLabel];
                    }

                    m_TicketFieldList.Add(fieldUIElement);
                    helpdeskScreen.AddFieldDic(field, fieldUIComponent);
                }
            }

            if (m_TicketPrivacySetting != null)
            {
                string[] formSectionTransData = { 
                    localizationManager.translationDic[RebotsUIStaticString.PrivacyPrpose],
                    localizationManager.translationDic[RebotsUIStaticString.PrivacyCollection],
                    localizationManager.translationDic[RebotsUIStaticString.PrivacyPeriod],
                    localizationManager.translationDic[RebotsUIStaticString.PrivacyConsignment],
                    localizationManager.translationDic[RebotsUIStaticString.PrivacyProviding],
                    localizationManager.translationDic[RebotsUIStaticString.PrivacyConsignmentPeriod]
                };

                TemplateContainer privacyUIElement = null;
                TicketCategoryInputField privacyField = new TicketCategoryInputField
                {
                    inputType = "privacy",
                };
                helpdeskScreen.rebotsUICreater.CreatePrivacyField(m_TicketPrivacySetting, formSectionTransData, helpdeskScreen.ClickTicketSubmit, out privacyUIElement);

                var m_RequiredLabel = privacyUIElement.Q<Label>(RebotsUIStaticString.RequiredLabel);
                m_RequiredLabel.text = localizationManager.translationDic[RebotsUIStaticString.RequiredLabel];

                var m_AgreeCheckLabel = privacyUIElement.Q<Label>(RebotsUIStaticString.AgreeCheckLabel);
                m_AgreeCheckLabel.text = localizationManager.translationDic[RebotsUIStaticString.AgreeCheckLabel];

                var m_PrivacyFieldLabel = privacyUIElement.Q<Label>(RebotsUIStaticString.PrivacyFieldLabel);
                m_PrivacyFieldLabel.text = localizationManager.translationDic[RebotsUIStaticString.PrivacyFieldLabel];

                var m_TicketSubmitLabel = privacyUIElement.Q<Label>(RebotsUIStaticString.TicketSubmitLabel);
                m_TicketSubmitLabel.text = localizationManager.translationDic[RebotsUIStaticString.TicketSubmitLabel];

                m_TicketFieldList.Add(privacyUIElement);
            }
        }

        public void OnTicketCreate(HelpDeskTicketCreateResponse response)
        {
            helpdeskScreen.ShowTicketSuccess();
        }

        public void OnMyTicketsUpdated(HelpDeskTicketListResponse response)
        {
            var tickets = response.items;

            m_PageTitleLabel.text = "My Tickets";

            if (tickets != null && tickets.Count() > 0)
            {
                tickets = tickets.OrderByDescending(x => x.created).ToArray();
                foreach (var item in tickets)
                {
                    TemplateContainer faqUIElement = null;
                    helpdeskScreen.rebotsUICreater.CreateTicket(item, helpdeskScreen.ClickTicket, out faqUIElement);

                    var ticketLanguage = item.data.language;
                    switch (ticketLanguage)
                    {
                        case "ko":
                            faqUIElement.styleSheets.Add(helpdeskScreen.fontKr);
                            break;
                        case "ja":
                            faqUIElement.styleSheets.Add(helpdeskScreen.fontJa);
                            break;
                        case "zh-cn":
                            faqUIElement.styleSheets.Add(helpdeskScreen.fontCn);
                            break;
                        case "zh-tw":
                            faqUIElement.styleSheets.Add(helpdeskScreen.fontTw);
                            break;
                        case "th":
                            faqUIElement.styleSheets.Add(helpdeskScreen.fontTh);
                            break;
                        default:
                            faqUIElement.styleSheets.Add(helpdeskScreen.fontEn);
                            break;
                    }

                    m_TicketList.Add(faqUIElement);
                }
            }
        }

        public void OnTicketDetailUpdated(HelpDeskTicketDetailResponse response)
        {
            var ticket = response.ticket;
            var ticketData = response.ticket.data;
            var answers = response.ticket.answers;

            m_PageTitleLabel.text = "My Tickets";

            m_TicketContainer.styleSheets.Clear();
            switch (ticketData.language)
            {
                case "ko":
                    m_TicketContainer.styleSheets.Add(helpdeskScreen.fontKr);
                    break;
                case "ja":
                    m_TicketContainer.styleSheets.Add(helpdeskScreen.fontJa);
                    break;
                case "zh-cn":
                    m_TicketContainer.styleSheets.Add(helpdeskScreen.fontCn);
                    break;
                case "zh-tw":
                    m_TicketContainer.styleSheets.Add(helpdeskScreen.fontTw);
                    break;
                case "th":
                    m_TicketContainer.styleSheets.Add(helpdeskScreen.fontTh);
                    break;
                default:
                    m_TicketContainer.styleSheets.Add(helpdeskScreen.fontEn);
                    break;
            }

            var dataDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(ticketData.data);

            if (dataDic != null && dataDic.Count() > 0)
            {
                foreach (var item in dataDic)
                {
                    TemplateContainer fieldUIElement = null;
                    helpdeskScreen.rebotsUICreater.CreateTicketDetail(item.Key, item.Value, RebotsTicketDetailAssetType.Field, out fieldUIElement);
                    m_TicketDetailList.Add(fieldUIElement);
                }
            }

            m_TicketContentsLabel.text = ticketData.content;

            if (answers != null && answers.Count() > 0)
            {
                foreach (var item in answers)
                {
                    TemplateContainer answerUIElement = null;
                    helpdeskScreen.rebotsUICreater.CreateTicketDetail(string.Format("{0:d}", item.answered), "", RebotsTicketDetailAssetType.Answer, out answerUIElement);

                    if (answerUIElement != null)
                    {
                        VisualElement AnswerContentContainer = answerUIElement.Q(RebotsUIStaticString.TicketAnswerContentContainer);
                        AnswerContentContainer.Clear();

                        var contentsDic = HtmlParser.HtmlToUnityTag(item.content.ToString());
                        int imgCount = 1;
                        foreach (var content in contentsDic)
                        {
                            switch (content.type)
                            {
                                case "text":
                                    helpdeskScreen.rebotsUICreater.CreateLabel(content.value, out Label labelUIElement);
                                    AnswerContentContainer.Add(labelUIElement);
                                    break;
                                case "link":
                                    var linkContents = content.value.Split(HtmlParser.linkSplitPoint);
                                    helpdeskScreen.rebotsUICreater.CreateLinkLabel(linkContents[0], linkContents[1], out Label linkLabelUIElement);
                                    AnswerContentContainer.Add(linkLabelUIElement);
                                    break;
                                case "img":
                                    var imgContents = content.value;
                                    helpdeskScreen.rebotsUICreater.CreateLinkLabel(imgContents, ("Image Link " + imgCount.ToString()), out Label imgLabelUIElement);
                                    AnswerContentContainer.Add(imgLabelUIElement);
                                    imgCount++;
                                    break;
                                case "iframe":
                                    var mediaContents = content.value;
                                    helpdeskScreen.rebotsUICreater.CreateLinkLabel(mediaContents, null, out Label iframeLabelUIElement);
                                    AnswerContentContainer.Add(iframeLabelUIElement);
                                    break;
                            }
                        }
                    }

                    m_TicketAnswerList.Add(answerUIElement);
                }
            }
        }
        #endregion

        #region (private) Set Sibling Category Scrolling Point
        private IEnumerator ScrollingSiblingCategory(int index)
        {
            yield return null;

            var allElementWidth = 0f;
            var forwardElementWidth = 0f;
            var categoryElements = m_SiblingCategoryList.Children();
            var i = 0;
            foreach (var categoryElement in categoryElements)
            {
                if (index == i++)
                {
                    forwardElementWidth = allElementWidth;
                }
                allElementWidth += (m_SiblingCategoryList.childCount == i) ? 0 : categoryElement.contentRect.width;
            }

            var scrollHighValue = m_SiblingCategoryScrollview.horizontalScroller.highValue;
            var scrollRange = forwardElementWidth / allElementWidth;
            var scrollValue = scrollRange * scrollHighValue;
            m_SiblingCategoryScrollview.horizontalScroller.value = scrollValue;
        }
        #endregion

        #region (private) Set Multi-Line Ellipsis Point
        private IEnumerator LabelMultilineEllipsis(Label labelElement)
        {
            yield return null;

            string text = labelElement.text;

            float areaWidth = labelElement.contentRect.width;
            float areaHeight = labelElement.contentRect.height;
            Rect textRect = new Rect(0, 0, areaWidth, areaHeight);

            GUIStyle textStyle = new GUIStyle();
            textStyle.font = helpdeskScreen.GetLanguageFontAsset(localizationManager.language);
            textStyle.fontSize = 16;
            textStyle.wordWrap = true;

            string breakText = helpdeskScreen.AddSpaceToWordWrapPoint(text, textRect, textStyle);
            if (!string.IsNullOrEmpty(breakText))
            {
                labelElement.text = breakText + "...";
            }
        }
        #endregion
    }
}