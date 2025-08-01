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
        if (isCharge)
        {
            ChargeCollider.SetActive(false);
        }
        //�ڽ� ũ��(������ 1, 1)�� ������ ũ�⸦ �����ش�.
    }

    public override void Attack()
    {
        if (!isCharge)
        {
            base.Attack();
        }
        RaycastHit2D hit = Physics2D.BoxCast(transform.position + (Vector3)Controller.LookDirection * CollideBoxSize.x, CollideBoxSize, 0, Vector2.zero, 0, target);
        //���� ��ġ����, �ٶ󺸰� �ִ� ����� x�� ������ ���ϰ�, ������ �ڽ� ũ�⿡, ���� 0, ���� 0, �Ÿ� 0(�ٰŸ���), ���̾�� Ÿ���̴�.
        if (hit.collider != null)
        {
            ResourceController resourceController = hit.collider.GetComponent<ResourceController>();//���� �༮�� ���ҽ� ��Ʈ�ѷ��� �����´�.
            if (resourceController != null)//������
            {
                if (!isCharge)
                {
                    resourceController.ChangeHealth(-Power);//ü���� ���
                    if (IsOnKnockback)//Knockback�� ����������(�ν����Ϳ��� ���������.)
                    {
                        BaseController controller = hit.collider.GetComponent<BaseController>();//���� �༮�� BaseController�� �������ְ�
                        if (controller != null)
                        {
                            controller.ApplyKnockback(transform, KnockbackPower, KnockbackTime);//�˹��� �ش�.
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

        controller.IsCharging = true;
        controller.Rigidbody.velocity = Vector3.zero;
        float navMeshSpeedHolder = Controller.gameObject.GetComponent<NavMeshAgent>().speed;
        Controller.gameObject.GetComponent<NavMeshAgent>().speed = 0;

        animator.TrueChargeAnimation();
        yield return new WaitForSeconds(1.5f);
        animator.FalseChargeAnimation();
        if (controller is BossController bossController1)
        {
            bossController1.Rigidbody.velocity = controller.LatestDirection * 50.0f;
        }
        else
        {
            controller.Rigidbody.velocity = controller.LatestDirection * 3.0f;
        }
        Controller.gameObject.GetComponent<NavMeshAgent>().speed = navMeshSpeedHolder;
        ChargeCollider.gameObject.SetActive(true);
        ChargeCollider.GetComponent<ChargeAttackController>().Init(controller.GetComponent<BaseController>(), this);
        yield return new WaitForSeconds(8);
        if (controller.IsCharging)
        {
            controller.IsCharging = false;
            if (controller is BossController bossController2)
            {
                bossController2.ChangeWeapon();
            }
        }
    }
    public override void Rotate(bool isLeft)
    {
        if (isLeft)
        {
            transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);//������ ���� y�� 180�� ������
        }
        else
        {
            transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);//�ƴϸ� �״�� �д�.
        }
    }
}