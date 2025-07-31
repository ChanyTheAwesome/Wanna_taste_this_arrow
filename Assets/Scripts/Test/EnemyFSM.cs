using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] public LayerMask targetLayer;

    public bool _isCloseToPlayer;
    private void Start()
    {
        _isCloseToPlayer = false;
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.SetDestination(target.transform.position);
    }

    private void Update()
    {
        if(target == null)
        {
            return;
        }
        if(!_isCloseToPlayer)
        {
            navMeshAgent.SetDestination(target.transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if(TargetLayerCheck(go))
        {
            _isCloseToPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (TargetLayerCheck(go))
        {
            _isCloseToPlayer = false;
        }
    }

    private bool TargetLayerCheck(GameObject go)
    {
        return targetLayer.value == (targetLayer.value | (1 << go.layer));
    }
}