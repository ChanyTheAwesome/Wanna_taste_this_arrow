using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class RangeWeaponHandler : WeaponHandler
{
    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectileSpawnPosition;
    [SerializeField] private Transform projectileSpawnBackPosition;

    [SerializeField] private int bulletIndex;
    public int BulletIndex { get { return bulletIndex; } }

    [SerializeField] private float bulletSize = 1.0f;
    public float BulletSize { get { return bulletSize; } }

    [SerializeField] private float duration;
    public float Duration { get { return duration; } }

    [SerializeField] private float spread;
    public float Spread { get { return spread; } }

    [SerializeField] private int numberofProjectilesPerShot;
    public int NumberofProjectilesPerShot { get { return numberofProjectilesPerShot; } set { numberofProjectilesPerShot = value; } }

    [SerializeField] private float multipleProjectileAngle;
    public float MultipleProjectileAngle { get { return multipleProjectileAngle; } set { multipleProjectileAngle = value; } }

    [SerializeField] private Color projectileColor;
    public Color ProjectileColor { get { return projectileColor; } }

    private bool _reverse;
    public bool Reverse { set { _reverse = value; } }

    private ProjectileManager _projectileManager;

    protected override void Start()
    {
        base.Start();
        _projectileManager = ProjectileManager.Instance;
    }

    public override void Attack()
    {
        base.Attack();//�ִϸ��̼� ����ϰ�, �Ҹ��� ���� ���̴�.
        float projectileAngleSpace = multipleProjectileAngle;
        int numberOfProjectilePerShot = NumberofProjectilesPerShot;
        float minAngle = -(numberOfProjectilePerShot/2.0f) * projectileAngleSpace;//�ּ� ������ �����Ѵ�.

        for(int i = 0; i<numberOfProjectilePerShot; i++)
        {
            float angle = minAngle + projectileAngleSpace * i;//�ּ� ������ ������ ���� ������ ���ϰ�
            float randomSpread = Random.Range(-spread, spread);//������ ���� �� ���ϰ�
            angle += randomSpread;//����
            CreateProjectile(Controller.LookDirection, angle);//�����Ѵ�.
        }//�̸� ����ü ������ŭ �ݺ��Ѵ�.
    }
    private void CreateProjectile(Vector2 _lookDirection, float angle)
    {//�Ѿ� �߻�, projectileManager���� �ִ� ShootBullet�� ȣ���Ѵ�.
        _projectileManager.ShootBullet(this, projectileSpawnPosition.position, RotateVector2(_lookDirection, angle));
        if(_reverse)
        {
            _projectileManager.ShootBullet(this, projectileSpawnBackPosition.position, RotateVector2(_lookDirection * -1, angle));
        }

    }//�����ִ� ���� �ڽŰ�, spawnPosition, �����ִ� ���⿡�� angle��ŭ ���� ������ vector�� ������.

    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }
}