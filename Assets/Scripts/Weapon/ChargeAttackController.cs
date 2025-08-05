using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;

public class ChargeAttackController : MonoBehaviour
{
    BaseController _baseController;
    MeleeWeaponHandler _weaponHandler;
    [SerializeField] private LayerMask levelCollisionLayer;
    private LayerMask _target;
    public void Init(BaseController controller, MeleeWeaponHandler melee)
    {
        _baseController = controller;
        _weaponHandler = melee;
        _target = _weaponHandler.target;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_target.value == (_target.value | (1 << collision.gameObject.layer)))
        {
            StartCoroutine(StruckToPlayer(collision));
        }
        else if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            StartCoroutine(StruckToNotPlayer());
        }
    }
    private IEnumerator StruckToNotPlayer()
    {
        _baseController.Rigidbody.velocity = Vector3.zero;
        float navMeshSpeedHolder = _baseController.gameObject.GetComponent<NavMeshAgent>().speed;

        _baseController.gameObject.GetComponent<NavMeshAgent>().speed = 0;
        yield return new WaitForSeconds(3);
        if (_baseController.IsCharging)
        {
            _baseController.IsCharging = false;
            _baseController.gameObject.GetComponent<NavMeshAgent>().speed = navMeshSpeedHolder;
        }
        if(_baseController is BossController bossController)
        {
            bossController.ChangeWeapon();
        }
    }
    private IEnumerator StruckToPlayer(Collider2D collision)
    {
        _baseController.Rigidbody.velocity = Vector3.zero;
        float navMeshSpeedHolder = _baseController.gameObject.GetComponent<NavMeshAgent>().speed;
        _baseController.gameObject.GetComponent<NavMeshAgent>().speed = 0;
        ResourceController resourceController = collision.GetComponent<ResourceController>();
        if (resourceController != null)
        {
            resourceController.ChangeHealth(-_weaponHandler.Power);//무기의 파워만큼 체력을 깎고
            if (_weaponHandler.IsOnKnockback)//넉백이 되는 무기라면
            {
                BaseController controller = collision.GetComponent<BaseController>();
                if (controller != null)
                {
                    controller.ApplyKnockback(transform, _weaponHandler.KnockbackPower, _weaponHandler.KnockbackTime);//넉백을 준다.
                }
            }
        }
        yield return new WaitForSeconds(1.5f);
        _baseController.IsCharging = false;
        _baseController.gameObject.GetComponent<NavMeshAgent>().speed = navMeshSpeedHolder;
        if (_baseController is BossController bossController)
        {
            bossController.ChangeWeapon();
        }
    }
}