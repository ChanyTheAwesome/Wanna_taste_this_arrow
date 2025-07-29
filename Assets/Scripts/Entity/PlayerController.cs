using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
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

    [SerializeField] private float findRadius = 5f;
    [SerializeField] private LayerMask enemyLayer;
    private GameObject _target;


    protected override void HandleAction()
    {
        OnMove();
        // 장애물에 안걸리는 에너미 찾아서 그쪽 방향으로 바라보기
        FindNearestEnemy();
        OnLook();
        // 공격
        //isAttacking = Input.GetMouseButton(0);

        //if(isAttacking) Attack();
    }

    public override void Death()
    {
        /*base.Death();
        gameManager.GameOver();*/
    }

    void OnMove()//InputValue inputValue)
    {
        // 키입력 방향키 or wasd
        // 이동 - 노멀라이즈 -> 부모moveDirection값 전달
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(horizontal, vertical).normalized;

        // 멈춰 있을 때 공격한다.
        if (movementDirection.x == 0 && movementDirection.y == 0)
        {
            isAttacking = _target != null ? true : false;
        }
        else isAttacking = false;
        
        //movementDirection = inputValue.Get<Vector2>();
        //movementDirection = movementDirection.normalized;//InputValue의 벡터를 정해준다.
    }

    void OnLook()//InputValue inputValue)
    {
        if (!isAttacking) return;
        if (!_target ) return;

        Vector2 targetPos = _target.transform.position;
        //Vector2 mousePosition = Input.mousePosition;//마우스의 위치 좌표를 받고
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(targetPos);//카메라의 월드 좌표를 받아

        lookDirection = (worldPos - (Vector2)transform.position);//빼본다

        if (lookDirection.magnitude < 0.9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;//이것으로 바라보는 방향의 벡터를 지정한다.
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

    void FindNearestEnemy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, findRadius, enemyLayer);

        if (colliders.Length == 0)
        {
            _target = null;
            return;
        }

        GameObject nearest = null;
        float minDistance = Mathf.Infinity;

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
}
