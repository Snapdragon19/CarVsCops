using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour
{
    [SerializeField] Transform _dangerSign;
    [SerializeField] float _offset;

    Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo))
        {
            Vector3 camDir = (hitInfo.point - _cam.transform.position).normalized;
            Vector3 signPos = _cam.transform.position + (camDir * _offset);
            Vector3 signLocalPos = _dangerSign.localPosition;
        }
    }
}
