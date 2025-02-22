using UnityEngine;

[CreateAssetMenu(menuName = "Data SO/Credit", fileName = "Credit Data")]
public class CreditDataSO : ScriptableObject
{
    [Header("Customize Credit Text :")]
    [SerializeField] [TextArea] string _devLabelText;
    [SerializeField] [TextArea] string _devText;
    [SerializeField] [TextArea] string _contactLabelText;
    [SerializeField] [TextArea] string _contactText;
    [SerializeField] [TextArea] string _sfxLabelText;
    [SerializeField] [TextArea] string _sfxText;

    public string DevLabelText => _devLabelText;
    public string DevText => _devText;
    public string ContactLabelText => _contactLabelText;
    public string ContactText => _contactText;
    public string SfxLabelText => _sfxLabelText;
    public string SfxText => _sfxText;
}
