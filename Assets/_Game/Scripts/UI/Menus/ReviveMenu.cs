using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReviveMenu : Menu
{
    [Header(" Inherit Variable :")]
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _skipButton;

    [Space]
    [SerializeField] TMP_Text _timerText;
    [SerializeField] Image _timerFill;

    private Timer _timer;

    protected override void Awake()
    {
        base.Awake();

        _timer = GetComponent<Timer>();
    }

    public override void SetEnable()
    {
        base.SetEnable();

        _continueButton.interactable = true;
        _skipButton.interactable = true;

        // start timer
        _timer.PlayTimer(i => _timerText.text = i, j => _timerFill.fillAmount = j, GameOver);

        LeanTween.scale(_continueButton.gameObject, Vector2.one * 1.1f, .3f).setEase(LeanTweenType.easeOutQuad).setLoopPingPong();
    }

    public override void SetDisable()
    {
        base.SetDisable();
        ResetWatchAdButton();
    }

    private void Start()
    {
        OnButtonPressed(_continueButton, HandleContinueButtonPressed);
        OnButtonPressed(_skipButton, HandleSkipButtonPressed);
    }

    private void HandleSkipButtonPressed()
    {
        _skipButton.interactable = false;
        ResetWatchAdButton();

        _timer.StopTimer();
        GameOver();
    }

    private void HandleContinueButtonPressed()
    {
        _continueButton.interactable = false;
        ResetWatchAdButton();

        _timer.StopTimer();
        AdsManager.GetInstance().ShowRewarded();
    }

    private void ResetWatchAdButton()
    {
        LeanTween.cancel(_continueButton.gameObject);
        _continueButton.transform.localScale = Vector3.one;
    }

    private void GameOver()
    {
        MenuManager.GetInstance().SwitchMenu(MenuType.Gameover);
    }
}