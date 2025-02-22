using UnityEngine;
using UnityEngine.UI;

public class ExitMenu : Menu
{
    [Header("Inherit Variable :")]
    [SerializeField] private Button _yesButton;
    [SerializeField] private Button _noButton;

    public override void SetEnable()
    {
        base.SetEnable();

        _noButton.interactable = true;
    }

    private void Start()
    {
        OnButtonPressed(_yesButton, HandleYesButtonPressed);
        OnButtonPressed(_noButton, HandleNoButtonPressed);
    }

    private void HandleYesButtonPressed()
    {
        Application.Quit();
    }

    private void HandleNoButtonPressed()
    {
        _noButton.interactable = false;

        MenuManager.GetInstance().CloseMenu();
    }
}
