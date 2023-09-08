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

        public VisualElement m_PopularFaqContainer;
        public Label m_PopularFaqCation;
        public VisualElement m_PopularFaqList;

        public VisualElement m_SearchFaqContainer;
        public Label m_SearchResultLabel;
        public Label m_SearchStringLabel;
        public VisualElement m_SearchFaqList;

        public VisualElement m_FaqCategoryContainer;
        public VisualElement m_FaqCategoryList;

        public VisualElement m_CsCategoryContainer;
        public VisualElement m_CsCategoryList;

        public VisualElement m_RouteContainer;
        public Button m_BackButton;
        public VisualElement m_RouteLabelContainer;
        public Label m_RouteLabel;

        public VisualElement m_MenuNameContainer;
        public Label m_MenuLabel;

        public VisualElement m_SiblingCategoryContainer;
        public ScrollView m_SiblingCategoryScrollview;
        public VisualElement m_SiblingCategoryList;

        public VisualElement m_TitleCategoryConatiner;
        public Label m_TitleCategoryLabel;

        public VisualElement m_SubCategoryContainer;
        public VisualElement m_SubCategoryList;

        public VisualElement m_FaqContainer;
        public VisualElement m_FaqDetailContainer;
        public Label m_FaqHelpfulLabel;
        public Label m_HelpfulYesLabel;
        public Label m_HelpfulNoLabel;

        public VisualElement m_TicketCreateContainer;
        public VisualElement m_TicketFieldList;
        public Button m_TicketSubmitButton;
        public Label m_TicketSubmitLabel;

        public VisualElement m_TicketSuccessContainer;
        public Label m_TicketReceivedLabel;
        public Label m_TicketReplyLabel;
        public Label m_TicketThankYouLabel;
        public Button m_TicketSuccessMainButton;
        public Label m_TicketReturnMainLabel;

        public VisualElement m_MyTicketContainer;
        public VisualElement m_TicketList;

        public VisualElement m_TicketContainer;
        public VisualElement m_TicketDetailList;
        public Label m_TicketContentsLabel;
        public VisualElement m_TicketAnswerList;
        #endregion

        private RebotsLocalizationManager localizationManager;

        public PrivacySetting m_TicketPrivacySetting { get; private set; }
        public string m_Theme { get; private set; }
        public Dictionary<string, string> m_ParameterDic { get; private set; }

        #region Run in 'Awake' call
        protected override void SetVisualElements()
        {
            base.SetVisualElements();

            m_PageConatiner = m_Root.Q(RebotsUIStaticString.PageConatiner);
            m_PopularFaqContainer = m_PageConatiner.Q(RebotsUIStaticString.PopularFaqContainer);
            m_PopularFaqCation = m_PopularFaqContainer.Q<Label>(RebotsUIStaticString.PopularFaqCation);
            m_PopularFaqList = m_PopularFaqContainer.Q(RebotsUIStaticString.FaqList);

            m_SearchFaqContainer = m_PageConatiner.Q(RebotsUIStaticString.SearchFaqContainer);
            m_SearchResultLabel = m_SearchFaqContainer.Q<Label>(RebotsUIStaticString.SearchResultLabel);
            m_SearchStringLabel = m_SearchFaqContainer.Q<Label>(RebotsUIStaticString.SearchStringLabel);
            m_SearchFaqList = m_SearchFaqContainer.Q(RebotsUIStaticString.FaqList);

            m_FaqCategoryContainer = m_PageConatiner.Q(RebotsUIStaticString.FaqCategoryContainer);
            m_FaqCategoryList = m_FaqCategoryContainer.Q(RebotsUIStaticString.FaqCategoryList);

            m_CsCategoryContainer = m_PageConatiner.Q(RebotsUIStaticString.CsCategoryContainer);
            m_CsCategoryList = m_CsCategoryContainer.Q(RebotsUIStaticString.CsCategoryList);

            m_RouteContainer = m_PageConatiner.Q(RebotsUIStaticString.RouteContainer);
            m_BackButton = m_RouteContainer.Q<Button>(RebotsUIStaticString.BackButton);
            m_RouteLabelContainer = m_RouteContainer.Q(RebotsUIStaticString.RouteLabelContainer);

            m_MenuNameContainer = m_PageConatiner.Q(RebotsUIStaticString.MenuNameContainer);
            m_MenuLabel = m_MenuNameContainer.Q<Label>(RebotsUIStaticString.MenuLabel);

            m_SiblingCategoryContainer = m_PageConatiner.Q(RebotsUIStaticString.SiblingCategoryContainer);
            m_SiblingCategoryScrollview = m_SiblingCategoryContainer.Q<ScrollView>(RebotsUIStaticString.SiblingCategoryScrollview);
            m_SiblingCategoryList = m_SiblingCategoryScrollview.Q(RebotsUIStaticString.SiblingCategoryList);

            m_TitleCategoryConatiner = m_PageConatiner.Q(RebotsUIStaticString.TitleCategoryConatiner);
            m_TitleCategoryLabel = m_TitleCategoryConatiner.Q<Label>(RebotsUIStaticString.CategoryLabel);

            m_SubCategoryContainer = m_PageConatiner.Q(RebotsUIStaticString.SubCategoryContainer);
            m_SubCategoryList = m_SubCategoryContainer.Q(RebotsUIStaticString.SubCategoryList);

            m_FaqContainer = m_PageConatiner.Q(RebotsUIStaticString.FaqContainer);
            m_FaqDetailContainer = m_FaqContainer.Q(RebotsUIStaticString.FaqDetailContainer);
            m_FaqHelpfulLabel = m_FaqContainer.Q<Label>(RebotsUIStaticString.FaqHelpfulLabel);
            m_HelpfulYesLabel = m_FaqContainer.Q<Label>(RebotsUIStaticString.HelpfulYesLabel);
            m_HelpfulNoLabel = m_FaqContainer.Q<Label>(RebotsUIStaticString.HelpfulNoLabel);

            m_TicketCreateContainer = m_PageConatiner.Q(RebotsUIStaticString.TicketCreateContainer);
            m_TicketFieldList = m_TicketCreateContainer.Q(RebotsUIStaticString.TicketFieldList);
            m_TicketSubmitButton = m_TicketCreateContainer.Q<Button>(RebotsUIStaticString.TicketSubmitButton);
            m_TicketSubmitLabel = m_TicketCreateContainer.Q<Label>(RebotsUIStaticString.TicketSubmitLabel);

            m_TicketSuccessContainer = m_PageConatiner.Q(RebotsUIStaticString.TicketSuccessContainer);
            m_TicketReceivedLabel = m_TicketSuccessContainer.Q<Label>(RebotsUIStaticString.TicketReceivedLabel);
            m_TicketReplyLabel = m_TicketSuccessContainer.Q<Label>(RebotsUIStaticString.TicketReplyLabel);
            m_TicketThankYouLabel = m_TicketSuccessContainer.Q<Label>(RebotsUIStaticString.TicketThankYouLabel);
            m_TicketSuccessMainButton = m_TicketSuccessContainer.Q<Button>(RebotsUIStaticString.MainButton);
            m_TicketReturnMainLabel = m_TicketSuccessContainer.Q<Label>(RebotsUIStaticString.TicketReturnMainLabel);

            m_MyTicketContainer = m_PageConatiner.Q(RebotsUIStaticString.MyTicketContainer);
            m_TicketList = m_MyTicketContainer.Q(RebotsUIStaticString.TicketList);

            m_TicketContainer = m_PageConatiner.Q(RebotsUIStaticString.TicketContainer);
            m_TicketDetailList = m_TicketContainer.Q(RebotsUIStaticString.TicketDetailList);
            m_TicketContentsLabel = m_TicketContainer.Q<Label>(RebotsUIStaticString.TicketContentsLabel);
            m_TicketAnswerList = m_TicketContainer.Q(RebotsUIStaticString.TicketAnswerList);
        }

        protected override void RegisterButtonCallbacks()
        {
            m_BackButton?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.ChangePage(true));
            m_TicketSubmitButton?.RegisterCallback<ClickEvent>(helpdeskScreen.ClickTicketSubmit);
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

            m_TicketSubmitLabel.text = localizationManager.translationDic[RebotsUIStaticString.TicketSubmitLabel];

            m_TicketReceivedLabel.text = localizationManager.translationDic[RebotsUIStaticString.TicketReceivedLabel];
            m_TicketReplyLabel.text = localizationManager.translationDic[RebotsUIStaticString.TicketReplyLabel];
            m_TicketThankYouLabel.text = localizationManager.translationDic[RebotsUIStaticString.TicketThankYouLabel];
            m_TicketReturnMainLabel.text = localizationManager.translationDic[RebotsUIStaticString.TicketReturnMainLabel];
        }

        public void SetHelpdeskData(HelpdeskSetting helpdeskSetting)
        {
            m_Theme = helpdeskSetting.theme;
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

                    var m_WasHelpfulLabel = faqUIElement.Q<Label>(RebotsUIStaticString.FaqWasHelpfulLabel);
                    m_WasHelpfulLabel.text = localizationManager.translationDic[RebotsUIStaticString.FaqWasHelpfulLabel];

                    faqUIElement.style.flexShrink = 0;
                    m_PopularFaqList.Add(faqUIElement);
                }
            }
            else
            {
                ShowVisualElement(m_PopularFaqContainer, false);
            }
        }

        public void OnFaqSearchUpdated(HelpdeskFaqSearchResponse response)
        {
            var searchResult = response;
            var faqs = response.faqs;
            var search = searchResult.search;

            var searchResultStr = localizationManager.translationDic[RebotsUIStaticString.SearchResultLabel];
            m_SearchResultLabel.text = string.Format(searchResultStr, searchResult.recordCount.ToString());
            m_SearchStringLabel.text = search;

            if (faqs != null && faqs.Count() > 0)
            {
                foreach (var item in faqs)
                {
                    TemplateContainer faqUIElement = null;
                    helpdeskScreen.rebotsUICreater.CreateFaq(item, RebotsFaqAssetType.Search, search, helpdeskScreen.ClickFaq, out faqUIElement);
                    m_SearchFaqList.Add(faqUIElement);
                }
            }
        }

        public void OnFaqUpdated(HelpdeskFaqResponse response)
        {
            var faq = response;

            var routeCategories = faq.categories;
            var routeStrFormat = "Main > FAQ > <color={0}>{1}</color>";
            var categoriesStr = faq.title;
            foreach (var category in routeCategories)
            {
                categoriesStr = category.name + " > " + categoriesStr;
            }
            var routeStr = string.Format(routeStrFormat, m_Theme, categoriesStr);

            TemplateContainer routeUIElement = null;
            helpdeskScreen.rebotsUICreater.CreateRouteLabel(routeStr, out routeUIElement);
            m_RouteLabelContainer.Add(routeUIElement);

            m_MenuLabel.text = "FAQ";
            m_TitleCategoryLabel.text = faq.title;

            var selectedCategory = routeCategories[0];
            var siblingCategories = faq.siblingCategories;
            var siblingCount = (siblingCategories != null) ? siblingCategories.Count() : 0;
            if (siblingCount > 0)
            {
                var selectedIndex = -1;
                for (int i = 0; i < siblingCount; i++)
                {
                    var item = siblingCategories[i];
                    TemplateContainer categoryUIElement = null;

                    if (item.id == selectedCategory.id)
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
                        helpdeskScreen.ImageUrlToTexture2D(new System.Uri(imgContents), imgContents);
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
            var faqCategories = (response.items != null) ? response.items.Where(x => x.use == 1) : null;
            if (faqCategories != null && faqCategories.Count() > 0)
            {
                foreach (var item in faqCategories)
                {
                    TemplateContainer categoryUIElement = null;
                    helpdeskScreen.rebotsUICreater.CreateCategory<Category>(item, RebotsCategoryAssetType.Faq, helpdeskScreen.ClickFaqCategory, out categoryUIElement);

                    var m_ViewMoreLabel = categoryUIElement.Q<Label>(RebotsUIStaticString.ViewMoreLabel);
                    m_ViewMoreLabel.text = localizationManager.translationDic[RebotsUIStaticString.ViewMoreLabel];

                    if (categoryUIElement != null)
                    {
                        VisualElement lowerList = categoryUIElement.Q(RebotsUIStaticString.LowerCategoryContainer);
                        lowerList.Clear();

                        var subCategories = (item.subCategories != null) ? item.subCategories.Where(x => x.use == 1) : null;
                        int countInt = 0;
                        if (subCategories != null && subCategories.Count() > 0)
                        {
                            foreach (var sub in subCategories.Take(5))
                            {
                                TemplateContainer subUIElement = null;
                                helpdeskScreen.rebotsUICreater.CreateCategory<Category>(sub, RebotsCategoryAssetType.Contents, helpdeskScreen.ClickFaqCategory, out subUIElement);
                                lowerList.Add(subUIElement);
                                countInt++;
                            }
                        }

                        var faqs = (item.faqs != null) ? item.faqs.Where(x => x.use == 1) : null;
                        if (faqs != null && faqs.Count() > 0)
                        {
                            foreach (var faq in faqs.Take(5 - countInt))
                            {
                                TemplateContainer faqUIElement = null;
                                helpdeskScreen.rebotsUICreater.CreateCategory<Faq>(faq, RebotsCategoryAssetType.Sub, helpdeskScreen.ClickFaq, out faqUIElement);
                                lowerList.Add(faqUIElement);
                            }
                        }
                    }
                    m_FaqCategoryList.Add(categoryUIElement);
                }
            }
        }

        public void OnCsCategoriesUpdated(HelpdeskTicketCategoriesResponse response)
        {
            var csCategories = (response.items != null) ? response.items.Where(x => x.use == 1) : null;
            if (csCategories != null && csCategories.Count() > 0)
            {
                foreach (var item in csCategories)
                {
                    TemplateContainer categoryUIElement = null;
                    helpdeskScreen.rebotsUICreater.CreateCategory<Category>(item, RebotsCategoryAssetType.Cs, helpdeskScreen.ClickCsCategory, out categoryUIElement);
                    m_CsCategoryList.Add(categoryUIElement);
                }
            }
        }

        public void OnSubCategoryUpdated(HelpdeskFaqCategoryResponse response)
        {
            var faqCategory = response;

            var routeCategories = faqCategory.categories;
            var routeStrFormat = "Main > FAQ > <color={0}>{1}</color>";
            var categoriesStr = faqCategory.name;
            foreach (var category in routeCategories)
            {
                categoriesStr = category.name + " > " + categoriesStr;
            }
            var routeStr = string.Format(routeStrFormat, m_Theme, categoriesStr);

            TemplateContainer routeUIElement = null;
            helpdeskScreen.rebotsUICreater.CreateRouteLabel(routeStr, out routeUIElement);
            m_RouteLabelContainer.Add(routeUIElement);

            m_MenuLabel.text = "FAQ";

            var siblingCategories = (faqCategory.siblingCategories != null) ? faqCategory.siblingCategories.Where(x => x.use == 1).ToList() : null;
            var siblingCount = (siblingCategories != null) ? siblingCategories.Count() : 0;
            if (siblingCount > 0)
            {
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

            m_TitleCategoryLabel.text = faqCategory.name;

            var subCategories = (faqCategory.subCategories != null) ? faqCategory.subCategories.Where(x => x.use == 1) : null;
            if (subCategories != null && subCategories.Count() > 0)
            {
                foreach (var item in subCategories)
                {
                    TemplateContainer categoryUIElement = null;
                    helpdeskScreen.rebotsUICreater.CreateCategory<Category>(item, RebotsCategoryAssetType.Contents, helpdeskScreen.ClickFaqCategory, out categoryUIElement);
                    m_SubCategoryList.Add(categoryUIElement);
                }
            }

            var faqs = (faqCategory.faqs != null) ? faqCategory.faqs.Where(x => x.use == 1) : null;
            if (faqs != null && faqs.Count() > 0)
            {
                foreach (var faq in faqs)
                {
                    TemplateContainer categoryUIElement = null;
                    helpdeskScreen.rebotsUICreater.CreateCategory<Faq>(faq, RebotsCategoryAssetType.Sub, helpdeskScreen.ClickFaq, out categoryUIElement);
                    m_SubCategoryList.Add(categoryUIElement);
                }
            }
        }

        public void OnSubCategoryUpdated(HelpdeskTicketCategoryResponse response)
        {
            var csCategory = response;

            var routeCategories = csCategory.categories;
            var routeStrFormat = "Main > Inquiry > <color={0}>{1}</color>";
            var categoriesStr = csCategory.name;
            foreach (var category in routeCategories)
            {
                categoriesStr = category.name + " > " + categoriesStr;
            }
            var routeStr = string.Format(routeStrFormat, m_Theme, categoriesStr);

            TemplateContainer routeUIElement = null;
            helpdeskScreen.rebotsUICreater.CreateRouteLabel(routeStr, out routeUIElement);
            m_RouteLabelContainer.Add(routeUIElement);

            m_MenuLabel.text = "Inquiry";

            var siblingCategories = (csCategory.siblingCategories != null) ? csCategory.siblingCategories.Where(x => x.use == 1).ToList() : null;
            var siblingCount = (siblingCategories != null) ? siblingCategories.Count() : 0;
            if (siblingCount > 0)
            {
                var selectedIndex = -1;
                for (int i = 0; i < siblingCount; i++)
                {
                    var item = siblingCategories[i];
                    TemplateContainer categoryUIElement = null;

                    if (item.id == csCategory.id)
                    {
                        selectedIndex = i;
                        helpdeskScreen.rebotsUICreater.CreateCategory<Category>(item, RebotsCategoryAssetType.Selected, helpdeskScreen.ClickCsCategory, out categoryUIElement);
                    }
                    else
                    {
                        helpdeskScreen.rebotsUICreater.CreateCategory<Category>(item, RebotsCategoryAssetType.Sibling, helpdeskScreen.ClickCsCategory, out categoryUIElement);
                    }
                    m_SiblingCategoryList.Add(categoryUIElement);
                }

                StartCoroutine(ScrollingSiblingCategory(selectedIndex));
            }

            m_TitleCategoryLabel.text = csCategory.name;

            var subCategories = (csCategory.subCategories != null) ? csCategory.subCategories.Where(x => x.use == 1) : null;
            if (subCategories != null && subCategories.Count() > 0)
            {
                foreach (var item in subCategories)
                {
                    TemplateContainer categoryUIElement = null;
                    if (item.useField)
                    {
                        helpdeskScreen.rebotsUICreater.CreateCategory<Category>(item, RebotsCategoryAssetType.Sub, helpdeskScreen.ClickCsCategory, out categoryUIElement); 
                    }
                    else
                    {
                        helpdeskScreen.rebotsUICreater.CreateCategory<Category>(item, RebotsCategoryAssetType.Contents, helpdeskScreen.ClickCsCategory, out categoryUIElement);
                    }
                    m_SubCategoryList.Add(categoryUIElement);
                }
            }
        }

        public void CheckCsCategoryPage(HelpdeskTicketCategoryResponse response)
        {
            var csCategory = response;

            if (csCategory.subCategories == null || csCategory.subCategories.Count() == 0) 
            { 
                helpdeskScreen.ShowTicketCreate(csCategory);
            }
            else
            {
                helpdeskScreen.ShowCsSubCategory(csCategory);
            }
        }

        public void OnCsCategoryFieldsUpdated(HelpdeskTicketCategoryField response)
        {
            var csCategory = response.category;

            var routeCategories = csCategory.categories;
            var routeStrFormat = "Main > Inquiry > <color={0}>{1}</color>";
            var categoriesStr = csCategory.name;
            foreach (var category in routeCategories)
            {
                categoriesStr = category.name + " > " + categoriesStr;
            }
            var routeStr = string.Format(routeStrFormat, m_Theme, categoriesStr);

            TemplateContainer routeUIElement = null;
            helpdeskScreen.rebotsUICreater.CreateRouteLabel(routeStr, out routeUIElement);
            m_RouteLabelContainer.Add(routeUIElement);

            m_MenuLabel.text = "Inquiry";
            m_TitleCategoryLabel.text = csCategory.name;

            var fields = csCategory.inputFields;
            if (fields != null && fields.Count() > 0)
            {
                foreach (var field in fields)
                {
                    string parameterValue = "";
                    m_ParameterDic.TryGetValue(field.name, out parameterValue);

                    TemplateContainer fieldUIElement = null;
                    object fieldUIComponent = null;
                    helpdeskScreen.rebotsUICreater.CreateCsCategoryField(field, parameterValue, out fieldUIElement, out fieldUIComponent);

                    var m_RequiredLabel = fieldUIElement.Q<Label>(RebotsUIStaticString.RequiredLabel);
                    m_RequiredLabel.text = localizationManager.translationDic[RebotsUIStaticString.RequiredLabel];

                    if (field.fieldType == RebotsInputFieldType.File)
                    {
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
                helpdeskScreen.rebotsUICreater.CreatePrivacyField(m_TicketPrivacySetting, formSectionTransData, out privacyUIElement);

                var m_RequiredLabel = privacyUIElement.Q<Label>(RebotsUIStaticString.RequiredLabel);
                m_RequiredLabel.text = localizationManager.translationDic[RebotsUIStaticString.RequiredLabel];

                var m_AgreeCheckLabel = privacyUIElement.Q<Label>(RebotsUIStaticString.AgreeCheckLabel);
                m_AgreeCheckLabel.text = localizationManager.translationDic[RebotsUIStaticString.AgreeCheckLabel];

                var m_PrivacyFieldLabel = privacyUIElement.Q<Label>(RebotsUIStaticString.PrivacyFieldLabel);
                m_PrivacyFieldLabel.text = localizationManager.translationDic[RebotsUIStaticString.PrivacyFieldLabel];

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

            TemplateContainer menuUIElement = null;
            helpdeskScreen.rebotsUICreater.CreateRouteLabel("Main > My Tickets", out menuUIElement);
            m_RouteLabelContainer.Add(menuUIElement);

            m_MenuLabel.text = "My Tickets";

            if (tickets != null && tickets.Count() > 0)
            {
                tickets = tickets.OrderByDescending(x => x.created).ToArray();
                foreach (var item in tickets)
                {
                    TemplateContainer faqUIElement = null;
                    helpdeskScreen.rebotsUICreater.CreateTicket(item, helpdeskScreen.ClickTicket, out faqUIElement);
                    m_TicketList.Add(faqUIElement);
                }
            }
        }

        public void OnTicketDetailUpdated(HelpDeskTicketDetailResponse response)
        {
            var ticket = response.ticket;
            var ticketData = response.ticket.data;
            var answers = response.ticket.answers;

            TemplateContainer menuUIElement = null;
            helpdeskScreen.rebotsUICreater.CreateRouteLabel("Main > My Tickets", out menuUIElement);
            m_RouteLabelContainer.Add(menuUIElement);

            m_MenuLabel.text = "My Tickets";

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

        #region 

        #endregion
    }
}