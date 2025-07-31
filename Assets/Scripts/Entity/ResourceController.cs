using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = 0.5f;

    private BaseController _baseController;
    private StatHandler _statHandler;
    private AnimationHandler _animationHandler;

    private float _timeSinceLastChange = float.MaxValue;

    public float CurrentHealth { get; private set; }
    public float Maxhealth => _statHandler.Health;

    public AudioClip damageClip;

    private Action<float, float> OnChangeHealth;

    private void Awake()
    {
        _baseController = GetComponent<BaseController>();
        _statHandler = GetComponent<StatHandler>();
        _animationHandler = GetComponent<AnimationHandler>();
    }

    private void Start()
    {
        CurrentHealth = _statHandler.Health;//ü���� �ִ�� �������ְ�
    }
    private void Update()
    {
        if (_timeSinceLastChange < healthChangeDelay)//������ ü�º�ȭ�ð����� ���� �ð��� ��ٸ�
        {
            _timeSinceLastChange += Time.deltaTime;//�ð��� ���غ���
            if (_timeSinceLastChange >= healthChangeDelay)
            {
                _animationHandler.InvincibilityEnd();//�����ٸ� �����ð��� ������.
            }
        }//���̷��� �߳ĸ�, �ܼ��� if-else�� �ߴٸ� InvincibilityEnd�� ��� ȣ����� ���̴�. �ѹ��� ȣ���ϵ��� �����ߴ�.
    }
    public bool ChangeHealth(float change)
    {
        if (change == 0 || _timeSinceLastChange < healthChangeDelay)
        {
            return false;//��ȭ���� ���ų�, �����ð����ȿ� false�� ������.
        }

        _timeSinceLastChange = 0.0f;//������ �ð��� 0�ʷ� �����ϰ�
        CurrentHealth += change;//��ȭ�� �� ����,
        CurrentHealth = (CurrentHealth > Maxhealth) ? Maxhealth : CurrentHealth;//��ȭ���� �ִ�ü�º��� ������ üũ,
        CurrentHealth = (CurrentHealth < 0) ? 0: CurrentHealth;//0���� ������ üũ�Ѵ�.

        OnChangeHealth?.Invoke(CurrentHealth, Maxhealth);//OnChangeHealth�� ��ϵ� �۵��� �ִٸ� �����Ѵ�.
        if(change < 0)
        {
            _animationHandler.Damage();//�¾Ҵٴ� �ִϸ��̼ǰ�
            if (damageClip != null)
            {
                //SoundManager.PlayClip(damageClip);//�Ҹ��� ����ϰ�
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
        _baseController.Death();
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
