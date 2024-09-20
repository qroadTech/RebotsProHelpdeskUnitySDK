using UnityEngine;
using UnityEngine.UIElements;

namespace Rebots.HelpDesk
{
    public class RebotsSpinnerScreen : RebotsModalScreen
    {
        public const string LoadingSpinnerContainer = "rebots-loading-spinner-container";
        public const string LoadingSpinner = "rebots-loading-spinner";

        public VisualElement m_LoadingSpinnerContainer;
        public VisualElement m_LoadingSpinner;

        private VisualElement circularProgressFill;
        private float currentProgress = 0f;
        private bool progressFlag = false;
         
        void OnEnable()
        {
            base.ScreenStarted += StartScreen;
            base.ScreenEnded += EndScreen;
        }

        protected override void SetVisualElements()
        {
            base.SetVisualElements();

            m_LoadingSpinnerContainer = m_Root.Q(LoadingSpinnerContainer);
            m_LoadingSpinner = m_Root.Q(LoadingSpinner);
            circularProgressFill = m_Root.Q<VisualElement>(className: "circular-progress-fill");
        }

        public override void ShowScreen()
        {
            base.ShowScreen();
            m_Screen.BringToFront();
        }

        public void StartScreen()
        {
            circularProgressFill.style.rotate = new Rotate(new Angle(0f));
            currentProgress = 0f;
            progressFlag = false;
            StartProgressSimulation();
        }

        public void EndScreen()
        {
            progressFlag = true;
        }

        void UpdateCircularProgress()
        {
            circularProgressFill.style.rotate = new Rotate(new Angle(currentProgress * 360f));
        }

        void StartProgressSimulation()
        {
            InvokeRepeating(nameof(IncrementProgress), 0.6f, 0.6f);
        }

        void IncrementProgress()
        {
            if (progressFlag)
            {
                CancelInvoke(nameof(IncrementProgress));
                return;
            }

            currentProgress += 1f; 
            UpdateCircularProgress();
        }


    }
}