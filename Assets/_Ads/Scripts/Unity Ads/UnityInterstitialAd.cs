using UnityEngine;
using UnityEngine.Advertisements;

public class UnityInterstitialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    private string _adUnitId;

    public string AdUnitID { set { _adUnitId = value; } }

    // Load content to the Ad Unit:
    public void LoadAd()
    {
        if (Advertisement.IsReady(_adUnitId)) return;

        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Advertisement.Load(_adUnitId, this);
    }

    // Show the loaded content in the Ad Unit: 
    public void ShowAd()
    {
        // Note that if the ad content wasn't previously loaded, this method will fail
        if (Advertisement.IsReady(_adUnitId))
            Advertisement.Show(_adUnitId, this);
    }

    // Load Listener
    public void OnUnityAdsAdLoaded(string placementId) { }
    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) { }

    // Show Listener
    public void OnUnityAdsShowStart(string placementId) { }
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) { }
    public void OnUnityAdsShowClick(string placementId) { }
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState) { }
}
