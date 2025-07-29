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

    
    private List<GameObject> _enemies; // ���ʹ̵� ��Ƽ� ��� List
    private GameObject _target;
    private float maxFindRange = 10f;


    protected override void HandleAction()
    {
        // Ű�Է� ����Ű or wasd
        // �̵� - ��ֶ�����
        OnMove();
        // ��ֹ��� �Ȱɸ��� ���ʹ� ã�Ƽ� ���� �������� �ٶ󺸱�
        FindNearestEnemy();
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
        movementDirection = new Vector2(horizontal, vertical).normalized;
        //movementDirection = inputValue.Get<Vector2>();
        //movementDirection = movementDirection.normalized;//InputValue�� ���͸� �����ش�.
    }

    void OnLook()//InputValue inputValue)
    {
        Vector2 targetPos = _target.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//���콺�� ��ġ ��ǥ�� �ް�
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(targetPos);//ī�޶��� ���� ��ǥ�� �޾�

        lookDirection = (worldPos - (Vector2)transform.position);//������

        //Debug.Log(Mathf.Abs(Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg));

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
        _enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        
        float shortDis = Vector3.Distance(transform.position, _enemies[0].transform.position);
        _target = _enemies[0];

        foreach (GameObject enemy in _enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

                Debug.Log(distance);

            if(distance <= maxFindRange)
            {
                _target = enemy;
                shortDis = distance;
            }
        }

    }
}
