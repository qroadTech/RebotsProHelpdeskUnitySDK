using HelpDesk.Sdk.Common.Objects;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using HelpDesk.Sdk.Common.Protocols.Responses;
using Unity.VisualScripting;
using Assets.Rebots;

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

        public void CreateRouteLabel(string categoryName, out TemplateContainer uiElement)
        {
            TemplateContainer routeLabelUIElement = RouteLabelAsset.Instantiate();
            Label routeLabel = routeLabelUIElement.Q<Label>(RebotsUIStaticString.RouteLabel);
            routeLabel.text = categoryName;

            uiElement = routeLabelUIElement;
        }

        public void CreateCategory<T>(T Category, RebotsCategoryAssetType type, Action<T> clickAction, out TemplateContainer uiElement)
        {
            VisualTreeAsset asset = FaqCategoryAsset;
            switch (type)
            {
                case RebotsCategoryAssetType.Cs:
                    asset = CsCategoryAsset;
                    break;
                case RebotsCategoryAssetType.Faq:
                    asset = FaqCategoryAsset;
                    break;
                case RebotsCategoryAssetType.Menu:
                    asset = MenuCategoryAsset;
                    break;
                case RebotsCategoryAssetType.Sibling:
                    asset = SiblingCategoryAsset;
                    break;
                case RebotsCategoryAssetType.Selected:
                    asset = SelectedCategoryAsset;
                    break;
                case RebotsCategoryAssetType.Sub:
                    asset = SubCategoryAsset;
                    break;
                case RebotsCategoryAssetType.Contents:
                    asset = ContentCategoryAsset;
                    break;
            }
            TemplateContainer categoryUIElement = asset.Instantiate();

            RebotsCategoryComponent<T> categoryComponent = new RebotsCategoryComponent<T>(Category);

            categoryComponent.SetVisualElements(categoryUIElement);
            categoryComponent.SetCategoryData(categoryUIElement);
            categoryComponent.RegisterCallbacks(clickAction);

            if (type == RebotsCategoryAssetType.Sibling || type == RebotsCategoryAssetType.Selected)
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
                case RebotsFaqAssetType.Popular:
                    asset = PopularFaqAsset;
                    break;
                case RebotsFaqAssetType.Search:
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

        public void CreateCsCategoryField(TicketCategoryInputField csCategoryField, string parameterValue, string[] validationComment, out TemplateContainer uiElement, out object uiComponent)
        {
            if (csCategoryField.fieldType == RebotsInputFieldType.Text || csCategoryField.fieldType == RebotsInputFieldType.Textarea)
            {
                VisualTreeAsset asset = TextFieldAsset;

                if (csCategoryField.fieldType == RebotsInputFieldType.Textarea)
                {
                    asset = TextareaFieldAsset;
                }

                TemplateContainer textFieldUIElement = asset.Instantiate();

                RebotsTextFieldComponent textFieldComponent = new RebotsTextFieldComponent(csCategoryField, parameterValue, validationComment);
                textFieldComponent.SetVisualElements(textFieldUIElement);
                textFieldComponent.SetFieldData(textFieldUIElement);

                uiElement = textFieldUIElement;
                uiComponent = textFieldComponent;
            }
            else if (csCategoryField.fieldType == RebotsInputFieldType.Dropdown)
            {
                TemplateContainer dropdownFieldUIElement = DropdownFieldAsset.Instantiate();

                RebotsDropdownFieldComponent dropdownComponent = new RebotsDropdownFieldComponent(csCategoryField, parameterValue, validationComment);
                dropdownComponent.SetVisualElement(dropdownFieldUIElement);
                dropdownComponent.SetFieldData(dropdownFieldUIElement);

                uiElement = dropdownFieldUIElement;
                uiComponent = dropdownComponent;
            }
            else if (csCategoryField.fieldType == RebotsInputFieldType.Checkbox || csCategoryField.fieldType == RebotsInputFieldType.Radiobutton)
            {
                TemplateContainer buttonGroupFieldUIElement = ButtonGroupFieldAsset.Instantiate();

                var buttonAsset = (csCategoryField.fieldType == RebotsInputFieldType.Checkbox) ? CheckAsset : RadioAsset;
                RebotsButtonGroupFieldComponent buttonGroupComponent = new RebotsButtonGroupFieldComponent(csCategoryField, buttonAsset, parameterValue, validationComment);
                buttonGroupComponent.SetVisualElement(buttonGroupFieldUIElement);
                buttonGroupComponent.SetFieldData(buttonGroupFieldUIElement);

                uiElement = buttonGroupFieldUIElement;
                uiComponent = buttonGroupComponent;
            }
            else if (csCategoryField.fieldType == RebotsInputFieldType.File)
            {
                TemplateContainer attachmentFieldUIElement = AttachmentFieldAsset.Instantiate();

                RebotsAttachmentFieldComponent attachmentComponent = new RebotsAttachmentFieldComponent(csCategoryField, AttachmentFileAsset, validationComment);
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

        public void CreatePrivacyField(PrivacySetting ticketPrivacySetting, string[] transData, Action<bool> clickAction, out TemplateContainer uiElement)
        {
            TemplateContainer PrivacyUIElement = PrivacyFieldAsset.Instantiate();

            RebotsPrivacyComponent privacyComponent = new RebotsPrivacyComponent(ticketPrivacySetting, transData);

            privacyComponent.SetVisualElements(PrivacyUIElement);
            privacyComponent.SetPrivacyData(PrivacyUIElement);
            privacyComponent.RegisterCallbacks(clickAction);

            uiElement = PrivacyUIElement;
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
                case RebotsTicketDetailAssetType.Field:
                    asset = TicketFieldAsset;
                    break;
                case RebotsTicketDetailAssetType.Answer:
                    asset = TicketAnswerAsset;
                    break;
            }

            TemplateContainer DetailUIElement = asset.Instantiate();

            RebotsTicketDetailComponent detailComponent = new RebotsTicketDetailComponent(sub, content);

            detailComponent.SetVisualElements(DetailUIElement);
            detailComponent.SetTicketDetailData(DetailUIElement);

            uiElement = DetailUIElement;
        }

        public void CreateLabel(string textString, out Label label)
        {
            var labelUIElement = new Label();

            labelUIElement.text = textString;
            labelUIElement.AddToClassList(RebotsUIStaticString.RebotsLabel_Regular16);
            labelUIElement.AddToClassList(RebotsUIStaticString.RebotsFontColor_Black);

            label = labelUIElement;
        }

        public void CreateLinkLabel(string linkString, string? textString, out Label label)
        {
            var labelUIElement = new Label();

            if (textString == null && string.IsNullOrEmpty(textString))
            {
                textString = linkString;
            }
            labelUIElement.text = "<u>" + textString + "</u>";
            labelUIElement?.RegisterCallback<ClickEvent>(evt => Application.OpenURL(linkString));
            labelUIElement.AddToClassList(RebotsUIStaticString.RebotsLabel_Regular16);
            labelUIElement.AddToClassList(RebotsUIStaticString.RebotsLabelLink);

            label = labelUIElement;
        }
         
        public void CreatePaging<T>(RebotsPagingData<T> pagingData, bool isCurrent, Action<T, int?> clickAction, out Label label)
        {
            var pageUIElement = new Label
            {
                text = pagingData.page.ToString()
            };
            pageUIElement.style.paddingBottom = 0;
            pageUIElement.style.paddingTop = 0;
            pageUIElement.style.paddingRight = 5;
            pageUIElement.style.paddingLeft = 5;

            if (isCurrent)
            {
                pageUIElement.AddToClassList(RebotsUIStaticString.RebotsLabel_Black16);
                pageUIElement.AddToClassList(RebotsUIStaticString.RebotsFontColor_Theme);
            }
            else
            {
                pageUIElement.AddToClassList(RebotsUIStaticString.RebotsLabel_Regular16);
                pageUIElement.AddToClassList(RebotsUIStaticString.RebotsFontColor_Black);
                pageUIElement?.RegisterCallback<ClickEvent>(evt => clickAction(pagingData.data, pagingData.page));
            }

            label = pageUIElement;
        }
    }
}
