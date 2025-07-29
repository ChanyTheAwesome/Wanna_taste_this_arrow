using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponHandler : WeaponHandler
{
    [Header("Melee Attack Info")]
    public Vector2 CollideBoxSize = Vector2.one;

    protected override void Start()
    {
        base.Start();
        CollideBoxSize = CollideBoxSize * WeaponSize;
        //�ڽ� ũ��(������ 1, 1)�� ������ ũ�⸦ �����ش�.
    }

    public override void Attack()
    {
        base.Attack();
        RaycastHit2D hit = Physics2D.BoxCast(transform.position + (Vector3)Controller.LookDirection * CollideBoxSize.x, CollideBoxSize, 0, Vector2.zero, 0, target);
        //���� ��ġ����, �ٶ󺸰� �ִ� ����� x�� ������ ���ϰ�, ������ �ڽ� ũ�⿡, ���� 0, ���� 0, �Ÿ� 0(�ٰŸ���), ���̾�� Ÿ���̴�.
        if (hit.collider != null)
        {
            ResourceController resourceController = hit.collider.GetComponent<ResourceController>();//���� �༮�� ���ҽ� ��Ʈ�ѷ��� �����´�.
            if(resourceController != null)//������
            {
                resourceController.ChangeHealth(-Power);//ü���� ���
                if (IsOnKnockback)//Knockback�� ����������(�ν����Ϳ��� ���������.)
                {
                    BaseController controller = hit.collider.GetComponent<BaseController>();//���� �༮�� BaseController�� �������ְ�
                    if(controller != null)
                    {
                        controller.ApplyKnockback(transform, KnockbackPower, KnockbackTime);//�˹��� �ش�.
                    }
                }
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
