using HelpDesk.Sdk.Common.Objects;
using UnityEditor;
using UnityEngine;

namespace Rebots.HelpDesk
{
    [CustomEditor(typeof(RebotsSettingManager))]
    public class RebotsSettingManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            RebotsSettingManager settingScript = (RebotsSettingManager)target;
            if (settingScript != null)
            {
                settingScript.ProjectMainKey = EditorGUILayout.TextField("Project Main Key", settingScript.ProjectMainKey);
                
                settingScript.translationFile = (TextAsset)EditorGUILayout.ObjectField("Rebots Translation File", settingScript.translationFile, typeof(TextAsset), true);

                settingScript.HelpdeskLanguage = (RebotsLanguage)EditorGUILayout.EnumPopup("Helpdesk Language", settingScript.HelpdeskLanguage);

                if (GUILayout.Button("Check Language Available"))
                {
                    settingScript.RebotsHelpdeskInitailize();
                }
            }
        }
    }
}
