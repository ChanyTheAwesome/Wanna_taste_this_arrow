using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    // 이동
    // 각각 상태에 따른 애니메이션
    // 공격 - 자동 - 가까운 적 찾기 - 장애물이 있다면 다른 적 찾기
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

    protected override void HandleAction()
    {
        // 키입력 방향키 or wasd
        // 이동 - 노멀라이즈
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(horizontal, vertical).normalized;

        // 장애물에 안걸리는 에너미 찾아서 그쪽 방향으로 바라보기
        OnLook();

        // 공격
        isAttacking = Input.GetKey(KeyCode.Space);
        Attack();
    }

    public override void Death()
    {
        /*base.Death();
        gameManager.GameOver();*/
    }

    void OnMove()//InputValue inputValue)
    {
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        _rigidbody.velocity = new Vector2(horizontal, vertical) * statHandler.Speed;

        Vector2 movement = new Vector2(horizontal, vertical);
        //movementDirection = inputValue.Get<Vector2>();
        //movementDirection = movementDirection.normalized;//InputValue의 벡터를 정해준다.
    }

    void OnLook()//InputValue inputValue)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//마우스의 위치 좌표를 받고
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePosition);//카메라의 월드 좌표를 받아

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
}
