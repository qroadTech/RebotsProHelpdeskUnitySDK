using HelpDesk.Sdk.Common.Objects;
using System;
using UnityEngine.UIElements;

namespace Rebots.HelpDesk
{
    public class RebotsLanguageComponent
    {
        const string LanguageButton = "rebots-language-button";
        const string LanguageLabel = "rebots-language-label";
        const string LanguageSelectedContainer = "rebots-language-check-container";

        Button m_LanguageButton;
        Label m_LanguageLabel;
        VisualElement m_LanguageSelectedContainer;

        private RebotsLanguageInfo languageInfo;
        private string lanuageText;

        public RebotsLanguageComponent(RebotsLanguageInfo settinglanguage, string lanuageText)
        {
            this.languageInfo = settinglanguage;
            this.lanuageText = lanuageText;
        }

        public void SetVisualElements(TemplateContainer languageUIElement)
        {
            if (languageUIElement == null)
            {
                return;
            }

            m_LanguageButton = languageUIElement.Q<Button>(LanguageButton);
            m_LanguageLabel = languageUIElement.Q<Label>(LanguageLabel);
            m_LanguageSelectedContainer = languageUIElement.Q(LanguageSelectedContainer);

            m_LanguageLabel.text = "";
        }

        public void SetLanguageData(TemplateContainer languageUIElement)
        {
            if (languageUIElement == null)
            {
                return;
            }

            m_LanguageLabel.text = lanuageText;

            if (languageInfo.isCurrent)
            {
                m_LanguageLabel.AddToClassList(RebotsUIStaticString.RebotsLanguageLabel_Black20);
                m_LanguageSelectedContainer.style.display = DisplayStyle.Flex;

                switch (languageInfo.languageValue)
                {
                    case RebotsLanguage.Korean:
                        m_LanguageLabel.AddToClassList(RebotsUIStaticString.RebotsFontKr_Black);
                        break;
                    case RebotsLanguage.Japanese:
                        m_LanguageLabel.AddToClassList(RebotsUIStaticString.RebotsFontJa_Black);
                        break;
                    case RebotsLanguage.SimplifiedChinese:
                        m_LanguageLabel.AddToClassList(RebotsUIStaticString.RebotsFontCn_Black);
                        break;
                    case RebotsLanguage.TraditionalChinese:
                        m_LanguageLabel.AddToClassList(RebotsUIStaticString.RebotsFontTw_Black);
                        break;
                    case RebotsLanguage.Thai:
                        m_LanguageLabel.AddToClassList(RebotsUIStaticString.RebotsFontTh_Black);
                        break;
                    case RebotsLanguage.None:
                    case RebotsLanguage.English:
                    case RebotsLanguage.Spanish:
                    case RebotsLanguage.Indonesian:
                    default:
                        m_LanguageLabel.AddToClassList(RebotsUIStaticString.RebotsFontEn_Black);
                        break;
                }
            }
            else
            {
                m_LanguageLabel.AddToClassList(RebotsUIStaticString.RebotsLanguageLabel_Bold20);
                m_LanguageSelectedContainer.style.display = DisplayStyle.None;

                switch (languageInfo.languageValue)
                {
                    case RebotsLanguage.Korean:
                        m_LanguageLabel.AddToClassList(RebotsUIStaticString.RebotsFontKr_Regular);
                        break;
                    case RebotsLanguage.Japanese:
                        m_LanguageLabel.AddToClassList(RebotsUIStaticString.RebotsFontJa_Regular);
                        break;
                    case RebotsLanguage.SimplifiedChinese:
                        m_LanguageLabel.AddToClassList(RebotsUIStaticString.RebotsFontCn_Regular);
                        break;
                    case RebotsLanguage.TraditionalChinese:
                        m_LanguageLabel.AddToClassList(RebotsUIStaticString.RebotsFontTw_Regular);
                        break;
                    case RebotsLanguage.Thai:
                        m_LanguageLabel.AddToClassList(RebotsUIStaticString.RebotsFontTh_Regular);
                        break;
                    case RebotsLanguage.None:
                    case RebotsLanguage.English:
                    case RebotsLanguage.Spanish:
                    case RebotsLanguage.Indonesian:
                    default:
                        m_LanguageLabel.AddToClassList(RebotsUIStaticString.RebotsFontEn_Regular);
                        break;
                }
            }
        }

        public void RegisterCallbacks(Action<RebotsLanguageInfo> action)
        {
            m_LanguageButton?.RegisterCallback<ClickEvent>(evt => action(languageInfo));
        }
    }
}
