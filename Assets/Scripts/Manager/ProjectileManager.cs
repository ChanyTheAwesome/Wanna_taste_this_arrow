using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private static ProjectileManager instance;
    public static ProjectileManager Instance { get { return instance; } }

    [SerializeField] private GameObject[] projectilePrefabs;
    public GameObject[] ProjectilePrefabs => projectilePrefabs;
    [SerializeField] private ParticleSystem impactParticleSystem;

    private bool _reflect;
    public bool Reflect { set { _reflect = value; } }

    private bool _penetrate;
    public bool Penetrate { set { _penetrate = value; } }
    private bool _reverse;
    public bool Reverse { set { _reverse = value; } }

    private bool _ricochet;
    public bool Ricochet { set { _ricochet = value; } }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPosition, Vector2 direction)
    {
        GameObject origin = projectilePrefabs[rangeWeaponHandler.BulletIndex];//프리팹에서 선택된 투사체를 선택받고
        GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);//생성한다.

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();//총알에 붙은 ProjectileController를 가져와

        if (rangeWeaponHandler.transform.root.gameObject.GetComponent<BaseController>() is PlayerController player)
        {
            projectileController.Init(direction, rangeWeaponHandler, this, _reflect, _penetrate, _ricochet);//방향과, 원거리 무기 핸들러와, 매니저를 보내준다.
            return;
        }

        else projectileController.Init(direction, rangeWeaponHandler, this);
    }

    public void CreateImpactParticlesAtPosition(Vector3 position, RangeWeaponHandler weaponHandler)
    {
        impactParticleSystem.transform.position = position;//부딪힌 곳의 포지션
        ParticleSystem.EmissionModule em = impactParticleSystem.emission;
        em.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(weaponHandler.BulletSize * 5)));//안에 들어간 매개변수는 시간과, 파티클 갯수. 
        //놀랍게도 총알의 크기가 커질수록 파티클의 갯수도 많아질 것이다! Ceil은 주어진 값보다 작거나 같은 최대 정수를 뱉어준다.

        ParticleSystem.MainModule mainModule = impactParticleSystem.main;//파티클 시스템의 메인으로 접근해

        mainModule.startSpeedMultiplier = weaponHandler.BulletSize * 10f;//시작 스피드를 결정하고
        impactParticleSystem.Play();//파티클을 재생한다.
    }
}
