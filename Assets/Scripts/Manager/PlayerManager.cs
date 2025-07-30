using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public int Exp { get; private set; }   // ?? 플레이어에서 그냥 시작할 때 경험치 0으로 초기화하면 되는 거 아님?
    public int Level { get; private set; }
    public int requiredExp => Level * 10 + 50;  // 일단 기본 50 + 레벨당 10으로 설정

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

    public void GetEXP(int exp) // 경험치 획득 적 죽을 때 실행하기
    {
        Exp += exp;
        LevelUpCheck();
    }

    public void LevelUpCheck()  // 레벨업 확인 및 레벨 상승
    {
        if(Exp >= requiredExp)
        {
            Exp -= requiredExp;
            Level++;
        }
    }

    public void ResetPlayer()    // 레벨, 경험치, 능력 초기화, 던전에서 나갈 때 실행하기
    {
        Level = 1;
        Exp = 0;
        // 능력 초기화
        // 능력 초기화는 능력이 어떻게 적용되는지에 따라 맞춰서 설정
    }
}
