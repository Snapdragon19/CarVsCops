using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : Menu
{
    [Header("Inherit Variable :")]
    [SerializeField] Button _playButton;
    [SerializeField] Button _creditButton;
    [SerializeField] Button _rateButton;
    [SerializeField] Button _musicButton;
    [SerializeField] Button _sfxButton;
    [SerializeField] Button _adsButton;

    [Header("Game Database :")]
    [SerializeField] GameData _data;

    [Header("SFX Icon Toggle")]
    [SerializeField] Sprite _sfxTrue;
    [SerializeField] Sprite _sfxFalse;

    [Header("Music Icon Toggle")]
    [SerializeField] Sprite _musicTrue;
    [SerializeField] Sprite _musicFalse;

    bool _sfxState;
    bool _musicState;

    Image _musicButtonImage;
    Image _sfxButtonImage;

    public override void SetEnable()
    {
        base.SetEnable();

        _playButton.interactable = true;
        _adsButton.interactable = true;

        LeanTween.scale(_playButton.gameObject, Vector2.one * 1.1f, .3f).setEase(LeanTweenType.easeOutQuad).setLoopPingPong();
    }

    public override void SetDisable()
    {
        base.SetDisable();

        LeanTween.cancel(_playButton.gameObject);
        _playButton.transform.localScale = Vector3.one;
    }

    protected override void Awake()
    {
        base.Awake();

        _sfxButtonImage = _sfxButton.transform.GetChild(0).GetComponent<Image>();
        _musicButtonImage = _musicButton.transform.GetChild(0).GetComponent<Image>();
    }

    private void Start()
    {
        SetButtonIconToggle();
        HandleButtonPressed();
    }

    private void HandleButtonPressed()
    {
        OnButtonPressed(_playButton, HandlePlayButtonPressed);
        OnButtonPressed(_creditButton, HandleCreditButtonPressed);
        OnButtonPressed(_rateButton, HandleRateButtonPressed);
        OnButtonPressed(_musicButton, HandleMusicButtonPressed);
        OnButtonPressed(_sfxButton, HandleSFXButtonPressed);
        OnButtonPressed(_adsButton, HandleAdsButtonPressed);
    }

    private void HandlePlayButtonPressed()
    {
        _playButton.interactable = false;
        LeanTween.cancelAll();
        _playButton.transform.localScale = Vector3.one;

        MenuManager.GetInstance().SwitchMenu(MenuType.Gameplay);
        FindObjectOfType<GameManager>().IsPlaying(true);
    }

    private void HandleCreditButtonPressed()
    {
        MenuManager.GetInstance().OpenMenu(MenuType.Credit);
    }

    private void HandleRateButtonPressed()
    {
        Application.OpenURL($"market://details?id={Application.identifier}");
    }

    private void HandleMusicButtonPressed()
    {
        SoundManager.GetInstance().ToggleMusic(ref _musicState);
        ToggleIconMusic();
    }

    private void HandleSFXButtonPressed()
    {
        SoundManager.GetInstance().ToggleFX(ref _sfxState);
        ToggleIconSFX();
    }

    private void HandleAdsButtonPressed()
    {
        _adsButton.interactable = false;

        AdsManager.GetInstance().DestroyObject();
        SoundManager.GetInstance().DestroyObject();

        PlayerPrefs.SetInt("npa", -1);

        //load gdpr scene
        StartCoroutine(LoadGDPR());
    }

    IEnumerator LoadGDPR()
    {
        yield return SceneManager.LoadSceneAsync(0);
        MenuManager.GetInstance().DestroyObject();
    }

    private void ToggleIconMusic()
    {
        _musicButtonImage.sprite = _musicState ? _musicTrue : _musicFalse;
    }

    private void ToggleIconSFX()
    {
        _sfxButtonImage.sprite = _sfxState ? _sfxTrue : _sfxFalse;
    }

    private void SetButtonIconToggle()
    {
        _musicState = PlayerPrefs.GetInt("musicState") == 0;
        _sfxState = PlayerPrefs.GetInt("sfxState") == 0;

        ToggleIconSFX();
        ToggleIconMusic();
    }
}
