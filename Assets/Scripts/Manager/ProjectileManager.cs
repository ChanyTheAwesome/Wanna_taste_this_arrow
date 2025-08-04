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
        GameObject origin = projectilePrefabs[rangeWeaponHandler.BulletIndex];//�����տ��� ���õ� ����ü�� ���ùް�
        GameObject obj = Instantiate(origin, startPosition, Quaternion.identity);//�����Ѵ�.

        ProjectileController projectileController = obj.GetComponent<ProjectileController>();//�Ѿ˿� ���� ProjectileController�� ������

        if (rangeWeaponHandler.transform.root.gameObject.GetComponent<BaseController>() is PlayerController player)
        {
            projectileController.Init(direction, rangeWeaponHandler, this, _reflect, _penetrate, _ricochet);//�����, ���Ÿ� ���� �ڵ鷯��, �Ŵ����� �����ش�.
            return;
        }

        else projectileController.Init(direction, rangeWeaponHandler, this);
    }

    public void CreateImpactParticlesAtPosition(Vector3 position, RangeWeaponHandler weaponHandler)
    {
        impactParticleSystem.transform.position = position;//�ε��� ���� ������
        ParticleSystem.EmissionModule em = impactParticleSystem.emission;
        em.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(weaponHandler.BulletSize * 5)));//�ȿ� �� �Ű������� �ð���, ��ƼŬ ����. 
        //����Ե� �Ѿ��� ũ�Ⱑ Ŀ������ ��ƼŬ�� ������ ������ ���̴�! Ceil�� �־��� ������ �۰ų� ���� �ִ� ������ ����ش�.

        ParticleSystem.MainModule mainModule = impactParticleSystem.main;//��ƼŬ �ý����� �������� ������

        mainModule.startSpeedMultiplier = weaponHandler.BulletSize * 10f;//���� ���ǵ带 �����ϰ�
        impactParticleSystem.Play();//��ƼŬ�� ����Ѵ�.
    }
}
