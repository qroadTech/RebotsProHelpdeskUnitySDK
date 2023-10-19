using HelpDesk.Sdk.Common.Objects;
using System;
using UnityEngine.UIElements;

namespace Rebots.HelpDesk
{
    public class RebotsCategoryComponent<T>
    {
        const string CategoryLabel = "rebots-contents-label";
        const string CategoryButton = "rebots-contents-button";

        Label m_CategoryLabel;
        Button m_CategoryButton;

        private T category;

        public RebotsCategoryComponent(T category)
        {
            this.category = category;
        }

        public void SetVisualElements(TemplateContainer categoryUIElement)
        {
            if (categoryUIElement == null)
            {
                return;
            }

            m_CategoryLabel = categoryUIElement.Q<Label>(CategoryLabel);
            m_CategoryButton = categoryUIElement.Q<Button>(CategoryButton);
        }

        public void SetCategoryData(TemplateContainer categoryUIElement)
        {
            if (categoryUIElement == null)
            {
                return;
            }

            if(typeof(T) == typeof(Category))
            {
                m_CategoryLabel.text = (category as Category).name;
            }
            else if (typeof(T) == typeof(Faq))
            {
                m_CategoryLabel.text = (category as Faq).title;
            }
        }

        public void RegisterCallbacks(Action<T> categoryAction)
        {
            m_CategoryButton?.RegisterCallback<ClickEvent>(evt => categoryAction(category));
        }
    }
}
