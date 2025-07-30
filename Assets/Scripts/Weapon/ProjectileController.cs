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
        if (!_isReady) return;//준비되지 않으면 update를 돌지 않는다.

        _currentDuration += Time.deltaTime;

        if (_currentDuration > rangeWeaponHandler.Duration)
        {
            DestroyProjectile(transform.position, false);//최대 사거리 느낌. 최대 사거리가 넘어가면 파괴한다.
        }
        if (!_explosionStart)
        {
            _rigidbody.velocity = _direction * rangeWeaponHandler.Speed;//그렇지 않다면 계속 정해진 방향과 정해진 속도로 나아간다.
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//뭔가 맞았다!
    {
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))//Level레이어는 Level-Grid-Collision에 붙어있다.
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
        {//타겟과 같다면,
            PlayerHitByProjectile(collision);
            if (_penetrate) return;
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestroy);//이후 투사체 파괴
        }
    }

    public void Init(Vector2 direction, RangeWeaponHandler weaponHandler, ProjectileManager projectileManager, bool reflect, bool penetrate)
    {//이 Init은 ProjectileManager에서 총알을 쐈을 때 호출된다.

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
            _pivot.localRotation = Quaternion.Euler(180, 0, 0);//방향이 왼쪽이라면 180도 틀어주고
        }
        else
        {
            _pivot.localRotation = Quaternion.Euler(0, 0, 0);//아니라면 그대로 둔다.
        }

        if (_isExplosive)
        {
            explosionRange.gameObject.SetActive(false);
        }

        _isReady = true;//준비 됐다.
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
            _projectileManager.CreateImpactParticlesAtPosition(position, rangeWeaponHandler);//projeectileManager의 파티클 생성 메서드로 보낸다.
        }

        Destroy(this.gameObject);
    }
    private void PlayerHitByProjectile(Collider2D collision)
    {
        ResourceController resourceController = collision.GetComponent<ResourceController>();
        if (resourceController != null)
        {
            resourceController.ChangeHealth(-rangeWeaponHandler.Power);//무기의 파워만큼 체력을 깎고
            if (rangeWeaponHandler.IsOnKnockback)//넉백이 되는 무기라면
            {
                BaseController controller = collision.GetComponent<BaseController>();
                if (controller != null)
                {
                    controller.ApplyKnockback(transform, rangeWeaponHandler.KnockbackPower, rangeWeaponHandler.KnockbackTime);//넉백을 준다.
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
