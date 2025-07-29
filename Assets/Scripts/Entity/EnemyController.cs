using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class EnemyController : BaseController
{
    private EnemyManager enemyManager;
    private Transform target;
    [SerializeField] private float followRange = 15.0f;

    public void Init(EnemyManager enemyManager, Transform target)
    {
        this.enemyManager = enemyManager;
        this.target = target; //Ÿ���� �ٸ� �ڵ忡�� ��������, ��� �÷��̾ Ÿ��
    }

    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position); //�÷��̾�� �� ������ �Ÿ� ��ȯ
    }

    protected Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized; //�÷��̾�� �� ������ ���� ��ȯ
    }

    protected override void HandleAction()
    {
        base.HandleAction();
        if (weaponHandler == null || target == null) //���� �����? Ÿ�� �����?
        {
            if (!movementDirection.Equals(Vector2.zero))
            {
                movementDirection = Vector2.zero;//�������� ������~
            }
            return;
        }
        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        isAttacking = false;

        if (distance <= followRange)//�ִ� �Ѿư��� �Ÿ����� �����ٸ�
        {
            lookDirection = direction;//�ٶ󺸴� ���� ������

            if (distance < weaponHandler.AttackRange)//���� ��Ÿ��� ��������
            {
                int layerMaskTarget = weaponHandler.target;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, weaponHandler.AttackRange * 1.5f, (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);
                //1 << LayerMask.NameToLayer("Level") �̰� ���� ���̳�? ������ ���ϸ�, Level�̶�� ģ���� ���̾� "��Ʈ" ���� �����ְ� �ִ� ���̴�.
                if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                {//���� ���� �ݶ��̴��� �ְ�, ���� �� ���� Ÿ���� ��¥ ���� �ֶ� "���ٸ�" 
                    isAttacking = true;//�����ϼ���
                }
                movementDirection = Vector2.zero;//������ ������ 0���� ���� ����
            }
            movementDirection = direction;//�ٽ� ������ ������ �ٶ󺸴� �������� �����ش�.
        }
    }
    public override void Death()
    {
        base.Death();
        enemyManager.RemoveEnemyOnDeath(this);
    }
}
