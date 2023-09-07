using HelpDesk.Sdk.Common.Objects;
using HelpDesk.Sdk.Common.Protocols.Responses;
using HelpDesk.Sdk.Library.Configuration;
using HelpDesk.Sdk.Unity.Library.Events;
using HelpDesk.Sdk.Unity.Library.Objects;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Rebots.HelpDesk
{
    /// <summary>
    /// Sample of Qroad RebotsPro HelpDesk SDK.
    /// 
    /// Main Initializer and SDK usage samples via MonoBehaviour Manager
    /// Objects. This object helps for HelpDesk project initialize,
    /// user initializize, get tickets and create ticket.
    /// </summary>
    public class RebotsSettingManager : MonoBehaviour
    {
        private bool projectInitialized;
        private string gameCustomerUuid;

        /// <summary>
        /// Public name is common string texts when you register our services.
        /// This name is public and not secured.
        /// 
        /// Already filled string is example of sample project.
        /// </summary>
        public string ProjectPublicName;

        /// <summary>
        /// Project main key is encrypted and used for main initialize REST API.
        /// You must fill specific key strings what you received from
        /// RebotsPro Web solutions(workspace).
        /// 
        /// Already filled string is example of sample project.
        /// </summary>
        public string ProjectMainKey;

        public RebotsLanguage HelpdeskLanguage;

        public TextAsset translationFile;

        [HideInInspector]
        public HelpdeskSetting helpdeskSetting { get; private set; }
        [HideInInspector]
        public PrivacySetting ticketPrivacySetting { get; private set; }

        [HideInInspector]
        public RebotsLocalizationManager localizationManager;

        private Configurations helpdeskConfig;
        private UnityWWWRestApiRequestEventBuilder helpdeskEvents;

        void Awake()
        {
            RebotsHelpdeskInitailize();
        }

        public void RebotsHelpdeskInitailize(string? publicName = "", string? mainKey = "", string? apiUri = "", string? apiStatisticsUri = "")
        {
            projectInitialized = false;

            localizationManager = new RebotsLocalizationManager(translationFile);

            if (!string.IsNullOrEmpty(publicName))
            {
                ProjectPublicName = publicName;
            }

            if (!string.IsNullOrEmpty(mainKey))
            {
                ProjectMainKey = mainKey;
            }

            // You can initialize with configuration builder class.
            var builder = new ConfigurationBuilder();

            builder
                /// This configuration is for REST API access URL but not neccesery.
                /// You can skip this settings.
                ///.UseApiUri("https://rp-api.rebotspro.com/")
                /// This configuration set main key. You must fill this. 
                /// If you don't it will casue <see cref="ArgumentNullException"/>.
                .UseProjectMainKey(ProjectMainKey)
                /// This configuration set project public name. You must fill this. 
                /// If you don't it will casue <see cref="ArgumentNullException"/>.
                .UseProjectPublicName(ProjectPublicName)
                /// This configuration set project default language. You must fill this.  
                /// If you don't it will casue <see cref="ArgumentNullException"/>.
                .UseProjecLanguage(HelpdeskLanguage);

            if (!string.IsNullOrEmpty(apiUri))
            {
                builder.UseApiUri(apiUri);
            }

            if (!string.IsNullOrEmpty(apiStatisticsUri))
            {
                builder.UseStatisticsApiUri(apiStatisticsUri);
            }

            /// configuration object can be constant.
            helpdeskConfig = new Configurations(builder);
            helpdeskConfig.SetJsonConverter(new NewtonJsonConverter());

            /// Event builders will manage callbacks during on HTTP transport.
            /// You can use indicator when start or ended, exception captured
            /// or HTTP Status errors caused. 
            /// (e.g: Bad Request(400) or Internal Server Error(500) and others.)
            /// Event callbacks are using <see cref="UnityEvent"/>.
            helpdeskEvents = new UnityWWWRestApiRequestEventBuilder();

            /// ---------------------------------------------------
            /// This events for HTTP transport start or finished.
            /// It used for indicator or logging.
            var onStartEvent = new UnityEvent();
            var onFinishedEvent = new UnityEvent();
            onStartEvent.AddListener(OnApiCallStart);
            onFinishedEvent.AddListener(OnApiCallFinished);
            /// ---------------------------------------------------

            /// ---------------------------------------------------
            /// This events for HTTP connection error caused.
            /// No have any informations or reason.
            var onConnectionError = new UnityEvent();
            /// ---------------------------------------------------

            /// ---------------------------------------------------
            /// This events for HTTP transport status error caused.
            /// It contains HTTP Status code(long) and message.
            /// (e.g: For error modal message)
            var onHttpError = new UnityEvent<long, string>();
            onHttpError.AddListener((status, message) =>
            {
                Debug.LogError($"HttpStatus : {status}");
                Debug.LogError($"Message : {message}");
            });
            /// ---------------------------------------------------

            /// ---------------------------------------------------
            /// This events for JSON Convert error if data is corrupted or invalid.
            var onJsonConvertError = new UnityEvent<Exception>();
            onJsonConvertError.AddListener((e) =>
            {
                Debug.LogError($"JsonConvert Failed : {e.Message}");
                Debug.LogError($"StackTrace : {e.StackTrace}");
            });
            /// ---------------------------------------------------

            helpdeskEvents
                .UseStart(onStartEvent)
                .UseFinished(onFinishedEvent)
                .UseConnectionError(onConnectionError)
                .UseJsonConvertError(onJsonConvertError)
                .UseHttpStatusError(onHttpError);

            Debug.Log("Helpdesk ProjectInitialize start.");
            HelpdeskInitialize();
        }

        #region API Call methods
        /// <summary>
        /// If you need to use HelpDesk SDK, you must call initializer methods.
        /// It will gain additional informations for using Qroad RebostProMax
        /// HelpDesk Services.
        /// </summary>
        public void HelpdeskInitialize(string? changeLanguage = "")
        {
            var evt = new UnityEvent<HelpdeskProjectInitializeResponse>();

            if (!string.IsNullOrEmpty(changeLanguage))
            {
                projectInitialized = false;
                localizationManager.SetLanguage(changeLanguage);
                evt.AddListener(OnLanguageInitializeSuccessed);
            }
            else
            {
                evt.AddListener(OnInitializeSuccessed);
            }

            /// You can use initialize REST API easily.
            /// Third parameter callback is using on successed to call initialize API call.
            var initializer = new UnityRebotsProMaxProjectInitializer(
                helpdeskConfig, helpdeskEvents, evt);

            /// If you call initialize method, it will trying REST API HTTP 
            /// transport execution. Exception or HTTP status error will be occured
            /// to event methods what you allocate to event builder.
            ///
            /// When it be successed to initialize, you can call user initialize.
            /// User initialize is used for game player information when your
            /// game login processes. (e.g: LoginScene, SocialLogin...)

            /// You need using coroutines for using API call.
            StartCoroutine(initializer.Initialize(changeLanguage));
        }

        /// <summary>
        ///  You can load event banners what you added from RebotsPro Web solutions(workspace).
        /// </summary>
        public void LoadEventBannerList(UnityAction<HelpdeskEventBannerResponses> listUpdate)
        {
            var evt = new UnityEvent<HelpdeskEventBannerResponses>();
            evt.AddListener(listUpdate);
            var req = new UnityRebotsProMaxGetEventBanners(helpdeskConfig, helpdeskEvents, evt);
            StartCoroutine(req.GetEventBanners());
        }

        /// <summary>
        ///  You can load faq cateogories what you added from RebotsPro Web solutions(workspace).
        /// </summary>
        public void LoadFaqCategoryList(UnityAction<HelpdeskFaqCategoriesResponse> listUpdate, bool? isRoot = false)
        {
            var evt = new UnityEvent<HelpdeskFaqCategoriesResponse>();
            evt.AddListener(listUpdate);
            var req = new UnityRebotsProMaxGetFaqCategories(helpdeskConfig, helpdeskEvents, evt);
            StartCoroutine(req.GetFaqCategories(isRoot, localizationManager.language));
        }

        /// <summary>
        ///  You can load faq cateogory what you added from RebotsPro Web solutions(workspace).
        /// </summary>
        public void LoadFaqCategory(UnityAction<HelpdeskFaqCategoryResponse> listUpdate, int categoryId)
        {
            var evt = new UnityEvent<HelpdeskFaqCategoryResponse>();
            evt.AddListener(listUpdate);
            var req = new UnityRebotsProMaxGetFaqCategory(helpdeskConfig, helpdeskEvents, evt);
            StartCoroutine(req.GetFaqCategory(categoryId));
        }

        /// <summary>
        ///  You can load recommend faq what you added from RebotsPro Web solutions(workspace).
        /// </summary>
        public void LoadFaqRecommendList(UnityAction<HelpdeskFaqListResponse> listUpdate)
        {
            var evt = new UnityEvent<HelpdeskFaqListResponse>();
            evt.AddListener(listUpdate);
            var req = new UnityRebotsProMaxGetFaqRecommendList(helpdeskConfig, helpdeskEvents, evt);
            StartCoroutine(req.GetRecommendFaqList(localizationManager.language));
        }

        /// <summary>
        ///  You can load recommend faq what you added from RebotsPro Web solutions(workspace).
        /// </summary>
        public void LoadFaqSearchList(UnityAction<HelpdeskFaqSearchResponse> listUpdate, string search, int page)
        {
            var evt = new UnityEvent<HelpdeskFaqSearchResponse>();
            evt.AddListener(listUpdate);
            var req = new UnityRebotsProMaxGetFaqSearchList(helpdeskConfig, helpdeskEvents, evt);
            StartCoroutine(req.GetFaqList(search, page, localizationManager.language));
        }

        /// <summary>
        /// You can load faqs what you added from RebotsPro Web solutions(workspace).
        /// </summary>
        public void LoadFaq(UnityAction<HelpdeskFaqResponse> listUpdate, int faqId)
        {
            var evt = new UnityEvent<HelpdeskFaqResponse>();
            evt.AddListener(listUpdate);
            var req = new UnityRebotsProMaxGetFaq(helpdeskConfig, helpdeskEvents, evt);
            StartCoroutine(req.GetFaq(faqId));
        }

        /// <summary>
        /// You can load cs cateogories what you added from RebotsPro Web solutions(workspace).
        /// </summary>
        public void LoadCsCategoryList(UnityAction<HelpdeskTicketCategoriesResponse> listUpdate, bool? isRoot = false)
        {
            var evt = new UnityEvent<HelpdeskTicketCategoriesResponse>();
            evt.AddListener(listUpdate);
            var req = new UnityRebotsProMaxGetTicketCagetories(helpdeskConfig, helpdeskEvents, evt);
            StartCoroutine(req.GetCategories(isRoot, localizationManager.language));
        }

        /// <summary>
        /// You can load cs cateogory what you added from RebotsPro Web solutions(workspace).
        /// </summary>
        public void LoadCsCategory(UnityAction<HelpdeskTicketCategoryResponse> listUpdate, int categoryId)
        {
            var evt = new UnityEvent<HelpdeskTicketCategoryResponse>();
            evt.AddListener(listUpdate);
            var req = new UnityRebotsProMaxGetTicketCagetory(helpdeskConfig, helpdeskEvents, evt);
            StartCoroutine(req.GetCategories(categoryId));
        }

        /// <summary>
        /// You can load fields that selected category which you have added from RebotsPro Web solutions(workspace).
        /// </summary>
        public void LoadCsCategoryFieldList(UnityAction<HelpdeskTicketCategoryField> listUpdate, int categoryId)
        {
            var evt = new UnityEvent<HelpdeskTicketCategoryField>();
            evt.AddListener(listUpdate);
            var req = new UnityRebotsProMaxGetTicketCategoryFields(helpdeskConfig, helpdeskEvents, evt);
            StartCoroutine(req.GetFields(categoryId));
        }

        /// <summary>
        /// You can send ticket to the RebotsPro Web solutions(workspace) you have added.
        /// </summary>
        public void CreateTicket(UnityAction<HelpDeskTicketCreateResponse> ticketCreate, TicketInputFormData ticketInputFields)
        {
            var evt = new UnityEvent<HelpDeskTicketCreateResponse>();
            evt.AddListener(ticketCreate);
            var req = new UnityRebotsProMaxCreateTicket(ticketInputFields, helpdeskConfig, helpdeskEvents, evt);
            StartCoroutine(req.CreateTicket());
        }

        /// <summary>
        /// You can load game player's tickets.
        /// </summary>
        public void LoadTicketList(UnityAction<HelpDeskTicketListResponse> listUpdate)
        {
            var evt = new UnityEvent<HelpDeskTicketListResponse>();
            evt.AddListener(listUpdate);
            var req = new UnityRebotsProMaxGetTicketList(helpdeskConfig, helpdeskEvents, evt);
            StartCoroutine(req.GetTickets());
        }

        /// <summary>
        /// You can load game player's ticket details.
        /// </summary>
        public void LoadTicketDetail(UnityAction<HelpDeskTicketDetailResponse> listUpdate, HelpdeskTicket ticket)
        {
            var evt = new UnityEvent<HelpDeskTicketDetailResponse>();
            evt.AddListener(listUpdate);
            var req = new UnityRebotsProMaxGetTicketDetail(ticket, helpdeskConfig, helpdeskEvents, evt);
            StartCoroutine(req.GetTickets());
        }

        public void LoadTexture(UnityAction<Texture2D, string> textureUpdate, Uri fileUrl, string externalLinkUrl)
        {
            var evt = new UnityEvent<Texture2D, string>();
            evt.AddListener(textureUpdate);
            var req = new UnityRebotsProMaxGetTexture(helpdeskConfig, helpdeskEvents, evt);
            StartCoroutine(req.GetTexture(fileUrl, externalLinkUrl));
        }
        #endregion

        #region User Initialize & Statistics Authorize
        /// <summary>
        /// When you successfully initialize, your next step is about
        /// your game player or customers. This action will create game player
        /// on your RebotsPro Web solutions(workspace) or get information current registered.
        /// 
        /// If API was called, it will gain game players UUID for get player's
        /// tickets or create it.
        /// </summary>
        public IEnumerator HelpdeskUserInitialize(
            string authKey,
            string email,
            string username)
        {
            int sec = 1;

            while (!projectInitialized) 
            {
                yield return new WaitForSeconds(1f);
                Debug.Log($"Waiting for Helpdesk Project initialize {sec} seconds.");
                if (++sec > 60) 
                {
                    Debug.LogWarning("Helpdesk Project is not initialized but timeout.");                                       
                    break;
                }
            }

            /// You just create API access class and call via coroutine.
            var user = new HelpdeskGameUser()
            {
                /// AuthKey is login id or key. 
                /// e.g) Google Auth Key, Apple Login Auth Key or your game's account name.
                AuthKey = authKey,
                /// Email is optional.
                Email = email,
                /// Displayed user name on RebotsPro Web solutions(workspace).
                Username = username,
                /// Player selected language. Must using which you received from 
                /// <see cref="HelpdeskUserInitializeResponse"/>
                Language = localizationManager.language
            };
            var evt = new UnityEvent<HelpdeskUserInitializeResponse>(); 
            evt.AddListener(OnUserInitializeSuccessed);
            var initializer = new UnityRebotsProMaxUserInitializer(
                user, helpdeskConfig, helpdeskEvents, evt);

            yield return initializer.Initialize();

            /// Required when using the 'Rebots Statistics'.
            if (helpdeskConfig.Helpdesk.Player.CustomerGuid != null && helpdeskConfig.Helpdesk.Player.CustomerGuid != Guid.Empty && !string.IsNullOrEmpty(helpdeskConfig.Helpdesk.Project.StatisticsKey))
            {
                yield return HelpdeskStatisticsAuthorize();
            }
        }

        /// <summary>
        /// Game client must authrorize statistics if you want collect Key-Value statistics data.
        /// This API call must called after user initialized.
        /// </summary>
        public IEnumerator HelpdeskStatisticsAuthorize()
        {
            /// This authorize require Project Statistics Key and Game Player's UUID where in Helpdesk Customer Data.
            /// Statistics key will loaded after <see cref="UnityRebotsProMaxProjectInitializer"/>.
            /// Customer UUID will loaded after <see cref="UnityRebotsProMaxUserInitializer"/>.
            Debug.Log("Start Helpdesk Statistics authroize.");
            var initializer = new UnityRebotsProMaxStatisticsAuth(helpdeskConfig, helpdeskEvents);
            yield return initializer.StatisticsAuthorize();
            RebotsStatisticsSingleton.Instance.Initialize(helpdeskConfig);
            Debug.Log("Helpdesk Statistics was authrized.");
        }
        #endregion

        #region Project and User Initialize Successed callback
        /// <summary>
        /// Response objects contains about Project information what you configured
        /// on RebotsPro Web solutions(workspace). <seealso cref="ProjectData"/>
        /// 
        /// Project Data contains CS Key and support languages.
        /// </summary>
        /// <param name="response">Helpdesk Initialize response.</param>
        void OnInitializeSuccessed(HelpdeskProjectInitializeResponse response)
        {
            var langs = response.data.languages;
            foreach (var item in langs)
            {
                localizationManager.SetAvailableLanguage(item.ToLower());
            }

            var languageCheck = localizationManager.CheckSupportedLanguage(HelpdeskLanguage);

            if (languageCheck)
            {
                Debug.Log("RebotsPro HelpDeskSdk was initialized.");
                Debug.Log($"Project CS Key : {response.data.csKey}");
                Debug.Log($"Project Supports language {string.Join(", ", response.data.languages)}.");

                helpdeskSetting = response.data.helpdeskSetting;
                ticketPrivacySetting = response.data.ticketPrivacySetting;
                localizationManager.SetLanguage(HelpdeskLanguage);

                localizationManager.ReadData();

                projectInitialized = true;
            }
            else
            {
                Debug.LogError("RebotsPro HelpDeskSdk was not initialized.");
                Debug.LogError($"Project Supports language {string.Join(", ", response.data.languages)}.");

                HelpdeskLanguage = RebotsLanguage.None;
                projectInitialized = false;
            }
        }

        /// <summary>
        /// Response objects contains about Project information what you configured
        /// on RebotsPro Web solutions(workspace). <seealso cref="ProjectData"/>
        /// 
        /// Project Data contains CS Key and support languages.
        /// </summary>
        /// <param name="response">Helpdesk Initialize response.</param>
        void OnLanguageInitializeSuccessed(HelpdeskProjectInitializeResponse response)
        {
            var languageCheck = localizationManager.CheckSupportedLanguage(HelpdeskLanguage);

            if (languageCheck)
            {
                Debug.Log("RebotsPro HelpDeskSdk was language initialized.");
                Debug.Log($"Project CS Key : {response.data.csKey}");
                Debug.Log($"Project Supports language {string.Join(", ", response.data.languages)}.");

                helpdeskSetting = response.data.helpdeskSetting;
                ticketPrivacySetting = response.data.ticketPrivacySetting;

                localizationManager.ReadData();

                projectInitialized = true;
            }
            else
            {
                Debug.LogError("RebotsPro HelpDeskSdk was not language initialized.");
                Debug.LogError($"Project Supports language {string.Join(", ", response.data.languages)}.");

                projectInitialized = false;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="response">Helpdesk UserInitialize response.</param>
        void OnUserInitializeSuccessed(HelpdeskUserInitializeResponse response)
        {
            Debug.Log("RebotsPro Game Player was initialized.");
            Debug.Log($"Player UUID : {response.data.customerUuid}");
            gameCustomerUuid = new string(response.data.customerUuid);
        }
        #endregion

        public void OnApiCallStart()
        {
            /// TODO : [UI Feature] 인디케이터 켜는 호출 삽입
            /// StartCoroutine(IndicatorOn);
            Debug.Log("RebotsPro HelpDeskSdk REST API call start.");
        }

        void OnApiCallFinished()
        {
            /// TODO : [UI Feature] 인디케이터 끄는 호출 삽입
            /// StartCoroutine(IndicatorOff);
            Debug.Log("RebotsPro HelpDeskSdk REST API call was finished.");
        }

        #region Initialize state
        public bool InitializeState()
        {
            return projectInitialized;
        }
        #endregion

        #region CustomCertificateHandler
        public class CustomCertificateHandler : CertificateHandler
        {
            protected override bool ValidateCertificate(byte[] certificateData)
            {
                return true;
            }
        }
        #endregion
    }
}