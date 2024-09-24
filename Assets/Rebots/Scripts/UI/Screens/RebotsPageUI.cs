using Assets.Rebots;
using HelpDesk.Sdk.Common.Objects;
using HelpDesk.Sdk.Common.Protocols.Responses;
using HelpDesk.Sdk.Library.Utility;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
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

        public VisualElement m_MainContainer;
        public VisualElement m_FaqPopularContainer;
        public Label m_PopularFaqCaption;
        public ScrollView m_PopularScrollview;
        public VisualElement m_FaqPopularList;
        public Button m_PopularLeftButton;
        public Button m_PopularRightButton;
        public VisualElement m_FaqCategoryContainer;
        public Label m_MainCategoryCaption;
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
        public Button m_SiblingLeftButton;
        public Button m_SiblingRightButton;
        public VisualElement m_LowerCategoryContainer;
        public ScrollView m_LowerCategoryScrollview;
        public VisualElement m_LowerCategoryList;
        public Button m_LowerLeftButton;
        public Button m_LowerRightButton;

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
        public Label m_TicketListResultLabel;
        public VisualElement m_TicketList;

        public VisualElement m_TicketContainer;
        public Button m_TicketDetailButton;
        public Label m_TicketDetailLabel;
        public Button m_TicketAnswerButton;
        public Label m_TicketAnswerLabel;
        public VisualElement m_TicketDetailContainer;
        public Label m_TicketCreateLabel;
        public Label m_TicketCategoryRouteLabel;
        public Label m_TicketEmailLabel;
        public Label m_TicketEmailValue;
        public Label m_TicketContentsLabel;
        public Label m_TicketContentsValue;
        public VisualElement m_TicketAttachmentContainer;
        public Foldout m_TicketDetailFoldout;
        public VisualElement m_TicketDetailList;
        public VisualElement m_TicketAnswerContainer;
        public VisualElement m_TicketAnswerList;
        public VisualElement m_TicketAnswerContentContainer;
        public VisualElement m_TicketAnswerNoneContainer;
        public Label m_TicketAnswerNoneLabel;
        public Label m_TicketAnswerNoticeLabel;
        public Label m_FileNameLabel;
        public Label m_FileSizeLabel;
        public Button m_FileRemoveButton;
        #endregion

        public RebotsLocalizationManager LocalizationManager { get; private set; }
        public PrivacySetting TicketPrivacySetting { get; private set; }
        public string Theme { get; private set; }
        public Dictionary<string, string> ParameterDic { get; private set; }
        public Dictionary<string, string> TicketAnswerDic { get; private set; }

        private float siblingHighValue = 0f;
        private int siblingIndex = 0;

        private float lowerHighValue = 0f;
        private int lowerIndex = 0;

        private float popularHighValue = 0f;
        private int popularIndex = 0;

        #region Run in 'Awake' call
        protected override void SetVisualElements()
        {
            base.SetVisualElements();

            m_PageConatiner = m_Root.Q(RebotsUIStaticString.PageConatiner);

            m_PageTitlleContainer = m_PageConatiner.Q(RebotsUIStaticString.PageTitlleContainer);
            m_PageBackButton = m_PageTitlleContainer.Q<Button>(RebotsUIStaticString.PageBackButton);
            m_PageTitleLabelContainer = m_PageTitlleContainer.Q(RebotsUIStaticString.PageTitleLabelContainer);
            m_PageTitleLabel = m_PageTitlleContainer.Q<Label>(RebotsUIStaticString.PageTitleLabel);

            m_MainContainer = m_PageConatiner.Q(RebotsUIStaticString.MainContainer);
            m_FaqPopularContainer = m_MainContainer.Q(RebotsUIStaticString.FaqPopularContainer);
            m_PopularFaqCaption = m_FaqPopularContainer.Q<Label>(RebotsUIStaticString.PopularFaqCaption);
            m_PopularScrollview = m_FaqPopularContainer.Q<ScrollView>(RebotsUIStaticString.PopularScrollview);
            m_FaqPopularList = m_PopularScrollview.Q(RebotsUIStaticString.List);
            m_PopularLeftButton = m_FaqPopularContainer.Q<Button>(RebotsUIStaticString.ScrollLeftButton);
            m_PopularRightButton = m_FaqPopularContainer.Q<Button>(RebotsUIStaticString.ScrollRightButton);
            m_FaqCategoryContainer = m_MainContainer.Q(RebotsUIStaticString.FaqCategoryContainer);
            m_MainCategoryCaption = m_FaqCategoryContainer.Q<Label>(RebotsUIStaticString.MainCategoryCaption);
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
            m_SiblingLeftButton = m_SiblingCategoryContainer.Q<Button>(RebotsUIStaticString.ScrollLeftButton);
            m_SiblingRightButton = m_SiblingCategoryContainer.Q<Button>(RebotsUIStaticString.ScrollRightButton);
            m_LowerCategoryContainer = m_FaqListContainer.Q(RebotsUIStaticString.LowerCategoryContainer);
            m_LowerCategoryScrollview = m_LowerCategoryContainer.Q<ScrollView>(RebotsUIStaticString.LowerCategoryScrollview);
            m_LowerCategoryList = m_LowerCategoryContainer.Q(RebotsUIStaticString.CategoryList);
            m_LowerLeftButton = m_LowerCategoryContainer.Q<Button>(RebotsUIStaticString.ScrollLeftButton);
            m_LowerRightButton = m_LowerCategoryContainer.Q<Button>(RebotsUIStaticString.ScrollRightButton);

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
            m_TicketListResultLabel = m_TicketListContainer.Q<Label>(RebotsUIStaticString.ListResultLabel);

            m_TicketContainer = m_PageConatiner.Q(RebotsUIStaticString.TicketContainer);
            m_TicketDetailButton = m_TicketContainer.Q<Button>(RebotsUIStaticString.TicketDetailButton);
            m_TicketDetailLabel = m_TicketContainer.Q<Label>(RebotsUIStaticString.InquiryPhrase);
            m_TicketAnswerButton = m_TicketContainer.Q<Button>(RebotsUIStaticString.TicketAnswerButton);
            m_TicketAnswerLabel = m_TicketContainer.Q<Label>(RebotsUIStaticString.AnswerPhrase);
            m_TicketDetailContainer = m_TicketContainer.Q(RebotsUIStaticString.TicketDetailContainer);
            m_TicketCreateLabel = m_TicketContainer.Q<Label>(RebotsUIStaticString.TicketCreateLabel);
            m_TicketCategoryRouteLabel = m_TicketContainer.Q<Label>(RebotsUIStaticString.TicketCategoryRouteLabel);
            m_TicketEmailLabel = m_TicketContainer.Q<Label>(RebotsUIStaticString.TicketEmailLabel);
            m_TicketEmailValue = m_TicketContainer.Q<Label>(RebotsUIStaticString.TicketEmailValue);
            m_TicketContentsLabel = m_TicketContainer.Q<Label>(RebotsUIStaticString.TicketContentsLabel);
            m_TicketContentsValue = m_TicketContainer.Q<Label>(RebotsUIStaticString.TicketContentsValue);
            m_TicketAttachmentContainer = m_TicketContainer.Q(RebotsUIStaticString.TicketAttachmentContainer);
            m_TicketDetailFoldout = m_TicketContainer.Q<Foldout>(RebotsUIStaticString.TicketDetailFoldout);
            m_TicketDetailList = m_TicketContainer.Q(RebotsUIStaticString.TicketDetailList);
            m_TicketAnswerContainer = m_TicketContainer.Q(RebotsUIStaticString.TicketAnswerContainer);
            m_TicketAnswerList = m_TicketContainer.Q(RebotsUIStaticString.TicketAnswerList);
            m_TicketAnswerContentContainer = m_TicketContainer.Q(RebotsUIStaticString.TicketAnswerContentContainer);
            m_TicketAnswerNoneContainer = m_TicketContainer.Q(RebotsUIStaticString.TicketAnswerNoneContainer);
            m_TicketAnswerNoneLabel = m_TicketContainer.Q<Label>(RebotsUIStaticString.AnswerNonePhrase);
            m_TicketAnswerNoticeLabel = m_TicketContainer.Q<Label>(RebotsUIStaticString.AnswerNoticePhrase);
            m_FileNameLabel = m_TicketContainer.Q<Label>(RebotsUIStaticString.FileNameLabel);
            m_FileSizeLabel = m_TicketContainer.Q<Label>(RebotsUIStaticString.FileSizeLabel);
            m_FileRemoveButton = m_TicketContainer.Q<Button>(RebotsUIStaticString.FileRemoveButton);
        }

        protected override void RegisterButtonCallbacks()
        {
            m_PageBackButton?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.ChangePage(true));
            m_TicketSuccessMainButton?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.ShowMain(false));
            m_TicketDetailButton?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.ClickTicketMenu(RebotsPageName.Ticket));
            m_TicketAnswerButton?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.ClickTicketMenu(RebotsPageName.Answer));

            m_SiblingCategoryScrollview.horizontalScroller.valueChanged += OnSiblingScrollValueChanged;
            m_SiblingLeftButton.RegisterCallback<ClickEvent>(evt => StartCoroutine(ScrollingSiblingCategory(-1)));
            m_SiblingRightButton.RegisterCallback<ClickEvent>(evt => StartCoroutine(ScrollingSiblingCategory(1)));

            m_LowerCategoryScrollview.horizontalScroller.valueChanged += OnLowerScrollValueChanged;
            m_LowerLeftButton.RegisterCallback<ClickEvent>(evt => StartCoroutine(ScrollingLowerCategory(-1)));
            m_LowerRightButton.RegisterCallback<ClickEvent>(evt => StartCoroutine(ScrollingLowerCategory(1)));

#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN || UNITY_WEBGL 
            m_PopularScrollview.horizontalScroller.valueChanged += OnPopularScrollValueChanged;
            m_PopularLeftButton.RegisterCallback<ClickEvent>(evt => StartCoroutine(ScrollingPopular(-1)));
            m_PopularRightButton.RegisterCallback<ClickEvent>(evt => StartCoroutine(ScrollingPopular(1)));

            m_SiblingCategoryScrollview.RegisterCallback<WheelEvent>(OnMouseWheel, TrickleDown.TrickleDown);
            m_LowerCategoryScrollview.RegisterCallback<WheelEvent>(OnMouseWheel, TrickleDown.TrickleDown);
            m_PopularScrollview.RegisterCallback<WheelEvent>(OnMouseWheel, TrickleDown.TrickleDown);
#endif
        }
        #endregion

        #region Set before show page
        public void SetTranslationText()
        {
            LocalizationManager = helpdeskScreen.rebotsSettingManager.localizationManager;

            m_PopularFaqCaption.text = LocalizationManager.translationDic[RebotsUIStaticString.PopularFaqCaption];
            m_MainCategoryCaption.text = LocalizationManager.translationDic[RebotsUIStaticString.MainCategoryCaption];

            m_FaqHelpfulLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.FaqHelpfulLabel];
            m_HelpfulYesLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.HelpfulYesLabel];
            m_HelpfulNoLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.HelpfulNoLabel];

            m_TicketReceivedLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.TicketReceivedLabel];
            m_TicketReplyLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.TicketReplyLabel];
            m_TicketThankYouLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.TicketThankYouLabel];
            m_TicketReturnMainLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.TicketReturnMainLabel];

            m_TicketDetailLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.InquiryPhrase];
            m_TicketAnswerLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.AnswerPhrase];
            m_TicketEmailLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.EmailPhrase];
            m_TicketContentsLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.ContentPhrase];
            m_TicketAnswerNoneLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.AnswerNonePhrase];
            m_TicketAnswerNoticeLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.AnswerNoticePhrase];
            m_TicketDetailFoldout.text = LocalizationManager.translationDic[RebotsUIStaticString.TicketDetailFoldoutOpen];
        }

        public void SetHelpdeskData(HelpdeskSetting helpdeskSetting)
        {
            Theme = helpdeskSetting.theme;
        }

        public void SetPrivacyData(PrivacySetting ticketPrivacySetting)
        {
            TicketPrivacySetting = ticketPrivacySetting;
        }

        public void SetParameterData(RebotsParameterData parameterData)
        {
            ParameterDic = new Dictionary<string, string>();
            ParameterDic = parameterData.parameters;
        }
        #endregion

        #region Set callback data after API
        public void OnFaqRecommendUpdated(HelpdeskFaqListResponse response)
        {
            var faqs = (response.faqs != null) ? response.faqs.Where(x => x.use == 1).ToArray() : null;
            int faqCount = (faqs != null) ? faqs.Count() : 0;
            m_PopularRightButton.style.display = DisplayStyle.None;
            m_PopularLeftButton.style.display = DisplayStyle.None;
            if (faqs != null && faqCount > 0)
            {
                ShowVisualElement(m_FaqPopularContainer, true);
                popularIndex = 0;
                for (int i = 0; i < faqCount; i++)
                {
                    var item = faqs[i];
                    TemplateContainer faqUIElement = null;
                    helpdeskScreen.rebotsUICreater.CreateFaq(item, RebotsFaqAssetType.Popular, null, helpdeskScreen.ShowFaq, out faqUIElement);

                    var categoryLabel = faqUIElement.Q<Label>(RebotsUIStaticString.TicketCategoryRouteLabel);
                    StartCoroutine(LabelMultilineEllipsis(categoryLabel, 14, 1));
                    m_FaqSearchList.Add(faqUIElement);

                    faqUIElement.style.flexShrink = 0;
                    m_FaqPopularList.Add(faqUIElement);
                }

#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN || UNITY_WEBGL 
                StartCoroutine(ScrollingPopular(0));
#endif
            }
            else
            {
                ShowVisualElement(m_FaqPopularContainer, false);
            }
        }

        public void OnFaqSearchUpdated(HelpdeskFaqSearchResponse response)
        {
            var faqs = (response.faqs != null) ? response.faqs.Where(x => x.use == 1).ToArray() : null; ;
            var search = response.search;
            var currentPage = response.page;
            var recordCount = response.recordCount;

            var recordCountStr = "<color=" + RebotsUIStaticString.BlackColor + ">" + recordCount.ToString() + "</color>";
            var searchResultStr = LocalizationManager.translationDic[RebotsUIStaticString.SearchResultLabel];
            m_SearchResultLabel.text = string.Format(searchResultStr, recordCountStr);

            m_PageTitleLabel.text = search;
            StartCoroutine(LabelMultilineEllipsis(m_PageTitleLabel, 16, 1));

            var faqCount = faqs.Count();
            if (faqs != null && faqCount > 0)
            {
                for (int i = 0; i < faqCount; i++)
                {
                    var item = faqs[i];
                    TemplateContainer faqUIElement = null;
                    helpdeskScreen.rebotsUICreater.CreateFaq(item, RebotsFaqAssetType.Search, search, helpdeskScreen.ShowFaq, out faqUIElement);

                    var categoryLabel = faqUIElement.Q<Label>(RebotsUIStaticString.TicketCategoryRouteLabel);
                    StartCoroutine(LabelMultilineEllipsis(categoryLabel, 14, 1));
                    m_FaqSearchList.Add(faqUIElement);

                    var contentsLabel = faqUIElement.Q<Label>(RebotsUIStaticString.ContentsLabel);
                    StartCoroutine(LabelMultilineEllipsis(contentsLabel, 16, 2));
                    m_FaqSearchList.Add(faqUIElement);
                }
            }

            int pageSize = RebotsHelpdeskScreen.ListPageSize;
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

            var titleCategory = faq.categories[0];
            m_PageTitleLabel.text = titleCategory.name;

            m_FaqTitleLabel.text = faq.title;

            var contentsDic = HtmlParser.HtmlToUnityTag(faq.contents.ToString());
            var count = contentsDic.Count();
            for (int i = 0; i < count; i++)
            {
                var item = contentsDic[i];
                switch (item.type)
                {
                    case "text":
                        helpdeskScreen.rebotsUICreater.CreateLabel(item.value, out Label labelUIElement);
                        m_FaqDetailContainer.Add(labelUIElement);
                        break;
                    case "img":
                        var imgContents = item.value;
                        helpdeskScreen.ImageUrlToTexture2D(OnFaqImageAdded, new Uri(imgContents), imgContents);
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

#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN || UNITY_WEBGL
            m_FaqCategoryList.style.flexDirection = FlexDirection.Row;
#else
            if (helpdeskScreen.ScreenPortrait)
                m_FaqCategoryList.style.flexDirection = FlexDirection.Column;
            else
                m_FaqCategoryList.style.flexDirection = FlexDirection.Row;
#endif

            var faqCategories = (response.items != null) ? response.items.Where(x => x.use == 1).ToArray() : new Category[0];
            for (int i = 0; i < faqCategories.Count(); i++)
            {
                var category = faqCategories[i];

                TemplateContainer categoryUIElement = null;
                helpdeskScreen.rebotsUICreater.CreateCategory<Category>(category, RebotsCategoryAssetType.Category, helpdeskScreen.ClickFaqCategory, out categoryUIElement);

#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN || UNITY_WEBGL
                if (i % 2 == 1)
                {
                    categoryUIElement.style.width = Length.Percent(49f);
                }
                else
                {
                    categoryUIElement.style.width = Length.Percent(50f);
                    categoryUIElement.style.paddingRight = 10f;
                }
#else
                if (helpdeskScreen.ScreenPortrait)
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
#endif
                m_FaqCategoryList.Add(categoryUIElement);
            }
        }

        public void OnCsCategoriesUpdated(HelpdeskTicketCategoriesResponse response)
        {
            var categories = response.items;

            m_PageTitleLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.InquiryPhrase];

#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN || UNITY_WEBGL
            m_InquiryList.style.flexDirection = FlexDirection.Row;
#else
            m_InquiryList.style.flexDirection = helpdeskScreen.ScreenPortrait ? FlexDirection.Column : FlexDirection.Row;
#endif

            var csCategories = (categories != null) ? categories.Where(x => x.use == 1).ToArray() : new Category[0];

            for (int i = 0; i < csCategories.Count(); i++)
            {
                var category = csCategories[i];
                TemplateContainer categoryUIElement = null;
                Action<Category> clickAction = (category.childFieldCount > 0) ? helpdeskScreen.ShowCsSubCategory : helpdeskScreen.ShowTicketCreate;
                helpdeskScreen.rebotsUICreater.CreateCategory<Category>(category, RebotsCategoryAssetType.Category, clickAction, out categoryUIElement);

#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN || UNITY_WEBGL
                if (i % 2 == 1)
                {
                    categoryUIElement.style.width = Length.Percent(49f);
                }
                else
                {
                    categoryUIElement.style.width = Length.Percent(50f);
                    categoryUIElement.style.paddingRight = 10f;
                }
#else
                if (helpdeskScreen.ScreenPortrait)
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
#endif
                m_InquiryList.Add(categoryUIElement);
            }
        }

        public void OnSubCategoryUpdated(HelpdeskFaqCategoryResponse response)
        {
            var faqCategory = response;

            m_PageTitleLabel.text = faqCategory.name;

            var siblingCategories = (faqCategory.siblingCategories != null) ? faqCategory.siblingCategories.Where(x => x.use == 1).ToArray() : null;
            var siblingCount = (siblingCategories != null) ? siblingCategories.Count() : 0;
            m_SiblingRightButton.style.display = DisplayStyle.None;
            m_SiblingLeftButton.style.display = DisplayStyle.None;
            if (siblingCount > 0)
            {
                ShowVisualElement(m_SiblingCategoryContainer, true);
                siblingIndex = 0;
                for (int i = 0; i < siblingCount; i++)
                {
                    var item = siblingCategories[i];
                    TemplateContainer categoryUIElement = null;

                    if (item.id == faqCategory.id)
                    {
                        siblingIndex = i;
                        helpdeskScreen.rebotsUICreater.CreateCategory<Category>(item, RebotsCategoryAssetType.Selected, helpdeskScreen.ClickFaqCategory, out categoryUIElement);
                    }
                    else
                    {
                        helpdeskScreen.rebotsUICreater.CreateCategory<Category>(item, RebotsCategoryAssetType.Sibling, helpdeskScreen.ClickFaqCategory, out categoryUIElement);
                    }

                    var categoryLabel = categoryUIElement.Q<Label>(RebotsUIStaticString.ContentsLabel);
                    StartCoroutine(ScrollViewLabelMultilineEllipsis(categoryLabel, 16, RebotsCategoryAssetType.Sibling));
                    m_SiblingCategoryList.Add(categoryUIElement);
                }

                StartCoroutine(ScrollingSiblingCategory(0));
            }
            else
            {
                ShowVisualElement(m_SiblingCategoryContainer, false);
            }

            var lowerCategories = (faqCategory.subCategories != null) ? faqCategory.subCategories.Where(x => x.use == 1).ToArray() : null;
            var lowerCount = (lowerCategories != null) ? lowerCategories.Count() : 0;
            m_LowerRightButton.style.display = DisplayStyle.None;
            m_LowerLeftButton.style.display = DisplayStyle.None;
            if (lowerCount > 0)
            {
                ShowVisualElement(m_LowerCategoryContainer, true);
                lowerIndex = 0;
                for (int i = 0; i < lowerCount; i++)
                {
                    var item = lowerCategories[i];
                    TemplateContainer categoryUIElement = null;
                    helpdeskScreen.rebotsUICreater.CreateCategory<Category>(item, RebotsCategoryAssetType.Lower, helpdeskScreen.ClickFaqCategory, out categoryUIElement);
                    
                    var categoryLabel = categoryUIElement.Q<Label>(RebotsUIStaticString.ContentsLabel);
                    StartCoroutine(ScrollViewLabelMultilineEllipsis(categoryLabel, 14, RebotsCategoryAssetType.Lower));
                    m_LowerCategoryList.Add(categoryUIElement);
                }

                StartCoroutine(ScrollingLowerCategory(0));
            }
            else
            {
                ShowVisualElement(m_LowerCategoryContainer, false);
            }

            int displayPage = faqCategory.page;
            int pageSize = RebotsHelpdeskScreen.ListPageSize;
            int recordCount = faqCategory.recordCount;
            string listResultFormat = LocalizationManager.translationDic[RebotsUIStaticString.ListResultLabel];
            m_ListResultLabel.text = string.Format(listResultFormat, recordCount.ToString());

            var faqs = (faqCategory.faqs != null) ? faqCategory.faqs.Where(x => x.use == 1).ToArray() : null;
            int faqCount = (faqs != null) ? faqs.Count() : 0;
            if (faqCount > 0)
            {
                for (int i = 0; i < faqCount; i++)
                {
                    var item = faqs[i];
                    TemplateContainer categoryUIElement = null;
                    helpdeskScreen.rebotsUICreater.CreateCategory<Faq>(item, RebotsCategoryAssetType.Faq, helpdeskScreen.ShowFaq, out categoryUIElement);
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

            m_PageTitleLabel.text = csCategory.name;

#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN || UNITY_WEBGL
            m_InquiryList.style.flexDirection = FlexDirection.Row;
#else
            m_InquiryList.style.flexDirection = helpdeskScreen.ScreenPortrait ? FlexDirection.Column : FlexDirection.Row;
#endif

            var subCategories = (csCategory.subCategories != null) ? csCategory.subCategories.Where(x => x.use == 1).ToArray() : new Category[0];
            for (int i = 0; i < subCategories.Count(); i++)
            {
                var category = subCategories[i];
                TemplateContainer categoryUIElement = null;
                Action<Category> clickAction = (category.childFieldCount > 0) ? helpdeskScreen.ShowCsSubCategory : helpdeskScreen.ShowTicketCreate;
                helpdeskScreen.rebotsUICreater.CreateCategory(category, RebotsCategoryAssetType.Category, clickAction, out categoryUIElement);

#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN || UNITY_WEBGL
                if (i % 2 == 1)
                {
                    categoryUIElement.style.width = Length.Percent(49f);
                }
                else
                {
                    categoryUIElement.style.width = Length.Percent(50f);
                    categoryUIElement.style.paddingRight = 10f;
                }
#else
                if (helpdeskScreen.ScreenPortrait)
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
#endif
                m_InquiryList.Add(categoryUIElement);
            }
        }

        public void OnCsCategoryFieldsUpdated(HelpdeskTicketCategoryField response)
        {
            var csCategory = response.category;

            m_PageTitleLabel.text = csCategory.name;

            var fields = csCategory.inputFields;
            var count = fields.Count();
            string validationField = LocalizationManager.translationDic[RebotsUIStaticString.ValidRequired];
            string validationEmail = LocalizationManager.translationDic[RebotsUIStaticString.ValidEmail];
            if (fields != null && count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    var field = fields[i];
                    ParameterDic.TryGetValue(field.name, out string parameterValue);

                    string[] validationComment = (field.name == "email") ? new string[] { validationField, validationEmail } : new string[] { validationField };

                    helpdeskScreen.rebotsUICreater.CreateCsCategoryField(field, parameterValue, validationComment, out TemplateContainer fieldUIElement, out object fieldUIComponent);

                    var m_RequiredLabel = fieldUIElement.Q<Label>(RebotsUIStaticString.RequiredLabel);
                    m_RequiredLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.RequiredLabel];

                    if (field.fieldType == RebotsInputFieldType.File)
                    {
                        var m_ChooseFileButtonLabel = fieldUIElement.Q<Label>(RebotsUIStaticString.ChooseFileButtonLabel);
                        m_ChooseFileButtonLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.ChooseFileButtonLabel];
                        var m_NoFileLabel = fieldUIElement.Q<Label>(RebotsUIStaticString.NoFileLabel);
                        m_NoFileLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.NoFileLabel];
                        var m_FileValidationLabel = fieldUIElement.Q<Label>(RebotsUIStaticString.FileValidationLabel);
                        m_FileValidationLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.FileValidationLabel];
                    }

                    m_TicketFieldList.Add(fieldUIElement);
                    helpdeskScreen.AddFieldDic(field, fieldUIComponent);
                }
            }

            if (TicketPrivacySetting != null)
            {
                string[] formSectionTransData = {
                    LocalizationManager.translationDic[RebotsUIStaticString.PrivacyPrpose],
                    LocalizationManager.translationDic[RebotsUIStaticString.PrivacyCollection],
                    LocalizationManager.translationDic[RebotsUIStaticString.PrivacyPeriod],
                    LocalizationManager.translationDic[RebotsUIStaticString.PrivacyConsignment],
                    LocalizationManager.translationDic[RebotsUIStaticString.PrivacyProviding],
                    LocalizationManager.translationDic[RebotsUIStaticString.PrivacyConsignmentPeriod]
                };

                TemplateContainer privacyUIElement = null;
                TicketCategoryInputField privacyField = new TicketCategoryInputField
                {
                    inputType = "privacy",
                };
                helpdeskScreen.rebotsUICreater.CreatePrivacyField(TicketPrivacySetting, formSectionTransData, validationField, csCategory, helpdeskScreen.ClickTicketSubmit, out privacyUIElement);

                var m_RequiredLabel = privacyUIElement.Q<Label>(RebotsUIStaticString.RequiredLabel);
                m_RequiredLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.RequiredLabel];

                var m_AgreeCheckLabel = privacyUIElement.Q<Label>(RebotsUIStaticString.AgreeCheckLabel);
                m_AgreeCheckLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.AgreeCheckLabel];

                var m_PrivacyFieldLabel = privacyUIElement.Q<Label>(RebotsUIStaticString.PrivacyFieldLabel);
                m_PrivacyFieldLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.PrivacyFieldLabel];

                var m_PrivacyLinkTitleLabel = privacyUIElement.Q<Label>(RebotsUIStaticString.PrivacyLinkTitleLabel);
                m_PrivacyLinkTitleLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.PrivacyFieldLabel];

                var m_PrivacyLinkLabel = privacyUIElement.Q<Label>(RebotsUIStaticString.PrivacyLinkLabel);
                m_PrivacyLinkLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.PrivacyLinkLabel];

                var m_TicketSubmitLabel = privacyUIElement.Q<Label>(RebotsUIStaticString.TicketSubmitLabel);
                m_TicketSubmitLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.TicketSubmitLabel];

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
            var count = tickets.Count();

            m_PageTitleLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.MyTicketLabel];

            var answersCount = tickets.Where(x => x.isAnswers).Count();
            var ticketCountPhrase = LocalizationManager.translationDic[RebotsUIStaticString.ListResultLabel];
            var answersCountPhrase = LocalizationManager.translationDic[RebotsUIStaticString.AnswerCountPhrase];
            var sb = new StringBuilder();
            sb.Append(string.Format(ticketCountPhrase, count));
            sb.Append(" ");
            sb.Append(string.Format(answersCountPhrase, answersCount));
            m_TicketListResultLabel.text = sb.ToString();

            if (tickets != null && count > 0)
            {
                string[] transData = {
                    LocalizationManager.translationDic[RebotsUIStaticString.TicketCompleted],
                    LocalizationManager.translationDic[RebotsUIStaticString.TicketWaiting]
                };

                tickets = tickets.OrderByDescending(x => x.created).ToArray();
                for (int i = 0; i < count; i++)
                {
                    var ticket = tickets[i];

                    var newAnswerIds = ticket.answers.OrderBy(x => x.id).Select(x => x.id).ToArray();
                    var newIdsString = string.Join(",", newAnswerIds);
                    bool isNewAnswer = false;
                    if (TicketAnswerDic != null && TicketAnswerDic.ContainsKey(ticket.uuid))
                    {
                        var oldIdsString = TicketAnswerDic[ticket.uuid];
                        isNewAnswer = (newIdsString != oldIdsString);
                    }
                    else
                    {
                        isNewAnswer = (newIdsString != "") ? true : false;
                    }

                    TemplateContainer ticketUIElement = null;
                    helpdeskScreen.rebotsUICreater.CreateTicket(ticket, isNewAnswer, transData, helpdeskScreen.ShowTicketDetail, out ticketUIElement);

                    var ticketPreviewLabel = ticketUIElement.Q<Label>(RebotsUIStaticString.TicketPreviewLabel);
                    StartCoroutine(LabelMultilineEllipsis(ticketPreviewLabel, 14, 2));
                    m_TicketList.Add(ticketUIElement);
                }
            }

            m_TicketDetailButton.RemoveFromClassList(RebotsUIStaticString.RebotsTicketMenuSelect);
            m_TicketAnswerButton.RemoveFromClassList(RebotsUIStaticString.RebotsTicketMenuSelect);
        }

        public void OnTicketDetailUpdated(HelpDeskTicketDetailResponse response)
        {
            var ticket = response.ticket ?? new();
            var answers = response.ticket.answers ?? null;
            var ticketAttachments = response.attachments ?? null;

            m_PageTitleLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.MyTicketLabel];
            m_TicketDetailButton.AddToClassList(RebotsUIStaticString.RebotsTicketMenuSelect);

            var createdLocalTime = ticket.created.ToLocalTime();
            m_TicketCreateLabel.text = createdLocalTime.ToString("yyyy.MM.dd HH:mm");

            var routeCategories = ticket.categories;
            var routeCategoryStr = "";
            foreach (var category in routeCategories)
            {
                routeCategoryStr = routeCategoryStr == "" ? category.name : category.name + " > " + routeCategoryStr;
            }
            m_TicketCategoryRouteLabel.text = routeCategoryStr;

            m_TicketEmailValue.text = string.IsNullOrEmpty(ticket.customerEmail) ? "-" : ticket.customerEmail;
            m_TicketContentsValue.text = ticket.data.content;

            m_TicketDetailFoldout.value = false;
            m_TicketDetailFoldout.RegisterValueChangedCallback(evt =>
            {
                bool isExpanded = evt.newValue;
                if (isExpanded)
                {
                    m_TicketDetailFoldout.text = LocalizationManager.translationDic[RebotsUIStaticString.TicketDetailFoldoutCollapse];
                }
                else
                {
                    m_TicketDetailFoldout.text = LocalizationManager.translationDic[RebotsUIStaticString.TicketDetailFoldoutOpen];
                }
            });
            var dataDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(ticket.data.data).ToArray();
            var dataCount = (dataDic != null) ? dataDic.Count() : 0;
            if (dataDic != null && dataCount > 0)
            {
                for (int i = 0; i < dataCount; i++)
                {
                    var data = dataDic[i];
                    TemplateContainer fieldUIElement = null;
                    helpdeskScreen.rebotsUICreater.CreateTicketDetail(data.Key, data.Value, RebotsTicketDetailAssetType.Field, out fieldUIElement);
                    m_TicketDetailList.Add(fieldUIElement);
                }
            }

            var ticketAttachmentsCount = (ticketAttachments != null) ? ticketAttachments.Count() : 0;
            if (ticketAttachments != null && ticketAttachmentsCount > 0)
            {
                for (int i = 0; i < ticketAttachmentsCount; i++)
                {
                    var ticketfile = ticketAttachments[i];

                    TemplateContainer fieldUIElement = null;
                    helpdeskScreen.rebotsUICreater.CreateTicketDetail(ticketfile.fileName, ticketfile.fileLink, RebotsTicketDetailAssetType.Attachment, out fieldUIElement);
                    m_TicketAttachmentContainer.Add(fieldUIElement);
                }
            }

            var answerCount = (answers != null) ? answers.Count() : 0;
            if (answers != null && answerCount > 0)
            {
                ShowVisualElement(m_TicketAnswerList, true);
                ShowVisualElement(m_TicketAnswerNoneContainer, false);

                string[] oldIdsArray = null;
                if (TicketAnswerDic.TryGetValue(ticket.uuid, out string oldIdsString))
                {
                    oldIdsArray = oldIdsString.Split(",");
                }
                answers = answers.OrderByDescending(x => x.id).ToArray();
                for (int i = 0; i < answerCount; i++)
                {
                    var answer = answers[i];
                    bool isNew = (oldIdsArray != null) ? !oldIdsArray.Contains(answer.id) : true;

                    TemplateContainer answerUIElement = null;

                    var answeredLocalTime = answer.answered.ToLocalTime();
                    helpdeskScreen.rebotsUICreater.CreateTicketDetail(createdLocalTime.ToString("yyyy.MM.dd HH:mm"), "", RebotsTicketDetailAssetType.Answer, out answerUIElement, isNew);

                    if (answerUIElement != null)
                    {
                        VisualElement AnswerContentContainer = answerUIElement.Q(RebotsUIStaticString.TicketAnswerContentContainer);
                        AnswerContentContainer.Clear();

                        var sb = new StringBuilder();
                        sb.Append(answer.head);
                        sb.Append(answer.content);
                        sb.Append(answer.tail);
                        var answerContent = sb.ToString();
                        var contentsDic = HtmlParser.HtmlToUnityTag(answerContent);
                        var contentCount = contentsDic.Count();
                        int imgCount = 1;
                        for (int c = 0; c < contentCount; c++)
                        {
                            var content = contentsDic[c];
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

                        var answerAttachmentsCount = (answer.attachments != null) ? answer.attachments.Count() : 0;
                        var answerAttachments = answer.attachments;
                        if (answerAttachments != null && answerAttachmentsCount > 0)
                        {
                            VisualElement AnswerAttachmentContainer = answerUIElement.Q(RebotsUIStaticString.TicketAttachmentContainer);
                            AnswerAttachmentContainer.Clear();

                            for (int j = 0; j < answerAttachmentsCount; j++)
                            {
                                var answerfile = answerAttachments[j];

                                TemplateContainer fieldUIElement = null;
                                helpdeskScreen.rebotsUICreater.CreateTicketDetail(answerfile.fileName, answerfile.fileLink, RebotsTicketDetailAssetType.Attachment, out fieldUIElement);
                                AnswerAttachmentContainer.Add(fieldUIElement);
                            }
                        }
                    }

                    m_TicketAnswerList.Add(answerUIElement);
                }

                var newAnswerIds = answers.OrderBy(x => x.id).Select(x => x.id).ToArray();
                if (TicketAnswerDic.ContainsKey(ticket.uuid))
                {
                    TicketAnswerDic[ticket.uuid] = string.Join(",", newAnswerIds);
                }
                else
                {
                    TicketAnswerDic.Add(ticket.uuid, string.Join(",", newAnswerIds));
                }
                SaveMyTicketAnswers(TicketAnswerDic, RebotsHelpdeskScreen.TicketAnswersFile);
            }
            else
            {
                ShowVisualElement(m_TicketAnswerList, false);
                ShowVisualElement(m_TicketAnswerNoneContainer, true);
            }
        }
        #endregion

        #region (private) On MouseWheel Control
        private void OnMouseWheel(WheelEvent evt)
        {
            evt.StopPropagation();
            evt.PreventDefault();

            Vector2 newScrollOffset = helpdeskScreen.rebotsLayoutUI.m_ScrollView.scrollOffset;
            newScrollOffset.y += evt.delta.y * 18f; 

            helpdeskScreen.rebotsLayoutUI.m_ScrollView.scrollOffset = newScrollOffset;
        }
        #endregion

        #region (private) Set Sibling Category Scrolling Point
        private IEnumerator ScrollingSiblingCategory(int index)
        {
            if (index != 0)
            {
                siblingIndex += index;
            }
            yield return null;
            
            siblingHighValue = m_SiblingCategoryScrollview.horizontalScroller.highValue;

            var forwardElementWidth = 0f;

            if (siblingIndex == 0)
            {
                m_SiblingCategoryScrollview.horizontalScroller.value = forwardElementWidth;
                OnSiblingScrollValueChanged(forwardElementWidth);
            }
            else
            {
                var categoryElements = m_SiblingCategoryList.Children().ToArray();
                var count = categoryElements.Length;

                for (int i = 0; i < count; i++)
                {
                    var categoryElement = categoryElements[i];
                    if (siblingIndex == i)
                    {
                        break;
                    }
                    forwardElementWidth += categoryElement.contentRect.width;
                }

                m_SiblingCategoryScrollview.horizontalScroller.value = forwardElementWidth;
            }
        }

        private void OnSiblingScrollValueChanged(float newValue)
        {
            if (siblingHighValue == 0f)
            {
                m_SiblingRightButton.style.display = DisplayStyle.None;
                m_SiblingLeftButton.style.display = DisplayStyle.None;
            }
            else if (siblingHighValue == newValue)
            {
                m_SiblingRightButton.style.display = DisplayStyle.None;
                m_SiblingLeftButton.style.display = DisplayStyle.Flex;
            }
            else if (0f == newValue)
            {
                m_SiblingRightButton.style.display = DisplayStyle.Flex;
                m_SiblingLeftButton.style.display = DisplayStyle.None;
            }
            else
            {
                m_SiblingRightButton.style.display = DisplayStyle.Flex;
                m_SiblingLeftButton.style.display = DisplayStyle.Flex;
            }
        }
        #endregion

        #region (private) Set Lower Category Scrolling Point
        private IEnumerator ScrollingLowerCategory(int index)
        {
            if (index != 0)
            {
                lowerIndex += index;
            }
            yield return null;

            lowerHighValue = m_LowerCategoryScrollview.horizontalScroller.highValue;

            var forwardElementWidth = 0f;

            if (lowerIndex == 0)
            {
                m_LowerCategoryScrollview.horizontalScroller.value = forwardElementWidth;
                OnLowerScrollValueChanged(forwardElementWidth);
            }
            else
            {
                var categoryElements = m_LowerCategoryList.Children().ToArray();
                var count = categoryElements.Length;

                for (int i = 0; i < count; i++)
                {
                    var categoryElement = categoryElements[i];
                    if (lowerIndex == i)
                    {
                        break;
                    }
                    forwardElementWidth += categoryElement.contentRect.width;
                }

                m_LowerCategoryScrollview.horizontalScroller.value = forwardElementWidth;
            }
        }

        private void OnLowerScrollValueChanged(float newValue)
        {
            if (lowerHighValue == 0f)
            {
                m_LowerRightButton.style.display = DisplayStyle.None;
                m_LowerLeftButton.style.display = DisplayStyle.None;
            }
            else if (lowerHighValue == newValue)
            {
                m_LowerRightButton.style.display = DisplayStyle.None;
                m_LowerLeftButton.style.display = DisplayStyle.Flex;
            }
            else if (0f == newValue)
            {
                m_LowerRightButton.style.display = DisplayStyle.Flex;
                m_LowerLeftButton.style.display = DisplayStyle.None;
            }
            else
            {
                m_LowerRightButton.style.display = DisplayStyle.Flex;
                m_LowerLeftButton.style.display = DisplayStyle.Flex;
            }
        }
        #endregion

        #region (private) Set Popular Scrolling Point
        private IEnumerator ScrollingPopular(int index)
        {
            if (index != 0)
            {
                popularIndex += index;
            }
            yield return null;

            popularHighValue = m_PopularScrollview.horizontalScroller.highValue;

            var forwardElementWidth = 0f;

            if (popularIndex == 0)
            {
                m_PopularScrollview.horizontalScroller.value = forwardElementWidth;
                OnPopularScrollValueChanged(forwardElementWidth);
            }
            else
            {
                var categoryElements = m_FaqPopularList.Children().ToArray();
                var count = categoryElements.Length;

                for (int i = 0; i < count; i++)
                {
                    var categoryElement = categoryElements[i];
                    if (popularIndex == i)
                    {
                        break;
                    }
                    forwardElementWidth += categoryElement.contentRect.width;
                }

                m_PopularScrollview.horizontalScroller.value = forwardElementWidth;
            }
        }

        private void OnPopularScrollValueChanged(float newValue)
        {
            if (popularHighValue == 0f)
            {
                m_PopularRightButton.style.display = DisplayStyle.None;
                m_PopularLeftButton.style.display = DisplayStyle.None;
            }
            else if (popularHighValue == newValue)
            {
                m_PopularRightButton.style.display = DisplayStyle.None;
                m_PopularLeftButton.style.display = DisplayStyle.Flex;
            }
            else if (0f == newValue)
            {
                m_PopularRightButton.style.display = DisplayStyle.Flex;
                m_PopularLeftButton.style.display = DisplayStyle.None;
            }
            else
            {
                m_PopularRightButton.style.display = DisplayStyle.Flex;
                m_PopularLeftButton.style.display = DisplayStyle.Flex;
            }
        }
        #endregion

        #region (private) Set Multi-Line Ellipsis Point
        private IEnumerator LabelMultilineEllipsis(Label labelElement, int fontSize, int setLine = 1)
        {
            yield return null;

            string text = labelElement.text;

            float areaWidth = labelElement.contentRect.width;
            float areaHeight = labelElement.contentRect.height;
            Rect textRect = new Rect(0, 0, areaWidth, areaHeight);

            GUIStyle textStyle = new GUIStyle();
            textStyle.font = helpdeskScreen.GetLanguageFontAsset();
            textStyle.fontSize = fontSize;
            textStyle.wordWrap = true;

            string breakText = helpdeskScreen.AddSpaceToWordWrapPoint(text, textRect, textStyle, setLine);
            if (!string.IsNullOrEmpty(breakText))
            {
                labelElement.text = breakText + "...";
            }
        }

        private IEnumerator ScrollViewLabelMultilineEllipsis(Label labelElement, int fontSize, RebotsCategoryAssetType type)
        {
            yield return null;

            bool setEllipsis = false;

            string text = labelElement.text;
            float textWidth = labelElement.contentRect.width;
            float textHeight = labelElement.contentRect.height;
            float textRectWidth = 0f;

            if (type == RebotsCategoryAssetType.Sibling)
            {
                float areaWidth = m_SiblingCategoryList.contentRect.width;
                textRectWidth = (areaWidth / 2) - 34f;

                if (textWidth > Math.Truncate(textRectWidth))
                {
                    Debug.Log($"- sibling {text}: {textWidth} / {textRectWidth}");
                    setEllipsis = true;
                }
            }
            else if (type == RebotsCategoryAssetType.Lower)
            {
                float areaWidth = m_LowerCategoryList.contentRect.width;
                textRectWidth = (areaWidth / 2) - 22f;

                if (textWidth > Math.Truncate(textRectWidth))
                {
                    Debug.Log($"- lower {text}: {textWidth} / {textRectWidth}");
                    setEllipsis = true;
                }
            }

            if (setEllipsis)
            {
                Rect textRect = new Rect(0, 0, textRectWidth, textHeight);

                GUIStyle textStyle = new GUIStyle();
                textStyle.font = helpdeskScreen.GetLanguageFontAsset();
                textStyle.fontSize = fontSize;
                textStyle.wordWrap = true;

                string breakText = helpdeskScreen.AddSpaceToWordWrapPoint(text, textRect, textStyle, 1);
                if (!string.IsNullOrEmpty(breakText))
                {
                    labelElement.text = breakText + "...";
                }
            }
        }
        #endregion

        #region Save & Load 'My Ticket > Answers' Cache Data
        private void SaveMyTicketAnswers(Dictionary<string, string> dic, string fileName)
        {
            string jsonFile = JsonConvert.SerializeObject(dic);

            if (RebotsFileManager.WriteToFile(fileName, jsonFile))
            {
                TicketAnswerDic = dic;
            }
        }

        public void LoadMyTicketAnswers(string fileName)
        {
            TicketAnswerDic = new Dictionary<string, string>();
            if (RebotsFileManager.LoadFromFile(fileName, out var jsonString))
            {
                if (!string.IsNullOrEmpty(jsonString))
                {
                    TicketAnswerDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
                }
            }
        }
        #endregion
    }
}