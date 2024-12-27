using Assets.Rebots;
using HelpDesk.Sdk.Common.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

namespace Rebots.HelpDesk
{
    public class RebotsHelpdeskScreen : RebotsModalScreen
    {
        public static event Action RebotsSpinnerShow;
        public static event Action RebotsSpinnerCancel;

        [Header("Rebots Manager")]
        public RebotsSettingManager rebotsSettingManager;

        [Header("Rebots Helpdesk UI")]
        public RebotsLayoutUI rebotsLayoutUI;
        public RebotsPageUI rebotsPageUI;
        public RebotsUICreater rebotsUICreater;

        #region - - - Helpdesk Resources - - - 
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

        [Header("Rebots Language Font Asset")]
        [SerializeField] public Font? fontAssetKR;
        [SerializeField] public Font? fontAssetEN;
        [SerializeField] public Font? fontAssetJP;
        [SerializeField] public Font? fontAssetCN;
        [SerializeField] public Font? fontAssetTH;
        #endregion

        #region - - - Helpdesk UI Element - - - 
        public VisualElement m_HelpdeskScreen;
        #endregion

        [HideInInspector]
        public GameObject? SystemEventGO;
        [HideInInspector]
        public ScreenOrientation OriginScreenOrientation;
        public bool ScreenPortrait { get; private set; } = true;

        public Dictionary<TicketCategoryInputField, object> FieldDictionary { get; private set; }
        public List<RebotsPageRecord> PageRecords { get; private set; } = new();

        private bool openMenu = false;
        private float verticalScrollValue = 0;
        private Button oldMenuButton = null;

        public const int ListPageSize = 10;
        public const string TicketAnswersFile = "rebots.ticket";

        #region Run in 'Awake' call
        protected override void SetVisualElements()
        {
            base.SetVisualElements();

            m_HelpdeskScreen = m_Root.Q(RebotsUIStaticString.HelpdeskScreen);

            rebotsUICreater.SaveVerticalScrollAction = SaveVerticalScroll;
            rebotsUICreater.SetVerticalScrollAction = SetVerticalScroll;
            rebotsUICreater.ClickAttachmentLinkAction = ClickAttachmentLink;
            rebotsUICreater.ClickMenuCategoryAction = ClickMenuCategory;
        }
        #endregion

        #region Show screen
        public override void ShowScreen()
        {
            Screen.orientation = (ScreenOrientation)rebotsSettingManager.RebotsScreenOrientation;
            ScreenPortrait = ((ScreenOrientation)rebotsSettingManager.RebotsScreenOrientation == ScreenOrientation.Portrait);

            rebotsPageUI.SetParameterData(rebotsSettingManager.rebotsParameterDataManager.ParameterData);

            rebotsPageUI.LoadMyTicketAnswers(TicketAnswersFile);

            SetLayout();
            ShowMain(true);

            base.ShowScreen();
            m_Screen.BringToFront();
        }

        public void SetLayout()
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN || UNITY_WEBGL
            if (SystemEventGO != null)
            {
                SystemEventGO.SetActive(false);
            }
#endif
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

#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN || UNITY_WEBGL
            if (SystemEventGO != null)
            {
                SystemEventGO.SetActive(true);
            }
#endif
            Screen.orientation = OriginScreenOrientation;
        }
        #endregion

        #region Show page
        public void ShowMain(bool isOpen, bool isLanguageChange = false)
        {
            rebotsSettingManager.SetApiCallFinishedWithProgress(RebotsSpinnerCancel);

            this.PageRecords = isOpen ? new List<RebotsPageRecord>() : this.PageRecords;
            HidePage(RebotsPageType.MainTitle);
            ClearData(RebotsPageType.MainTitle, RebotsPageName.Main);

            rebotsSettingManager.LoadFaqRecommendList(rebotsPageUI.OnFaqRecommendUpdated);
            rebotsSettingManager.LoadFaqCategoryList(rebotsPageUI.OnFaqCategoriesUpdated, false);

            AddPageState(RebotsPageType.MainTitle, RebotsPageName.Main);
            ShowPage(RebotsPageType.MainTitle, RebotsPageName.Main);
        }

        public void ShowCsCategory()
        {
            HidePage(RebotsPageType.NoTitleRoute);
            ClearData(RebotsPageType.NoTitleRoute, RebotsPageName.InquiryList);

            rebotsSettingManager.LoadCsCategoryList(rebotsPageUI.OnCsCategoriesUpdated, false);

            AddPageState(RebotsPageType.NoTitleRoute, RebotsPageName.InquiryList);
            ShowPage(RebotsPageType.NoTitleRoute, RebotsPageName.InquiryList);
        }

        public void ShowFaq(Faq faq)
        {
            HidePage(RebotsPageType.PageTitle);
            ClearData(RebotsPageType.PageTitle, RebotsPageName.Faq);

            rebotsSettingManager.LoadFaq(rebotsPageUI.OnFaqUpdated, faq.id);

            AddPageState(RebotsPageType.PageTitle, RebotsPageName.Faq, faq);
            ShowPage(RebotsPageType.PageTitle, RebotsPageName.Faq);
        }

        public void ShowSearch(RebotsPagingData<string> pagingData)
        {
            HidePage(RebotsPageType.PageTitle);
            ClearData(RebotsPageType.PageTitle, RebotsPageName.SearchList);

            rebotsSettingManager.LoadFaqSearchList(rebotsPageUI.OnFaqSearchUpdated, pagingData.Data, pagingData.Page);

            if (pagingData.Page == 1)
            {
                AddPageState(RebotsPageType.PageTitle, RebotsPageName.SearchList, pagingData.Data);
            }
            ShowPage(RebotsPageType.PageTitle, RebotsPageName.SearchList);
        }

        public void ShowFaqSubCategory(RebotsPagingData<Category> pagingData)
        {
            if (pagingData.IsClickAction)
            {
                HidePage(RebotsPageType.PageTitle);
                ClearData(RebotsPageType.PageTitle, RebotsPageName.FaqList);

                rebotsSettingManager.LoadFaqCategory(rebotsPageUI.OnSubCategoryUpdated, pagingData.Data.id, pagingData.Page, ListPageSize);

                if (pagingData.Page == 1)
                {
                    AddPageState(RebotsPageType.PageTitle, RebotsPageName.FaqList, pagingData.Data);
                }
                ShowPage(RebotsPageType.PageTitle, RebotsPageName.FaqList);
            }
        }

        public void ShowCsSubCategory(Category category)
        {
            HidePage(RebotsPageType.PageTitle);
            ClearData(RebotsPageType.PageTitle, RebotsPageName.InquiryList);

            rebotsSettingManager.LoadCsCategory(rebotsPageUI.OnSubCategoryUpdated, category.id);

            AddPageState(RebotsPageType.PageTitle, RebotsPageName.InquiryList, category);
            ShowPage(RebotsPageType.PageTitle, RebotsPageName.InquiryList);
        }

        public void ShowTicketCreate(Category category)
        {
            HidePage(RebotsPageType.PageTitle);
            ClearData(RebotsPageType.PageTitle, RebotsPageName.TicketCreate);

            this.FieldDictionary = new Dictionary<TicketCategoryInputField, object>();
            rebotsSettingManager.LoadCsCategoryFieldList(rebotsPageUI.OnCsCategoryFieldsUpdated, category.id);

            AddPageState(RebotsPageType.PageTitle, RebotsPageName.TicketCreate, category);
            ShowPage(RebotsPageType.PageTitle, RebotsPageName.TicketCreate);
        }

        public void ShowTicketSuccess()
        {
            HidePage(RebotsPageType.PageTitle);

            ShowPage(RebotsPageType.Layout, RebotsPageName.TicketSuccess);
        }

        public void ShowMyTicket()
        {
            HidePage(RebotsPageType.NoTitleRoute);
            ClearData(RebotsPageType.NoTitleRoute, RebotsPageName.TicketList);

            rebotsSettingManager.LoadTicketList(rebotsPageUI.OnMyTicketsUpdated);

            AddPageState(RebotsPageType.NoTitleRoute, RebotsPageName.TicketList);
            ShowPage(RebotsPageType.NoTitleRoute, RebotsPageName.TicketList);
        }

        public void ShowTicketDetail(HelpdeskTicket ticket)
        {
            HidePage(RebotsPageType.NoTitleRoute);
            ClearData(RebotsPageType.NoTitleRoute, RebotsPageName.Ticket);

            rebotsSettingManager.LoadTicketDetail(rebotsPageUI.OnTicketDetailUpdated, ticket);

            AddPageState(RebotsPageType.NoTitleRoute, RebotsPageName.Ticket, ticket);
            ShowPage(RebotsPageType.NoTitleRoute, RebotsPageName.Ticket);
        }

        public void ShowPage(RebotsPageType type, RebotsPageName name)
        {
            switch (type)
            {
                case RebotsPageType.MainTitle:
                    ShowVisualElement(rebotsLayoutUI.m_MainTitleContainer, true);
                    break;
                case RebotsPageType.PageTitle:
                    ShowVisualElement(rebotsPageUI.m_PageTitlleContainer, true);
                    ShowVisualElement(rebotsPageUI.m_PageTitleLabelContainer, true);
                    break;
                case RebotsPageType.NoTitleLabel:
                    ShowVisualElement(rebotsPageUI.m_PageTitlleContainer, true);
                    ShowVisualElement(rebotsPageUI.m_PageTitleLabelContainer, false);
                    break;
                case RebotsPageType.NoTitleRoute:
                    ShowVisualElement(rebotsPageUI.m_PageTitlleContainer, true);
                    ShowVisualElement(rebotsPageUI.m_PageTitleLabelContainer, true);
                    break;
                case RebotsPageType.Layout:
                default:
                    break;
            }

            switch (name)
            {
                case RebotsPageName.Main:
                    ShowVisualElement(rebotsPageUI.m_MainContainer, true);
                    ShowVisualElement(rebotsPageUI.m_PageConatiner, true);
                    ShowVisualElement(rebotsLayoutUI.m_TicketButtonContainer, true);
                    break;
                case RebotsPageName.InquiryList:
                    ShowVisualElement(rebotsPageUI.m_InquiryListContainer, true);
                    ShowVisualElement(rebotsPageUI.m_PageConatiner, true);
                    break;
                case RebotsPageName.FaqList:
                    ShowVisualElement(rebotsPageUI.m_FaqListContainer, true);
                    ShowVisualElement(rebotsPageUI.m_PagingContainer, true);
                    ShowVisualElement(rebotsPageUI.m_PageConatiner, true);
                    ShowVisualElement(rebotsLayoutUI.m_TicketButtonContainer, true);
                    break;
                case RebotsPageName.Faq:
                    ShowVisualElement(rebotsPageUI.m_FaqContainer, true);
                    ShowVisualElement(rebotsPageUI.m_PageConatiner, true);
                    ShowVisualElement(rebotsLayoutUI.m_TicketButtonContainer, true);
                    break;
                case RebotsPageName.SearchList:
                    ShowVisualElement(rebotsPageUI.m_FaqSearchContainer, true);
                    ShowVisualElement(rebotsPageUI.m_PagingContainer, true);
                    ShowVisualElement(rebotsPageUI.m_PageConatiner, true);
                    break;
                case RebotsPageName.TicketCreate:
                    ShowVisualElement(rebotsPageUI.m_TicketCreateContainer, true);
                    ShowVisualElement(rebotsPageUI.m_PageConatiner, true);
                    break;
                case RebotsPageName.TicketSuccess:
                    ShowVisualElement(rebotsPageUI.m_TicketSuccessContainer, true);
                    ShowVisualElement(rebotsPageUI.m_PageConatiner, true);
                    break;
                case RebotsPageName.TicketList:
                    ShowVisualElement(rebotsPageUI.m_TicketListContainer, true);
                    ShowVisualElement(rebotsPageUI.m_PageConatiner, true);
                    break;
                case RebotsPageName.Ticket:
                    ShowVisualElement(rebotsPageUI.m_TicketContainer, true);
                    ShowVisualElement(rebotsPageUI.m_TicketDetailContainer, true);
                    ShowVisualElement(rebotsPageUI.m_TicketAnswerContainer, false);
                    ShowVisualElement(rebotsPageUI.m_PageConatiner, true);
                    break;
                case RebotsPageName.Answer:
                    ShowVisualElement(rebotsPageUI.m_TicketContainer, true);
                    ShowVisualElement(rebotsPageUI.m_TicketDetailContainer, false);
                    ShowVisualElement(rebotsPageUI.m_TicketAnswerContainer, true);
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
                    rebotsLayoutUI.m_BackgroundContainer.style.top = 0f;
                    ShowVisualElement(rebotsLayoutUI.m_SearchContainer, false);
                    ShowVisualElement(rebotsLayoutUI.m_LanguageContainer, false);
                    ShowVisualElement(rebotsLayoutUI.m_BackgroundContainer, true);
                    ShowVisualElement(rebotsLayoutUI.m_MenuContainer, true);
                    break;
            }

            rebotsLayoutUI.m_ScrollView.verticalScroller.value = 0f;
        }

        public void HidePage(RebotsPageType type)
        {
            if (PageRecords.Count != 0 && type != RebotsPageType.Layout)
            {
                RebotsSpinnerShow.Invoke();
            }

            if (type != RebotsPageType.Layout)
            {
                ShowVisualElement(rebotsLayoutUI.m_MainTitleContainer, false);
                ShowVisualElement(rebotsPageUI.m_PageTitlleContainer, false);
                ShowVisualElement(rebotsLayoutUI.m_TicketButtonContainer, false);

                ShowVisualElement(rebotsPageUI.m_MainContainer, false);
                ShowVisualElement(rebotsPageUI.m_InquiryListContainer, false);
                ShowVisualElement(rebotsPageUI.m_FaqListContainer, false);
                ShowVisualElement(rebotsPageUI.m_FaqContainer, false);
                ShowVisualElement(rebotsPageUI.m_FaqSearchContainer, false);
                ShowVisualElement(rebotsPageUI.m_TicketCreateContainer, false);
                ShowVisualElement(rebotsPageUI.m_TicketSuccessContainer, false);
                ShowVisualElement(rebotsPageUI.m_TicketListContainer, false);
                ShowVisualElement(rebotsPageUI.m_TicketContainer, false);
                ShowVisualElement(rebotsPageUI.m_TicketDetailContainer, false);
                ShowVisualElement(rebotsPageUI.m_TicketAnswerContainer, false);
                ShowVisualElement(rebotsPageUI.m_PagingContainer, false);
            }
            else
            {
                rebotsLayoutUI.m_BackgroundContainer.style.top = 50f;
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
                case RebotsPageType.PageTitle:
                    break;
            }

            switch (name)
            {
                case RebotsPageName.Main:
                    rebotsPageUI.m_FaqPopularList.Clear();
                    rebotsPageUI.m_FaqCategoryList.Clear();
                    break;
                case RebotsPageName.InquiryList:
                    rebotsPageUI.m_InquiryList.Clear();
                    break;
                case RebotsPageName.FaqList:
                    rebotsPageUI.m_FaqList.Clear();
                    rebotsPageUI.m_SiblingCategoryList.Clear();
                    rebotsPageUI.m_LowerCategoryList.Clear();
                    rebotsPageUI.m_PagingContainer.Clear();
                    break;
                case RebotsPageName.Faq:
                    rebotsPageUI.m_FaqDetailContainer.Clear();
                    break;
                case RebotsPageName.SearchList:
                    rebotsPageUI.m_FaqSearchList.Clear();
                    rebotsPageUI.m_PagingContainer.Clear();
                    break;
                case RebotsPageName.TicketCreate:
                    rebotsPageUI.m_TicketFieldList.Clear();
                    break;
                case RebotsPageName.TicketList:
                    rebotsPageUI.m_TicketList.Clear();
                    break;
                case RebotsPageName.Ticket:
                    rebotsPageUI.m_TicketDetailList.Clear();
                    rebotsPageUI.m_TicketAttachmentContainer.Clear();
                    rebotsPageUI.m_TicketAnswerList.Clear();
                    break;
            }
        }
        #endregion

        #region Click Action
        public void MenuOpen()
        {
            rebotsLayoutUI.m_MenuContainer.style.left = -300;
            ShowPage(RebotsPageType.Layout, RebotsPageName.Menu);
            StartCoroutine(SlideIn(rebotsLayoutUI.m_MenuContainer));
        }

        public void LayoutClose()
        {
            if (openMenu)
            {
                StartCoroutine(SlideOut(rebotsLayoutUI.m_MenuContainer));
            }
            else
            {
                HidePage(RebotsPageType.Layout);
            }
        }

        public void ClickTopButton(RebotsPageName page)
        {
            if (page == RebotsPageName.Language && rebotsLayoutUI.m_LanguageContainer.style.display == DisplayStyle.None)
            {
                ShowPage(RebotsPageType.Layout, RebotsPageName.Language);
            }
            else if (page == RebotsPageName.Search && rebotsLayoutUI.m_SearchContainer.style.display == DisplayStyle.None)
            {
                ShowPage(RebotsPageType.Layout, RebotsPageName.Search);
            }
            else
            {
                HidePage(RebotsPageType.Layout);
            }
        }

        public void ClickLanguage(RebotsLanguageInfo language)
        {
            RebotsSpinnerShow.Invoke();
            rebotsSettingManager.SetApiCallFinished();

            rebotsSettingManager.HelpdeskInitialize(language.languageCode);

            StartCoroutine(LanguageInitialize());
        }

        public void ClickFaqCategory(Category category)
        {
            ShowFaqSubCategory(new RebotsPagingData<Category>(category));
        }

        public void ClickSearch()
        {
            var searchString = rebotsLayoutUI.m_SearchField.GetValue().Trim();
            if (string.IsNullOrEmpty(searchString))
            {
                return;
            }
            ShowSearch(new RebotsPagingData<string>(searchString));
        }

        public void ClickTicketSubmit(bool privacyValue, Category category)
        {
            bool checkValidation = true;
            var ticketInputFields = new DictionaryTicketInputFormData();
            var ticketAddFieldDic = new Dictionary<string, string>();
            RebotsTicketAttachment[] ticketAttachments = null;
            ticketInputFields.SelectedCategory = category;
            var fieldDic = FieldDictionary.ToArray();
            var fieldCount = fieldDic.Length;
            for (int i = 0; i < fieldCount; i++)
            {
                var item = fieldDic[i];
                var field = item.Key;
                if (field.fieldType == RebotsInputFieldType.Text || field.fieldType == RebotsInputFieldType.Textarea)
                {
                    var component = item.Value as RebotsTextFieldComponent;
                    if (!component.CheckFieldValid())
                    {
                        if (checkValidation)
                        {
                            verticalScrollValue = component.GetVerticalPsition();
                        }
                        checkValidation = false;
                        continue;
                    }
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
                    if (!component.CheckFieldValid())
                    {
                        if (checkValidation)
                        {
                            verticalScrollValue = component.GetVerticalPsition();
                        }
                        checkValidation = false;
                        continue;
                    }
                    var fieldValue = component.GetFieldValue();

                    ticketAddFieldDic.Add(field.text, fieldValue);
                }
                else if (field.fieldType == RebotsInputFieldType.Checkbox || field.fieldType == RebotsInputFieldType.Radiobutton)
                {
                    var component = item.Value as RebotsButtonGroupFieldComponent;
                    if (!component.CheckFieldValid())
                    {
                        if (checkValidation)
                        {
                            verticalScrollValue = component.GetVerticalPsition();
                        }
                        checkValidation = false;
                        continue;
                    }
                    var fieldValue = component.GetFieldValue();

                    ticketAddFieldDic.Add(field.text, fieldValue);
                }
                else if (field.fieldType == RebotsInputFieldType.File)
                {
                    var component = item.Value as RebotsAttachmentFieldComponent;
                    if (!component.CheckFieldValid())
                    {
                        if (checkValidation)
                        {
                            verticalScrollValue = component.GetVerticalPsition();
                        }
                        checkValidation = false;
                        continue;
                    }
                    ticketAttachments = component.GetFieldValue();
                }
            }

            if (!privacyValue)
            {
                if (!checkValidation)
                {
                    SetVerticalScroll();
                    return;
                }
                checkValidation = false;
                return;
            }

            if (checkValidation)
            {
                ticketInputFields.Data = ticketAddFieldDic;
                ticketInputFields.Language = rebotsSettingManager.localizationManager.language;
                if (ticketAttachments != null)
                {
                    foreach (var value in ticketAttachments)
                    {
                        ticketInputFields.AddAttachment(value.content as FileStream);
                    }
                }

                rebotsSettingManager.CreateTicket(rebotsPageUI.OnTicketCreate, ticketInputFields);
            }
            else
            {
                SetVerticalScroll();
            }
        }

        public void ClickTicketMenu(RebotsPageName name)
        {
            rebotsPageUI.m_TicketDetailButton.RemoveFromClassList(RebotsUIStaticString.RebotsTicketMenuSelect);
            rebotsPageUI.m_TicketAnswerButton.RemoveFromClassList(RebotsUIStaticString.RebotsTicketMenuSelect);
            if (name == RebotsPageName.Ticket)
            {
                rebotsPageUI.m_TicketDetailButton.AddToClassList(RebotsUIStaticString.RebotsTicketMenuSelect);

                ShowPage(RebotsPageType.NoTitleRoute, RebotsPageName.Ticket);
            }
            else
            {
                rebotsPageUI.m_TicketAnswerButton.AddToClassList(RebotsUIStaticString.RebotsTicketMenuSelect);

                ShowPage(RebotsPageType.NoTitleRoute, RebotsPageName.Answer);
            }
        }

        public void ClickMenuCategory(Button newMenuButton)
        {
            if (oldMenuButton != null)
            {
                oldMenuButton.RemoveFromClassList("rebots-menu__select");
            }
            newMenuButton.AddToClassList("rebots-menu__select");

            oldMenuButton = newMenuButton;
        }
        #endregion

        #region Page Recording Controller 
        private void AddPageState(RebotsPageType type, RebotsPageName name, object parameter = null)
        {
            PageRecords.Add(new RebotsPageRecord()
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
                if (PageRecords.Count <= 1)
                {
                    PageRecords = new List<RebotsPageRecord>();
                    ShowMain(false);
                    return;
                }
            }

            var CurrentPage = PageRecords[PageRecords.Count - 1];
            var ReloadPage = CurrentPage;
            if (isGoBack)
            {
                var BackPage = PageRecords[PageRecords.Count - 2];
                ReloadPage = BackPage;
                PageRecords.Remove(BackPage);
            }
            PageRecords.Remove(CurrentPage);

            switch (ReloadPage.PageName)
            {
                case RebotsPageName.Main:
                    ShowMain(false);
                    break;
                case RebotsPageName.InquiryList:
                    if (ReloadPage.PageType == RebotsPageType.NoTitleRoute)
                    {
                        ShowCsCategory();
                    }
                    else
                    {
                        ShowCsSubCategory(ReloadPage.Parameter as Category);
                    }
                    break;
                case RebotsPageName.FaqList:
                    ShowFaqSubCategory(new RebotsPagingData<Category>(ReloadPage.Parameter as Category));
                    break;
                case RebotsPageName.Faq:
                    ShowFaq(ReloadPage.Parameter as Faq);
                    break;
                case RebotsPageName.TicketCreate:
                    ShowTicketCreate(ReloadPage.Parameter as Category);
                    break;
                case RebotsPageName.TicketSuccess:
                    ShowTicketSuccess();
                    break;
                case RebotsPageName.SearchList:
                    ShowSearch(new RebotsPagingData<string>(ReloadPage.Parameter as string));
                    break;
                case RebotsPageName.TicketList:
                    ShowMyTicket();
                    break;
                case RebotsPageName.Ticket:
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

        #region (public) Page Vertical Scroll
        public void SaveVerticalScroll()
        {
            verticalScrollValue = rebotsLayoutUI.m_ScrollView.verticalScroller.value;
        }

        public void SetVerticalScroll()
        {
            StartCoroutine(ScrollCoroutine());
        }

        IEnumerator ScrollCoroutine()
        {
            yield return null;
            rebotsLayoutUI.m_ScrollView.schedule.Execute(() =>
            {
                rebotsLayoutUI.m_ScrollView.verticalScroller.value = verticalScrollValue;
            }).ExecuteLater(0);
        }
        #endregion

        #region (public) Get Helpdesk Resources
        public StyleSheet GetThemeStyleSheet(string theme)
        {
            switch (theme)
            {
                case RebotsUIStaticString.ThemeCode1:
                    return theme1;
                case RebotsUIStaticString.ThemeCode2:
                    return theme2;
                case RebotsUIStaticString.ThemeCode3:
                    return theme3;
                case RebotsUIStaticString.ThemeCode4:
                    return theme4;
                case RebotsUIStaticString.ThemeCode5:
                    return theme5;
                case RebotsUIStaticString.ThemeCode6:
                    return theme6;
                case RebotsUIStaticString.ThemeCode7:
                    return theme7;
                case RebotsUIStaticString.ThemeCode8:
                    return theme8;
                case RebotsUIStaticString.ThemeCode9:
                    return theme9;
                case RebotsUIStaticString.ThemeCode10:
                    return theme10;
                case RebotsUIStaticString.ThemeCode11:
                    return theme11;
                case RebotsUIStaticString.ThemeCode12:
                    return theme12;
                case RebotsUIStaticString.ThemeCode13:
                    return theme13;
                case RebotsUIStaticString.ThemeCode14:
                    return theme14;
                case RebotsUIStaticString.ThemeCode15:
                    return theme15;
                case RebotsUIStaticString.ThemeCode16:
                    return theme16;
                default:
                    return theme1;
            }
        }

        public Font? GetLanguageFontAsset()
        {
            switch (rebotsSettingManager.localizationManager.language.ToLower())
            {
                case "en":
                case "es":
                case "id":
                    return fontAssetEN;
                case "ja":
                    return fontAssetJP;
                case "zh-cn":
                case "zh-tw":
                    return fontAssetCN;
                case "th":
                    return fontAssetTH;
                case "ko":
                default:
                    return fontAssetKR;
            }
        }
        #endregion

        #region (public) Add FieldDictionary
        public void AddFieldDic(TicketCategoryInputField field, object fieldUIComponent)
        {
            FieldDictionary.Add(field, fieldUIComponent);
        }
        #endregion

        #region (public) Image Url to Texture2D 
        public void ImageUrlToTexture2D(UnityAction<Texture2D, string> callbackAction, Uri fileUrl, string externalLinkUrl)
        {
            rebotsSettingManager.LoadTexture(callbackAction, fileUrl, externalLinkUrl);
        }
        #endregion

        #region (public) Add space to word-wrap point
        public string AddSpaceToWordWrapPoint(string text, Rect availableSpace, GUIStyle labelStyle, int setLine = 1)
        {
            string resultStr = "";
            GUIContent content = new GUIContent(text);
            Vector2 contentSize = labelStyle.CalcSize(content);

            if (contentSize.x > availableSpace.width)
            {
                int currentLine = 1;
                float lineWidth = 0f;
                int breakPoint = -1;

                for (int i = 0; i < text.Length; i++)
                {
                    char c = text[i];
                    float charWidth = labelStyle.CalcSize(new GUIContent(c.ToString())).x;

                    if (lineWidth + charWidth <= availableSpace.width)
                    {
                        lineWidth += charWidth;
                    }
                    else
                    {
                        if (setLine == currentLine++)
                        {
                            //breakPoint = i - 1;
                            breakPoint = i;
                            break;
                        }
                        else
                        {
                            text = text.Insert((i - 1), " ");
                            lineWidth = 0f;
                        }
                    }
                }

                if (breakPoint >= 0)
                {
                    string wrappedText = text.Substring(0, (breakPoint - 2));
                    resultStr =  wrappedText;
                }
            }
            return resultStr;
        }
        #endregion

        #region (private) Menu Slide Snimation
        private IEnumerator SlideIn(VisualElement element)
        {
            yield return null;

            element.experimental.animation.Start(
                new StyleValues { left = 0 },
                400
            ).OnCompleted(() => { openMenu = true; });
        }

        private IEnumerator SlideOut(VisualElement element)
        {
            element.experimental.animation.Start(
                new StyleValues { left = -300 },
                400
            ).OnCompleted(() => {
                openMenu = false;
                HidePage(RebotsPageType.Layout);
            });

            yield return null;
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

        #region (public) Download And Save Attachment Image
        public void ClickAttachmentLink(string imageUrl, string fileName)
        {
            StartCoroutine(DownloadAndSaveImage(imageUrl, fileName));
        }

        IEnumerator DownloadAndSaveImage(string url, string fileName)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error downloading image: " + request.error);
            }
            else
            {
                Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                SaveTextureToFile(texture, fileName);
            }
        }

        private void SaveTextureToFile(Texture2D texture, string fileName)
        {
            byte[] bytes = texture.EncodeToPNG(); 

            string filePath = "";
#if UNITY_EDITOR || UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN || UNITY_WEBGL
            filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", fileName);
#endif

            try
            {
#if UNITY_IOS || UNITY_ANDROID
                NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(texture, "Helpdesk", fileName, (success, path) => Debug.Log("Media save result: " + success + " " + path));
                if (permission != NativeGallery.Permission.Granted)
                {
                    Debug.LogError("Failed to save image to gallery.");
                }
                Destroy(texture);
#else
                System.IO.File.WriteAllBytes(filePath, bytes);
                Debug.Log("Image saved to: " + filePath);
#endif
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to save image: " + e.Message);
            }
        }
#endregion
    }
}
