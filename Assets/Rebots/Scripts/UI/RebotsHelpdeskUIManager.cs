using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;

namespace Rebots.HelpDesk
{
    [RequireComponent(typeof(UIDocument))]
    public class RebotsHelpdeskUIManager : MonoBehaviour
    {
        [Header("Screens")]
        [SerializeField] RebotsHelpdeskScreen m_RebotsHelpdeskScreen;
        [SerializeField] RebotsBannerScreen m_RebotsBannerScreen;

        List<RebotsModalScreen> m_AllModalScreens = new List<RebotsModalScreen>();

        UIDocument m_UIDocument;
        public UIDocument UIDocument => m_UIDocument;

        void OnEnable()
        {
            m_UIDocument = GetComponent<UIDocument>();

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

            m_RebotsBannerScreen?.CheckEventBanner();
        }

        void SetupModalScreens()
        {
            if (m_RebotsHelpdeskScreen != null)
                m_AllModalScreens.Add(m_RebotsHelpdeskScreen);

            if (m_RebotsBannerScreen != null)
                m_AllModalScreens.Add(m_RebotsBannerScreen);
        }

        void ShowModalScreen(RebotsModalScreen modalScreen)
        {
            foreach (RebotsModalScreen m in m_AllModalScreens)
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
            this.m_RebotsBannerScreen?.CheckEventBanner();
        }

        public void RebotsHelpdeskShow()
        {
            ShowModalScreen(m_RebotsHelpdeskScreen);
        }

        public void RebotsBannerShow()
        {
            ShowModalScreen(m_RebotsBannerScreen);
        }
    }
}
