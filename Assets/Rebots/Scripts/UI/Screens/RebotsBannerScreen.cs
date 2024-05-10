using System.Linq;
using UnityEngine.UIElements;
using UnityEngine;
using HelpDesk.Sdk.Common.Protocols.Responses;
using System;

namespace Rebots.HelpDesk
{
    public class RebotsBannerScreen : RebotsModalScreen
    {
        public static event Action rebotsBannerShow;

        [Header("Rebots Manager")]
        public RebotsSettingManager rebotsSettingManager;

        [Header("Rebots Banner Asset")]
        [SerializeField] VisualTreeAsset BannerAsset;

        const string k_BannerScrollView = "rebots-banner-scrollview";
        const string k_BannerList = "unity-content-container";
        const string k_BannerCloseButton = "rebots-banner-close-button";
        const string k_BannerImgContainer = "rebots-banner-img-container";

        ScrollView m_BannerScrollView;
        VisualElement m_BannerList;
        Button m_BannerCloseButton;

        private static int bannerCount = 0;

        void OnEnable()
        {
        }

        void OnDisable()
        {
        }

        protected override void SetVisualElements()
        {
            base.SetVisualElements();

            m_BannerScrollView = m_Root.Q<ScrollView>(k_BannerScrollView);
            m_BannerList = m_BannerScrollView.Q(k_BannerList);
            m_BannerCloseButton = m_Root.Q<Button>(k_BannerCloseButton);
        }

        protected override void RegisterButtonCallbacks()
        {
            m_BannerCloseButton?.RegisterCallback<ClickEvent>(evt => ClosePanel());
        }

        public override void ShowScreen()
        {
            base.ShowScreen();
            m_Screen.BringToFront();
        }

        void ClosePanel()
        {
            HideScreen();
        }

        public void CheckEventBanner()
        {
            rebotsSettingManager.LoadEventBannerList(OnBannersUpdated);
        }

        public void OnBannersUpdated(HelpdeskEventBannerResponses response)
        {
            var banners = response.banners;
            bannerCount = banners.Count();

            if (banners != null && bannerCount > 0)
            {
                m_BannerList.Clear();
                for (int i = 0; i < bannerCount; i++)
                {
                    var item = banners[i];
                    ImageUrlToTexture2D(new Uri(item.fileUrl), item.externalLinkUrl);
                }
            }
            else
            {
                ClosePanel();
            }
        }

        private void ImageUrlToTexture2D(Uri fileUrl, string externalLinkUrl)
        {
            rebotsSettingManager.LoadTexture(SetBanner, fileUrl, externalLinkUrl);
        }

        private void SetBanner(Texture2D texture, string externalLinkUri)
        {
            if (texture != null && !string.IsNullOrEmpty(externalLinkUri))
            {
                TemplateContainer bannerUIElement = BannerAsset.Instantiate();

                var m_BannerImgContainer = bannerUIElement.Q(k_BannerImgContainer);
                m_BannerImgContainer?.RegisterCallback<ClickEvent>(evt => ClikckBanner(externalLinkUri));

                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2));
                m_BannerImgContainer.style.backgroundImage = new StyleBackground(sprite);

                bannerUIElement.style.width = Length.Percent(100);
                bannerUIElement.style.flexShrink = 0;
                bannerUIElement.style.flexGrow = 1;

                m_BannerList.Add(bannerUIElement);
            }

            if (m_BannerList.childCount == bannerCount)
            {
                rebotsBannerShow.Invoke();
            }
        }

        private void ClikckBanner(string url)
        {
            Application.OpenURL(url);
        }
    }
}
