using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RebotsExport : MonoBehaviour
{
    [MenuItem("RebotsPackage/RebotsSDK Export")]
    static void Export()
    {
        var exportedPackageAssetList = new List<string>();

        exportedPackageAssetList.Add("Assets/Editor/Rebots");

        exportedPackageAssetList.Add("Assets/Plugins/NativeGallery");
        exportedPackageAssetList.Add("Assets/Plugins/Newtonsoft.Json.dll");
        exportedPackageAssetList.Add("Assets/Plugins/Newtonsoft.Json.pdb");
        exportedPackageAssetList.Add("Assets/Plugins/HelpDesk.Sdk.Common.dll");
        exportedPackageAssetList.Add("Assets/Plugins/HelpDesk.Sdk.Common.pdb");
        exportedPackageAssetList.Add("Assets/Plugins/HelpDesk.Sdk.Library.dll");
        exportedPackageAssetList.Add("Assets/Plugins/HelpDesk.Sdk.Library.pdb");
        exportedPackageAssetList.Add("Assets/Plugins/HelpDesk.Sdk.Unity.Library.dll");
        exportedPackageAssetList.Add("Assets/Plugins/HelpDesk.Sdk.Unity.Library.pdb");

        exportedPackageAssetList.Add("Assets/Scenes/Rebots.unity");

        exportedPackageAssetList.Add("Assets/Resources/Rebots");

        exportedPackageAssetList.Add("Assets/Rebots");
        exportedPackageAssetList.Add("Assets/Sample");

        AssetDatabase.ExportPackage(exportedPackageAssetList.ToArray(), "RebotsSDK.unitypackage",
            ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies);
    }
}