using HelpDesk.Sdk.Common.Objects.Enums;
using HelpDesk.Sdk.Common.Objects;
using HelpDesk.Sdk.Common.Protocols.Responses;
using System.Linq;
using UnityEngine.UIElements;
using UnityEngine;

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
        public Foldout m_MenuCsFoldout;
        public VisualElement m_MenuCsCategoryList;
        public Button m_ExitButton;
        public Label m_ExitLabel;
        #endregion
        #region - - - Image Title UI Element - - - 
        public VisualElement m_TitleContainer;
        public Label m_TitleHelpdeskLabel;
        #endregion
        #region - - - Ticket Button UI Element - - - 
        public VisualElement m_TicketButtonContainer;
        public Label m_NeedMoreLabel;
        public Label m_SubmitTicketLabel;
        public Label m_SendUsLabel;
        public Button m_TicketButton;
        #endregion
        #region - - - Footer UI Element - - - 
        public VisualElement m_FooterContainer;
        public VisualElement m_FooterInfoContainer;
        public Label m_OperatingTimeLabel;
        public VisualElement m_OperatingBar;
        public Button m_TermsButton;
        public Label m_TermsLabel;
        public Button m_CookieButton;
        public Label m_CookieLabel;
        public VisualElement m_CookieBar;
        public Label m_TelLabel;
        public VisualElement m_FooterCopyrightConatiner;
        public Label m_CopyrightLabel;
        #endregion

        private RebotsLocalizationManager localizationManager;

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
            m_MenuFaqCategoryList = m_MenuFaqFoldout.Q(RebotsUIStaticString.FaqCategoryList);
            m_MenuCsFoldout = m_MenuContainer.Q<Foldout>(RebotsUIStaticString.MenuCsFoldout);
            m_MenuCsCategoryList = m_MenuCsFoldout.Q(RebotsUIStaticString.CsCategoryList);
            m_ExitButton = m_MenuContainer.Q<Button>(RebotsUIStaticString.ExitButton);
            m_ExitLabel = m_MenuContainer.Q<Label>(RebotsUIStaticString.ExitLabel);

            m_TitleContainer = m_HelpdeskLayout.Q(RebotsUIStaticString.TitleContainer);
            m_TitleHelpdeskLabel = m_TitleContainer.Q<Label>(RebotsUIStaticString.HelpdeskLabel);

            m_TicketButtonContainer = m_HelpdeskLayout.Q(RebotsUIStaticString.TicketButtonContainer);
            m_NeedMoreLabel = m_TicketButtonContainer.Q<Label>(RebotsUIStaticString.NeedMoreLabel);
            m_SubmitTicketLabel = m_TicketButtonContainer.Q<Label>(RebotsUIStaticString.SubmitTicketLabel);
            m_SendUsLabel = m_TicketButtonContainer.Q<Label>(RebotsUIStaticString.SendUsLabel);
            m_TicketButton = m_TicketButtonContainer.Q<Button>(RebotsUIStaticString.TicketButton);

            m_FooterContainer = m_HelpdeskLayout.Q(RebotsUIStaticString.FooterContainer);
            m_FooterInfoContainer = m_FooterContainer.Q(RebotsUIStaticString.FooterInfoContainer);
            m_OperatingTimeLabel = m_FooterContainer.Q<Label>(RebotsUIStaticString.OperatingTimeLabel);
            m_OperatingBar = m_FooterContainer.Q(RebotsUIStaticString.OperatingBar);
            m_TermsButton = m_FooterContainer.Q<Button>(RebotsUIStaticString.TermsButton);
            m_TermsLabel = m_FooterContainer.Q<Label>(RebotsUIStaticString.TermsLabel);
            m_CookieButton = m_FooterContainer.Q<Button>(RebotsUIStaticString.CookieButton);
            m_CookieLabel = m_FooterContainer.Q<Label>(RebotsUIStaticString.CookieLabel);
            m_CookieBar = m_FooterContainer.Q(RebotsUIStaticString.CookieBar);
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
            m_TicketButton?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.ClickTicketCreate());
        }
        #endregion

        #region Run in 'Start' call
        public void SetTranslationText()
        {
            localizationManager = helpdeskScreen.rebotsSettingManager.localizationManager;

            m_SearchCaption.text = localizationManager.translationDic[RebotsUIStaticString.SearchCaption];
            m_SearchField.UsePlaceholder(localizationManager.translationDic[RebotsUIStaticString.SearchPlaceholder]);
            m_SearchField.InitializeTextField();

            m_MainLabel.text = localizationManager.translationDic[RebotsUIStaticString.MainLabel];
            m_MenuCsFoldout.text = localizationManager.translationDic[RebotsUIStaticString.InquiryLabel];
            m_ExitLabel.text = localizationManager.translationDic[RebotsUIStaticString.ExitLabel];

            m_NeedMoreLabel.text = localizationManager.translationDic[RebotsUIStaticString.NeedMoreLabel];
            m_SubmitTicketLabel.text = localizationManager.translationDic[RebotsUIStaticString.SubmitTicketLabel];
            m_SendUsLabel.text = localizationManager.translationDic[RebotsUIStaticString.SendUsLabel];

            m_TermsLabel.text = localizationManager.translationDic[RebotsUIStaticString.TermsLabel];
            m_CookieLabel.text = localizationManager.translationDic[RebotsUIStaticString.CookieLabel];
        }
        #endregion

        public void SetHelpdeskData(HelpdeskSetting helpdeskSetting)
        {
            m_MenuHelpdeskLabel.text = helpdeskSetting.helpdeskName;
            m_TitleHelpdeskLabel.text = helpdeskSetting.helpdeskName;

            m_HelpdeskLayout.styleSheets.Clear();

            switch (helpdeskSetting.theme)
            {
                case RebotsUIStaticString.ThemeCode1:
                    m_HelpdeskLayout.styleSheets.Add(helpdeskScreen.theme1);
                    break;
                case RebotsUIStaticString.ThemeCode2:
                    m_HelpdeskLayout.styleSheets.Add(helpdeskScreen.theme2);
                    break;
                case RebotsUIStaticString.ThemeCode3:
                    m_HelpdeskLayout.styleSheets.Add(helpdeskScreen.theme3);
                    break;
                case RebotsUIStaticString.ThemeCode4:
                    m_HelpdeskLayout.styleSheets.Add(helpdeskScreen.theme4);
                    break;
                case RebotsUIStaticString.ThemeCode5:
                    m_HelpdeskLayout.styleSheets.Add(helpdeskScreen.theme5);
                    break;
                case RebotsUIStaticString.ThemeCode6:
                    m_HelpdeskLayout.styleSheets.Add(helpdeskScreen.theme6);
                    break;
                case RebotsUIStaticString.ThemeCode7:
                    m_HelpdeskLayout.styleSheets.Add(helpdeskScreen.theme7);
                    break;
                case RebotsUIStaticString.ThemeCode8:
                    m_HelpdeskLayout.styleSheets.Add(helpdeskScreen.theme8);
                    break;
                case RebotsUIStaticString.ThemeCode9:
                    m_HelpdeskLayout.styleSheets.Add(helpdeskScreen.theme9);
                    break;
                case RebotsUIStaticString.ThemeCode10:
                    m_HelpdeskLayout.styleSheets.Add(helpdeskScreen.theme10);
                    break;
                case RebotsUIStaticString.ThemeCode11:
                    m_HelpdeskLayout.styleSheets.Add(helpdeskScreen.theme11);
                    break;
                case RebotsUIStaticString.ThemeCode12:
                    m_HelpdeskLayout.styleSheets.Add(helpdeskScreen.theme12);
                    break;
                case RebotsUIStaticString.ThemeCode13:
                    m_HelpdeskLayout.styleSheets.Add(helpdeskScreen.theme13);
                    break;
                case RebotsUIStaticString.ThemeCode14:
                    m_HelpdeskLayout.styleSheets.Add(helpdeskScreen.theme14);
                    break;
                case RebotsUIStaticString.ThemeCode15:
                    m_HelpdeskLayout.styleSheets.Add(helpdeskScreen.theme15);
                    break;
                case RebotsUIStaticString.ThemeCode16:
                    m_HelpdeskLayout.styleSheets.Add(helpdeskScreen.theme16);
                    break;
                default:
                    m_HelpdeskLayout.styleSheets.Add(helpdeskScreen.theme1);
                    break;
            }

            switch (localizationManager.language)
            {
                case "ko":
                    m_HelpdeskLayout.styleSheets.Add(helpdeskScreen.fontKr);
                    break;
                case "ja":
                    m_HelpdeskLayout.styleSheets.Add(helpdeskScreen.fontJa);
                    break;
                case "zh-cn":
                    m_HelpdeskLayout.styleSheets.Add(helpdeskScreen.fontCn);
                    break;
                case "zh-tw":
                    m_HelpdeskLayout.styleSheets.Add(helpdeskScreen.fontTw);
                    break;
                case "th":
                    m_HelpdeskLayout.styleSheets.Add(helpdeskScreen.fontTh);
                    break;
                default:
                    m_HelpdeskLayout.styleSheets.Add(helpdeskScreen.fontEn);
                    break;
            }

            if (helpdeskSetting.useOperatingTime)
            {
                var operatingTime = helpdeskSetting.operatingTime.Split("/");
                var operatingTimeStr = localizationManager.translationDic[RebotsUIStaticString.OperatingTimeLabel];
                m_OperatingTimeLabel.text = string.Format(operatingTimeStr, operatingTime[0], operatingTime[1]);
                ShowVisualElement(m_OperatingTimeLabel, true);
                ShowVisualElement(m_OperatingBar, true);
            }
            else
            {
                ShowVisualElement(m_OperatingTimeLabel, false);
                ShowVisualElement(m_OperatingBar, false);
            }

            if (helpdeskSetting.useTermsService)
            {
                m_TermsButton?.RegisterCallback<ClickEvent>(evt => Application.OpenURL(helpdeskSetting.termsServiceUrl));
                ShowVisualElement(m_TermsButton, true);
            }
            else
            {
                ShowVisualElement(m_TermsButton, false);
                ShowVisualElement(m_OperatingBar, false);
            }

            if (helpdeskSetting.useCookiePolicy)
            {
                m_CookieButton?.RegisterCallback<ClickEvent>(evt => Application.OpenURL(helpdeskSetting.cookiePolicyUrl));
                ShowVisualElement(m_CookieButton, true);
                ShowVisualElement(m_CookieBar, true);
            }
            else
            {
                ShowVisualElement(m_CookieButton, false);
                ShowVisualElement(m_CookieBar, false);
            }

            if (helpdeskSetting.useCallNumber)
            {
                var telStr = localizationManager.translationDic[RebotsUIStaticString.TelLabel];
                m_TelLabel.text = string.Format(telStr, helpdeskSetting.callNumber);
                ShowVisualElement(m_TelLabel, true);
            }
            else
            {
                ShowVisualElement(m_TelLabel, false);
                ShowVisualElement(m_CookieBar, false);
            }
        }

        public void SetLanguageUI()
        {
            var settingLanguages = localizationManager.settingLanguages.OrderBy(x => x.index).ToList();
            m_LanguageList.Clear();

            foreach (var item in settingLanguages)
            {
                var lanuageText = localizationManager.translationDic[string.Format(RebotsUIStaticString.LanguageLabel, item.languageCode.ToLower())];

                TemplateContainer languageUIElement = null;
                helpdeskScreen.rebotsUICreater.CreateLanguage(item, lanuageText, helpdeskScreen.ClickLanguage, out languageUIElement);
                m_LanguageList.Add(languageUIElement);
            }            
        }

        public void SetTicketButtonAction(Category category = null)
        {
            m_TicketButton?.RegisterCallback<ClickEvent>(evt => helpdeskScreen.ClickTicketCreate(category));
        }

        #region Layout Data API Callback
        public void OnFaqMenuUpdated(HelpdeskFaqCategoriesResponse response)
        {
            var faqCategories = response.items;
            if (faqCategories != null && faqCategories.Count() > 0)
            {
                foreach (var item in faqCategories)
                {
                    TemplateContainer menuUIElement = null;
                    helpdeskScreen.rebotsUICreater
                        .CreateCategory<Category>(item, RebotsCategoryAssetType.menu, helpdeskScreen.ClickFaqCategory, out menuUIElement);
                    m_MenuFaqCategoryList.Add(menuUIElement);
                }
            }                     
            m_MenuFaqFoldout.value = false;
        }
         
        public void OnCsMenuUpdated(HelpdeskTicketCategoriesResponse response)
        {
            var csCategories = response.items;
            if (csCategories != null && csCategories.Count() > 0)
            {
                foreach (var item in csCategories)
                {
                    TemplateContainer menuUIElement = null;
                    helpdeskScreen.rebotsUICreater
                        .CreateCategory<Category>(item, RebotsCategoryAssetType.menu, helpdeskScreen.ClickCsCategory, out menuUIElement);
                    m_MenuCsCategoryList.Add(menuUIElement);
                }
            }
            m_MenuCsFoldout.value = false;
        }
        #endregion
    }
}
