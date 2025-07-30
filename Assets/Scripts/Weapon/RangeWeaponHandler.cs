using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeaponHandler : WeaponHandler
{
    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectileSpawnPosition;

    [SerializeField] private int bulletIndex;

    public int BulletIndex { get { return bulletIndex; } }
    [SerializeField] private float bulletSize = 1.0f;
    public float BulletSize { get { return bulletSize; } }
    [SerializeField] private float duration;
    public float Duration { get { return duration; } }
    [SerializeField] private float spread;
    public float Spread { get { return spread; } }
    [SerializeField] private int numberofProjectilesPerShot;
    public int NumberofProjectilesPerShot { get { return numberofProjectilesPerShot; } }
    [SerializeField] private float multipleProjectileAngle;
    public float MultipleProjectileAngle { get { return multipleProjectileAngle; } }
    [SerializeField] private Color projectileColor;
    public Color ProjectileColor { get { return projectileColor; } }

    private ProjectileManager projectileManager;

    protected override void Start()
    {
        base.Start();
        projectileManager = ProjectileManager.Instance;
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
        projectileManager.ShootBullet(this, projectileSpawnPosition.position, RotateVector2(_lookDirection, angle));
    }//�����ִ� ���� �ڽŰ�, spawnPosition, �����ִ� ���⿡�� angle��ŭ ���� ������ vector�� ������.
    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }
}
