using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreditMenu : Menu
{
    [Header("Inherit Variable :")]
    [SerializeField] private Button _backButton;
    [SerializeField] private TMP_Text _devLabelText;
    [SerializeField] private TMP_Text _devText;
    [SerializeField] private TMP_Text _contactLabelText;
    [SerializeField] private TMP_Text _contactText;
    [SerializeField] private TMP_Text _sfxLabelText;
    [SerializeField] private TMP_Text _sfxText;

    [Header("Game Database :")]
    [SerializeField] private CreditDataSO _data;

    public override void SetEnable()
    {
        base.SetEnable();

        _backButton.interactable = true;
    }

    private void Start()
    {
        OnButtonPressed(_backButton, HandleBackButtonPressed);

        _devText.text = _data.DevText;
        _devLabelText.text = _data.DevLabelText;
        _contactText.text = _data.ContactText;
        _contactLabelText.text = _data.ContactLabelText;
        _sfxText.text = _data.SfxText;
        _sfxLabelText.text = _data.SfxLabelText;
    }

    private void HandleBackButtonPressed()
    {
        _backButton.interactable = false;

        MenuManager.GetInstance().CloseMenu();
    }
}
