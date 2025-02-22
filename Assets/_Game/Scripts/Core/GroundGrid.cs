using UnityEngine;

public class GroundGrid : MonoBehaviour
{
    [SerializeField] int _width;
    [SerializeField] int _height;
    [SerializeField] int _groundSize;
    [SerializeField] GameObject _groundPrefab;
    [SerializeField] Transform _target;

    int _leftId = 0;
    int _topId = 0;
    Vector3 _midPosition;

    GameObject[,] _groundList;

    private void Awake()
    {
        _groundList = new GameObject[_width, _height];
    }

    private void Start()
    {
        GenerateGround();
    }

    private void Update()
    {
        CalculateGroundPosition();
    }

    private void CalculateGroundPosition()
    {
        int LIndex = (int)Mathf.Repeat(_leftId, _width);
        int RIndex = (int)Mathf.Repeat(_leftId + (_width - 1), _width);

        int TIndex = (int)Mathf.Repeat(_topId, _height);
        int BIndex = (int)Mathf.Repeat(_topId + (_height - 1), _height);

        _midPosition.x = (_groundList[LIndex, TIndex].transform.position.x + _groundList[RIndex, BIndex].transform.position.x) / 2;
        _midPosition.z = (_groundList[LIndex, TIndex].transform.position.z + _groundList[RIndex, BIndex].transform.position.z) / 2;

        MoveGrid(LIndex, RIndex, TIndex, BIndex);
    }

    private void MoveGrid(int LIndex, int RIndex, int TIndex, int BIndex)
    {
        if (_target.position.x > (_midPosition.x + _groundSize / 2))
        {
            for (int z = 0; z < _height; z++)
            {
                Vector3 tmpPosition = _groundList[LIndex, z].transform.position;
                tmpPosition.x += (_groundSize * _width);
                _groundList[LIndex, z].transform.position = tmpPosition;
            }

            _leftId++;
        }
        else if ((_target.position.x < (_midPosition.x - _groundSize / 2)))
        {
            for (int z = 0; z < _height; z++)
            {
                Vector3 tmpPosition = _groundList[RIndex, z].transform.position;
                tmpPosition.x -= (_groundSize * _width);
                _groundList[RIndex, z].transform.position = tmpPosition;
            }

            _leftId--;
        }

        if (_target.position.z > (_midPosition.z + _groundSize / 2))
        {
            for (int x = 0; x < _width; x++)
            {
                Vector3 tmpPosition = _groundList[x, TIndex].transform.position;
                tmpPosition.z += (_groundSize * _height);
                _groundList[x, TIndex].transform.position = tmpPosition;
            }

            _topId++;
        }
        else if ((_target.position.z < (_midPosition.z - _groundSize / 2)))
        {
            for (int x = 0; x < _width; x++)
            {
                Vector3 tmpPosition = _groundList[x, BIndex].transform.position;
                tmpPosition.z -= (_groundSize * _height);
                _groundList[x, BIndex].transform.position = tmpPosition;
            }

            _topId--;
        }
    }

    void GenerateGround()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _height; z++)
            {
                Vector3 pos = new Vector3(x * _groundSize, 0, z * _groundSize);
                _groundList[x, z] = Instantiate(_groundPrefab, pos, Quaternion.identity);
                _groundList[x, z].transform.SetParent(transform);
                _groundList[x, z].name = $"Ground [{x}, {z}]";
            }
        }
    }
}
