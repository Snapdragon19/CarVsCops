using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Range(0, 1)] [SerializeField] float _smoothTime = 0.45f;

    Transform _target;
    Vector3 _offset;
    Vector3 _currentVelocity;

    private void Awake()
    {
        _target = FindObjectOfType<PlayerMovement>().transform;
    }

    private void Start()
    {
        if (!_target) return;

        _offset = transform.position - _target.position;
    }

    private void LateUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        Vector3 targetPosition = _target.position + _offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, _smoothTime);
    }
}
