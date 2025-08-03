using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class EnemyController : BaseController
{
    private EnemyManager enemyManager;
    [SerializeField] private Transform target; // EnemyManager 제작 후 [SerializeField] 빼야됨
    [SerializeField] private float followRange = 15.0f;

    public void SetEnemyHealth(float multiplier)//적의 체력을 재설정한다.
    {
        statHandler.Health = (int)Mathf.Ceil(statHandler.Health * multiplier);
    }

    public void Init(EnemyManager enemyManager,Transform target)
    {
        this.enemyManager = enemyManager;
        this.target = target; //타겟은 다른 코드에서 정해줬음, 얘는 플레이어가 타겟
        if (DungeonManager.Instance.CurrentDungeonID == 2)   // 동굴 던전이면
            this.GetComponentInChildren<SpriteRenderer>().material = DungeonManager.Instance.CaveMaterial;  // Material 속성 변경
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

    protected override void Rotate(Vector2 direction)
    {
        base.Rotate(direction);
        characterRenderer.flipX = isLeft;//90도 넘나요? 예-> 왼쪽 보셈 아니오-> 오른쪽 보셈
    }
    protected Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized; //플레이어와 얘 사이의 방향 반환
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
        base.Death();
        if (!DungeonManager.Instance.CheckBossStage())
        {
            PlayerManager.Instance.GetEXP(Random.Range(8, 11)); // 적 죽을 때 8에서 10사이의 경험치 얻음
        }
        enemyManager.RemoveEnemyOnDeath(this);
    }
}