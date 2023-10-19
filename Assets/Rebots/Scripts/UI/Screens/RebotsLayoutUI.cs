using HelpDesk.Sdk.Common.Objects;
using HelpDesk.Sdk.Common.Protocols.Responses;
using System.Linq;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.Networking;
using System;

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
        public Button m_MenuOpenButton;
        public Button m_TopSearchButton;
        public Button m_TopLanguageButton;
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
        public Label m_MenuHelpdeskLabel;
        public Button m_MenuMainButton;
        public Label m_MainLabel;
        public Button m_MenuMyTicketButton;
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
        public VisualElement m_TermsBar;
        public Button m_CookieButton;
        public Label m_CookieLabel;
        public Button m_PrivacyButton;
        public Label m_PrivacyLabel;
        public VisualElement m_PrivacyBar;
        public Button m_OperatingButton;
        public Label m_OperatingLabel;
        public Label m_TelLabel;
        public VisualElement m_FooterCopyrightConatiner;
        public Label m_CopyrightLabel;
        #endregion

        public RebotsLocalizationManager LocalizationManager { get; private set; }

        #region Run in 'Awake' call
        protected override void SetVisualElements()
        {
            base.SetVisualElements();
            
            m_HelpdeskLayout = m_Root.Q(RebotsUIStaticString.HelpdeskLayout);
            m_ScrollView = m_HelpdeskLayout.Q<ScrollView>(RebotsUIStaticString.ScrollView);
            m_BackgroundContainer = m_HelpdeskLayout.Q(RebotsUIStaticString.BackgroundContainer);

            m_TopContainer = m_HelpdeskLayout.Q(RebotsUIStaticString.TopContainer);
            m_MenuOpenButton = m_TopContainer.Q<Button>(RebotsUIStaticString.MenuOpenButton);
            m_TopSearchButton = m_TopContainer.Q<Button>(RebotsUIStaticString.SearchButton);
            m_TopLanguageButton = m_TopContainer.Q<Button>(RebotsUIStaticString.LanguageButton);

            m_SearchContainer = m_HelpdeskLayout.Q(RebotsUIStaticString.SearchContainer);
            m_SearchCaption = m_SearchContainer.Q<Label>(RebotsUIStaticString.SearchCaption);
            m_SearchField = new RebotsTextField(m_SearchContainer.Q<TextField>(RebotsUIStaticString.SearchField));
            m_SearchInputButton = m_SearchContainer.Q<Button>(RebotsUIStaticString.SearchButton);

            m_LanguageContainer = m_HelpdeskLayout.Q(RebotsUIStaticString.LanguageContainer);
            m_LanguageList = m_LanguageContainer.Q(RebotsUIStaticString.LanguageList);

            m_MenuContainer = m_HelpdeskLayout.Q(RebotsUIStaticString.MenuContainer);
            m_MenuCloseButton = m_MenuContainer.Q<Button>(RebotsUIStaticString.MenuCloseButton);
            m_MenuHelpdeskLabel = m_MenuContainer.Q<Label>(RebotsUIStaticString.HelpdeskLabel);
            m_MenuMainButton = m_MenuContainer.Q<Button>(RebotsUIStaticString.MainButton);
            m_MainLabel = m_MenuContainer.Q<Label>(RebotsUIStaticString.MainLabel);
            m_MenuMyTicketButton = m_MenuContainer.Q<Button>(RebotsUIStaticString.MenuMyTicketButton);
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
            m_TermsBar = m_FooterContainer.Q(RebotsUIStaticString.TermsBar);
            m_CookieButton = m_FooterContainer.Q<Button>(RebotsUIStaticString.CookieButton);
            m_CookieLabel = m_FooterContainer.Q<Label>(RebotsUIStaticString.CookieLabel);
            m_PrivacyButton = m_FooterContainer.Q<Button>(RebotsUIStaticString.PrivacyButton);
            m_PrivacyLabel = m_FooterContainer.Q<Label>(RebotsUIStaticString.PrivacyLabel);
            m_PrivacyBar = m_FooterContainer.Q(RebotsUIStaticString.PrivacyBar);
            m_OperatingButton = m_FooterContainer.Q<Button>(RebotsUIStaticString.OperatingButton);
            m_OperatingLabel = m_FooterContainer.Q<Label>(RebotsUIStaticString.OperatingLabel);
            m_TelLabel = m_FooterContainer.Q<Label>(RebotsUIStaticString.TelLabel);
            m_FooterCopyrightConatiner = m_FooterContainer.Q(RebotsUIStaticString.FooterCopyrightConatiner);
            m_CopyrightLabel = m_FooterContainer.Q<Label>(RebotsUIStaticString.CopyrightLabel);
        }

        protected override void RegisterButtonCallbacks()
        {
            m_TopSearchButton?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.ShowPage(RebotsPageType.Layout, RebotsPageName.Search));
            m_TopLanguageButton?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.ShowPage(RebotsPageType.Layout, RebotsPageName.Language));
            m_MenuOpenButton?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.ShowPage(RebotsPageType.Layout, RebotsPageName.Menu));
            m_MenuCloseButton?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.HidePage(RebotsPageType.Layout));
            m_BackgroundContainer?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.HidePage(RebotsPageType.Layout));
            m_MenuMainButton?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.ShowMain(false));
            m_MenuMyTicketButton?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.ShowMyTicket());
            m_SearchInputButton?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.ClickSearch());
            m_ExitButton?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.ClosePanel());
            m_TicketButton?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.ShowCsCategory());
        }
        #endregion

        #region Set before show screen
        public void SetTranslationText()
        {
            LocalizationManager = helpdeskScreen.rebotsSettingManager.localizationManager;

            m_SearchCaption.text = LocalizationManager.translationDic[RebotsUIStaticString.SearchCaption];
            m_SearchField.UsePlaceholder(LocalizationManager.translationDic[RebotsUIStaticString.SearchPlaceholder]);
            m_SearchField.InitializeTextField();

            m_MainLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.MainLabel];
            m_MenuInquiryFoldout.text = LocalizationManager.translationDic[RebotsUIStaticString.InquiryLabel];
            m_ExitLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.ExitLabel];

            m_NeedMoreLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.NeedMoreLabel];
            m_SubmitTicketLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.SubmitTicketLabel];

            m_TermsLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.TermsLabel];
            m_CookieLabel.text = LocalizationManager.translationDic[RebotsUIStaticString.CookieLabel];
        }

        public void SetHelpdeskData(HelpdeskSetting helpdeskSetting)
        {
            m_MenuHelpdeskLabel.text = helpdeskSetting.helpdeskName;
            m_TitleHelpdeskLabel.text = helpdeskSetting.helpdeskName;

            m_HelpdeskLayout.styleSheets.Clear();
            m_HelpdeskLayout.styleSheets.Add(helpdeskScreen.GetThemeStyleSheet(helpdeskSetting.theme));
            m_HelpdeskLayout.styleSheets.Add(helpdeskScreen.GetLanguageStyleSheet(LocalizationManager.language));

            #region setting Main Image
            if (helpdeskSetting.useMainImage && helpdeskSetting.mainImageMobileUrl != null && !string.IsNullOrEmpty(helpdeskSetting.mainImageMobileUrl))
            {
                var imgUrl = helpdeskSetting.mainImageMobileUrl.Trim();
                helpdeskScreen.ImageUrlToTexture2D(OnTitleImageUpdated, new System.Uri(imgUrl), imgUrl);
            }
            else
            {
                m_TitleImageContainer.style.backgroundImage = null;
            }
            #endregion

            #region setting Footer
            if (helpdeskSetting.useOperatingTime && !string.IsNullOrEmpty(helpdeskSetting.operatingTime))
            {
                var operatingTime = helpdeskSetting.operatingTime.Split("/");
                var operatingTimeZone = helpdeskSetting.operatingTimezone.ToString();
                var operatingTimeStr = LocalizationManager.translationDic[RebotsUIStaticString.OperatingTimeLabel];
                m_OperatingTimeLabel.text = string.Format(operatingTimeStr, operatingTime[0].Trim(), operatingTime[1].Trim(), operatingTimeZone);
                ShowVisualElement(m_OperatingTimeLabel, true);
            }
            else
            {
                ShowVisualElement(m_OperatingTimeLabel, false);
            }

            if (helpdeskSetting.useTermsService && !string.IsNullOrEmpty(helpdeskSetting.termsServiceUrl))
            {
                m_TermsButton?.RegisterCallback<ClickEvent>(evt => Application.OpenURL(helpdeskSetting.termsServiceUrl));
                ShowVisualElement(m_TermsButton, true);
                ShowVisualElement(m_TermsBar, true);
            }
            else
            {
                ShowVisualElement(m_TermsButton, false);
                ShowVisualElement(m_TermsBar, false);
            }

            if (helpdeskSetting.useCookiePolicy && !string.IsNullOrEmpty(helpdeskSetting.cookiePolicyUrl))
            {
                m_CookieButton?.RegisterCallback<ClickEvent>(evt => Application.OpenURL(helpdeskSetting.cookiePolicyUrl));
                ShowVisualElement(m_CookieButton, true);
            }
            else
            {
                ShowVisualElement(m_CookieButton, false);
                ShowVisualElement(m_TermsBar, false);
            }

            if (helpdeskSetting.usePrivacyPolicyURL && !string.IsNullOrEmpty(helpdeskSetting.privacyPolicyURL))
            {
                m_PrivacyButton?.RegisterCallback<ClickEvent>(evt => Application.OpenURL(helpdeskSetting.privacyPolicyURL));
                ShowVisualElement(m_PrivacyButton, true);
                ShowVisualElement(m_PrivacyBar, true);
            }
            else
            {
                ShowVisualElement(m_PrivacyButton, false);
                ShowVisualElement(m_PrivacyBar, false);
            }

            helpdeskSetting.useOperatingPolicy = true;
            helpdeskSetting.operatingPolicyURL = "https://google.com/";
            if (helpdeskSetting.useOperatingPolicy && !string.IsNullOrEmpty(helpdeskSetting.operatingPolicyURL))
            {
                m_OperatingButton?.RegisterCallback<ClickEvent>(evt => Application.OpenURL(helpdeskSetting.operatingPolicyURL));
                ShowVisualElement(m_OperatingButton, true);
            }
            else
            {
                ShowVisualElement(m_OperatingButton, false);
                ShowVisualElement(m_PrivacyBar, false);
            }

            if (helpdeskSetting.useCallNumber && !string.IsNullOrEmpty(helpdeskSetting.callNumber))
            {
                var telStr = LocalizationManager.translationDic[RebotsUIStaticString.TelLabel];
                m_TelLabel.text = string.Format(telStr, helpdeskSetting.callNumber);
                ShowVisualElement(m_TelLabel, true);
            }
            else
            {
                ShowVisualElement(m_TelLabel, false);
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
    }
}
