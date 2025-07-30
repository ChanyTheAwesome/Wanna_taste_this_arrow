using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] public LayerMask targetLayer;

    public bool _isCloseToPlayer;
    private void Start()
    {
        _isCloseToPlayer = false;
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        _navMeshAgent.SetDestination(target.transform.position);
    }

    private void Update()
    {
        if(target == null)
        {
            Debug.Log("test");
            return;
        }
        if(_isCloseToPlayer)
        {

        }
        else
        {
            _navMeshAgent.SetDestination(target.transform.position);
        }
        Debug.Log(_isCloseToPlayer + "," + this.gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(targetLayer.value == (targetLayer.value | (1 << collision.gameObject.layer)))
        {
            _isCloseToPlayer = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (targetLayer.value == (targetLayer.value | (1 << collision.gameObject.layer)))
        {
            _isCloseToPlayer = false;
        }
    }
}