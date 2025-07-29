using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    private float camMoveDelay = 5f;
    private Vector3 currentPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentPos = transform.position;
        Vector3 targetPos = target.position;

        targetPos.z = -10;

        transform.position = Vector3.Lerp(currentPos, targetPos, camMoveDelay * Time.deltaTime);
    }
}
