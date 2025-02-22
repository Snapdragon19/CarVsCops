using UnityEngine;

[CreateAssetMenu(menuName = "Game Database/New Game Data")]
public class GameData : ScriptableObject
{
    [Header("Cops Data :")]
    [SerializeField] CopData[] _cops;

    public CopData[] Cops => _cops;
}

[System.Serializable]
public class CopData
{
    public float _moveSpeed;
    public float _rotationSpeed;
    public LayerMask _mask;
}
