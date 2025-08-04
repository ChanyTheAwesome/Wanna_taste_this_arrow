using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    [SerializeField]
    private float _camMoveDelay = 5f;
    private Vector3 _currentPos;

    void Update()
    {
        if (target == null) return;
        _currentPos = transform.position;
        Vector3 targetPos = target.position;

        targetPos.z = -10;

        transform.position = Vector3.Lerp(_currentPos, targetPos, _camMoveDelay * Time.deltaTime);
    }
}
