using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class ChargeAttackController : MonoBehaviour
{
    EnemyController _enemyController;
    MeleeWeaponHandler _weaponHandler;
    private LayerMask _target;
    public void Init(EnemyController enemy, MeleeWeaponHandler melee)
    {
        Debug.Log("Test");
        _enemyController = enemy;
        _weaponHandler = melee;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit!");
        if (_target.value == (_target.value | (1 << collision.gameObject.layer)))
        {
            StartCoroutine(StruckToPlayer(collision));
        }
        else 
        {
            StartCoroutine(StruckToNotPlayer());
        }
    }
    private IEnumerator StruckToNotPlayer()
    {
        _enemyController.Rigidbody.velocity = Vector3.zero;
        yield return new WaitForSeconds(3);
        _enemyController.IsCharging = false;
    }
    private IEnumerator StruckToPlayer(Collider2D collision)
    {
        _enemyController.Rigidbody.velocity = Vector3.zero;
        ResourceController resourceController = collision.GetComponent<ResourceController>();
        if (resourceController != null)
        {
            resourceController.ChangeHealth(-_weaponHandler.Power);//������ �Ŀ���ŭ ü���� ���
            if (_weaponHandler.IsOnKnockback)//�˹��� �Ǵ� ������
            {
                BaseController controller = collision.GetComponent<BaseController>();
                if (controller != null)
                {
                    controller.ApplyKnockback(transform, _weaponHandler.KnockbackPower, _weaponHandler.KnockbackTime);//�˹��� �ش�.
                }
            }
        }
        yield return new WaitForSeconds(1.5f);
        _enemyController.IsCharging = false;
    }
}