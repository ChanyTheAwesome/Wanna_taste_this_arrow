using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossController : BaseController
{
    [SerializeField] private Transform target;

    [SerializeField] private float followRange = 15.0f;
    [SerializeField] private WeaponHandler[] weaponHandlers; // ���� ���� ���⸦ ���� �� ����
    private int weaponIndex = 0; // ���� ������ �ε���

    protected override void Awake()
    {
        if(WeaponPrefab == null)
        {
            WeaponPrefab = GetRandomWeapon();
        }
        base.Awake();
    }
    public void ChangeWeapon()
    {
        Destroy(weaponHandler.gameObject); // ���� ���⸦ �ı��ϰ�
        weaponHandler = Instantiate(GetRandomWeapon(), weaponPivot); // ���� ������ ���⸦ �����Ѵ�.
    }
    public void SetEnemyHealth(float multiplier)//���� ü���� �缳���Ѵ�.
    {
        statHandler.Health = (int)Mathf.Ceil(statHandler.Health * multiplier);
    }

    public void Init(Transform target)
    {
        this.target = target; //Ÿ���� �ٸ� �ڵ忡�� ��������, ��� �÷��̾ Ÿ��
    }
    protected override void Update()
    {
        base.Update();
    }

    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position); //�÷��̾�� �� ������ �Ÿ� ��ȯ
    }
    protected override void Movement(Vector2 direction)
    {
        base.Movement(direction);
        animationhandler.Move(direction); // �̵� �ִϸ��̼� Ű���� ��� ��
    }
    protected override void HandleAttackDelay()
    {
        if (weaponHandler == null)
        {
            return;
        }
        if (timeSincelastAttack <= weaponHandler.Delay && !IsCharging)
        {
            timeSincelastAttack += Time.deltaTime;
        }
        if (isAttacking && timeSincelastAttack > weaponHandler.Delay && !IsCharging)
        {
            timeSincelastAttack = 0.0f;
            Attack();
            if(weaponHandler is MeleeWeaponHandler meleeweapon)
            {
                if(meleeweapon.IsCharge)
                {
                    return;
                }
            }
            ChangeWeapon();
        }
    }
    protected override void Rotate(Vector2 direction)
    {
        base.Rotate(direction);
        characterRenderer.flipX = isLeft;//90�� �ѳ���? ��-> ���� ���� �ƴϿ�-> ������ ����
    }
    protected Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized; //�÷��̾�� �� ������ ���� ��ȯ
    }
    private WeaponHandler GetRandomWeapon()
    {
        weaponIndex = Random.Range(0, weaponHandlers.Length); // ���� �ڵ鷯 �迭���� �������� �ε��� ����
        return weaponHandlers[weaponIndex];
    }
    protected override void HandleAction()
    {
        base.HandleAction();
        movementDirection = DirectionToTarget();
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

        //weaponHandler = GetRangomWeapon(); // ���⸦ �������� ����

        if (distance <= followRange)//�ִ� �Ѿư��� �Ÿ����� �����ٸ�
        {
            lookDirection = direction;//�ٶ󺸴� ���� ������
        }
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
    public override void Death()
    {
        Destroy(this.gameObject);
        DungeonManager.Instance.CheckClearStage();
    }
}
