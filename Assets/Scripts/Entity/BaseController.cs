using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    public Rigidbody2D Rigidbody
    {
        get { return _rigidbody; }
        set { _rigidbody = value; }
    }
    
    [SerializeField] protected SpriteRenderer characterRenderer;
    [SerializeField] private Transform weaponPivot;
    [SerializeField] public WeaponHandler WeaponPrefab;
    [SerializeField] public ProjectileManager ProjectileManager;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection {  get { return movementDirection; }}

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; }}

    private Vector2 _knockback = Vector2.zero;
    private float _knockbackDuration = 0.0f;

    protected AnimationHandler animationhandler;
    protected StatHandler statHandler;
    protected WeaponHandler weaponHandler;

    protected bool isAttacking;
    protected bool isLeft;

    private float timeSincelastAttack = float.MaxValue;

    public bool IsCharging = false;
    public bool GotChargeWeapon = false;
    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationhandler = GetComponent<AnimationHandler>();
        statHandler = GetComponent<StatHandler>();
        
        if(WeaponPrefab != null)
        {
            weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
        }
        else
        {
            weaponHandler = GetComponentInChildren<WeaponHandler>(); // WeaponPrefab�� null�̶�� ã�ƺ���.
        }
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleAction();
        Rotate(lookDirection);
        HandleAttackDelay();
    }

    protected virtual void FixedUpdate()
    {
        if (!IsCharging)
        {
            Movement(movementDirection);
            
        }
        if (_knockbackDuration > 0.0f)
        {
            _knockbackDuration -= Time.fixedDeltaTime; // knockbackDuration�� �� �����Ӹ��� ���ش�.
        }
        Debug.Log(Rigidbody.velocity);
    }

    protected virtual void HandleAction()
    {

    }

    protected virtual void Movement(Vector2 direction)
    {
        direction = direction * statHandler.Speed; // �޾ƿ� direction�� speed��ŭ �̵�, ���� EnemyController�� handleAction��, Player�� OnMove�� �����Ǿ�����.
        if(_knockbackDuration > 0.0f)
        {
            direction *= 0.2f;
            direction += _knockback;
        }//�˹� ���̶�� �˹��� �ϵ��� ��, �̵��� ��ü ũ�⸦ 0.2��ŭ ���߰�, knockback ���͸� direction�� ����)
        _rigidbody.velocity = direction; // ���� ������ �ϴ� rigidbody�� velocity�� direction�� �־���
    }

    protected virtual void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //���� ���ϱ�, ���� ���� ���� ������ �츮�� �� �ƴ� ������ ��ȯ���ִ� ��.
        isLeft = Mathf.Abs(rotZ) > 90; 
        

        if (weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0f, 0f, rotZ);//���⵵ ���� �����ּ���
        }
        weaponHandler?.Rotate(isLeft); //�¿� �ݴ뵵 ���ּ���
    }

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        _knockbackDuration = duration;
        _knockback = -(other.position - transform.position).normalized * power; //�� ������ ��ġ - ���� ���� ��ġ�� ���͸� power��ŭ knockback��Ų��.
    }

    private void HandleAttackDelay()
    {
        if (weaponHandler == null)
        {
            return;
        }
        if (timeSincelastAttack <= weaponHandler.Delay && !IsCharging)
        {
            timeSincelastAttack += Time.deltaTime;
        }
        if(isAttacking && timeSincelastAttack > weaponHandler.Delay)
        {
            timeSincelastAttack = 0.0f;
            Attack();
        }
    }
    protected virtual void Attack()
    {
        if(lookDirection != Vector2.zero && !IsCharging)
        {
            weaponHandler?.Attack();//�׳� ������ ���������� �ʴٸ� �������� ������, �� lookDirection�� �����ϱ� ������ �������� �ʴ� �ڵ�?
        }
    }

    public virtual void Death()
    {
        _rigidbody.velocity = Vector3.zero; //�̵��� ���߰�

        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color; //���İ��� 0.3���� �� ���ϰڽ��ϴ�~
        }

        foreach(Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = false; //������Ʈ�� �� �������ڽ��ϴ�~
        }

        Destroy(gameObject, 2.0f); //2�� �ڿ� �����ع����ڽ��ϴ�~
    }
}