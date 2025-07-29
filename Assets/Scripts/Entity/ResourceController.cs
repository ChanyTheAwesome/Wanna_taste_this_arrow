using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = 0.5f;
    private BaseController baseController;
    private StatHandler statHandler;
    private AnimationHandler animationHandler;

    private float timeSinceLastChange = float.MaxValue;

    public float CurrentHealth { get; private set; }
    public float Maxhealth => statHandler.Health;

    public AudioClip damageClip;

    private Action<float, float> OnChangeHealth;

    private void Awake()
    {
        baseController = GetComponent<BaseController>();
        statHandler = GetComponent<StatHandler>();
        animationHandler = GetComponent<AnimationHandler>();
    }

    private void Start()
    {
        CurrentHealth = statHandler.Health;//ü���� �ִ�� �������ְ�
    }
    private void Update()
    {
        if (timeSinceLastChange < healthChangeDelay)//������ ü�º�ȭ�ð����� ���� �ð��� ��ٸ�
        {
            timeSinceLastChange += Time.deltaTime;//�ð��� ���غ���
            if (timeSinceLastChange >= healthChangeDelay)
            {
                animationHandler.InvincibilityEnd();//�����ٸ� �����ð��� ������.
            }
        }//���̷��� �߳ĸ�, �ܼ��� if-else�� �ߴٸ� InvincibilityEnd�� ��� ȣ����� ���̴�. �ѹ��� ȣ���ϵ��� �����ߴ�.
    }
    public bool ChangeHealth(float change)
    {
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
        {
            return false;//��ȭ���� ���ų�, �����ð����ȿ� false�� ������.
        }

        timeSinceLastChange = 0.0f;//������ �ð��� 0�ʷ� �����ϰ�
        CurrentHealth += change;//��ȭ�� �� ����,
        CurrentHealth = (CurrentHealth > Maxhealth) ? Maxhealth : CurrentHealth;//��ȭ���� �ִ�ü�º��� ������ üũ,
        CurrentHealth = (CurrentHealth < 0) ? 0: CurrentHealth;//0���� ������ üũ�Ѵ�.

        OnChangeHealth?.Invoke(CurrentHealth, Maxhealth);//OnChangeHealth�� ��ϵ� �۵��� �ִٸ� �����Ѵ�.
        if(change < 0)
        {
            animationHandler.Damage();//�¾Ҵٴ� �ִϸ��̼ǰ�
            if (damageClip != null)
            {
                SoundManager.PlayClip(damageClip);//�Ҹ��� ����ϰ�
            }
        }

        if (CurrentHealth <= 0.0f)
        {
            Death();//ü���� 0�̵Ǹ� ��� �޼��带 ȣ���Ѵ�.
        }

        return true;
    }
    private void Death()
    {
        baseController.Death();
    }

    public void AddHealthChangeEvent(Action<float, float> action)
    {
        OnChangeHealth += action;//OnChangeHealth�׼ǿ� �̺�Ʈ�� �߰��Ѵ�.
    }

    public void RemoveHealthChangeEvent(Action<float, float> action)
    {
        OnChangeHealth -= action;//OnChangeHealth�׼ǿ� �̺�Ʈ�� �����Ѵ�.
    }
}
