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

        List<RebotsModalScreen> m_AllModalScreens = new List<RebotsModalScreen>();

        UIDocument m_UIDocument;
        public UIDocument UIDocument => m_UIDocument;

        void OnEnable()
        {
            m_UIDocument = GetComponent<UIDocument>();

            SetupModalScreens();
        }

        void OnDisable()
        {
        }

        void Start()
        {
            Time.timeScale = 1f;
        }

        void SetupModalScreens()
        {
            if (m_RebotsHelpdeskScreen != null)
                m_AllModalScreens.Add(m_RebotsHelpdeskScreen);
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

        public void RebotsHelpdeskShow()
        {
            ShowModalScreen(m_RebotsHelpdeskScreen);
        }
    }
}
