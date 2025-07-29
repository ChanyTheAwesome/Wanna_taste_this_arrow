using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    // �̵�
    // ���� ���¿� ���� �ִϸ��̼�
    // ���� - �ڵ� - ����� �� ã�� - ��ֹ��� �ִٸ� �ٸ� �� ã��
    // ���� ��ġ�� ���� flipX ����
    // ���� ���� �Ұ�� attack = true => ����ü �߻� ����

    // �� �Ŀ� ����ü ����, ��� �ɷ� ����


    //private Camera camera;

    //private GameManager gameManager;

    /*public void Init(GameManager gameManager)
    {
        //this.gameManager = gameManager;
        camera = Camera.main;
    }*/

    protected override void HandleAction()
    {
        // Ű�Է� ����Ű or wasd
        // �̵� - ��ֶ�����
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(horizontal, vertical).normalized;

        // ��ֹ��� �Ȱɸ��� ���ʹ� ã�Ƽ� ���� �������� �ٶ󺸱�
        OnLook();

        // ����
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
        //movementDirection = movementDirection.normalized;//InputValue�� ���͸� �����ش�.
    }

    void OnLook()//InputValue inputValue)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//���콺�� ��ġ ��ǥ�� �ް�
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePosition);//ī�޶��� ���� ��ǥ�� �޾�

        lookDirection = (worldPos - (Vector2)transform.position);//������
        if (lookDirection.magnitude < 0.9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;//�̰����� �ٶ󺸴� ������ ���͸� �����Ѵ�.
        }
    }

    void OnFire()//InputValue inputValue)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;//��� ĵ������ �׸� �Ϳ� ���콺�� �ø� �ڿ� ���콺�� ������ �����ع����ڴٴ� ��
        }
        //isAttacking = inputValue.isPressed;//isAttacking�� ���콺�� ���ȴ��� �����ش�.
    }
}
