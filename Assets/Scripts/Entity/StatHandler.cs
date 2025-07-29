using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour//�ܼ��� ������ ������ �ִ�.
{
    [Range(1, 100)][SerializeField] private int health = 10;//SerializeField�� ���� �ν����Ϳ��� �� �� �ְ� �ϰ�, ������ ������ �� �ֵ��� �ߴ�. �����̴��� �����!
    public int Health { get => health; set => health = Mathf.Clamp(value, 0, 100); }//Clamp�� �ּ�/�ִ밪 �ȿ����� �� �� �ְ� �ϴ� �༮�̴�.

    [Range(1.0f, 20.0f)][SerializeField] private float speed = 3.0f;
    public float Speed { get => speed; set => speed = Mathf.Clamp(value, 0, 20); }
}
