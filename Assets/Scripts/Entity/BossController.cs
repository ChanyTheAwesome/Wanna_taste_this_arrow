using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossController : BaseController
{
    [SerializeField] private Transform target;

    [SerializeField] private float followRange = 15.0f;
    [SerializeField] private WeaponHandler[] weaponHandlers; // 여러 개의 무기를 가질 수 있음
    private int weaponIndex = 0; // 현재 무기의 인덱스

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
        Destroy(weaponHandler.gameObject); // 현재 무기를 파괴하고
        weaponHandler = Instantiate(GetRandomWeapon(), weaponPivot); // 새로 지정된 무기를 생성한다.
    }
    public void SetEnemyHealth(float multiplier)//적의 체력을 재설정한다.
    {
        statHandler.Health = (int)Mathf.Ceil(statHandler.Health * multiplier);
    }

    public void Init(Transform target)
    {
        this.target = target; //타겟은 다른 코드에서 정해줬음, 얘는 플레이어가 타겟
    }
    protected override void Update()
    {
        base.Update();
    }

    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position); //플레이어와 얘 사이의 거리 반환
    }
    protected override void Movement(Vector2 direction)
    {
        base.Movement(direction);
        animationhandler.Move(direction); // 이동 애니메이션 키세요 라는 뜻
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
        characterRenderer.flipX = isLeft;//90도 넘나요? 예-> 왼쪽 보셈 아니오-> 오른쪽 보셈
    }
    protected Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized; //플레이어와 얘 사이의 방향 반환
    }
    private WeaponHandler GetRandomWeapon()
    {
        weaponIndex = Random.Range(0, weaponHandlers.Length); // 무기 핸들러 배열에서 랜덤으로 인덱스 선택
        return weaponHandlers[weaponIndex];
    }
    protected override void HandleAction()
    {
        base.HandleAction();
        movementDirection = DirectionToTarget();
        if (weaponHandler == null || target == null) //무기 없어요? 타겟 없어요?
        {
            if (!movementDirection.Equals(Vector2.zero))
            {
                movementDirection = Vector2.zero;//움직이지 마세요~
            }
            return;
        }
        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        isAttacking = false;

        //weaponHandler = GetRangomWeapon(); // 무기를 랜덤으로 선택

        if (distance <= followRange)//최대 쫓아가기 거리보다 가깝다면
        {
            lookDirection = direction;//바라보는 방향 정해줌
        }
        if (distance < weaponHandler.AttackRange)//공격 사거리에 들어왔으면
        {
            int layerMaskTarget = weaponHandler.target;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, weaponHandler.AttackRange * 1.5f, (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);
            //1 << LayerMask.NameToLayer("Level") 이건 무슨 뜻이냐? 간단히 말하면, Level이라는 친구의 레이어 "비트" 값을 보내주고 있는 것이다.
            if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
            {//맞은 것의 콜라이더가 있고, 무기 든 놈의 타겟이 진짜 맞은 애랑 "같다면" 
                isAttacking = true;//공격하세용
            }
            movementDirection = Vector2.zero;//움직임 방향을 0으로 만든 다음
        }
        movementDirection = direction;//다시 움직임 방향을 바라보는 방향으로 정해준다.
    }
    public override void Death()
    {
        Destroy(this.gameObject);
        DungeonManager.Instance.CheckClearStage();
    }
}
