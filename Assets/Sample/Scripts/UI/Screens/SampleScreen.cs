using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Rebots.HelpDesk
{
    public class SampleScreen : MonoBehaviour
    {
        [SerializeField] RebotsHelpdeskUIManager rebotsHelpdeskUIManager;
        public Button RebotsHelpdeskOpen;

        void Start()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            RebotsHelpdeskOpen = root.Q<Button>("rebots-helpdesk-open");

            RebotsHelpdeskOpen.clicked += rebotsHelpdeskUIManager.RebotsHelpdeskShow;
        }
    }
}
