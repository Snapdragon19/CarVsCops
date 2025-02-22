using UnityEngine;
using UnityEngine.Advertisements;

public class UnityRewardedAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    private string _adUnitId;

    public string AdUnitID { set { _adUnitId = value; } }

    public bool IsRewardedAdLoaded { get; private set; }

    public void LoadAd()
    {
        if (Advertisement.IsReady(_adUnitId)) return;

        Advertisement.Load(_adUnitId, this);
    }

    public void ShowAd()
    {
        if (Advertisement.IsReady(_adUnitId))
            Advertisement.Show(_adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        IsRewardedAdLoaded = true;
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) { }
    public void OnUnityAdsShowClick(string placementId) { }
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            // Grant a reward.
            AdsManager.HandleRewardedAdWatchedComplete();
        }
    }
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) { }
    public void OnUnityAdsShowStart(string placementId)
    {
        IsRewardedAdLoaded = false;

#if UNITY_EDITOR
        // Grant a reward.
        AdsManager.HandleRewardedAdWatchedComplete();
#endif
    }
}
