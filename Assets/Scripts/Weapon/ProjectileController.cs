using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    private RangeWeaponHandler rangeWeaponHandler;
    private Transform _pivot;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    private float _currentDuration;
    private Vector2 _direction;
    private bool _isReady;

    private bool _fxOnDestroy = true;
    public bool fxOnDestroy => _fxOnDestroy;

    ProjectileManager _projectileManager;

    [Header("Fields for Player Abilities")]
    private bool _reflect = false;
    private bool _penetrate = false;

    [Header("Fields for explosive bullets")]
    [SerializeField] private bool _isExplosive = false;
    [SerializeField] private GameObject explosionRange;
    private bool _explosionStart = false;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _pivot = transform.GetChild(0);
    }

    private void Update()
    {
        if (!_isReady) return;//�غ���� ������ update�� ���� �ʴ´�.

        _currentDuration += Time.deltaTime;

        if (_currentDuration > rangeWeaponHandler.Duration)
        {
            DestroyProjectile(transform.position, false);//�ִ� ��Ÿ� ����. �ִ� ��Ÿ��� �Ѿ�� �ı��Ѵ�.
        }
        if (!_explosionStart)
        {
            _rigidbody.velocity = _direction * rangeWeaponHandler.Speed;//�׷��� �ʴٸ� ��� ������ ����� ������ �ӵ��� ���ư���.
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//���� �¾Ҵ�!
    {
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))//Level���̾�� Level-Grid-Collision�� �پ��ִ�.
        {
            if (_reflect)
            {
                ReflectProjectileControl();
            }
            else
            {
                DestroyProjectile(collision.ClosestPoint(transform.position) - _direction * 0.2f, fxOnDestroy);
            }
        }
        else if (rangeWeaponHandler.target.value == (rangeWeaponHandler.target.value | (1 << collision.gameObject.layer)))
        {//Ÿ�ٰ� ���ٸ�,
            PlayerHitByProjectile(collision);
            if (_penetrate) return;
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestroy);//���� ����ü �ı�
        }
    }

    public void Init(Vector2 direction, RangeWeaponHandler weaponHandler, ProjectileManager projectileManager, bool reflect, bool penetrate)
    {//�� Init�� ProjectileManager���� �Ѿ��� ���� �� ȣ��ȴ�.

        Debug.Log(weaponHandler.target.ToString());

        if (reflect) _reflect = reflect;
        if (penetrate) _penetrate = penetrate;

        this._projectileManager = projectileManager;
        rangeWeaponHandler = weaponHandler;
        this._direction = direction;
        _currentDuration = 0;
        transform.localScale = Vector3.one * weaponHandler.BulletSize;
        _spriteRenderer.color = weaponHandler.ProjectileColor;

        transform.right = this._direction;

        if (direction.x < 0)
        {
            _pivot.localRotation = Quaternion.Euler(180, 0, 0);//������ �����̶�� 180�� Ʋ���ְ�
        }
        else
        {
            _pivot.localRotation = Quaternion.Euler(0, 0, 0);//�ƴ϶�� �״�� �д�.
        }

        if (_isExplosive)
        {
            explosionRange.gameObject.SetActive(false);
        }

        _isReady = true;//�غ� �ƴ�.
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
            _projectileManager.CreateImpactParticlesAtPosition(position, rangeWeaponHandler);//projeectileManager�� ��ƼŬ ���� �޼���� ������.
        }

        Destroy(this.gameObject);
    }
    private void PlayerHitByProjectile(Collider2D collision)
    {
        ResourceController resourceController = collision.GetComponent<ResourceController>();
        if (resourceController != null)
        {
            resourceController.ChangeHealth(-rangeWeaponHandler.Power);//������ �Ŀ���ŭ ü���� ���
            if (rangeWeaponHandler.IsOnKnockback)//�˹��� �Ǵ� ������
            {
                BaseController controller = collision.GetComponent<BaseController>();
                if (controller != null)
                {
                    controller.ApplyKnockback(transform, rangeWeaponHandler.KnockbackPower, rangeWeaponHandler.KnockbackTime);//�˹��� �ش�.
                }
            }
        }
    }

    private void ReflectProjectileControl()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _direction, 1f, levelCollisionLayer);

        if (hit.collider != null)
        {
            Vector2 normal = hit.normal;
            _direction = Vector2.Reflect(_direction, normal);
            transform.right = _direction;
        }
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
        if (!_penetrate) _penetrate = true;
    }
}
