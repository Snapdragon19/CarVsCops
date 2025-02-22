using UnityEditor;
using UnityEngine;

public class AdsMenuItem : MonoBehaviour
{
    [MenuItem("SansDev/Customize/Ads Data")]
    static void OpenMenu1()
    {
        string path = "Assets/_Ads/SO/Ads Data.asset";
        AdsDataSO data = (AdsDataSO)AssetDatabase.LoadAssetAtPath(path, typeof(AdsDataSO));
        Selection.activeObject = data;
    }

    [MenuItem("SansDev/Customize/Privacy Policy Link")]
    static void OpenMenu2()
    {
        string path = "Assets/_Ads/SO/Policy Data.asset";
        PolicyDataSO data = (PolicyDataSO)AssetDatabase.LoadAssetAtPath(path, typeof(PolicyDataSO));
        Selection.activeObject = data;
    }
}
