using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public int Exp { get; private set; }   // ?? �÷��̾�� �׳� ������ �� ����ġ 0���� �ʱ�ȭ�ϸ� �Ǵ� �� �ƴ�?
    public int Level { get; private set; }
    public int requiredExp => Level * 10 + 50;  // �ϴ� �⺻ 50 + ������ 10���� ����

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetEXP(int exp) // ����ġ ȹ�� �� ���� �� �����ϱ�
    {
        Exp += exp;
        LevelUpCheck();
    }

    public void LevelUpCheck()  // ������ Ȯ�� �� ���� ���
    {
        if(Exp >= requiredExp)
        {
            Exp -= requiredExp;
            Level++;
        }
    }

    public void ResetPlayer()    // ����, ����ġ, �ɷ� �ʱ�ȭ, �������� ���� �� �����ϱ�
    {
        Level = 1;
        Exp = 0;
        // �ɷ� �ʱ�ȭ
        // �ɷ� �ʱ�ȭ�� �ɷ��� ��� ����Ǵ����� ���� ���缭 ����
    }
}
