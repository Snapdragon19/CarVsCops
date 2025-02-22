using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] float _rotationSpeed;

    bool _isDeath;

    public static event Action OnPlayerDeath;

    private void Update()
    {
        if (_isDeath) return;

        Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _isDeath = true;
            gameObject.SetActive(false);
            OnPlayerDeath?.Invoke();
        }
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
    }

    public void LeftTurn()
    {
        transform.Rotate(Vector3.up * -_rotationSpeed * Time.deltaTime);
    }

    public void RightTurn()
    {
        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
    }

    public void Revive()
    {
        _isDeath = false;
        gameObject.SetActive(true);
    }
}
