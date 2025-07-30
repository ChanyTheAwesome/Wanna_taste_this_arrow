using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;
    private RangeWeaponHandler rangeWeaponHandler;

    private float currentDuration;
    private Vector2 direction;
    private bool isReady;
    private Transform pivot;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer spriteRenderer;

    public bool fxOnDestroy = true;

    ProjectileManager projectileManager;

    private bool _reflect = false;
    private bool _penetrate = false;

    [Header("Fields for explosive bullets")]
    [SerializeField] private bool _isExplosive = false;
    [SerializeField] private GameObject explosionRange;
    private bool _explosionStart = false;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        pivot = transform.GetChild(0);
    }

    private void Update()
    {
        if (!isReady) return;//�غ���� ������ update�� ���� �ʴ´�.

        currentDuration += Time.deltaTime;

        if (currentDuration > rangeWeaponHandler.Duration)
        {
            DestroyProjectile(transform.position, false);//�ִ� ��Ÿ� ����. �ִ� ��Ÿ��� �Ѿ�� �ı��Ѵ�.
        }
        if (!_explosionStart)
        {
            _rigidbody.velocity = direction * rangeWeaponHandler.Speed;//�׷��� �ʴٸ� ��� ������ ����� ������ �ӵ��� ���ư���.
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//���� �¾Ҵ�!
    {
        if(levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))//Level���̾�� Level-Grid-Collision�� �پ��ִ�.
        {
            if(_reflect)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, levelCollisionLayer);

                if (hit.collider != null)
                {
                    Vector2 normal = hit.normal;
                    direction = Vector2.Reflect(direction, normal);
                    transform.right = direction;
                }
            }
            else
            {
                DestroyProjectile(collision.ClosestPoint(transform.position) - direction * 0.2f, fxOnDestroy);

            }

        }
        else if(rangeWeaponHandler.target.value == (rangeWeaponHandler.target.value | (1 << collision.gameObject.layer)))
        {//Ÿ�ٰ� ���ٸ�,
            ResourceController resourceController = collision.GetComponent<ResourceController>();
            if(resourceController != null)
            {
                resourceController.ChangeHealth(-rangeWeaponHandler.Power);//������ �Ŀ���ŭ ü���� ���
                if (rangeWeaponHandler.IsOnKnockback)//�˹��� �Ǵ� ������
                {
                    BaseController controller = collision.GetComponent<BaseController>();
                    if(controller != null)
                    {
                        controller.ApplyKnockback(transform, rangeWeaponHandler.KnockbackPower, rangeWeaponHandler.KnockbackTime);//�˹��� �ش�.
                    }
                }
            }
            if (_penetrate) return;
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestroy);//���� ����ü �ı�
        }
    }

    public void Init(Vector2 direction, RangeWeaponHandler weaponHandler, ProjectileManager projectileManager)
    {//�� Init�� ProjectileManager���� �Ѿ��� ���� �� ȣ��ȴ�.
        this.projectileManager = projectileManager;
        rangeWeaponHandler = weaponHandler;
        this.direction = direction;
        currentDuration = 0;
        transform.localScale = Vector3.one * weaponHandler.BulletSize;
        spriteRenderer.color = weaponHandler.ProjectileColor;

        transform.right = this.direction;

        if (direction.x < 0)
        {
            pivot.localRotation = Quaternion.Euler(180, 0, 0);//������ �����̶�� 180�� Ʋ���ְ�
        }
        else
        {
            pivot.localRotation = Quaternion.Euler(0, 0, 0);//�ƴ϶�� �״�� �д�.
        }
        if (_isExplosive)
        {
            explosionRange.gameObject.SetActive(false);
        }
        isReady = true;//�غ� �ƴ�.
    }

    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        if (_isExplosive)
        {
            ExplosiveProjectileControl(rangeWeaponHandler);
            return;
        }
        if (createFx)
        {
            projectileManager.CreateImpactParticlesAtPosition(position, rangeWeaponHandler);//projeectileManager�� ��ƼŬ ���� �޼���� ������.
        }
        Destroy(this.gameObject);
    }
    private void ExplosiveProjectileControl(RangeWeaponHandler rangeWeaponHander)
    {
        _explosionStart = true;
        _rigidbody.velocity = Vector2.zero;

        ExplosiveAftermathController explosiveAftermathController = explosionRange.GetComponent<ExplosiveAftermathController>();
        explosiveAftermathController.Init(rangeWeaponHandler);
    }
    public void ReflectOn()
    {
        if (!_reflect) _reflect = true;
    }

    public void PenetrateOn()
    {
        if(!_penetrate) _penetrate = true;
    }
}
