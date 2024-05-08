using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;

namespace Rebots.HelpDesk
{
    [RequireComponent(typeof(UIDocument))]
    public class RebotsHelpdeskUIManager : MonoBehaviour
    {
        [Header("Screens")]
        [SerializeField] RebotsHelpdeskScreen rebotsHelpdeskScreen;
        [SerializeField] RebotsBannerScreen rebotsBannerScreen;

        [Header("Unity Editor Play (Option)")]
        [Tooltip("GameObject with Event System")]
        [SerializeField] GameObject? systemEventGO;

        List<RebotsModalScreen> AllModalScreens = new List<RebotsModalScreen>();

        UIDocument uiDocument;
        public UIDocument UIDocument => uiDocument;

        void OnEnable()
        {
            this.uiDocument = GetComponent<UIDocument>();

            SetupModalScreens();

            RebotsBannerScreen.rebotsBannerShow += RebotsBannerShow;
        }

        void OnDisable()
        {
            RebotsBannerScreen.rebotsBannerShow -= RebotsBannerShow;
        }

        void Start()
        {
            Time.timeScale = 1f;

            rebotsBannerScreen?.CheckEventBanner();
        }

        void SetupModalScreens()
        {
            if (rebotsHelpdeskScreen != null)
                AllModalScreens.Add(rebotsHelpdeskScreen);

            if (rebotsBannerScreen != null)
                AllModalScreens.Add(rebotsBannerScreen);
        }

        void ShowModalScreen(RebotsModalScreen modalScreen)
        {
            foreach (RebotsModalScreen m in AllModalScreens)
            {
                if (m == modalScreen)
                {
                    m?.ShowScreen();
                }
                else
                {
                    m?.HideScreen();
                }
            } 
        }

        public void PublicCallCheckEventBanner()
        {
            this.rebotsBannerScreen?.CheckEventBanner();
        }

        public void RebotsHelpdeskShow()
        {
            rebotsHelpdeskScreen.OriginScreenOrientation = Screen.orientation;
            rebotsHelpdeskScreen.SystemEventGO = systemEventGO;

            ShowModalScreen(rebotsHelpdeskScreen);
        }

        public void RebotsBannerShow()
        {
            ShowModalScreen(rebotsBannerScreen);
        }
    }
}
