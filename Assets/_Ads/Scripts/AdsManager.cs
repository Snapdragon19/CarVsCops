using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdsManager : Singleton<AdsManager>
{
    [SerializeField] int GDPRSceneIndex = -1;
    [SerializeField] int gameplaySceneIndex;

    [SerializeField] AdsDataSO _data;
    [SerializeField] Transform _adsTransform;

    Ads _ads;

    //bool _noAds = false;

    bool _isInterstitialShouldShow = false;

    bool _isInterstitialTimerPassed = false;
    float _interstitialTimer;

    bool _isRewardedTimerPassed = false;
    float _RewardedTimer;

    static int _gameplayCount;

    public AdsType adsType { get; private set; }

    public static event Action OnRewardedAdWatchedComplete;
    public static void HandleRewardedAdWatchedComplete() => OnRewardedAdWatchedComplete?.Invoke();

    private void OnEnable() => SceneManager.sceneLoaded += HandleSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= HandleSceneLoaded;

    private void HandleSceneLoaded(Scene s, LoadSceneMode sm)
    {
        if (s.buildIndex == gameplaySceneIndex)
        {
            _gameplayCount++;
            int interval = Mathf.Clamp(_data.InterstitialAdInterval, 1, 100);

            if (_gameplayCount % interval == 0)
            {
                _isInterstitialShouldShow = true;
            }

            switch (_data.GetAdsType)
            {
                case AdsType.Admob:
                    ShowBanner();
                    break;
                case AdsType.UnityAds:
                    if (_ads != null) _ads.LoadAds();
                    break;
                default:
                    break;
            }
        }
        else if (s.buildIndex == GDPRSceneIndex)
        {
            Destroy(this.gameObject);
        }
    }

    protected override void Awake()
    {
        base.Awake();

        if(_ads == null)
        {
            _ads = GetAdsChoice();
        }

        switch (_data.GetAdsType)
        {
            case AdsType.Admob:
                adsType = AdsType.Admob;
                SetSceneIndex(0, 1);
                break;
            case AdsType.UnityAds:
                adsType = AdsType.UnityAds;
                SetSceneIndex(-1, 0);
                break;
        }
    }

    private void Start()
    {
        _interstitialTimer = _data.MinDelayBetweenInterstitial;
        _RewardedTimer = _data.MinDelayBetweenRewarded;
    }

    public void SetSceneIndex(int GDPRIndex, int gameplayIndex)
    {
        GDPRSceneIndex = GDPRIndex;
        gameplaySceneIndex = gameplayIndex;
    }

    Ads GetAdsChoice()
    {
        switch (_data.GetAdsType)
        {
            case AdsType.Admob:
                _adsTransform = transform.GetChild(0);
                break;
            case AdsType.UnityAds:
                _adsTransform = transform.GetChild(1);
                break;
            default:
                break;
        }

        if (_adsTransform != null)
        {
            _adsTransform.gameObject.SetActive(true);
            _ads = _adsTransform.gameObject.GetComponent<Ads>();
        }
        return _ads;
    }

    private void Update()
    {
        if (!_isInterstitialTimerPassed && Time.time > _interstitialTimer)
        {
            _isInterstitialTimerPassed = true;
        }

        if (!_isRewardedTimerPassed && Time.time > _RewardedTimer)
        {
            _isRewardedTimerPassed = true;
        }
    }

    public bool IsRewardedAdLoaded()
    {
        //if (!_isInterstitialShouldShow && UnityEngine.Random.Range(0f, 1f) <= _data.RewardedAdFrequency)
        if (UnityEngine.Random.Range(0f, 1f) <= _data.RewardedAdFrequency)
        {
            return _ads.IsRewardedAdLoaded();
        }
        else
        {
            return false;
        }
    }

    public void ShowBanner()
    {
        _ads.ShowBanner();
    }

    public void ShowInterstitial()
    {
        if (_isInterstitialTimerPassed && _isInterstitialShouldShow)
        {
            _isInterstitialShouldShow = false;

            _isInterstitialTimerPassed = false;
            _interstitialTimer = Time.time + _data.MinDelayBetweenInterstitial;

            _ads.ShowInterstitial();
        }
    }

    public void ShowRewarded()
    {
        if (_isRewardedTimerPassed)
        {
            _isRewardedTimerPassed = false;
            _RewardedTimer = Time.time + _data.MinDelayBetweenRewarded;

            _ads.ShowRewarded();
        }
    }
}
