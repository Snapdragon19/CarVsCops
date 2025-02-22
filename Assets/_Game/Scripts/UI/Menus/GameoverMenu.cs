using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameoverMenu : Menu
{
    [Header("Inherit Variable :")]
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _homeButton;

    MenuManager _menuController;

    protected override void Awake()
    {
        base.Awake();

        _menuController = MenuManager.GetInstance();
    }

    private void Start()
    {
        HandleButtonPressed();
    }

    private void HandleButtonPressed()
    {
        OnButtonPressed(_restartButton, () =>
        {
            StartCoroutine(LevelLoader.ReloadLevelAsync(ReloadLevel));
        });

        OnButtonPressed(_homeButton, () =>
        {
            StartCoroutine(LevelLoader.ReloadLevelAsync(HandleLevelLoaded));
        });
    }

    private void ReloadLevel()
    {
        _menuController.CloseMenu();
        _menuController.SwitchMenu(MenuType.Gameplay);
        FindObjectOfType<GameManager>().IsPlaying(true);
    }

    private void HandleLevelLoaded()
    {
        _menuController.CloseMenu();
        _menuController.SwitchMenu(MenuType.Main);
    }
}