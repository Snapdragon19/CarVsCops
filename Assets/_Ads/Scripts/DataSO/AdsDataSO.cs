using UnityEngine;

[CreateAssetMenu()]
public class AdsDataSO : ScriptableObject
{
    [SerializeField] int _interstitialAdInterval;
    [SerializeField] float _minDelayBetweenInterstitial = 20f;

    [SerializeField] [Range(0f, 1f)] float _rewardedAdFrequency = .5f;
    [SerializeField] float _minDelayBetweenRewarded = 20f;

    [SerializeField] AdsType _adsType;
    public AdsType GetAdsType => _adsType;

    [SerializeField] bool _enableBanner = true;
    [SerializeField] bool _enableInterstitial = true;
    [SerializeField] bool _enableRewarded = true;

    // ADMOB
    // Test App Id = "ca-app-pub-3940256099942544~3347511713";
    [SerializeField] [TextArea(1, 2)] string idBanner = "ca-app-pub-3940256099942544/6300978111";
    [SerializeField] [TextArea(1, 2)] string idInterstitial = "ca-app-pub-3940256099942544/1033173712";
    [SerializeField] [TextArea(1, 2)] string idReward = "ca-app-pub-3940256099942544/5224354917";

    // UNITY ADS
    //[Header("Unity Settings :")]
    [SerializeField] string _androidGameId;
    [SerializeField] string _iosGameId;
    [SerializeField] bool _testMode = true;

    //[Header("Android Ad Unit ID :")]
    [SerializeField] string _androInterstitialId = "Interstitial_Android";
    [SerializeField] string _androRewardedId = "Rewarded_Android";
    [SerializeField] string _androBannerId = "Banner_Android";

    //[Header("IOS Ad Unit ID :")]
    [SerializeField] string _iosInterstitialId = "Interstitial_iOS";
    [SerializeField] string _iosRewardedId = "Rewarded_iOS";
    [SerializeField] string _iosBannerId = "Banner_iOS";

    public int InterstitialAdInterval => _interstitialAdInterval;
    public float RewardedAdFrequency => _rewardedAdFrequency;

    public float MinDelayBetweenInterstitial => _minDelayBetweenInterstitial;
    public float MinDelayBetweenRewarded => _minDelayBetweenRewarded;

    public bool BannerEnabled => _enableBanner;
    public bool InterstitialEnabled => _enableInterstitial;
    public bool RewardedEnabled => _enableRewarded;

    public bool TestMode => _testMode;
    public string GameID => (Application.platform == RuntimePlatform.IPhonePlayer) ? _iosGameId : _androidGameId;

    public string BannerID
    {
        get
        {
            switch (_adsType)
            {
                case AdsType.Admob:
                    return idBanner;
                case AdsType.UnityAds:
                    return (Application.platform == RuntimePlatform.IPhonePlayer) ? _iosBannerId : _androBannerId;
                default:
                    return "";
            }
        }
    }

    public string InterstitialID
    {
        get
        {
            switch (_adsType)
            {
                case AdsType.Admob:
                    return idInterstitial;
                case AdsType.UnityAds:
                    return (Application.platform == RuntimePlatform.IPhonePlayer) ? _iosInterstitialId : _androInterstitialId;
                default:
                    return "";
            }
        }
    }

    public string RewardedID
    {
        get
        {
            switch (_adsType)
            {
                case AdsType.Admob:
                    return idReward;
                case AdsType.UnityAds:
                    return (Application.platform == RuntimePlatform.IPhonePlayer) ? _iosRewardedId : _androRewardedId;
                default:
                    return "";
            }
        }
    }
}

public enum AdsType { Admob, UnityAds }
