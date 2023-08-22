using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Rebots.HelpDesk
{
    public class RebotsUIStaticString
    {
        #region - - - Layout UI name - - - 
        public const string HelpdeskScreen = "rebots-helpdesk-screen";
        public const string HelpdeskLayout = "rebots-helpdesk-layout";
        public const string ScrollView = "rebots-scrollview";
        public const string BackgroundContainer = "rebots-background";
        #endregion
        #region - - - Common UI name - - -
        public const string HelpdeskLabel = "rebots-helpdesk-label";
        public const string TicketButton = "rebots-ticket-button";
        public const string FaqCategoryList = "rebots-faq-category-list-container";
        public const string FaqList = "rebots-faq-list-container";
        public const string CsCategoryList = "rebots-cs-category-list-container";
        public const string CategoryLabel = "rebots-category-label";
        public const string RouteLabel = "rebots-route-label";
        public const string MainButton = "rebots-main-button";
        
        public const string FaqWasHelpfulLabel = "rebots-it-was-helpful";
        public const string ViewMoreLabel = "rebots-view-more";
        #endregion
        #region - - - Top Bar UI name - - - 
        public const string TopContainer = "rebots-top-container";
        public const string MenuOpenButton = "rebots-menu-open-button";
        public const string SearchButton = "rebots-search-button";
        public const string LanguageButton = "rebots-language-button";
        #endregion
        #region - - - Search Bar UI name - - - 
        public const string SearchContainer = "rebots-search-container";
        public const string SearchCaption = "rebots-search-caption";
        public const string SearchField = "rebots-search-field";
        #endregion
        #region - - - Language Bar UI name - - - 
        public const string LanguageContainer = "rebots-language-container";
        public const string LanguageList = "rebots-language-list-container";
        #endregion
        #region - - - Side Menu UI name - - - 
        public const string MenuContainer = "rebots-menu-container";
        public const string MenuCloseButton = "rebots-menu-close-button";
        public const string MainLabel = "rebots-main";
        public const string MenuMyTicketButton = "rebots-my-ticket-button";
        public const string MenuFaqFoldout = "rebots-menu-faq-foldout";
        public const string MenuCsFoldout = "rebots-menu-cs-foldout";
        public const string InquiryLabel = "rebots-inquiry";
        public const string ExitButton = "rebots-exit-button";
        public const string ExitLabel = "rebots-exit";
        #endregion
        #region - - - Image Title UI name - - - 
        public const string TitleContainer = "rebots-title-container";
        #endregion
        #region - - - Page UI name - - - 
        public const string PageConatiner = "rebots-page-container";

        public const string PopularFaqContainer = "rebots-popular-faq-container";
        public const string PopularFaqCation = "rebots-top-faq";

        public const string SearchFaqContainer = "rebots-search-faq-container";
        public const string SearchResultLabel = "rebots-search-result";
        public const string SearchStringLabel = "rebots-search-string-label";

        public const string FaqCategoryContainer = "rebots-faq-category-container";
        public const string LowerCategoryContainer = "rebots-lower-category-container";

        public const string CsCategoryContainer = "rebots-cs-category-container";

        public const string RouteContainer = "rebots-route-container";
        public const string BackButton = "rebots-back-button";
        public const string RouteLabelContainer = "rebots-route-label-container";

        public const string MenuNameContainer = "rebots-menu-name-container";
        public const string MenuLabel = "rebots-menu-label";

        public const string SiblingCategoryContainer = "rebots-sibling-category-container";
        public const string SiblingCategoryList = "rebots-sibling-category-list-container";

        public const string TitleCategoryConatiner = "rebots-title-category-container";

        public const string SubCategoryContainer = "rebots-sub-category-container";
        public const string SubCategoryList = "rebots-sub-category-list-container";

        public const string FaqContainer = "rebots-faq-container";
        public const string FaqDetailContainer = "rebots-faq-detail-container";
        public const string FaqHelpfulLabel = "rebots-was-it-helpful";
        public const string HelpfulYesLabel = "rebots-yes";
        public const string HelpfulNoLabel = "rebots-no";

        public const string TicketCreateContainer = "rebots-ticket-create-container";
        public const string TicketFieldList = "rebots-field-list-container";
        public const string RequiredLabel = "rebots-required";
        public const string AgreeCheckLabel = "rebots-i-agree";
        public const string TicketSubmitButton = "rebots-submit-button";
        public const string TicketSubmitLabel = "rebots-submit";

        public const string TicketSuccessContainer = "rebots-ticket-success-container";
        public const string TicketReceivedLabel = "rebots-been-received";
        public const string TicketReplyLabel = "rebots-will-reply";
        public const string TicketThankYouLabel = "rebots-thank-you";
        public const string TicketReturnMainLabel = "rebots-return-main";

        public const string MyTicketContainer = "rebots-my-ticket-container";
        public const string TicketList = "rebots-ticket-list-container";

        public const string TicketContainer = "rebots-ticket-container";
        public const string TicketDetailList = "rebots-ticket-detail-list-container";
        public const string TicketContentsLabel = "rebots-ticket-contents-label";
        public const string TicketAnswerList = "rebots-ticket-answer-list-container";
        #endregion
        #region - - - Ticket Button UI name - - - 
        public const string TicketButtonContainer = "rebots-ticket-button-container";
        public const string NeedMoreLabel = "rebots-need-more";
        public const string SubmitTicketLabel = "rebots-submit-ticket";
        public const string SendUsLabel = "rebots-send-us";
        #endregion
        #region - - - Footer UI name - - - 
        public const string FooterContainer = "rebots-footer-container";
        public const string FooterInfoContainer = "rebots-info-container";
        public const string OperatingTimeLabel = "rebots-operating-time";
        public const string OperatingBar = "rebots-operating-bar";
        public const string TermsButton = "rebots-terms-button";
        public const string TermsLabel = "rebots-terms";
        public const string CookieButton = "rebots-cookie-button";
        public const string CookieLabel = "rebots-cookie";
        public const string CookieBar = "rebots-cookie-bar";
        public const string TelLabel = "rebots-tel";
        public const string FooterCopyrightConatiner = "rebots-copyright-container";
        public const string CopyrightLabel = "rebots-copyright";
        #endregion

        #region - - - Localization Key String - - - 
        public const string LanguageLabel = "rebots-{0}-label";
        public const string SearchPlaceholder = "rebots-search-placeholder";
        public const string NoFileLabel = "rebots-no-file";
        public const string PrivacyFieldLabel = "rebots-privacy-policy";
        public const string PrivacyPrpose = "rebots-privacy-purpose";
        public const string PrivacyCollection = "rebots-privacy-collection";
        public const string PrivacyPeriod = "rebots-privacy-period";
        public const string PrivacyConsignment = "rebots-privacy-consignment";
        public const string PrivacyProviding = "rebots-privacy-providing";
        public const string PrivacyConsignmentPeriod = "rebots-privacy-consignment-period";
        #endregion

        #region - - - uss Class Name - - - 
        public const string RebotsLanguageLabel_Black20 = "rebots-language-label__black20";
        public const string RebotsLanguageLabel_Bold20 = "rebots-language-label__bold20";
        public const string RebotsFontKr_Black = "rebots-font-kr__black";
        public const string RebotsFontJa_Black = "rebots-font-ja__black";
        public const string RebotsFontCn_Black = "rebots-font-cn__black";
        public const string RebotsFontTw_Black = "rebots-font-tw__black";
        public const string RebotsFontTh_Black = "rebots-font-th__black";
        public const string RebotsFontEn_Black = "rebots-font-en__black";
        public const string RebotsFontKr_Regular = "rebots-font-kr__regular";
        public const string RebotsFontJa_Regular = "rebots-font-ja__regular";
        public const string RebotsFontCn_Regular = "rebots-font-cn__regular";
        public const string RebotsFontTw_Regular = "rebots-font-tw__regular";
        public const string RebotsFontTh_Regular = "rebots-font-th__regular";
        public const string RebotsFontEn_Regular = "rebots-font-en__regular";
        public const string RebotsBackgroundColor_Theme = "rebots-background-color__theme";
        public const string RebotsBackgroundColor_Grey = "rebots-background-color__grey";
        public const string RebotsLabel_Regular16 = "rebots-label__regular16";
        public const string RebotsFontColor_Black = "rebots-font-color__black";
        public const string RebotsFontColor_Grey = "rebots-font-color__grey";
        public const string RebotsFontColor_Theme = "rebots-font-color__theme";
        #endregion

        #region - - - Theme Code String - - - 
        public const string ThemeCode1 = "#343A40";
        public const string ThemeCode2 = "#DC3545";
        public const string ThemeCode3 = "#FD7E14";
        public const string ThemeCode4 = "#FFC107";
        public const string ThemeCode5= "#28A746";
        public const string ThemeCode6 = "#20C997";
        public const string ThemeCode7 = "#4DBFD1";
        public const string ThemeCode8 = "#E83E8C";
        public const string ThemeCode9 = "#8E8E8E";
        public const string ThemeCode10 = "#8A3C43";
        public const string ThemeCode11 = "#B77233";
        public const string ThemeCode12 = "#AA9900";
        public const string ThemeCode13 = "#5F8B43";
        public const string ThemeCode14 = "#3E8EBB";
        public const string ThemeCode15 = "#38518F";
        public const string ThemeCode16 = "#8C32A3";
        #endregion
    }
}
