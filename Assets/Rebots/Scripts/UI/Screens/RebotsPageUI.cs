using HelpDesk.Sdk.Common.Objects;
using HelpDesk.Sdk.Common.Protocols.Responses;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
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
        private PrivacySetting m_ticketPrivacySetting;
        private Dictionary<string, string> m_parameterDic = new Dictionary<string, string>();

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
            m_SiblingCategoryList = m_SiblingCategoryContainer.Q(RebotsUIStaticString.SiblingCategoryList);

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
            m_BackButton?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.ChangePage(true, false));
            m_TicketSubmitButton?.RegisterCallback<ClickEvent>(helpdeskScreen.ClickTicketSubmit);
            m_TicketSuccessMainButton?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.ShowMain(false));
        }
        #endregion

        #region Run in 'Start' call
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
        #endregion

        public void SetPrivacyData(PrivacySetting ticketPrivacySetting)
        {
            m_ticketPrivacySetting = ticketPrivacySetting;
        }

        public void SetParameterData(RebotsParameterData parameterData)
        {
            m_parameterDic = parameterData.parameters;
        }

        #region Page Data API Callback
        public void OnFaqRecommendUpdated(HelpdeskFaqListResponse response)
        {
            var faqs = response.faqs;

            if (faqs != null && faqs.Count() > 0)
            {
                foreach (var item in faqs)
                {
                    TemplateContainer faqUIElement = null;
                    helpdeskScreen.rebotsUICreater.CreateFaq(item, RebotsFaqAssetType.popular, null, helpdeskScreen.ClickFaq, out faqUIElement);

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
                    helpdeskScreen.rebotsUICreater.CreateFaq(item, RebotsFaqAssetType.search, search, helpdeskScreen.ClickFaq, out faqUIElement);
                    m_SearchFaqList.Add(faqUIElement);
                }
            }
        }

        public void OnFaqCategoriesUpdated(HelpdeskFaqCategoriesResponse response)
        {
            var faqCategories = response.items;
            if (faqCategories != null && faqCategories.Count() > 0)
            {
                foreach (var item in faqCategories)
                {
                    TemplateContainer categoryUIElement = null;
                    helpdeskScreen.rebotsUICreater.CreateCategory<Category>(item, RebotsCategoryAssetType.faq, helpdeskScreen.ClickFaqCategory, out categoryUIElement);

                    var m_ViewMoreLabel = categoryUIElement.Q<Label>(RebotsUIStaticString.ViewMoreLabel);
                    m_ViewMoreLabel.text = localizationManager.translationDic[RebotsUIStaticString.ViewMoreLabel];

                    if (categoryUIElement != null)
                    {
                        VisualElement lowerList = categoryUIElement.Q(RebotsUIStaticString.LowerCategoryContainer);
                        lowerList.Clear();

                        var subCategories = item.subCategories;
                        int countInt = 0;
                        if (subCategories != null && subCategories.Count() > 0)
                        {
                            foreach (var sub in subCategories.Take(5))
                            {
                                TemplateContainer subUIElement = null;
                                helpdeskScreen.rebotsUICreater.CreateCategory<Category>(sub, RebotsCategoryAssetType.contents, helpdeskScreen.ClickFaqCategory, out subUIElement);
                                lowerList.Add(subUIElement);
                                countInt++;
                            }
                        }

                        var faqs = item.faqs;
                        if (faqs != null && faqs.Count() > 0)
                        {
                            foreach (var faq in faqs.Take(5 - countInt))
                            {
                                TemplateContainer faqUIElement = null;
                                helpdeskScreen.rebotsUICreater.CreateCategory<Faq>(faq, RebotsCategoryAssetType.sub, helpdeskScreen.ClickFaq, out faqUIElement);
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
            var csCategories = response.items;
            if (csCategories != null && csCategories.Count() > 0)
            {
                foreach (var item in csCategories)
                {
                    TemplateContainer categoryUIElement = null;
                    helpdeskScreen.rebotsUICreater.CreateCategory<Category>(item, RebotsCategoryAssetType.cs, helpdeskScreen.ClickCsCategory, out categoryUIElement);
                    m_CsCategoryList.Add(categoryUIElement);
                }
            }
        }

        public void OnSubCategoryUpdated(HelpdeskFaqCategoryResponse response)
        {
            var faqCategory = response;

            TemplateContainer menuUIElement = null;
            helpdeskScreen.rebotsUICreater.CreateRouteLabel("Main > FAQ > ", false, out menuUIElement);
            m_RouteLabelContainer.Add(menuUIElement);

            var routeCategories = faqCategory.categories;
            var routeCategoryStr = faqCategory.name;
            foreach (var category in routeCategories)
            {
                routeCategoryStr = category.name + " > " + routeCategoryStr;
            }

            TemplateContainer routeUIElement = null;
            helpdeskScreen.rebotsUICreater.CreateRouteLabel(routeCategoryStr, true, out routeUIElement);
            m_RouteLabelContainer.Add(routeUIElement);

            m_MenuLabel.text = "FAQ";

            var sliblingCategories = faqCategory.siblingCategories;
            if (sliblingCategories != null && sliblingCategories.Count() > 0)
            {
                foreach (var item in sliblingCategories)
                {
                    TemplateContainer categoryUIElement = null;
                    if (item.id == faqCategory.id)
                    {
                        helpdeskScreen.rebotsUICreater.CreateCategory<Category>(item, RebotsCategoryAssetType.selected, helpdeskScreen.ClickFaqCategory, out categoryUIElement);
                    }
                    else
                    {
                        helpdeskScreen.rebotsUICreater.CreateCategory<Category>(item, RebotsCategoryAssetType.sibling, helpdeskScreen.ClickFaqCategory, out categoryUIElement);
                    }
                    m_SiblingCategoryList.Add(categoryUIElement);
                }
            }

            m_TitleCategoryLabel.text = faqCategory.name;

            var subCategories = faqCategory.subCategories;
            if (subCategories != null && subCategories.Count() > 0)
            {
                foreach (var item in subCategories)
                {
                    TemplateContainer categoryUIElement = null;
                    helpdeskScreen.rebotsUICreater.CreateCategory<Category>(item, RebotsCategoryAssetType.contents, helpdeskScreen.ClickFaqCategory, out categoryUIElement);
                    m_SubCategoryList.Add(categoryUIElement);
                }
            }

            var faqs = faqCategory.faqs;
            if (faqs != null && faqs.Count() > 0)
            {
                foreach (var faq in faqs)
                {
                    TemplateContainer categoryUIElement = null;
                    helpdeskScreen.rebotsUICreater.CreateCategory<Faq>(faq, RebotsCategoryAssetType.sub, helpdeskScreen.ClickFaq, out categoryUIElement);
                    m_SubCategoryList.Add(categoryUIElement);
                }
            }
        }

        public void OnSubCategoryUpdated(HelpdeskTicketCategoryResponse response)
        {
            var csCategory = response;

            TemplateContainer menuUIElement = null;
            helpdeskScreen.rebotsUICreater.CreateRouteLabel("Main > Inquiry > ", false, out menuUIElement);
            m_RouteLabelContainer.Add(menuUIElement);

            var routeCategories = csCategory.categories;
            var routeCategoryStr = csCategory.name;
            foreach (var category in routeCategories)
            {
                routeCategoryStr = category.name + " > " + routeCategoryStr;
            }

            TemplateContainer routeUIElement = null;
            helpdeskScreen.rebotsUICreater.CreateRouteLabel(routeCategoryStr, true, out routeUIElement);
            m_RouteLabelContainer.Add(routeUIElement);

            m_MenuLabel.text = "Inquiry";

            var sliblingCategories = csCategory.siblingCategories;
            if (sliblingCategories != null && sliblingCategories.Count() > 0)
            {
                foreach (var item in sliblingCategories)
                {
                    TemplateContainer categoryUIElement = null;
                    if (item.id == csCategory.id)
                    {
                        helpdeskScreen.rebotsUICreater.CreateCategory<Category>(item, RebotsCategoryAssetType.selected, helpdeskScreen.ClickCsCategory, out categoryUIElement);
                    }
                    else
                    {
                        helpdeskScreen.rebotsUICreater.CreateCategory<Category>(item, RebotsCategoryAssetType.sibling, helpdeskScreen.ClickCsCategory, out categoryUIElement);
                    }
                    m_SiblingCategoryList.Add(categoryUIElement);
                }
            }

            m_TitleCategoryLabel.text = csCategory.name;

            var subCategories = csCategory.subCategories;
            if (subCategories != null && subCategories.Count() > 0)
            {
                foreach (var item in subCategories)
                {
                    TemplateContainer categoryUIElement = null;
                    if (item.useField)
                    {
                        helpdeskScreen.rebotsUICreater.CreateCategory<Category>(item, RebotsCategoryAssetType.sub, helpdeskScreen.ClickCsCategory, out categoryUIElement); 
                    }
                    else
                    {
                        helpdeskScreen.rebotsUICreater.CreateCategory<Category>(item, RebotsCategoryAssetType.contents, helpdeskScreen.ClickCsCategory, out categoryUIElement);
                    }
                    m_SubCategoryList.Add(categoryUIElement);
                }
            }
        }

        public void OnFaqUpdated(HelpdeskFaqResponse response)
        {
            var faq = response;

            TemplateContainer menuUIElement = null;
            helpdeskScreen.rebotsUICreater.CreateRouteLabel("Main > FAQ > ", false, out menuUIElement);
            m_RouteLabelContainer.Add(menuUIElement);

            var routeCategories = faq.categories;
            var routeCategoryStr = "";
            foreach (var category in routeCategories)
            {
                routeCategoryStr = routeCategoryStr == "" ? category.name : category.name + " > " + routeCategoryStr;
            }

            TemplateContainer routeUIElement = null;
            helpdeskScreen.rebotsUICreater.CreateRouteLabel(routeCategoryStr, true, out routeUIElement);
            m_RouteLabelContainer.Add(routeUIElement);

            m_MenuLabel.text = "FAQ";
            m_TitleCategoryLabel.text = faq.title;

            var sliblingCategories = faq.siblingCategories;
            if (sliblingCategories != null && sliblingCategories.Count() > 0)
            {
                foreach (var item in sliblingCategories)
                {
                    TemplateContainer categoryUIElement = null;
                    if (item.id == faq.id)
                    {
                        helpdeskScreen.rebotsUICreater.CreateCategory<Category>(item, RebotsCategoryAssetType.selected, helpdeskScreen.ClickFaqCategory, out categoryUIElement);
                    }
                    else
                    {
                        helpdeskScreen.rebotsUICreater.CreateCategory<Category>(item, RebotsCategoryAssetType.sibling, helpdeskScreen.ClickFaqCategory, out categoryUIElement);
                    }
                    m_SiblingCategoryList.Add(categoryUIElement);
                }
            }
            else
            {
                ShowVisualElement(m_SiblingCategoryContainer, false);
            }

            var contents = new Label();
            contents.text = faq.contents;
            contents.AddToClassList(RebotsUIStaticString.RebotsLabel_Regular16);
            contents.AddToClassList(RebotsUIStaticString.RebotsFontColor_Black);
            m_FaqDetailContainer.Add(contents);
        }

        public void OnCsCategoryFieldsUpdated(HelpdeskTicketCategoryField response)
        {
            var csCategory = response.category;

            TemplateContainer menuUIElement = null;
            helpdeskScreen.rebotsUICreater.CreateRouteLabel("Main > Inquiry > ", false, out menuUIElement);
            m_RouteLabelContainer.Add(menuUIElement);

            var routeCategories = csCategory.categories;
            var routeCategoryStr = csCategory.name;
            foreach (var category in routeCategories)
            {
                routeCategoryStr = category.name + " > " + routeCategoryStr;
            }

            TemplateContainer routeUIElement = null;
            helpdeskScreen.rebotsUICreater.CreateRouteLabel(routeCategoryStr, true, out routeUIElement);
            m_RouteLabelContainer.Add(routeUIElement);

            m_MenuLabel.text = "Inquiry";
            m_TitleCategoryLabel.text = csCategory.name;

            var fields = csCategory.inputFields;
            if (fields != null && fields.Count() > 0)
            {
                foreach (var field in fields)
                {
                    string parameterValue = "";
                    m_parameterDic.TryGetValue(field.name, out parameterValue);

                    TemplateContainer fieldUIElement = null;
                    object fieldUIComponent = null;
                    helpdeskScreen.rebotsUICreater.CreateCsCategoryField(field, parameterValue, out fieldUIElement, out fieldUIComponent);

                    var m_RequiredLabel = fieldUIElement.Q<Label>(RebotsUIStaticString.RequiredLabel);
                    m_RequiredLabel.text = localizationManager.translationDic[RebotsUIStaticString.RequiredLabel];

                    if (field.fieldType == RebotsInputFieldType.file)
                    {
                        var m_NoFileLabel = fieldUIElement.Q<Label>(RebotsUIStaticString.NoFileLabel);
                        m_NoFileLabel.text = localizationManager.translationDic[RebotsUIStaticString.NoFileLabel];
                    }

                    m_TicketFieldList.Add(fieldUIElement);
                    helpdeskScreen.m_fieldDic.Add(field, fieldUIComponent);
                }
            }

            if (m_ticketPrivacySetting != null)
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
                helpdeskScreen.rebotsUICreater.CreatePrivacyField(m_ticketPrivacySetting, formSectionTransData, out privacyUIElement);

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
            helpdeskScreen.rebotsUICreater.CreateRouteLabel("Main > My Tickets", false, out menuUIElement);
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
            helpdeskScreen.rebotsUICreater.CreateRouteLabel("Main > My Tickets", false, out menuUIElement);
            m_RouteLabelContainer.Add(menuUIElement);

            m_MenuLabel.text = "My Tickets";

            var dataDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(ticketData.data);

            if (dataDic != null && dataDic.Count() > 0)
            {
                foreach (var item in dataDic)
                {
                    TemplateContainer fieldUIElement = null;
                    helpdeskScreen.rebotsUICreater.CreateTicketDetail(item.Key, item.Value, RebotsTicketDetailAssetType.field, out fieldUIElement);
                    m_TicketDetailList.Add(fieldUIElement);
                }
            }

            m_TicketContentsLabel.text = ticketData.content;

            if (answers != null && answers.Count() > 0)
            {
                foreach (var item in answers)
                {
                    TemplateContainer answerUIElement = null;
                    helpdeskScreen.rebotsUICreater.CreateTicketDetail(string.Format("{0:d}", item.answered), item.content, RebotsTicketDetailAssetType.answer, out answerUIElement);
                    m_TicketAnswerList.Add(answerUIElement);
                }
            }
        }
        #endregion
    }
}