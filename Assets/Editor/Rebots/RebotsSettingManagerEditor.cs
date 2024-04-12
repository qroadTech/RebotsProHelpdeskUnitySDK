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
                settingScript.ProjectMainKey = "Xf37b8ce929Zo1mcQkmqbsM";

                settingScript.ProjectMainKey = EditorGUILayout.TextField("Project Main Key", settingScript.ProjectMainKey);
                
                settingScript.translationFile = (TextAsset)EditorGUILayout.ObjectField("Rebots Translation File", settingScript.translationFile, typeof(TextAsset), true);

                settingScript.HelpdeskLanguage = (RebotsLanguage)EditorGUILayout.EnumPopup("Helpdesk Language", settingScript.HelpdeskLanguage);

                settingScript.RebotsScreenOrientation = (RebotsScreenOrientation)EditorGUILayout.EnumPopup("Helpdesk Screen Orientation", settingScript.RebotsScreenOrientation);

                if (GUILayout.Button("Check Language Available"))
                {
                    settingScript.RebotsHelpdeskInitailize();
                }
            }
        }
    }
}
