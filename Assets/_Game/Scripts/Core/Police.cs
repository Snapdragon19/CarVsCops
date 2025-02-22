using System;
using UnityEngine;

public class Police : MonoBehaviour
{
    [SerializeField] GameData _data;
    [SerializeField] int _copIndex;
    [SerializeField] AudioSource _sirenSource;

    Transform _target;
    GameManager _gameManager;
    CopData _cop;
    GameObject _dangerSign;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _target = FindObjectOfType<PlayerMovement>().transform;
        _cop = _data.Cops[_copIndex];
    }

    private void Start()
    {
        _sirenSource.clip = SoundManager.GetInstance().GetClip(AudioType.SIREN);
        _sirenSource.Play();
    }

    private void Update()
    {
        if (!_target) return;

        LookAtTarget();
        Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_target) return;

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            _gameManager.PlayExplosionFx(collision.GetContact(0).point);

            if (!collision.gameObject.CompareTag("Enemy")) return;

            int score = (_copIndex + 1) * 5;
            _gameManager.AddScore(score);
        }
    }

    private void LookAtTarget()
    {
        if (!_target.gameObject.activeSelf)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = (_target.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        Quaternion r = Quaternion.AngleAxis(angle, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, r, _cop._rotationSpeed * Time.deltaTime);
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * _cop._moveSpeed * Time.deltaTime);
    }
}
