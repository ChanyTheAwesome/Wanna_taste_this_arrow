using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
//using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    // ���Ĺ����ڴ�.
    // �÷��̾�� �̵��� ������ ��
    // �̵� �߿��� ���� x, �̵��� ���߸� ������ ��
    // �̵� �߿� Ű�� flipX ����, ���߰� ������ ���� ���� ã�Ƽ� ȸ���� ��
    // 
    // ���� ���¿� ���� �ִϸ��̼�
    // ���� - �ڵ� - ���� ���� ���� ����� �� ã�� - ��ֹ��� �ִٸ� �ٸ� �� ã��
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

    [SerializeField] private float findRadius = 5f;
    [SerializeField] private LayerMask enemyLayer;
    private GameObject _target;


    protected override void HandleAction()
    {
        OnMove();
        // ��ֹ��� �Ȱɸ��� ���ʹ� ã�Ƽ� ���� �������� �ٶ󺸱�
        FindNearestEnemy();
        OnLook();
        // ����
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
        // Ű�Է� ����Ű or wasd
        // �̵� - ��ֶ����� -> �θ�moveDirection�� ����
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(horizontal, vertical).normalized;

        // ���� ���� �� �����Ѵ�.
        if (movementDirection.x == 0 && movementDirection.y == 0)
        {
            isAttacking = _target != null ? true : false;
        }
        else isAttacking = false;
        
        //movementDirection = inputValue.Get<Vector2>();
        //movementDirection = movementDirection.normalized;//InputValue�� ���͸� �����ش�.
    }

    void OnLook()//InputValue inputValue)
    {
        if (!isAttacking) return;
        if (!_target ) return;

        Vector2 targetPos = _target.transform.position;
        //Vector2 mousePosition = Input.mousePosition;//���콺�� ��ġ ��ǥ�� �ް�
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(targetPos);//ī�޶��� ���� ��ǥ�� �޾�

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
