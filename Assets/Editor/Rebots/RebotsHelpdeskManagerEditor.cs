using HelpDesk.Sdk.Common.Objects.Enums;
using UnityEditor;
using UnityEngine;

namespace Rebots.HelpDesk
{
    [CustomEditor(typeof(RebotsSettingManager))]
    public class RebotsHelpdeskManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            RebotsSettingManager helpdeskScript = (RebotsSettingManager)target;
            if (helpdeskScript != null)
            {
                helpdeskScript.ProjectPublicName = EditorGUILayout.TextField("Rebots Project Public Name", helpdeskScript.ProjectPublicName);
                helpdeskScript.ProjectMainKey = EditorGUILayout.TextField("Rebots Project Main Key", helpdeskScript.ProjectMainKey);

                helpdeskScript.translationFile = (TextAsset)EditorGUILayout.ObjectField("Rebots Translation File", helpdeskScript.translationFile, typeof(TextAsset), true);

                helpdeskScript.HelpdeskLanguage = (RebotsLanguage)EditorGUILayout.EnumPopup("Rebots Helpdesk Language", helpdeskScript.HelpdeskLanguage);

                if (GUILayout.Button("Check Language Available"))
                {
                    helpdeskScript.RebotsHelpdeskInitailize();
                }
            }
        }
    }
}
