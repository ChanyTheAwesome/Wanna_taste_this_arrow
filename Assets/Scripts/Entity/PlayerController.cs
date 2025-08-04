using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : BaseController
{
    // 플레이어는 이동과 공격을 함
    // 이동 중에는 공격 x, 이동을 멈추면 공격을 함
    // 이동 중에 키로 flipX 조작, 멈추고 공격할 때는 적을 찾아서 회전을 함
    // 
    // 각각 상태에 따른 애니메이션
    // 공격 - 자동 - 일정 범위 내의 가까운 적 찾기 - 장애물이 있다면 다른 적 찾기
    // 적의 위치에 따른 flipX 세팅
    // 적이 존재 할경우 attack = true => 투사체 발사 시작

    // 그 후에 투사체 개수, 등등 능력 구현

    private GameManager gameManager;

    protected override void Awake()
    {
        base.Awake();
        if(GameManager.Instance.player == null)
        {
            GameManager.Instance.player = this.gameObject;
        }

        //PlayerManager.Instance._playerController = this;    //테스트로 _playerController, 끝나면 PlayerController로 바꿔야함
        
        //PlayerManager.Instance.nowAnim = this.GetComponentInChildren<Animator>();
    }
    private static int _maxHp;
    private static float _currentHp;
    private static int _numberOfPojectiles = 1;
    private static bool _reverse = false;
    private static float moveSpeed;
    private static float projectileSpeed;
    private static float delay;
    private static float power;

    private void Start()
    {
        PlayerManager.Instance.SetCharacter();  // 캐릭터 외형 변경
        if (DungeonManager.Instance.CurrentDungeonID == 2)
        {
            Debug.Log("머티리얼 설정 시도는 함"); // 테스트용
            this.GetComponentInChildren<SpriteRenderer>().material = DungeonManager.Instance.CaveMaterial;
        }

        _maxHp = 100;
        _currentHp = _maxHp;
        moveSpeed = statHandler.Speed;
        projectileSpeed = weaponHandler.Speed;
        delay = weaponHandler.Delay;
        power = weaponHandler.Power;
    }

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        
    }

    [SerializeField] private float findRadius = 10f;  // 감지범위
    [SerializeField] private LayerMask enemyLayer; // 에너미 레이어
    [SerializeField] private LayerMask levelLayer;
    
    private GameObject _target;                      // 공격할 타겟

    protected override void HandleAction()
    {
        _currentHp = resource.CurrentHealth;

        UpdateHpBar();
        OnMove();
        // 장애물에 안걸리는 에너미 찾아서 그쪽 방향으로 바라보기
        OnLook();
        // 공격 
        FindNearestEnemy();
    }

    public override void Death()
    {
        base.Death();
        DungeonManager.Instance.GameOver();
        //gameManager.GameOver();
    }

    void OnMove()
    {
        // 키입력 방향키 or wasd
        // 이동 - 노멀라이즈 -> 부모moveDirection값 전달
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(horizontal, vertical).normalized;
        animationhandler.Move(movementDirection);
        // 멈춰 있을 때 공격한다.

        if (movementDirection.x == 0 && movementDirection.y == 0) isAttacking = _target != null;
        else isAttacking = false;
    }

    void OnLook()
    {
        // 지금 공격중이 아니거나 타겟이 없다면 리턴
        Vector2 targetPos;

        if (!isAttacking) return;
        if (!_target ) return;

        targetPos = _target.transform.position;

        if (movementDirection.magnitude > 0)
        {
            lookDirection = movementDirection;
        }
        else
        {
            lookDirection = (targetPos - (Vector2)transform.position);//빼본다

            if (lookDirection.magnitude < 0.9f)
            {
                lookDirection = Vector2.zero;
            }
            else
            {
                lookDirection = lookDirection.normalized;//이것으로 바라보는 방향의 벡터를 지정한다.
            }
        }
    }

    // 범위 내에 가까운적 찾기 -> 타겟 설정
    void FindNearestEnemy()
    {
        // 설정한 범위 내에 콜라이더가 에너미 레이어라면 모아서 배열로 만든다
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, findRadius, enemyLayer);

        // 배열의 길이가 0이라면 타겟은 없앤후 리턴
        if (colliders.Length == 0)
        {
            _target = null;
            return;
        }

        float minDistance = Mathf.Infinity;

        // 반복으로 배열안에 에너미의 거리 계산 후 가장 가까운 것을 타겟으로 설정
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.layer == gameObject.layer) continue;

            float distance = Vector2.Distance(transform.position, collider.transform.position);

            RaycastHit2D hit = Physics2D.Linecast(transform.position, collider.transform.position, levelLayer);

            if (hit.collider != null) continue;


            if (distance < minDistance && distance < findRadius)
            {
                minDistance = distance;
                _target = collider.gameObject;
            }
        }
    }

    public void UpdateHpBar()
    {
        if (hpSlider != null) hpSlider.fillAmount = _currentHp / _maxHp;
    }

    // 감지 범위 그리기 - 나중에 지워도 되는 부분
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, findRadius);
    }

    public void ApplyAbility(string abilityName)
    {
        switch (abilityName)
        {
            case "와~":
                RecoveryHp();
                break;
            case "와!!!":
                MaxHpUp();
                break;
            case "나비처럼 날기":
                MoveSpeedUp();
                break;
            case "급하다 급해":
                AttackSpeedUp();
                break;
            case "삽 부스트":
                ProjectileSpeedUp();
                break;
            case "벌처럼 쏘기":
                AttackPowerUp();
                break;
            case "쌉":
                TripleShot();
                break;
            case "샵":
                PenetrationShot();
                break;
            case "벽 반삽":
                ReflectShot();
                break;
            case "후방주의":
                BackShot();
                break;
            case "왜 이러시는 건데요":
                Ricochet();
                break;
        }
    }


    // 최대 체력 증가
    public void MaxHpUp()
    {
        if (statHandler == null) return;
        statHandler.Health += 200;
        _maxHp = statHandler.Health;
    }

    // 이동 속도 증가
    public void MoveSpeedUp()
    {
        if (statHandler == null) return;
        moveSpeed += 10f;
        statHandler.Speed = moveSpeed;
    }

    // 공격 속도
    public void AttackSpeedUp()
    {
        if (weaponHandler == null) return;
        delay -= 0.8f;
        weaponHandler.Delay = delay;
    }

    // 발사체 속도 증가
    public void ProjectileSpeedUp()
    {
        if (weaponHandler == null) return;
        projectileSpeed += 10f;
        weaponHandler.Speed = projectileSpeed;
    }

    // 공격력 증가
    public void AttackPowerUp()
    {
        if (weaponHandler == null) return;
        power += 10f;
        weaponHandler.Power = power;
    }

    // 한번에 세개
    public void TripleShot()
    {
        if (weaponHandler == null) return;
        _numberOfPojectiles++;
        RangeWeaponHandler weapon = weaponHandler.GetComponent<RangeWeaponHandler>();
        weapon.NumberofProjectilesPerShot = _numberOfPojectiles;
        weapon.MultipleProjectileAngle = 30f;
    }

    //관통
    public void PenetrationShot()
    {
        // 여기서 관통을 On 해주자
        ProjectileManager.Instance.Penetrate = true;
    }

    // 튕기기
    public void ReflectShot()
    {
        // 반사 on -> ProjectijleController 에 벽이랑 부딪혔을 시 반사되게끔 구현

        ProjectileManager.Instance.Reflect = true;
    }

    // 후방화살
    public void BackShot()
    {
        if (weaponHandler == null) return;
        _reverse = true;
        RangeWeaponHandler weapon = weaponHandler.GetComponent<RangeWeaponHandler>();
        weapon.Reverse = _reverse;
    }

    // 도탄
    public void Ricochet()
    {
        ProjectileManager.Instance.Ricochet = true;
    }

    // 회복?
    public void RecoveryHp()
    {
        _currentHp += 20;
        if(_maxHp > _currentHp) return;
        else _currentHp = _maxHp;
    }

    public void NextStageEntryInitailize()
    {
        RangeWeaponHandler weapon = weaponHandler.GetComponent<RangeWeaponHandler>();
        weapon.NumberofProjectilesPerShot = _numberOfPojectiles;
        weapon.MultipleProjectileAngle = 30f;
        weapon.Reverse = _reverse;

        statHandler.Speed = moveSpeed;
        weaponHandler.Speed = projectileSpeed;
        weaponHandler.Delay = delay;
        weaponHandler.Power = power;
    }

    public void FirstStageAbilityInit()
    {
        _numberOfPojectiles = 1;
        RangeWeaponHandler weapon = weaponHandler.GetComponent<RangeWeaponHandler>();
        weapon.NumberofProjectilesPerShot = _numberOfPojectiles;
        weapon.MultipleProjectileAngle = 0f;
        _reverse = false;
        weapon.Reverse = _reverse;

        _maxHp = statHandler.Health;
        _currentHp = _maxHp;
        moveSpeed = statHandler.Speed;
        projectileSpeed = weaponHandler.Speed;
        delay = weaponHandler.Delay;
        power = weaponHandler.Power;
        ProjectileManager.Instance.Reflect = false;
        ProjectileManager.Instance.Penetrate = false;
        ProjectileManager.Instance.Ricochet = false;
    }
}
