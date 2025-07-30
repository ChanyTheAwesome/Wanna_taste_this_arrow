using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    // 미쳐버리겠다.
    // 플레이어는 이동과 공격을 함
    // 이동 중에는 공격 x, 이동을 멈추면 공격을 함
    // 이동 중에 키로 flipX 조작, 멈추고 공격할 때는 적을 찾아서 회전을 함
    // 
    // 각각 상태에 따른 애니메이션
    // 공격 - 자동 - 일정 범위 내의 가까운 적 찾기 - 장애물이 있다면 다른 적 찾기
    // 적의 위치에 따른 flipX 세팅
    // 적이 존재 할경우 attack = true => 투사체 발사 시작

    // 그 후에 투사체 개수, 등등 능력 구현


    //private Camera camera;

    //private GameManager gameManager;

    /*public void Init(GameManager gameManager)
    {
        //this.gameManager = gameManager;
        camera = Camera.main;
    }*/
    [SerializeField] private float findRadius = 5f;  // 감지범위
    [SerializeField] private LayerMask enemyLayer;   // 에너미 레이어
    private GameObject _target;                      // 공격할 타겟
    private bool _isDebug;

    [SerializeField] private Slider hpSlider;

    int maxHp;
    int currentHp;

    protected override void HandleAction()
    {
        maxHp = statHandler.Health;
        currentHp = maxHp;
        hpSlider.value = currentHp;

        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (_isDebug == false) _isDebug = true;
            else _isDebug = false;
        }

        OnMove();
        // 장애물에 안걸리는 에너미 찾아서 그쪽 방향으로 바라보기
        //FindNearestEnemy();
        OnLook();
        // 공격
        // 디버그용 발사체 발사
        if (_isDebug == true)
        {
            if (Input.GetMouseButton(0))
            {
                //isAttacking = true;
                //statHandler.Health -= 1;
                ReflectShot();
            }
            if(Input.GetMouseButton(1))
            {
                isAttacking = true;
            }
        }
    }

    public override void Death()
    {
        /*base.Death();
        gameManager.GameOver();*/
    }

    void OnMove()
    {
        // 키입력 방향키 or wasd
        // 이동 - 노멀라이즈 -> 부모moveDirection값 전달
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(horizontal, vertical).normalized;
        
        // 멈춰 있을 때 공격한다.
        if (_isDebug == false)
        {
            if (movementDirection.x == 0 && movementDirection.y == 0) isAttacking = _target != null ? true : false;
            else isAttacking = false;
        }
    }

    void OnLook()
    {
        // 지금 공격중이 아니거나 타겟이 없다면 리턴
        Vector2 targetPos;
        if (_isDebug == true)
        {

            Vector2 mousePos = Input.mousePosition;
            targetPos = Camera.main.ScreenToWorldPoint(mousePos);
        }
        else
        {
            if (!isAttacking) return;
            if (!_target ) return;

            targetPos = _target.transform.position;
        }

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

    void OnFire()//InputValue inputValue)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;//요건 캔버스가 그린 것에 마우스를 올린 뒤에 마우스를 누르면 리턴해버리겠다는 뜻
        }
        //isAttacking = inputValue.isPressed;//isAttacking에 마우스가 눌렸는지 보내준다.
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

        GameObject nearest = null;
        float minDistance = Mathf.Infinity;

        // 반복으로 배열안에 에너미의 거리 계산 후 가장 가까운 것을 타겟으로 설정
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.layer == gameObject.layer) continue;

            float distance = Vector2.Distance(transform.position, collider.transform.position);

            if (distance < minDistance && distance < findRadius)
            {
                minDistance = distance;
                _target = collider.gameObject;
            }
        }
    }

    // 감지 범위 그리기
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }

    // 능력 구현?
    public void AttackSpeedUp()
    {
        if (weaponHandler == null) return;
        weaponHandler.Delay -= 0.15f;
    }

    public void DoubleShot()
    {
        if (weaponHandler == null) return;
        weaponHandler.Power += 2f;
        
    }

    public void PenetrationShot()
    {
        // 여기서 관통을 On 해주자
    }

    public void ReflectShot()
    {
        // 반사 on -> ProjectijleController 에 벽이랑 부딪혔을 시 반사되게끔 구현
        
    }
}
