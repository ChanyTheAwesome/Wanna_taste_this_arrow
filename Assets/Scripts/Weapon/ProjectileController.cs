using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private LayerMask enemyLayer;
    private List<Transform> _hitEnemies = new List<Transform>();
    [SerializeField] private bool _reflect = false;
    private bool _penetrate = false;
    private bool _ricochet = false;
    [SerializeField] private int MaxRicochetCount;
    private int _ricochetCount = 0;

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
                Debug.Log("발사체 충돌");
                DestroyProjectile(collision.ClosestPoint(transform.position) - _direction * 0.2f, fxOnDestroy);
            }
        }
        else if (rangeWeaponHandler.target.value == (rangeWeaponHandler.target.value | (1 << collision.gameObject.layer)))
        {//Ÿ�ٰ� ���ٸ�,
            PlayerHitByProjectile(collision);
            if(!_hitEnemies.Contains(collision.transform)) _hitEnemies.Add(collision.transform);
            if(_ricochet)
            {
                bool ricocheted = RicochetProjectileControl(collision.transform);
                if (ricocheted) return;
            }
            if (!_penetrate && !_ricochet)
            {
                DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestroy);//���� ����ü �ı�
            }
        }
    }

    public void Init(Vector2 direction, RangeWeaponHandler weaponHandler, ProjectileManager projectileManager)
    {//�� Init�� ProjectileManager���� �Ѿ��� ���� �� ȣ��ȴ�.

        Debug.Log(weaponHandler.target.ToString());


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
        if(DungeonManager.Instance.CurrentDungeonID == 2)   // 동굴 던전이면
            this.GetComponentInChildren<SpriteRenderer>().material = DungeonManager.Instance.CaveMaterial;  // Material 속성 변경

        _isReady = true;//�غ� �ƴ�.
    }

    public void Init(Vector2 direction, RangeWeaponHandler weaponHandler, ProjectileManager projectileManager, bool reflect, bool penetarte, bool recochet)
    {//�� Init�� ProjectileManager���� �Ѿ��� ���� �� ȣ��ȴ�.

        this._projectileManager = projectileManager;
        rangeWeaponHandler = weaponHandler;
        this._direction = direction;
        _currentDuration = 0;
        transform.localScale = Vector3.one * weaponHandler.BulletSize;
        _spriteRenderer.color = weaponHandler.ProjectileColor;
        transform.right = this._direction;
        _reflect = reflect;
        _penetrate = penetarte;
        _ricochet = recochet;
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

        if (DungeonManager.Instance.CurrentDungeonID == 2)   // 동굴 던전이면
            this.GetComponentInChildren<SpriteRenderer>().material = DungeonManager.Instance.CaveMaterial;  // Material 속성 변경

        _isReady = true;//�غ� �ƴ�.
    }

    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        Debug.Log("Destroy");
        if (_isExplosive)
        {
            ExplosiveProjectileControl(rangeWeaponHandler);
            return;
        }

        if (createFx)
        {
            _projectileManager.CreateImpactParticlesAtPosition(position, rangeWeaponHandler);//projeectileManager�� ��ƼŬ ���� �޼���� ������.
        }
        Debug.Log("파괴 직전");
        Destroy(this.gameObject);
        Debug.Log("파괴 후");
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

    private bool RicochetProjectileControl(Transform hitEnemy)
    {
        if (_ricochetCount >= MaxRicochetCount)
        {
            DestroyProjectile(transform.position, fxOnDestroy);
            return false;
        }

        Transform nearestEnemy = FindNearestEnemy(transform.position, hitEnemy);

        if (nearestEnemy != null)
        {
            RedirectTo(nearestEnemy);
            _ricochetCount++;
            return true;
        }

        return false;
    }

    private Transform FindNearestEnemy(Vector2 from, Transform exclude)
    {
        float radius = 5f;
        float minDist = float.MaxValue;
        Transform nearest = null;

        Collider2D[] allhits = Physics2D.OverlapCircleAll(from, radius, enemyLayer);
        List<Collider2D> hits = new List<Collider2D>(allhits);
        

        hits.RemoveAll(hit => _hitEnemies.Contains(hit.transform));

        foreach (Collider2D hit in hits)
        {
            if (hit.transform == exclude) continue; 

            float dist = Vector2.Distance(from, hit.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = hit.transform;
            }
        }

        return nearest;
    }

    private void RedirectTo(Transform target)
    {
        Vector2 dir = (target.position - transform.position).normalized;
        _direction = dir;
        _rigidbody.velocity = _direction * rangeWeaponHandler.Speed;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3f);
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
