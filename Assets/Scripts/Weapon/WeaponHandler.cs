using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [Header("Attack Info")]
    [SerializeField] private float delay = 1.0f;
    public float Delay { get => delay; set => delay = value; }

    [SerializeField] private float weaponSize = 1.0f;
    public float WeaponSize { get => weaponSize; set => weaponSize = value; }

    [SerializeField] private float power = 1.0f;
    public float Power { get => power; set => power = value; }

    [SerializeField] private float speed = 1.0f;
    public float Speed { get => speed; set => speed = value; }

    [SerializeField] private float attackRange = 10.0f;
    public float AttackRange { get => attackRange; set => attackRange = value; }

    public LayerMask target;

    [Header("Knock Back Info")]
    [SerializeField] private bool isOnKnockback = false;
    public bool IsOnKnockback { get => isOnKnockback; set => isOnKnockback = value; }

    [SerializeField] private float knockbackPower = 0.1f;
    public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }

    [SerializeField] private float knockbackTime = 0.5f;
    public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }

    private static readonly int IsAttack = Animator.StringToHash("IsAttack");

    public BaseController Controller { get; private set; }
    public AudioClip attackSoundClip;
    private Animator _animator;
    private SpriteRenderer _weaponRenderer;

    

    protected virtual void Awake()
    {
        Controller = GetComponentInParent<BaseController>();
        _animator = GetComponentInChildren<Animator>();
        _weaponRenderer = GetComponentInChildren<SpriteRenderer>();

        _animator.speed = 1.0f / delay;//�ӵ��� 1.0f�� delay�� ���� ����ŭ���� �����ȴ�. �� delay�� ������ �������� �ӵ��� ��������.
        transform.localScale = Vector3.one * weaponSize;
    }

    protected virtual void Start()
    {

    }

    public virtual void Attack()
    {
        AttackAnimation();//�ִϸ��̼��� ȣ���ϰ�

        /*if(attackSoundClip != null)
        {
            SoundManager.PlayClip(attackSoundClip);//�Ҹ��� ����Ѵ�.
        }*/
    }

    public void AttackAnimation()
    {
        _animator.SetTrigger(IsAttack);//���ݽ� Ʈ���� Ȱ��ȭ
    }

    public virtual void Rotate(bool isLeft)
    {
        _weaponRenderer.flipY = isLeft;//���ʺ����? ������ �����?
    }
}