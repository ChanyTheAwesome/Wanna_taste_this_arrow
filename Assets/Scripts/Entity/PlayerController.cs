using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    private GameManager gameManager;

    protected override void Awake()
    {
        base.Awake();
        if(GameManager.Instance.player == null)
        {
            GameManager.Instance.player = this.gameObject;
        }

        //PlayerManager.Instance._playerController = this;    //�׽�Ʈ�� _playerController, ������ PlayerController�� �ٲ����
        
        //PlayerManager.Instance.nowAnim = this.GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        PlayerManager.Instance.SetCharacter();  // ĳ���� ���� ����
        if (DungeonManager.Instance.CurrentDungeonID == 2)
        {
            Debug.Log("��Ƽ���� ���� �õ��� ��"); // �׽�Ʈ��
            this.GetComponentInChildren<SpriteRenderer>().material = DungeonManager.Instance.CaveMaterial;
        }
    }

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        
    }

    [SerializeField] private float findRadius = 10f;  // ��������
    [SerializeField] private LayerMask enemyLayer; // ���ʹ� ���̾�
    [SerializeField] private LayerMask levelLayer;
    
    private GameObject _target;                      // ������ Ÿ��
    private bool _isDebug;

    private int _maxHp = 100;
    private float _currentHp;
    private static int _numberOfPojectiles = 1;
    private static bool _reverse = false;

    protected override void HandleAction()
    {
        _currentHp = resource.CurrentHealth;
        UpdateHpBar();

        if(Input.GetMouseButtonDown(0))
        {
            AttackPowerUp();
        }

        OnMove();
        // ��ֹ��� �Ȱɸ��� ���ʹ� ã�Ƽ� ���� �������� �ٶ󺸱�
        FindNearestEnemy();
        OnLook();
        // ����
        // ����׿� �߻�ü �߻�
    }

    public override void Death()
    {
        base.Death();
        //gameManager.GameOver();
    }

    void OnMove()
    {
        // Ű�Է� ����Ű or wasd
        // �̵� - ��ֶ����� -> �θ�moveDirection�� ����
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(horizontal, vertical).normalized;
        animationhandler.Move(movementDirection);
        // ���� ���� �� �����Ѵ�.

        if (movementDirection.x == 0 && movementDirection.y == 0) isAttacking = _target != null;
        else isAttacking = false;
    }

    void OnLook()
    {
        // ���� �������� �ƴϰų� Ÿ���� ���ٸ� ����
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
            lookDirection = (targetPos - (Vector2)transform.position);//������

            if (lookDirection.magnitude < 0.9f)
            {
                lookDirection = Vector2.zero;
            }
            else
            {
                lookDirection = lookDirection.normalized;//�̰����� �ٶ󺸴� ������ ���͸� �����Ѵ�.
            }
        }
    }

    // ���� ���� ������� ã�� -> Ÿ�� ����
    void FindNearestEnemy()
    {
        // ������ ���� ���� �ݶ��̴��� ���ʹ� ���̾��� ��Ƽ� �迭�� �����
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, findRadius, enemyLayer);

        // �迭�� ���̰� 0�̶�� Ÿ���� ������ ����
        if (colliders.Length == 0)
        {
            _target = null;
            return;
        }

        float minDistance = Mathf.Infinity;

        // �ݺ����� �迭�ȿ� ���ʹ��� �Ÿ� ��� �� ���� ����� ���� Ÿ������ ����
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

    // ���� ���� �׸��� - ���߿� ������ �Ǵ� �κ�
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, findRadius);
    }

    public void ApplyAbility(string abilityName)
    {
        switch (abilityName)
        {
            case "��~":
                RecoveryHp();
                break;
            case "��!!!":
                MaxHpUp();
                break;
            case "����ó�� ����":
                MoveSpeedUp();
                break;
            case "���ϴ� ����":
                AttackSpeedUp();
                break;
            case "�� �ν�Ʈ":
                ProjectileSpeedUp();
                break;
            case "��ó�� ���":
                AttackPowerUp();
                break;
            case "��":
                TripleShot();
                break;
            case "��":
                PenetrationShot();
                break;
            case "�� �ݻ�":
                ReflectShot();
                break;
            case "�Ĺ�����":
                BackShot();
                break;
            case "�� �̷��ô� �ǵ���":
                Ricochet();
                break;
        }
    }


    // �ִ� ü�� ����
    public void MaxHpUp()
    {
        if (statHandler == null) return;
        statHandler.Health += 20;
        _maxHp = statHandler.Health;
    }

    // �̵� �ӵ� ����
    public void MoveSpeedUp()
    {
        if (statHandler == null) return;
        statHandler.Speed += 1f;
    }

    // ���� �ӵ�
    public void AttackSpeedUp()
    {
        if (weaponHandler == null) return;
        weaponHandler.Delay -= 0.15f;
    }

    // �߻�ü �ӵ� ����
    public void ProjectileSpeedUp()
    {
        if (weaponHandler == null) return;
        weaponHandler.Speed += 1f;
    }

    // ���ݷ� ����
    public void AttackPowerUp()
    {
        if (weaponHandler == null) return;
        weaponHandler.Power += 2f;
    }

    // �ѹ��� ����
    public void TripleShot()
    {
        if (weaponHandler == null) return;
        _numberOfPojectiles++;
        RangeWeaponHandler weapon = weaponHandler.GetComponent<RangeWeaponHandler>();
        weapon.NumberofProjectilesPerShot = _numberOfPojectiles;
        weapon.MultipleProjectileAngle = 30f;
    }

    //����
    public void PenetrationShot()
    {
        // ���⼭ ������ On ������
        ProjectileManager.Instance.Penetrate = true;
    }

    // ƨ���
    public void ReflectShot()
    {
        // �ݻ� on -> ProjectijleController �� ���̶� �ε����� �� �ݻ�ǰԲ� ����

        ProjectileManager.Instance.Reflect = true;
    }

    // �Ĺ�ȭ��
    public void BackShot()
    {
        if (weaponHandler == null) return;
        _reverse = true;
        RangeWeaponHandler weapon = weaponHandler.GetComponent<RangeWeaponHandler>();
        weapon.Reverse = _reverse;
    }

    // ��ź
    public void Ricochet()
    {
        ProjectileManager.Instance.Ricochet = true;
    }

    // ȸ��?
    public void RecoveryHp()
    {
        _currentHp += 20;
        if(_maxHp > _currentHp) return;
        else _currentHp = _maxHp;
    }

    public void NextStageEntryInitailize()
    {
        if (weaponHandler == null) return;

        RangeWeaponHandler weapon = weaponHandler.GetComponent<RangeWeaponHandler>();
        weapon.NumberofProjectilesPerShot = _numberOfPojectiles;
        weapon.MultipleProjectileAngle = 30f;
        weapon.Reverse = _reverse;

    }

    public void FirstStageAbilityInit()
    {
        _numberOfPojectiles = 1;
        RangeWeaponHandler weapon = weaponHandler.GetComponent<RangeWeaponHandler>();
        weapon.NumberofProjectilesPerShot = _numberOfPojectiles;
        weapon.MultipleProjectileAngle = 0f;
        _reverse = false;
        weapon.Reverse = _reverse;
    }
}
