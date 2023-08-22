using HelpDesk.Sdk.Common.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace Rebots.HelpDesk
{
    public class RebotsCategoryComponent<T>
    {
        const string k_CategoryLabel = "rebots-category-label";
        const string k_CategoryButton = "rebots-category-button";

        T m_Category;
        Label m_CategoryLabel;
        Button m_CategoryButton;

        public RebotsCategoryComponent(T category)
        {
            m_Category = category;
        }

        public void SetVisualElements(TemplateContainer categoryUIElement)
        {
            if (categoryUIElement == null)
            {
                return;
            }

            m_CategoryLabel = categoryUIElement.Q<Label>(k_CategoryLabel);
            m_CategoryButton = categoryUIElement.Q<Button>(k_CategoryButton);
        }

        public void SetCategoryData(TemplateContainer categoryUIElement)
        {
            if (categoryUIElement == null)
            {
                return;
            }

            if(typeof(T) == typeof(Category))
            {
                m_CategoryLabel.text = (m_Category as Category).name;
            }
            else if (typeof(T) == typeof(Faq))
            {
                m_CategoryLabel.text = (m_Category as Faq).title;
            }
        }

        public void RegisterCallbacks(Action<T> categoryAction)
        {
            m_CategoryButton?.RegisterCallback<ClickEvent>(evt => categoryAction(m_Category));
        }
    }
}
