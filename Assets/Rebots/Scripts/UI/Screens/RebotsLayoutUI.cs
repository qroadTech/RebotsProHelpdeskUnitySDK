using HelpDesk.Sdk.Common.Objects;
using HelpDesk.Sdk.Common.Protocols.Responses;
using System.Linq;
using UnityEngine.UIElements;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Rebots.HelpDesk
{
    public class RebotsLayoutUI : RebotsModalScreen
    {
        public RebotsHelpdeskScreen helpdeskScreen;

        #region - - - Layout UI Element - - - 
        public VisualElement m_HelpdeskLayout;
        public ScrollView m_ScrollView;
        public VisualElement m_BackgroundContainer;
        #endregion
        #region - - - Top Bar UI Element - - - 
        public VisualElement m_TopContainer;
        public Label m_HelpdeskPhrase;
        public Button m_MenuOpenButton;
        public Button m_TopSearchButton;
        public Button m_TopLanguageButton;
        public VisualElement m_TopExitContainer;
        public Button m_TopExitButton;
        #endregion
        #region - - - Search Bar UI Element - - - 
        public VisualElement m_SearchContainer;
        public Label m_SearchCaption;
        public RebotsTextField m_SearchField;
        public Button m_SearchInputButton;
        #endregion
        #region - - - Language Bar UI Element - - - 
        public VisualElement m_LanguageContainer;
        public VisualElement m_LanguageList;
        #endregion
        #region - - - Side Menu UI Element - - - 
        public VisualElement m_MenuContainer;
        public Button m_MenuCloseButton;
        public Button m_MenuMainButton;
        public Label m_MainLabel;
        public Button m_MenuMyTicketButton;
        public Label m_MyTicketLabel;
        public Foldout m_MenuFaqFoldout;
        public VisualElement m_MenuFaqCategoryList;
        public Foldout m_MenuInquiryFoldout;
        public VisualElement m_MenuCsCategoryList;
        public Button m_ExitButton;
        public Label m_ExitLabel;
        #endregion
        #region - - - Image Title UI Element - - - 
        public VisualElement m_MainTitleContainer;
        public VisualElement m_TitleImageContainer;
        public Label m_TitleHelpdeskLabel;
        #endregion
        #region - - - Ticket Button UI Element - - - 
        public VisualElement m_TicketButtonContainer;
        public Label m_NeedMoreLabel;
        public Label m_SubmitTicketLabel;
        public Button m_TicketButton;
        #endregion
        #region - - - Footer UI Element - - - 
        public VisualElement m_FooterContainer;
        public VisualElement m_FooterInfoContainer;
        public Label m_OperatingTimeLabel;
        public Button m_TermsButton;
        public Label m_TermsLabel;
        public Button m_CookieButton;
        public Label m_CookieLabel;
        public Button m_PrivacyButton;
        public Label m_PrivacyLabel;
        public Button m_OperatingButton;
        public Label m_OperatingLabel;
        public Label m_TelLabel;
        public VisualElement m_TermsBar;
        public VisualElement m_CookieBar;
        public VisualElement m_FooterLowConatiner;
        public VisualElement m_PrivacyBar;
        public VisualElement m_OperatingBar;
        public VisualElement m_TelContainer;
        public VisualElement m_TelBar;
        public VisualElement m_FooterCopyrightConatiner;
        public Label m_CopyrightLabel;
        #endregion

        public RebotsLocalizationManager LocalizationManager { get; private set; }

        private EventCallback<ClickEvent> termsButtonCallback;
        private EventCallback<ClickEvent> cookieButtonCallback;
        private EventCallback<ClickEvent> privacyButtonCallback;
        private EventCallback<ClickEvent> operatingButtonCallback;

        #region Run in 'Awake' call
        protected override void SetVisualElements()
        {
            base.SetVisualElements();
            
            m_HelpdeskLayout = m_Root.Q(RebotsUIStaticString.HelpdeskLayout);
            m_ScrollView = m_HelpdeskLayout.Q<ScrollView>(RebotsUIStaticString.ScrollView);
            m_BackgroundContainer = m_HelpdeskLayout.Q(RebotsUIStaticString.BackgroundContainer);

            m_TopContainer = m_HelpdeskLayout.Q(RebotsUIStaticString.TopContainer);
            m_HelpdeskPhrase = m_TopContainer.Q<Label>(RebotsUIStaticString.HelpdeskPhrase);
            m_MenuOpenButton = m_TopContainer.Q<Button>(RebotsUIStaticString.MenuOpenButton);
            m_TopSearchButton = m_TopContainer.Q<Button>(RebotsUIStaticString.SearchButton);
            m_TopLanguageButton = m_TopContainer.Q<Button>(RebotsUIStaticString.LanguageButton);
            m_TopExitContainer = m_TopContainer.Q(RebotsUIStaticString.TopExitContainer);
            m_TopExitButton = m_TopExitContainer.Q<Button>(RebotsUIStaticString.ExitButton);

            m_SearchContainer = m_HelpdeskLayout.Q(RebotsUIStaticString.SearchContainer);
            m_SearchCaption = m_SearchContainer.Q<Label>(RebotsUIStaticString.SearchCaption);
            m_SearchField = new RebotsTextField(m_SearchContainer.Q<TextField>(RebotsUIStaticString.SearchField));
            m_SearchInputButton = m_SearchContainer.Q<Button>(RebotsUIStaticString.SearchButton);

            m_LanguageContainer = m_HelpdeskLayout.Q(RebotsUIStaticString.LanguageContainer);
            m_LanguageList = m_LanguageContainer.Q(RebotsUIStaticString.LanguageList);

            m_MenuContainer = m_HelpdeskLayout.Q(RebotsUIStaticString.MenuContainer);
            m_MenuCloseButton = m_MenuContainer.Q<Button>(RebotsUIStaticString.MenuCloseButton);
            m_MenuMainButton = m_MenuContainer.Q<Button>(RebotsUIStaticString.MainButton);
            m_MainLabel = m_MenuContainer.Q<Label>(RebotsUIStaticString.MainLabel);
            m_MenuMyTicketButton = m_MenuContainer.Q<Button>(RebotsUIStaticString.MenuMyTicketButton);
            m_MyTicketLabel = m_MenuMyTicketButton.Q<Label>(RebotsUIStaticString.MyTicketLabel);
            m_MenuFaqFoldout = m_MenuContainer.Q<Foldout>(RebotsUIStaticString.MenuFaqFoldout);
            m_MenuFaqCategoryList = m_MenuFaqFoldout.Q(RebotsUIStaticString.List);
            m_MenuInquiryFoldout = m_MenuContainer.Q<Foldout>(RebotsUIStaticString.MenuInquiryFoldout);
            m_MenuCsCategoryList = m_MenuInquiryFoldout.Q(RebotsUIStaticString.List);
            m_ExitButton = m_MenuContainer.Q<Button>(RebotsUIStaticString.ExitButton);
            m_ExitLabel = m_MenuContainer.Q<Label>(RebotsUIStaticString.ExitLabel);

            m_MainTitleContainer = m_HelpdeskLayout.Q(RebotsUIStaticString.MainTitleContainer);
            m_TitleImageContainer = m_MainTitleContainer.Q(RebotsUIStaticString.TitleImageContainer);
            m_TitleHelpdeskLabel = m_MainTitleContainer.Q<Label>(RebotsUIStaticString.HelpdeskLabel);

            m_TicketButtonContainer = m_HelpdeskLayout.Q(RebotsUIStaticString.TicketButtonContainer);
            m_NeedMoreLabel = m_TicketButtonContainer.Q<Label>(RebotsUIStaticString.NeedMoreLabel);
            m_SubmitTicketLabel = m_TicketButtonContainer.Q<Label>(RebotsUIStaticString.SubmitTicketLabel);
            m_TicketButton = m_TicketButtonContainer.Q<Button>(RebotsUIStaticString.TicketButton);

            m_FooterContainer = m_HelpdeskLayout.Q(RebotsUIStaticString.FooterContainer);
            m_FooterInfoContainer = m_FooterContainer.Q(RebotsUIStaticString.FooterInfoContainer);
            m_OperatingTimeLabel = m_FooterContainer.Q<Label>(RebotsUIStaticString.OperatingTimeLabel);
            m_TermsButton = m_FooterContainer.Q<Button>(RebotsUIStaticString.TermsButton);
            m_TermsLabel = m_FooterContainer.Q<Label>(RebotsUIStaticString.TermsLabel);
            m_CookieButton = m_FooterContainer.Q<Button>(RebotsUIStaticString.CookieButton);
            m_CookieLabel = m_FooterContainer.Q<Label>(RebotsUIStaticString.CookieLabel);
            m_PrivacyButton = m_FooterContainer.Q<Button>(RebotsUIStaticString.PrivacyButton);
            m_PrivacyLabel = m_FooterContainer.Q<Label>(RebotsUIStaticString.PrivacyLabel);
            m_OperatingButton = m_FooterContainer.Q<Button>(RebotsUIStaticString.OperatingButton);
            m_OperatingLabel = m_FooterContainer.Q<Label>(RebotsUIStaticString.OperatingLabel);
            m_TelLabel = m_FooterContainer.Q<Label>(RebotsUIStaticString.TelLabel);
            m_TermsBar = m_FooterContainer.Q(RebotsUIStaticString.TermsBar);
            m_CookieBar = m_FooterContainer.Q(RebotsUIStaticString.CookieBar);
            m_FooterLowConatiner = m_FooterContainer.Q(RebotsUIStaticString.FooterLowConatiner);
            m_PrivacyBar = m_FooterContainer.Q(RebotsUIStaticString.PrivacyBar);
            m_OperatingBar = m_FooterContainer.Q(RebotsUIStaticString.OperatingBar);
            m_TelContainer = m_FooterContainer.Q(RebotsUIStaticString.TelContainer);
            m_TelBar = m_FooterContainer.Q(RebotsUIStaticString.TelBar);
            m_FooterCopyrightConatiner = m_FooterContainer.Q(RebotsUIStaticString.FooterCopyrightConatiner);
            m_CopyrightLabel = m_FooterContainer.Q<Label>(RebotsUIStaticString.CopyrightLabel);
        }

        protected override void RegisterButtonCallbacks()
        {
            m_TopSearchButton?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.ClickTopButton(RebotsPageName.Search));
            m_TopLanguageButton?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.ClickTopButton(RebotsPageName.Language));
            m_TopExitButton?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.ClosePanel());
            m_MenuOpenButton?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.MenuOpen());
            m_MenuCloseButton?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.LayoutClose());
            m_BackgroundContainer?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.LayoutClose());
            m_MenuMainButton?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.ShowMain(false));
            m_MenuMyTicketButton?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.ShowMyTicket());
            m_SearchField.KeyDownEvent(OnEnterKeyDown);
            m_SearchInputButton?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.ClickSearch());
            m_ExitButton?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.ClosePanel());
            m_TicketButton?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.ShowCsCategory());
        }
        #endregion

        #region Set before show screen
        public void SetTranslationText()
        {
            LocalizationManager = helpdeskScreen.rebotsSettingManager.localizationManager;

            m_HelpdeskPhrase.text = LocalizationManager.translationDic[RebotsUIStaticString.HelpdeskPhrase];

            m_SearchCaption.text = LocalizationManager.translationDic[RebotsUIStaticString.SearchCaption];
            m_SearchField.UsePlaceholder(LocalizationManager.translationDic[RebotsUIStaticString.SearchPlaceholder]);
            m_SearchField.InitializeTextField();

            m_MainLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.MainLabel];
            m_MyTicketLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.MyTicketLabel];
            m_MenuFaqFoldout.text = LocalizationManager.translationDic[RebotsUIStaticString.FaqPhrase];
            m_MenuInquiryFoldout.text = LocalizationManager.translationDic[RebotsUIStaticString.InquiryPhrase];
            m_ExitLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.ExitLabel];

            m_NeedMoreLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.NeedMoreLabel];
            m_SubmitTicketLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.SubmitTicketLabel];

            m_TermsLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.TermsLabel];
            m_CookieLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.CookieLabel];
            m_PrivacyLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.PrivacyLabel];
            m_OperatingLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.OperatingLabel];
        }

        public void SetHelpdeskData(HelpdeskSetting helpdeskSetting)
        {
            m_TitleHelpdeskLabel.text = helpdeskSetting.helpdeskName;

            m_HelpdeskLayout.ClearClassList();
            string langClass = LocalizationManager.GetCurrentLanguageFont();
            m_HelpdeskLayout.AddToClassList(langClass);

            m_HelpdeskLayout.styleSheets.Clear();
            m_HelpdeskLayout.styleSheets.Add(helpdeskScreen.GetThemeStyleSheet(helpdeskSetting.theme));

            #region setting Top Exit Button
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN || UNITY_WEBGL
            ShowVisualElement(m_TopExitContainer, true);
#else
            if (helpdeskScreen.ScreenPortrait)
            {
                ShowVisualElement(m_TopExitContainer, false);
            }
            else
            {
                ShowVisualElement(m_TopExitContainer, true);
            }
#endif
            #endregion

            #region setting Main Image
            if (helpdeskSetting.useMainImage && helpdeskSetting.mainImageMobileUrl != null && !string.IsNullOrEmpty(helpdeskSetting.mainImageMobileUrl))
            {
                m_MainTitleContainer.AddToClassList("rebots-background-color__black");
                var imgUrl = helpdeskSetting.mainImageMobileUrl.Trim();
                helpdeskScreen.ImageUrlToTexture2D(OnTitleImageUpdated, new System.Uri(imgUrl), imgUrl);
            }
            else
            {
                m_MainTitleContainer.AddToClassList("rebots-background-color__theme");
                m_TitleImageContainer.style.backgroundImage = null;
            }
            #endregion

            #region setting Footer
            bool isRow = true;
            string bydaySeperator = " ";
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN || UNITY_WEBGL
            m_FooterInfoContainer.style.flexDirection = FlexDirection.Row;
            m_FooterLowConatiner.style.flexDirection = FlexDirection.Row;
            m_FooterLowConatiner.style.flexGrow = 0;
            m_TelContainer.style.flexGrow = 0;
#else
            if (helpdeskScreen.ScreenPortrait)
            {
                isRow = false;
                bydaySeperator = "\n";
                m_FooterInfoContainer.style.flexDirection = FlexDirection.Column;
                m_FooterLowConatiner.style.flexDirection = FlexDirection.Column;
                m_FooterLowConatiner.style.flexGrow = 1;
                m_TelContainer.style.flexGrow = 1;
            }
            else
            {
                m_FooterInfoContainer.style.flexDirection = FlexDirection.Row;
                m_FooterLowConatiner.style.flexDirection = FlexDirection.Row;
                m_FooterLowConatiner.style.flexGrow = 0;
                m_TelContainer.style.flexGrow = 0;
            }
#endif

            if (helpdeskSetting.useOperatingTime && !string.IsNullOrEmpty(helpdeskSetting.operatingTime))
            {
                var operatingTime = helpdeskSetting.operatingTime.Split("/");
                var operatingTimeZone = helpdeskSetting.operatingTimezone.ToString();
                var operatingTimeTrans = LocalizationManager.translationDic[RebotsUIStaticString.OperatingTimeLabel];

                var operatingDayTrans = " ";
                switch (helpdeskSetting.operatingData.type.ToLower())
                {
                    case "weekday":
                        operatingDayTrans = operatingDayTrans + LocalizationManager.translationDic[RebotsUIStaticString.OperatingTimeWeekday];
                        break;
                    case "always":
                        operatingDayTrans = operatingDayTrans + LocalizationManager.translationDic[RebotsUIStaticString.OperatingTimeAlways];
                        break;
                    case "byday":
                        List<string> strs = new List<string>();
                        if (helpdeskSetting.operatingData.mon)
                            strs.Add(LocalizationManager.translationDic[RebotsUIStaticString.OperatingTimeMon]);
                        if (helpdeskSetting.operatingData.tue)
                            strs.Add(LocalizationManager.translationDic[RebotsUIStaticString.OperatingTimeTue]);
                        if (helpdeskSetting.operatingData.wed)
                            strs.Add(LocalizationManager.translationDic[RebotsUIStaticString.OperatingTimeWed]);
                        if (helpdeskSetting.operatingData.thu)
                            strs.Add(LocalizationManager.translationDic[RebotsUIStaticString.OperatingTimeThu]);
                        if (helpdeskSetting.operatingData.fri)
                            strs.Add(LocalizationManager.translationDic[RebotsUIStaticString.OperatingTimeFri]);
                        if (helpdeskSetting.operatingData.sat)
                            strs.Add(LocalizationManager.translationDic[RebotsUIStaticString.OperatingTimeSat]);
                        if (helpdeskSetting.operatingData.sun)
                            strs.Add(LocalizationManager.translationDic[RebotsUIStaticString.OperatingTimeSun]);

                        operatingDayTrans = bydaySeperator + string.Join(", ", strs);
                        break;
                    default:
                        break;
                }

                var operatingTimeText = string.Format(operatingTimeTrans, operatingTime[0].Trim(), operatingTime[1].Trim(), operatingTimeZone) + operatingDayTrans;
                m_OperatingTimeLabel.text = operatingTimeText;
            }
            else
            {
                ShowVisualElement(m_OperatingTimeLabel, false);
            }

            if (helpdeskSetting.useTermsService && !string.IsNullOrEmpty(helpdeskSetting.termsServiceUrl))
            {
                m_TermsButton?.UnregisterCallback(termsButtonCallback);
                termsButtonCallback = evt => OpenURL(helpdeskSetting.termsServiceUrl);
                m_TermsButton?.RegisterCallback(termsButtonCallback);
                ShowVisualElement(m_TermsButton, true);
                ShowVisualElement(m_TermsBar, isRow);
            }
            else
            {
                ShowVisualElement(m_TermsButton, false);
                ShowVisualElement(m_TermsBar, false);
            }

            if (helpdeskSetting.useCookiePolicy && !string.IsNullOrEmpty(helpdeskSetting.cookiePolicyUrl))
            {
                m_CookieButton?.UnregisterCallback(cookieButtonCallback);
                cookieButtonCallback = evt => OpenURL(helpdeskSetting.cookiePolicyUrl);
                m_CookieButton?.RegisterCallback(cookieButtonCallback);
                ShowVisualElement(m_CookieButton, true);
                ShowVisualElement(m_CookieBar, helpdeskSetting.useTermsService ? true : isRow);
            }
            else
            {
                ShowVisualElement(m_CookieButton, false);
                ShowVisualElement(m_CookieBar, false);
            }

            if (helpdeskSetting.usePrivacyPolicyURL && !string.IsNullOrEmpty(helpdeskSetting.privacyPolicyURL))
            {
                m_PrivacyButton?.UnregisterCallback(privacyButtonCallback);
                privacyButtonCallback = evt => OpenURL(helpdeskSetting.privacyPolicyURL);
                m_PrivacyButton?.RegisterCallback(privacyButtonCallback);
                ShowVisualElement(m_PrivacyButton, true);
                ShowVisualElement(m_PrivacyBar, isRow);
            }
            else
            {
                ShowVisualElement(m_PrivacyButton, false);
                ShowVisualElement(m_PrivacyBar, false);
            }

            if (helpdeskSetting.useOperatingPolicy && !string.IsNullOrEmpty(helpdeskSetting.operatingPolicyURL))
            {
                m_OperatingButton?.UnregisterCallback(operatingButtonCallback);
                operatingButtonCallback = evt => OpenURL(helpdeskSetting.operatingPolicyURL);
                m_OperatingButton?.RegisterCallback(operatingButtonCallback);
                ShowVisualElement(m_OperatingButton, true);
                ShowVisualElement(m_OperatingBar, helpdeskSetting.usePrivacyPolicyURL ? true : isRow);
            }
            else
            {
                ShowVisualElement(m_OperatingButton, false);
                ShowVisualElement(m_OperatingBar, false);
            }

            if (helpdeskSetting.useCallNumber && !string.IsNullOrEmpty(helpdeskSetting.callNumber))
            {
                var telStr = LocalizationManager.translationDic[RebotsUIStaticString.TelLabel];
                m_TelLabel.text = string.Format(telStr, helpdeskSetting.callNumber);
                ShowVisualElement(m_TelLabel, true);
                ShowVisualElement(m_TelBar, isRow);
            }
            else
            {
                ShowVisualElement(m_TelLabel, false);
                ShowVisualElement(m_TelBar, false);
            }

            if (helpdeskSetting.useCopyright && !string.IsNullOrEmpty(helpdeskSetting.copyrightText))
            {
                var telStr = LocalizationManager.translationDic[RebotsUIStaticString.CopyrightLabel];
                m_CopyrightLabel.text = string.Format(telStr, helpdeskSetting.copyrightText);
                ShowVisualElement(m_FooterCopyrightConatiner, true);
            }
            else
            {
                ShowVisualElement(m_FooterCopyrightConatiner, false);
            }
            #endregion
        }

        public void SetLanguageUI()
        {
            var settingLanguages = LocalizationManager.settingLanguages.OrderBy(x => x.index).ToArray();
            m_LanguageList.Clear();

            foreach (var item in settingLanguages)
            {
                var lanuageText = LocalizationManager.translationDic[string.Format(RebotsUIStaticString.LanguageLabel, item.languageCode.ToLower())];

                TemplateContainer languageUIElement = null;
                helpdeskScreen.rebotsUICreater.CreateLanguage(item, lanuageText, helpdeskScreen.ClickLanguage, out languageUIElement);
                m_LanguageList.Add(languageUIElement);
            }
        }
        #endregion
        
        #region Set callback data after API
        public void OnTitleImageUpdated(Texture2D texture, string externalLinkUri)
        {
            if (texture != null)
            {
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2));

                if (texture.width > texture.height)
                {
                    m_TitleImageContainer.style.unityBackgroundScaleMode = ScaleMode.ScaleAndCrop;
                }
                else
                {
                    m_TitleImageContainer.style.unityBackgroundScaleMode = ScaleMode.ScaleToFit;
                }
                m_TitleImageContainer.style.backgroundImage = new StyleBackground(sprite);
            }
        }

        public void OnFaqMenuUpdated(HelpdeskFaqCategoriesResponse response)
        {
            var faqCategories = response.items;
            var count = faqCategories.Count();
            if (faqCategories != null && count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    var item = faqCategories[i];
                    TemplateContainer menuUIElement = null;
                    helpdeskScreen.rebotsUICreater
                        .CreateCategory<Category>(item, RebotsCategoryAssetType.Menu, helpdeskScreen.ClickFaqCategory, out menuUIElement);
                    m_MenuFaqCategoryList.Add(menuUIElement);
                }
            }                     
            m_MenuFaqFoldout.value = false;
        }
         
        public void OnCsMenuUpdated(HelpdeskTicketCategoriesResponse response)
        {
            var csCategories = response.items;
            var count = csCategories.Count();
            if (csCategories != null && count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    var item = csCategories[i];
                    TemplateContainer menuUIElement = null;
                    Action<Category> clickAction = (item.childFieldCount > 0) ? helpdeskScreen.ShowCsSubCategory : helpdeskScreen.ShowTicketCreate;
                    helpdeskScreen.rebotsUICreater
                        .CreateCategory<Category>(item, RebotsCategoryAssetType.Menu, clickAction, out menuUIElement);
                    m_MenuCsCategoryList.Add(menuUIElement);
                }
            }
            m_MenuInquiryFoldout.value = false;
        }
        #endregion

        #region On Key Event
        public void OnEnterKeyDown(KeyDownEvent evt)
        {
            if (m_SearchContainer.style.display == DisplayStyle.Flex)
            {
                if (evt.keyCode == KeyCode.Return || evt.keyCode == KeyCode.KeypadEnter)
                {
                    helpdeskScreen.ClickSearch();
                }
            }
        }
        #endregion

        #region 
        public void OpenURL(string url)
        {
            Application.OpenURL(url);
        }
        #endregion
    }
}
