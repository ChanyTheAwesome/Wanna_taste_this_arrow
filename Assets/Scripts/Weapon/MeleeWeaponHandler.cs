using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeWeaponHandler : WeaponHandler
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [Header("Melee Attack Info")]
    public Vector2 CollideBoxSize = Vector2.one;

    [Header("Fields for Charge Attack")]
    [SerializeField] private bool isCharge;
    [SerializeField] private GameObject ChargeCollider;
    protected override void Start()
    {
        base.Start();
        CollideBoxSize = CollideBoxSize * WeaponSize;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        ChargeCollider.SetActive(false);
        //박스 크기(기존은 1, 1)에 무기의 크기를 곱해준다.
    }

    public override void Attack()
    {
        if (!isCharge)
        {
            base.Attack();
        }
        RaycastHit2D hit = Physics2D.BoxCast(transform.position + (Vector3)Controller.LookDirection * CollideBoxSize.x, CollideBoxSize, 0, Vector2.zero, 0, target);
        //무기 위치에서, 바라보고 있는 방향과 x를 곱해줘 더하고, 지정된 박스 크기에, 각도 0, 방향 0, 거리 0(근거리라서), 레이어는 타겟이다.
        if (hit.collider != null)
        {
            ResourceController resourceController = hit.collider.GetComponent<ResourceController>();//맞은 녀석의 리소스 컨트롤러를 가져온다.
            if (resourceController != null)//있으면
            {
                if (!isCharge)
                {
                    resourceController.ChangeHealth(-Power);//체력을 깎고
                    if (IsOnKnockback)//Knockback이 켜져있으면(인스펙터에서 지정해줬다.)
                    {
                        BaseController controller = hit.collider.GetComponent<BaseController>();//맞은 녀석의 BaseController를 지정해주고
                        if (controller != null)
                        {
                            controller.ApplyKnockback(transform, KnockbackPower, KnockbackTime);//넉백을 준다.
                        }
                    }
                }
                else
                {
                    StartCoroutine(ChargeAttack());
                }
            }
        }
    }
    private IEnumerator ChargeAttack()
    {
        GameObject rootObject = transform.root.gameObject;
        BaseController controller = rootObject.GetComponent<BaseController>();
        AnimationHandler animator = controller.GetComponent<AnimationHandler>();

        Vector3 rbVelocityHolder = controller.Rigidbody.velocity;
        Debug.Log(rbVelocityHolder);
        controller.IsCharging = true;
        controller.Rigidbody.velocity = Vector3.zero;
        float navMeshSpeedHolder = Controller.gameObject.GetComponent<NavMeshAgent>().speed;
        Controller.gameObject.GetComponent<NavMeshAgent>().speed = 0;

        animator.TrueChargeAnimation();
        yield return new WaitForSeconds(1.5f);
        animator.FalseChargeAnimation();
        controller.Rigidbody.velocity = rbVelocityHolder * 1.5f;
        if(rootObject.GetComponent<EnemyController>() == null)
        {
            Debug.Log("test");
        }

        transform.gameObject.GetComponent<ChargeAttackController>().Init(controller.GetComponent<EnemyController>(), this);
        
    }
    public override void Rotate(bool isLeft)
    {
        if (isLeft)
        {
            transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);//왼쪽을 보면 y를 180도 돌리고
        }
        else
        {
            transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);//아니면 그대로 둔다.
        }
    }
}