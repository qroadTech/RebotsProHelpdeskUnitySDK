using HelpDesk.Sdk.Common.Objects;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using HelpDesk.Sdk.Common.Protocols.Responses;
using Assets.Rebots;
using UnityEditor;

namespace Rebots.HelpDesk
{
    public class RebotsUICreater : MonoBehaviour
    {
        [Header("Rebots Helpdesk Asset")]
        [SerializeField] VisualTreeAsset AttachmentFieldAsset;
        [SerializeField] VisualTreeAsset AttachmentFileAsset;
        [SerializeField] VisualTreeAsset ButtonGroupFieldAsset;
        [SerializeField] VisualTreeAsset CategoryAsset;
        [SerializeField] VisualTreeAsset CheckAsset;
        [SerializeField] VisualTreeAsset DropdownFieldAsset;
        [SerializeField] VisualTreeAsset FaqAsset;
        [SerializeField] VisualTreeAsset LanguageAsset;
        [SerializeField] VisualTreeAsset LowerCategoryAsset;
        [SerializeField] VisualTreeAsset MenuCategoryAsset;
        [SerializeField] VisualTreeAsset PagingNavigator;
        [SerializeField] VisualTreeAsset PopularFaqAsset;
        [SerializeField] VisualTreeAsset PrivacyFieldAsset;
        [SerializeField] VisualTreeAsset RadioAsset;
        [SerializeField] VisualTreeAsset RouteLabelAsset;
        [SerializeField] VisualTreeAsset SearchFaqAsset;
        [SerializeField] VisualTreeAsset SiblingCategoryAsset;
        [SerializeField] VisualTreeAsset TextareaFieldAsset;
        [SerializeField] VisualTreeAsset TextFieldAsset;
        [SerializeField] VisualTreeAsset TicketAsset;
        [SerializeField] VisualTreeAsset TicketAnswerAsset;
        [SerializeField] VisualTreeAsset TicketFieldAsset;

        [HideInInspector]
        public Action SaveVerticalScrollAction;
        public Action SetVerticalScrollAction;
        public Action<string, string> ClickAttachmentLinkAction;
        public Action<Button> ClickMenuCategoryAction;

        public void CreateLanguage(RebotsLanguageInfo settinglanguage, string lanuageText, Action<RebotsLanguageInfo> clickAction, out TemplateContainer uiElement)
        {
            TemplateContainer languageUIElement = LanguageAsset.Instantiate();

            RebotsLanguageComponent languageComponent = new RebotsLanguageComponent(settinglanguage, lanuageText);
            languageComponent.SetVisualElements(languageUIElement);
            languageComponent.SetLanguageData(languageUIElement);
            languageComponent.RegisterCallbacks(clickAction);

            uiElement = languageUIElement;
        }

        public void CreateRouteLabel(Category category, bool isSelected, Action<Category>? clickAction, out Label label)
        {
            Label labelUIElement = new Label();
            if (isSelected)
            {
                labelUIElement.text = category.name;
                labelUIElement.AddToClassList(RebotsUIStaticString.RebotsLabel_Black16);
                labelUIElement.AddToClassList(RebotsUIStaticString.RebotsFontColor_Black);
            }
            else
            {
                labelUIElement.text = category.name + " >";
                labelUIElement.AddToClassList(RebotsUIStaticString.RebotsLabel_Bold16);
                labelUIElement.AddToClassList(RebotsUIStaticString.RebotsFontColor_Grey);
                if (clickAction != null)
                {
                    labelUIElement.RegisterCallback<ClickEvent>(evt => clickAction(category));
                }
                labelUIElement.style.paddingRight = 5f;
            }

            label = labelUIElement;
        }

        public void CreateCategory<T>(T Category, RebotsCategoryAssetType type, Action<T> clickAction, out TemplateContainer uiElement)
        {
            TemplateContainer categoryUIElement = null;
            switch (type)
            {
                case RebotsCategoryAssetType.Category:
                    categoryUIElement = CategoryAsset.Instantiate();
                    break;
                case RebotsCategoryAssetType.Faq:
                    categoryUIElement = FaqAsset.Instantiate();
                    break;
                case RebotsCategoryAssetType.Lower:
                    categoryUIElement = LowerCategoryAsset.Instantiate();
                    break;
                case RebotsCategoryAssetType.Menu:
                    categoryUIElement = MenuCategoryAsset.Instantiate();
                    break;
                case RebotsCategoryAssetType.Sibling:
                    categoryUIElement = SiblingCategoryAsset.Instantiate();
                    categoryUIElement.RemoveAt(1);
                    break;
                case RebotsCategoryAssetType.Selected:
                    categoryUIElement = SiblingCategoryAsset.Instantiate();
                    categoryUIElement.RemoveAt(0);
                    break;
            }

            RebotsCategoryComponent<T> categoryComponent = new RebotsCategoryComponent<T>(Category);

            categoryComponent.SetVisualElements(categoryUIElement);
            categoryComponent.SetCategoryData(categoryUIElement);

            // Test SideMenuClick
            //if (type == RebotsCategoryAssetType.Menu)
            //{
            //    categoryComponent.RegisterCallbacksAtMenu(ClickMenuCategoryAction, clickAction);
            //}
            //else
            //{
            //    categoryComponent.RegisterCallbacks(clickAction);
            //}
            categoryComponent.RegisterCallbacks(clickAction);

            if (type == RebotsCategoryAssetType.Sibling || type == RebotsCategoryAssetType.Selected || type == RebotsCategoryAssetType.Lower)
            {
                categoryUIElement.style.flexGrow = 0;
                categoryUIElement.style.flexShrink = 0;
                categoryUIElement.style.maxWidth = new StyleLength(Length.Percent(50));
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
                dropdownComponent.SetVisualElements(dropdownFieldUIElement);
                dropdownComponent.SetFieldData(dropdownFieldUIElement);

                uiElement = dropdownFieldUIElement;
                uiComponent = dropdownComponent;
            }
            else if (csCategoryField.fieldType == RebotsInputFieldType.Checkbox || csCategoryField.fieldType == RebotsInputFieldType.Radiobutton)
            {
                TemplateContainer buttonGroupFieldUIElement = ButtonGroupFieldAsset.Instantiate();

                var buttonAsset = (csCategoryField.fieldType == RebotsInputFieldType.Checkbox) ? CheckAsset : RadioAsset;
                RebotsButtonGroupFieldComponent buttonGroupComponent = new RebotsButtonGroupFieldComponent(csCategoryField, buttonAsset, parameterValue, validationComment);
                buttonGroupComponent.SetVisualElements(buttonGroupFieldUIElement);
                buttonGroupComponent.SetFieldData(buttonGroupFieldUIElement);

                uiElement = buttonGroupFieldUIElement;
                uiComponent = buttonGroupComponent;
            }
            else if (csCategoryField.fieldType == RebotsInputFieldType.File)
            {
                TemplateContainer attachmentFieldUIElement = AttachmentFieldAsset.Instantiate();

                RebotsAttachmentFieldComponent attachmentComponent = new RebotsAttachmentFieldComponent(csCategoryField, AttachmentFileAsset, validationComment);
                attachmentComponent.SetVisualElements(attachmentFieldUIElement);
                attachmentComponent.SetFieldData(attachmentFieldUIElement);
                attachmentComponent.SetAction(SaveVerticalScrollAction, SetVerticalScrollAction);

                uiElement = attachmentFieldUIElement;
                uiComponent = attachmentComponent;
            }
            else
            {
                uiElement = null;
                uiComponent = null;
            }
        }

        public void CreatePrivacyField(PrivacySetting ticketPrivacySetting, string[] transData, string validationComment, Category category, Action<bool, Category> clickAction, out TemplateContainer uiElement)
        {
            TemplateContainer PrivacyUIElement = PrivacyFieldAsset.Instantiate();

            RebotsPrivacyComponent privacyComponent = new RebotsPrivacyComponent(ticketPrivacySetting, transData, validationComment, category);

            privacyComponent.SetVisualElements(PrivacyUIElement);
            privacyComponent.SetPrivacyData(PrivacyUIElement);
            privacyComponent.RegisterCallbacks(clickAction);

            uiElement = PrivacyUIElement;
        }
        
        public void CreateTicket(HelpdeskTicket ticket, bool isNewAnswer, string[] transData, Action<HelpdeskTicket> clickAction, out TemplateContainer uiElement)
        {
            TemplateContainer ticketUIElement = TicketAsset.Instantiate();

            RebotsTicketComponent ticketComponent = new RebotsTicketComponent(ticket, isNewAnswer, transData);

            ticketComponent.SetVisualElements(ticketUIElement);
            ticketComponent.SetTicketData(ticketUIElement);
            ticketComponent.RegisterCallbacks(clickAction);

            uiElement = ticketUIElement;
        }

        public void CreateTicketDetail(string sub, string content, RebotsTicketDetailAssetType type, out TemplateContainer uiElement, bool isNewAnswer = false)
        {
            VisualTreeAsset asset = TicketFieldAsset;
            TemplateContainer DetailUIElement = null;

            if (type == RebotsTicketDetailAssetType.Attachment)
            {
                asset = AttachmentFileAsset;
                DetailUIElement = asset.Instantiate();

                var m_FileNameLabel = DetailUIElement.Q<Label>(RebotsUIStaticString.FileNameLabel);
                var m_FileSizeLabel = DetailUIElement.Q<Label>(RebotsUIStaticString.FileSizeLabel);
                var m_FileRemoveButton = DetailUIElement.Q<Button>(RebotsUIStaticString.FileRemoveButton);
                var m_FileDownloadButton = DetailUIElement.Q<Button>(RebotsUIStaticString.FileDownloadButton);

                m_FileNameLabel.text = sub;
                m_FileSizeLabel.style.display = DisplayStyle.None;
                m_FileRemoveButton.style.display = DisplayStyle.None;
                m_FileDownloadButton.style.display = DisplayStyle.Flex;
                DetailUIElement?.RegisterCallback<ClickEvent>(evt => ClickAttachmentLinkAction(content, sub));
            }
            else
            {
                switch (type)
                {
                    case RebotsTicketDetailAssetType.Field:
                        asset = TicketFieldAsset;
                        break;
                    case RebotsTicketDetailAssetType.Answer:
                        asset = TicketAnswerAsset;
                        break;
                }
                DetailUIElement = asset.Instantiate();

                RebotsTicketDetailComponent detailComponent = new RebotsTicketDetailComponent(sub, content, isNewAnswer);

                detailComponent.SetVisualElements(DetailUIElement);
                detailComponent.SetTicketDetailData(DetailUIElement);
            }

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

        public void CreatePaging<T>(RebotsPagingData<T> pagingData, Action<RebotsPagingData<T>> clickAction, out TemplateContainer uiElement)
        {
            TemplateContainer pagingNavigatorUIElement = PagingNavigator.Instantiate();

            var pageList = pagingNavigatorUIElement.Q(RebotsUIStaticString.PageList);
            Button startNavigator = pagingNavigatorUIElement.Q<Button>(RebotsUIStaticString.PagingStartButton);
            Button previousNavigator = pagingNavigatorUIElement.Q<Button>(RebotsUIStaticString.PagingPreviousButton);
            Button nextNavigator = pagingNavigatorUIElement.Q<Button>(RebotsUIStaticString.PagingNextButton);
            Button endNavigator = pagingNavigatorUIElement.Q<Button>(RebotsUIStaticString.PagingEndButton);

            var startIndex = (pagingData.SelectedPage % 5 == 0) ? pagingData.SelectedPage - 5 : (pagingData.SelectedPage / 5) * 5;
            var endIndex = (pagingData.SelectedPage % 5 == 0) ? pagingData.SelectedPage : ((pagingData.SelectedPage / 5) + 1) * 5;
            endIndex = (pagingData.TotalPage < endIndex) ? pagingData.TotalPage : endIndex;

            while (startIndex++ < endIndex)
            {
                var data = pagingData.SetPage(startIndex);
                var pageUIElement = new Label
                {
                    text = data.Page.ToString()
                };
                pageUIElement.style.width = 22f;
                pageUIElement.style.paddingBottom = 0;
                pageUIElement.style.paddingTop = 0;
                pageUIElement.style.paddingRight = 0;
                pageUIElement.style.paddingLeft = 0;
                pageUIElement.style.marginRight = 5;
                pageUIElement.style.marginLeft = 5;

                if (data.IsClickAction)
                {
                    pageUIElement.AddToClassList(RebotsUIStaticString.RebotsLabel_Regular16);
                    pageUIElement.AddToClassList(RebotsUIStaticString.RebotsFontColor_Grey);
                    pageUIElement?.RegisterCallback<ClickEvent>(evt => clickAction(data));
                }
                else
                {
                    pageUIElement.AddToClassList(RebotsUIStaticString.RebotsLabel_Black16);
                    pageUIElement.AddToClassList(RebotsUIStaticString.RebotsFontColor_Black);
                }

                pageList.Add(pageUIElement);
            }

            if (pagingData.TotalPage > 5)
            {
                startNavigator?.RegisterCallback<ClickEvent>(evt => clickAction(pagingData.SetStartPage()));
                previousNavigator?.RegisterCallback<ClickEvent>(evt => clickAction(pagingData.SetPreviousPage()));
                nextNavigator?.RegisterCallback<ClickEvent>(evt => clickAction(pagingData.SetNextPage()));
                endNavigator?.RegisterCallback<ClickEvent>(evt => clickAction(pagingData.SetEndPage()));
            }
            else
            {
                startNavigator.style.display = DisplayStyle.None;
                previousNavigator.style.display = DisplayStyle.None;
                nextNavigator.style.display = DisplayStyle.None;
                endNavigator.style.display = DisplayStyle.None;
            }
            
            uiElement = pagingNavigatorUIElement;
        }
    }
}
        