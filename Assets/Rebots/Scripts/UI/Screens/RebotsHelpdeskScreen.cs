using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using HelpDesk.Sdk.Common.Objects;
using System.IO;
using System;
using System.Collections;

namespace Rebots.HelpDesk
{
    public class RebotsHelpdeskScreen : RebotsModalScreen
    {
        [Header("Rebots Manager")]
        public RebotsSettingManager rebotsSettingManager;
        public RebotsParameterDataManager rebotsParameterDataManager;

        [Header("Rebots Helpdesk UI")]
        public RebotsLayoutUI rebotsLayoutUI;
        public RebotsPageUI rebotsPageUI;
        public RebotsUICreater rebotsUICreater;

        #region - - - Theme Style Sheet - - - 
        [Header("Rebots Theme StyleSheet")]
        [SerializeField] public StyleSheet theme1;
        [SerializeField] public StyleSheet theme2;
        [SerializeField] public StyleSheet theme3;
        [SerializeField] public StyleSheet theme4;
        [SerializeField] public StyleSheet theme5;
        [SerializeField] public StyleSheet theme6;
        [SerializeField] public StyleSheet theme7;
        [SerializeField] public StyleSheet theme8;
        [SerializeField] public StyleSheet theme9;
        [SerializeField] public StyleSheet theme10;
        [SerializeField] public StyleSheet theme11;
        [SerializeField] public StyleSheet theme12;
        [SerializeField] public StyleSheet theme13;
        [SerializeField] public StyleSheet theme14;
        [SerializeField] public StyleSheet theme15;
        [SerializeField] public StyleSheet theme16;
        #endregion

        #region - - - Language Font Style Sheet - - - 
        [Header("Rebots Language Font StyleSheet")]
        [SerializeField] public StyleSheet fontEn;
        [SerializeField] public StyleSheet fontKr;
        [SerializeField] public StyleSheet fontJa;
        [SerializeField] public StyleSheet fontCn;
        [SerializeField] public StyleSheet fontTw;
        [SerializeField] public StyleSheet fontTh;
        #endregion

        #region - - - Helpdesk UI Element - - - 
        public VisualElement m_HelpdeskScreen;
        #endregion

        public Category m_Category { get; private set; }
        public Faq m_Faq { get; private set; }
        public string m_SearchString { get; private set; }
        public Dictionary<TicketCategoryInputField, object> m_FieldDic { get; private set; }

        private List<RebotsPageRecord> pageRecords = new();

        #region Run in 'Awake' call
        protected override void SetVisualElements()
        {
            base.SetVisualElements();

            m_HelpdeskScreen = m_Root.Q(RebotsUIStaticString.HelpdeskScreen);
        }

        protected override void RegisterButtonCallbacks()
        {

        }
        #endregion

        #region Show screen
        public override void ShowScreen()
        {
            rebotsPageUI.SetParameterData(rebotsParameterDataManager.ParameterData);

            SetLayout();
            ShowMain(true);

            base.ShowScreen();
            m_Screen.BringToFront();
        }

        public void SetLayout()
        {
            rebotsLayoutUI.SetTranslationText();
            rebotsLayoutUI.SetLanguageUI();
            rebotsLayoutUI.SetHelpdeskData(rebotsSettingManager.helpdeskSetting);

            rebotsPageUI.SetTranslationText();
            rebotsPageUI.SetHelpdeskData(rebotsSettingManager.helpdeskSetting);
            rebotsPageUI.SetPrivacyData(rebotsSettingManager.ticketPrivacySetting);

            ClearLayoutData();

            rebotsSettingManager.LoadFaqCategoryList(rebotsLayoutUI.OnFaqMenuUpdated, true);
            rebotsSettingManager.LoadCsCategoryList(rebotsLayoutUI.OnCsMenuUpdated, true);
        }
        #endregion

        #region Close screen
        public void ClosePanel()
        {
            HideScreen();
        }
        #endregion

        #region Show page
        public void ShowMain(bool isOpen, bool isLanguageChange = false)
        {
            HidePage(RebotsPageType.Main);
            this.pageRecords = isOpen ? new List<RebotsPageRecord>() : this.pageRecords;
            ClearData(RebotsPageType.Main, RebotsPageName.Main);

            rebotsSettingManager.LoadFaqRecommendList(rebotsPageUI.OnFaqRecommendUpdated);
            rebotsSettingManager.LoadFaqCategoryList(rebotsPageUI.OnFaqCategoriesUpdated, false);

            AddPageState(RebotsPageType.Main, RebotsPageName.Main);
            ShowPage(RebotsPageType.Main, RebotsPageName.Main);
        }                                                  
        
        public void ShowCsCategory()
        {
            HidePage(RebotsPageType.Main);
            ClearData(RebotsPageType.Main, RebotsPageName.Cs);

            rebotsSettingManager.LoadCsCategoryList(rebotsPageUI.OnCsCategoriesUpdated, true);

            ShowPage(RebotsPageType.Main, RebotsPageName.Cs);
        }

        public void ShowFaq(Faq faq)
        {
            HidePage(RebotsPageType.Category);
            ClearData(RebotsPageType.Category, RebotsPageName.FaqDetail);

            this.m_Faq = faq;
            rebotsSettingManager.LoadFaq(rebotsPageUI.OnFaqUpdated, m_Faq.id);

            AddPageState(RebotsPageType.Category, RebotsPageName.FaqDetail, faq);
            ShowPage(RebotsPageType.Category, RebotsPageName.FaqDetail);
        }

        public void ShowSearch(string searchStr, int? page = 1)
        {
            HidePage(RebotsPageType.Search);
            ClearData(RebotsPageType.Search, RebotsPageName.SearchResult);

            this.m_SearchString = searchStr;
            rebotsSettingManager.LoadFaqSearchList(rebotsPageUI.OnFaqSearchUpdated, searchStr, page.Value);

            AddPageState(RebotsPageType.Search, RebotsPageName.SearchResult, searchStr);
            ShowPage(RebotsPageType.Search, RebotsPageName.SearchResult);
        }

        public void ShowFaqSubCategory(Category category)
        {
            HidePage(RebotsPageType.Category);
            ClearData(RebotsPageType.Category, RebotsPageName.FaqSubCategory);

            this.m_Category = category;
            rebotsSettingManager.LoadFaqCategory(rebotsPageUI.OnSubCategoryUpdated, m_Category.id);

            AddPageState(RebotsPageType.Category, RebotsPageName.FaqSubCategory, category);
            ShowPage(RebotsPageType.Category, RebotsPageName.FaqSubCategory);
        }

        public void ShowCsSubCategory(Category category)
        {
            HidePage(RebotsPageType.Category);
            ClearData(RebotsPageType.Category, RebotsPageName.CsSubCategory);

            this.m_Category = category;
            rebotsSettingManager.LoadCsCategory(rebotsPageUI.OnSubCategoryUpdated, m_Category.id);

            AddPageState(RebotsPageType.Category, RebotsPageName.CsSubCategory, category);
            ShowPage(RebotsPageType.Category, RebotsPageName.CsSubCategory);
        }

        public void ShowTicketCreate(Category category)
        {
            HidePage(RebotsPageType.Ticket);
            ClearData(RebotsPageType.Ticket, RebotsPageName.TicketCreate);

            this.m_FieldDic = new Dictionary<TicketCategoryInputField, object>();
            this.m_Category = category;
            rebotsSettingManager.LoadCsCategoryFieldList(rebotsPageUI.OnCsCategoryFieldsUpdated, m_Category.id);

            AddPageState(RebotsPageType.Ticket, RebotsPageName.TicketCreate, category);
            ShowPage(RebotsPageType.Ticket, RebotsPageName.TicketCreate);
        }

        public void ShowTicketSuccess()
        {
            HidePage(RebotsPageType.OnlyContent);

            ShowPage(RebotsPageType.OnlyContent, RebotsPageName.TicketSuccess);
        }

        public void ShowMyTicket()
        {
            HidePage(RebotsPageType.MyTicket);
            ClearData(RebotsPageType.MyTicket, RebotsPageName.MyTicket);

            rebotsSettingManager.LoadTicketList(rebotsPageUI.OnMyTicketsUpdated);

            AddPageState(RebotsPageType.MyTicket, RebotsPageName.MyTicket);
            ShowPage(RebotsPageType.MyTicket, RebotsPageName.MyTicket);
        }

        public void ShowTicketDetail(HelpdeskTicket ticket)
        {
            HidePage(RebotsPageType.MyTicket);
            ClearData(RebotsPageType.MyTicket, RebotsPageName.TicketDetail);

            rebotsSettingManager.LoadTicketDetail(rebotsPageUI.OnTicketDetailUpdated, ticket);

            AddPageState(RebotsPageType.MyTicket, RebotsPageName.TicketDetail, ticket);
            ShowPage(RebotsPageType.MyTicket, RebotsPageName.TicketDetail);
        }

        public void ShowPage(RebotsPageType type, RebotsPageName name)
        {
            switch (type)
            {
                case RebotsPageType.Main:
                    ShowVisualElement(rebotsLayoutUI.m_TitleContainer, true);
                    break;
                case RebotsPageType.Category:
                    ShowVisualElement(rebotsPageUI.m_RouteContainer, true);
                    ShowVisualElement(rebotsPageUI.m_RouteLabelContainer, true);
                    ShowVisualElement(rebotsPageUI.m_MenuNameContainer, true);
                    ShowVisualElement(rebotsPageUI.m_SiblingCategoryContainer, true);
                    ShowVisualElement(rebotsPageUI.m_TitleCategoryConatiner, true);
                    break;
                case RebotsPageType.Search:
                    ShowVisualElement(rebotsPageUI.m_RouteContainer, true);
                    ShowVisualElement(rebotsPageUI.m_RouteLabelContainer, true);
                    break;
                case RebotsPageType.Ticket:
                    ShowVisualElement(rebotsPageUI.m_RouteContainer, true);
                    ShowVisualElement(rebotsPageUI.m_RouteLabelContainer, true);
                    ShowVisualElement(rebotsPageUI.m_TitleCategoryConatiner, true);
                    break;
                case RebotsPageType.MyTicket:
                    ShowVisualElement(rebotsPageUI.m_RouteContainer, true);
                    ShowVisualElement(rebotsPageUI.m_RouteLabelContainer, true);
                    ShowVisualElement(rebotsPageUI.m_MenuNameContainer, true);
                    break;
                case RebotsPageType.Layout:
                default:
                    break;
            }

            switch (name)
            {
                case RebotsPageName.Main:
                    ShowVisualElement(rebotsPageUI.m_PopularFaqContainer, true);
                    ShowVisualElement(rebotsPageUI.m_FaqCategoryContainer, true);
                    ShowVisualElement(rebotsPageUI.m_PageConatiner, true);
                    ShowVisualElement(rebotsLayoutUI.m_TicketButtonContainer, true);
                    break;
                case RebotsPageName.Cs:
                    ShowVisualElement(rebotsPageUI.m_CsCategoryContainer, true);
                    ShowVisualElement(rebotsPageUI.m_PageConatiner, true);
                    break;
                case RebotsPageName.CsSubCategory:
                    ShowVisualElement(rebotsPageUI.m_SubCategoryContainer, true);
                    ShowVisualElement(rebotsPageUI.m_PageConatiner, true);
                    break;
                case RebotsPageName.FaqSubCategory:
                    ShowVisualElement(rebotsPageUI.m_SubCategoryContainer, true);
                    ShowVisualElement(rebotsPageUI.m_PageConatiner, true);
                    ShowVisualElement(rebotsLayoutUI.m_TicketButtonContainer, true);
                    break;
                case RebotsPageName.FaqDetail:
                    ShowVisualElement(rebotsPageUI.m_FaqContainer, true);
                    ShowVisualElement(rebotsPageUI.m_PageConatiner, true);
                    ShowVisualElement(rebotsLayoutUI.m_TicketButtonContainer, true);
                    break;
                case RebotsPageName.SearchResult:
                    ShowVisualElement(rebotsPageUI.m_SearchFaqContainer, true);
                    ShowVisualElement(rebotsPageUI.m_PageConatiner, true);
                    ShowVisualElement(rebotsPageUI.m_RouteLabelContainer, false);
                    ShowVisualElement(rebotsLayoutUI.m_TicketButtonContainer, true);
                    break;
                case RebotsPageName.TicketCreate:
                    ShowVisualElement(rebotsPageUI.m_TicketCreateContainer, true);
                    ShowVisualElement(rebotsPageUI.m_PageConatiner, true);
                    break;
                case RebotsPageName.TicketSuccess:
                    ShowVisualElement(rebotsPageUI.m_TicketSuccessContainer, true);
                    ShowVisualElement(rebotsPageUI.m_PageConatiner, true);
                    break;
                case RebotsPageName.MyTicket:
                    ShowVisualElement(rebotsPageUI.m_MyTicketContainer, true);
                    ShowVisualElement(rebotsPageUI.m_PageConatiner, true);
                    break;
                case RebotsPageName.TicketDetail:
                    ShowVisualElement(rebotsPageUI.m_TicketContainer, true);
                    ShowVisualElement(rebotsPageUI.m_PageConatiner, true);
                    break;
                case RebotsPageName.Search:
                    ShowVisualElement(rebotsLayoutUI.m_MenuContainer, false);
                    ShowVisualElement(rebotsLayoutUI.m_LanguageContainer, false);
                    ShowVisualElement(rebotsLayoutUI.m_BackgroundContainer, true);
                    ShowVisualElement(rebotsLayoutUI.m_SearchContainer, true);
                    break;
                case RebotsPageName.Language:
                    ShowVisualElement(rebotsLayoutUI.m_MenuContainer, false);
                    ShowVisualElement(rebotsLayoutUI.m_SearchContainer, false);
                    ShowVisualElement(rebotsLayoutUI.m_BackgroundContainer, true);
                    ShowVisualElement(rebotsLayoutUI.m_LanguageContainer, true);
                    break;
                case RebotsPageName.Menu:
                    ShowVisualElement(rebotsLayoutUI.m_SearchContainer, false);
                    ShowVisualElement(rebotsLayoutUI.m_LanguageContainer, false);
                    ShowVisualElement(rebotsLayoutUI.m_BackgroundContainer, true);
                    ShowVisualElement(rebotsLayoutUI.m_MenuContainer, true);
                    break;
            }
        }

        public void HidePage(RebotsPageType type)
        {
            if (type != RebotsPageType.Layout)
            {
                ShowVisualElement(rebotsLayoutUI.m_TicketButtonContainer, false);

                ShowVisualElement(rebotsPageUI.m_PopularFaqContainer, false);
                ShowVisualElement(rebotsPageUI.m_SearchFaqContainer, false);
                ShowVisualElement(rebotsPageUI.m_FaqCategoryContainer, false);
                ShowVisualElement(rebotsPageUI.m_CsCategoryContainer, false);
                ShowVisualElement(rebotsPageUI.m_SubCategoryContainer, false);
                ShowVisualElement(rebotsPageUI.m_FaqContainer, false);
                ShowVisualElement(rebotsPageUI.m_TicketCreateContainer, false);
                ShowVisualElement(rebotsPageUI.m_TicketSuccessContainer, false);
                ShowVisualElement(rebotsPageUI.m_MyTicketContainer, false);
                ShowVisualElement(rebotsPageUI.m_TicketContainer, false);

                switch (type)
                {
                    case RebotsPageType.Main:
                        ShowVisualElement(rebotsPageUI.m_RouteContainer, false);
                        ShowVisualElement(rebotsPageUI.m_MenuNameContainer, false);
                        ShowVisualElement(rebotsPageUI.m_SiblingCategoryContainer, false);
                        ShowVisualElement(rebotsPageUI.m_TitleCategoryConatiner, false);
                        break;
                    case RebotsPageType.Category:
                        ShowVisualElement(rebotsLayoutUI.m_TitleContainer, false);
                        break;
                    case RebotsPageType.Search:
                        ShowVisualElement(rebotsLayoutUI.m_TitleContainer, false);
                        ShowVisualElement(rebotsPageUI.m_MenuNameContainer, false);
                        ShowVisualElement(rebotsPageUI.m_SiblingCategoryContainer, false);
                        ShowVisualElement(rebotsPageUI.m_TitleCategoryConatiner, false);
                        break;
                    case RebotsPageType.Ticket:
                        ShowVisualElement(rebotsLayoutUI.m_TitleContainer, false);
                        ShowVisualElement(rebotsPageUI.m_MenuNameContainer, false);
                        ShowVisualElement(rebotsPageUI.m_SiblingCategoryContainer, false);
                        break;
                    case RebotsPageType.MyTicket:
                        ShowVisualElement(rebotsLayoutUI.m_TitleContainer, false);
                        ShowVisualElement(rebotsPageUI.m_SiblingCategoryContainer, false);
                        ShowVisualElement(rebotsPageUI.m_TitleCategoryConatiner, false);
                        break;
                    case RebotsPageType.OnlyContent:
                        ShowVisualElement(rebotsLayoutUI.m_TitleContainer, false);
                        ShowVisualElement(rebotsPageUI.m_RouteContainer, false);
                        ShowVisualElement(rebotsPageUI.m_MenuNameContainer, false);
                        ShowVisualElement(rebotsPageUI.m_SiblingCategoryContainer, false);
                        ShowVisualElement(rebotsPageUI.m_TitleCategoryConatiner, false);
                        break;
                }
            }
            ShowVisualElement(rebotsLayoutUI.m_BackgroundContainer, false);
            ShowVisualElement(rebotsLayoutUI.m_MenuContainer, false);
            ShowVisualElement(rebotsLayoutUI.m_SearchContainer, false);
            ShowVisualElement(rebotsLayoutUI.m_LanguageContainer, false);
        }
        #endregion

        #region UI Clear
        private void ClearLayoutData()
        {
            rebotsLayoutUI.m_MenuFaqCategoryList.Clear();
            rebotsLayoutUI.m_MenuCsCategoryList.Clear();
        }

        private void ClearData(RebotsPageType type, RebotsPageName name)
        {
            switch (type)
            {
                case RebotsPageType.Category:
                    rebotsPageUI.m_RouteLabelContainer.Clear();
                    rebotsPageUI.m_SiblingCategoryList.Clear();
                    break;
                case RebotsPageType.Ticket:
                case RebotsPageType.MyTicket:
                    rebotsPageUI.m_RouteLabelContainer.Clear();
                    break;
            }

            switch (name)
            {
                case RebotsPageName.Main:
                    rebotsPageUI.m_PopularFaqList.Clear();
                    rebotsPageUI.m_FaqCategoryList.Clear();
                    break;
                case RebotsPageName.Cs:
                    rebotsPageUI.m_CsCategoryList.Clear();
                    break;
                case RebotsPageName.CsSubCategory:
                case RebotsPageName.FaqSubCategory:
                    rebotsPageUI.m_SubCategoryList.Clear();
                    break;
                case RebotsPageName.FaqDetail:
                    rebotsPageUI.m_FaqDetailContainer.Clear();
                    break;
                case RebotsPageName.SearchResult:
                    rebotsPageUI.m_SearchFaqList.Clear();
                    break;
                case RebotsPageName.TicketCreate:
                    rebotsPageUI.m_TicketFieldList.Clear();
                    break;
                case RebotsPageName.MyTicket:
                    rebotsPageUI.m_TicketList.Clear();
                    break;
                case RebotsPageName.TicketDetail:
                    rebotsPageUI.m_TicketDetailList.Clear();
                    rebotsPageUI.m_TicketAnswerList.Clear();
                    break;
            }
        }
        #endregion

        #region Click Action
        public void ClickLanguage(RebotsLanguageInfo language)
        {
            rebotsSettingManager.HelpdeskInitialize(language.languageCode);

            StartCoroutine(LanguageInitialize());
        }

        public void ClickCsCategory(Category category)
        {
            if (category.useField)
            {
                ShowTicketCreate(category);
            }
            else
            {
                ShowCsSubCategory(category);
            }
        }

        public void ClickFaqCategory(Category category)
        {
            ShowFaqSubCategory(category);
        }

        public void ClickFaq(Faq faq)
        {
            ShowFaq(faq);
        }

        public void ClickSearch()
        {
            var searchStr = rebotsLayoutUI.m_SearchField.GetValue();
            if (string.IsNullOrEmpty(searchStr))
            {
                return;
            }
            ShowSearch(searchStr);
        }

        public void ClickTicketSubmit(ClickEvent evt)
        {
            var ticketInputFields = new DictionaryTicketInputFormData();
            var ticketAddFieldDic = new Dictionary<string, string>();
            ticketInputFields.SelectedCategory = m_Category;
            foreach (var item in m_FieldDic)
            {
                var field = item.Key;
                if (field.fieldType == RebotsInputFieldType.Text || field.fieldType == RebotsInputFieldType.Textarea)
                {
                    var component = item.Value as RebotsTextFieldComponent;
                    var fieldValue = component.GetFieldValue();

                    if (field.name == "email")
                    {
                        ticketInputFields.Email = fieldValue;
                    }
                    else if (field.name == "content")
                    {
                        ticketInputFields.Content = fieldValue;
                    }
                    else
                    {
                        ticketAddFieldDic.Add(field.text, fieldValue);
                    }
                }
                else if (field.fieldType == RebotsInputFieldType.Dropdown)
                {
                    var component = item.Value as RebotsDropdownFieldComponent;
                    var fieldValue = component.GetFieldValue();

                    ticketAddFieldDic.Add(field.text, fieldValue);
                }
                else if (field.fieldType == RebotsInputFieldType.Checkbox || field.fieldType == RebotsInputFieldType.Radiobutton)
                {
                    var component = item.Value as RebotsButtonGroupFieldComponent;
                    var fieldValue = component.GetFieldValue();

                    ticketAddFieldDic.Add(field.text, fieldValue);
                }
                else if (field.fieldType == RebotsInputFieldType.File)
                {
                    var component = item.Value as RebotsAttachmentFieldComponent;
                    var fieldValue = component.GetFieldValue();

                    foreach (var value in fieldValue)
                    {
                        ticketInputFields.AddAttachment(value.content as FileStream);
                    }
                }
            }
            ticketInputFields.Data = ticketAddFieldDic;
            ticketInputFields.Language = rebotsSettingManager.localizationManager.language;

            rebotsSettingManager.CreateTicket(rebotsPageUI.OnTicketCreate, ticketInputFields);
        }

        public void ClickTicket(HelpdeskTicket ticket)
        {
            ShowTicketDetail(ticket);
        }
        #endregion

        #region Page Recording Controller 
        private void AddPageState(RebotsPageType type, RebotsPageName name, object parameter = null)
        {
            pageRecords.Add(new RebotsPageRecord()
            {
                PageType = type,
                PageName = name,
                Parameter = parameter
            });
        }

        public void ChangePage(bool isGoBack)
        {
            if (isGoBack)
            {
                if (pageRecords.Count <= 1)
                {
                    pageRecords = new List<RebotsPageRecord>();
                    ShowMain(false);
                    return;
                }
            }

            var CurrentPage = pageRecords[pageRecords.Count - 1];
            var ReloadPage = CurrentPage;
            if (isGoBack)
            {
                var BackPage = pageRecords[pageRecords.Count - 2];
                ReloadPage = BackPage;
                pageRecords.Remove(BackPage);
            }
            pageRecords.Remove(CurrentPage);

            switch (ReloadPage.PageName)
            {
                case RebotsPageName.Main:
                    ShowMain(false);
                    break;
                case RebotsPageName.Cs:
                    ShowCsCategory();
                    break;
                case RebotsPageName.CsSubCategory:
                    ShowCsSubCategory(ReloadPage.Parameter as Category);
                    break;
                case RebotsPageName.FaqSubCategory:
                    ShowFaqSubCategory(ReloadPage.Parameter as Category);
                    break;
                case RebotsPageName.FaqDetail:
                    ShowFaq(ReloadPage.Parameter as Faq);
                    break;
                case RebotsPageName.TicketCreate:
                    ShowTicketCreate(ReloadPage.Parameter as Category);
                    break;
                case RebotsPageName.TicketSuccess:
                    ShowTicketSuccess();
                    break;
                case RebotsPageName.SearchResult:
                    ShowSearch(ReloadPage.Parameter as string);
                    break;
                case RebotsPageName.MyTicket:
                    ShowMyTicket();
                    break;
                case RebotsPageName.TicketDetail:
                    ShowTicketDetail(ReloadPage.Parameter as HelpdeskTicket);
                    break;
                case RebotsPageName.Search:
                case RebotsPageName.Menu:
                default:
                    ShowMain(false);
                    break;
            }
        }
        #endregion

        #region (public) Add m_FieldDic
        public void AddFieldDic(TicketCategoryInputField field, object fieldUIComponent)
        {
            m_FieldDic.Add(field, fieldUIComponent);
        }
        #endregion

        #region (private) Language Initialize
        private IEnumerator LanguageInitialize()
        {
            int sec = 1;

            while (!rebotsSettingManager.InitializeState())
            {
                yield return new WaitForSeconds(1f);
                if (++sec > 4)
                {
                    break;
                }
            }

            if (rebotsSettingManager.InitializeState())
            {
                SetLayout();
                ShowMain(true);
            }
            else
            {
                ClosePanel();
            }
        }
        #endregion
    }
}
