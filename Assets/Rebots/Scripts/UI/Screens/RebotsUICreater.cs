using HelpDesk.Sdk.Common.Objects;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using HelpDesk.Sdk.Common.Protocols.Responses;

namespace Rebots.HelpDesk
{
    public class RebotsUICreater : MonoBehaviour
    {
        [Header("Rebots Helpdesk Asset")]
        [SerializeField] VisualTreeAsset AttachmentFieldAsset;
        [SerializeField] VisualTreeAsset AttachmentFileAsset;
        [SerializeField] VisualTreeAsset ButtonGroupFieldAsset;
        [SerializeField] VisualTreeAsset CheckAsset;
        [SerializeField] VisualTreeAsset ContentCategoryAsset;
        [SerializeField] VisualTreeAsset CsCategoryAsset;
        [SerializeField] VisualTreeAsset DropdownFieldAsset;
        [SerializeField] VisualTreeAsset FaqCategoryAsset;
        [SerializeField] VisualTreeAsset LanguageAsset;
        [SerializeField] VisualTreeAsset MenuCategoryAsset;
        [SerializeField] VisualTreeAsset PopularFaqAsset;
        [SerializeField] VisualTreeAsset PrivacyFieldAsset;
        [SerializeField] VisualTreeAsset RadioAsset;
        [SerializeField] VisualTreeAsset RouteLabelAsset;
        [SerializeField] VisualTreeAsset SearchFaqAsset;
        [SerializeField] VisualTreeAsset SelectedCategoryAsset;
        [SerializeField] VisualTreeAsset SiblingCategoryAsset;
        [SerializeField] VisualTreeAsset SubCategoryAsset;
        [SerializeField] VisualTreeAsset TextareaFieldAsset;
        [SerializeField] VisualTreeAsset TextFieldAsset;
        [SerializeField] VisualTreeAsset TicketAsset;
        [SerializeField] VisualTreeAsset TicketAnswerAsset;
        [SerializeField] VisualTreeAsset TicketFieldAsset;

        public void CreateLanguage(RebotsLanguageInfo settinglanguage, string lanuageText, Action<RebotsLanguageInfo> clickAction, out TemplateContainer uiElement)
        {
            TemplateContainer languageUIElement = LanguageAsset.Instantiate();

            RebotsLanguageComponent languageComponent = new RebotsLanguageComponent(settinglanguage, lanuageText);
            languageComponent.SetVisualElements(languageUIElement);
            languageComponent.SetLanguageData(languageUIElement);
            languageComponent.RegisterCallbacks(clickAction);

            uiElement = languageUIElement;
        }

        public void CreateRouteLabel(string categoryName, bool isColor, out TemplateContainer uiElement)
        {
            TemplateContainer routeLabelUIElement = RouteLabelAsset.Instantiate();
            Label routeLabel = routeLabelUIElement.Q<Label>(RebotsUIStaticString.RouteLabel);
            routeLabel.text = categoryName;
            if (isColor)
            {
                routeLabel.AddToClassList(RebotsUIStaticString.RebotsFontColor_Theme);
            }

            uiElement = routeLabelUIElement;
        }

        public void CreateCategory<T>(T Category, RebotsCategoryAssetType type, Action<T> clickAction, out TemplateContainer uiElement)
        {
            VisualTreeAsset asset = FaqCategoryAsset;
            switch (type)
            {
                case RebotsCategoryAssetType.cs:
                    asset = CsCategoryAsset;
                    break;
                case RebotsCategoryAssetType.faq:
                    asset = FaqCategoryAsset;
                    break;
                case RebotsCategoryAssetType.menu:
                    asset = MenuCategoryAsset;
                    break;
                case RebotsCategoryAssetType.sibling:
                    asset = SiblingCategoryAsset;
                    break;
                case RebotsCategoryAssetType.selected:
                    asset = SelectedCategoryAsset;
                    break;
                case RebotsCategoryAssetType.sub:
                    asset = SubCategoryAsset;
                    break;
                case RebotsCategoryAssetType.contents:
                    asset = ContentCategoryAsset;
                    break;
            }
            TemplateContainer categoryUIElement = asset.Instantiate();

            RebotsCategoryComponent<T> categoryComponent = new RebotsCategoryComponent<T>(Category);

            categoryComponent.SetVisualElements(categoryUIElement);
            categoryComponent.SetCategoryData(categoryUIElement);
            categoryComponent.RegisterCallbacks(clickAction);

            if (type == RebotsCategoryAssetType.sibling || type == RebotsCategoryAssetType.selected)
            {
                categoryUIElement.style.flexGrow = 1;
                categoryUIElement.style.flexShrink = 0;
                uiElement = categoryUIElement;
            }
            else
            {
                uiElement = categoryUIElement;
            }
        }

        public void CreateFaq(Faq faq, RebotsFaqAssetType type, string? search, Action<Faq> clickAction, out TemplateContainer uiElement)
        {
            VisualTreeAsset asset = PopularFaqAsset;
            switch (type)
            {
                case RebotsFaqAssetType.popular:
                    asset = PopularFaqAsset;
                    break;
                case RebotsFaqAssetType.search:
                    asset = SearchFaqAsset;
                    break;
            }

            TemplateContainer faqUIElement = asset.Instantiate();

            RebotsFaqComponent faqComponent = new RebotsFaqComponent(faq, type, search);

            faqComponent.SetVisualElements(faqUIElement);
            faqComponent.SetFaqData(faqUIElement);
            faqComponent.RegisterCallbacks(clickAction);

            uiElement = faqUIElement;
        }

        public void CreateCsCategoryField(TicketCategoryInputField csCategoryField, string parameterValue, out TemplateContainer uiElement, out object uiComponent)
        {
            if (csCategoryField.fieldType == RebotsInputFieldType.text || csCategoryField.fieldType == RebotsInputFieldType.textarea)
            {
                VisualTreeAsset asset = TextFieldAsset;

                if (csCategoryField.fieldType == RebotsInputFieldType.textarea)
                {
                    asset = TextareaFieldAsset;
                }

                TemplateContainer textFieldUIElement = asset.Instantiate();

                RebotsTextFieldComponent textFieldComponent = new RebotsTextFieldComponent(csCategoryField, parameterValue);
                textFieldComponent.SetVisualElements(textFieldUIElement);
                textFieldComponent.SetFieldData(textFieldUIElement);

                uiElement = textFieldUIElement;
                uiComponent = textFieldComponent;
            }
            else if (csCategoryField.fieldType == RebotsInputFieldType.dropdown)
            {
                TemplateContainer dropdownFieldUIElement = DropdownFieldAsset.Instantiate();

                RebotsDropdownFieldComponent dropdownComponent = new RebotsDropdownFieldComponent(csCategoryField, parameterValue);
                dropdownComponent.SetVisualElement(dropdownFieldUIElement);
                dropdownComponent.SetFieldData(dropdownFieldUIElement);

                uiElement = dropdownFieldUIElement;
                uiComponent = dropdownComponent;
            }
            else if (csCategoryField.fieldType == RebotsInputFieldType.checkbox || csCategoryField.fieldType == RebotsInputFieldType.radiobutton)
            {
                TemplateContainer buttonGroupFieldUIElement = ButtonGroupFieldAsset.Instantiate();

                var buttonAsset = (csCategoryField.fieldType == RebotsInputFieldType.checkbox) ? CheckAsset : RadioAsset;
                RebotsButtonGroupFieldComponent buttonGroupComponent = new RebotsButtonGroupFieldComponent(csCategoryField, buttonAsset, parameterValue);
                buttonGroupComponent.SetVisualElement(buttonGroupFieldUIElement);
                buttonGroupComponent.SetFieldData(buttonGroupFieldUIElement);

                uiElement = buttonGroupFieldUIElement;
                uiComponent = buttonGroupComponent;
            }
            else if (csCategoryField.fieldType == RebotsInputFieldType.file)
            {
                TemplateContainer attachmentFieldUIElement = AttachmentFieldAsset.Instantiate();

                RebotsAttachmentFieldComponent attachmentComponent = new RebotsAttachmentFieldComponent(csCategoryField, AttachmentFileAsset);
                attachmentComponent.SetVisualElement(attachmentFieldUIElement);
                attachmentComponent.SetFieldData(attachmentFieldUIElement);

                uiElement = attachmentFieldUIElement;
                uiComponent = attachmentComponent;
            }
            else
            {
                uiElement = null;
                uiComponent = null;
            }
        }

        public void CreateTicket(HelpdeskTicket ticket, Action<HelpdeskTicket> clickAction, out TemplateContainer uiElement)
        {
            TemplateContainer ticketUIElement = TicketAsset.Instantiate();

            RebotsTicketComponent ticketComponent = new RebotsTicketComponent(ticket);

            ticketComponent.SetVisualElements(ticketUIElement);
            ticketComponent.SetTicketData(ticketUIElement);
            ticketComponent.RegisterCallbacks(clickAction);

            uiElement = ticketUIElement;
        }

        public void CreateTicketDetail(string sub, string content, RebotsTicketDetailAssetType type, out TemplateContainer uiElement)
        {
            VisualTreeAsset asset = TicketFieldAsset;
            switch (type)
            {
                case RebotsTicketDetailAssetType.field:
                    asset = TicketFieldAsset;
                    break;
                case RebotsTicketDetailAssetType.answer:
                    asset = TicketAnswerAsset;
                    break;
            }

            TemplateContainer DetailUIElement = asset.Instantiate();

            RebotsTicketDetailComponent detailComponent = new RebotsTicketDetailComponent(sub, content);

            detailComponent.SetVisualElements(DetailUIElement);
            detailComponent.SetTicketDetailData(DetailUIElement);

            uiElement = DetailUIElement;
        }

        public void CreatePrivacyField(PrivacySetting ticketPrivacySetting, string[] transData, out TemplateContainer uiElement)
        {
            TemplateContainer PrivacyUIElement = PrivacyFieldAsset.Instantiate();

            RebotsPrivacyComponent privacyComponent = new RebotsPrivacyComponent(ticketPrivacySetting, transData);

            privacyComponent.SetVisualElements(PrivacyUIElement);
            privacyComponent.SetPrivacyData(PrivacyUIElement);

            uiElement = PrivacyUIElement;
        }
    }
}
