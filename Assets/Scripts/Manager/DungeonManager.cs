using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager instance;

    public List<Dungeon> dungeonList = new List<Dungeon>();

    public bool isClear = false;    // �������� Ŭ���� �ߴ���

    public int currentDungeonID = 0;

    bool isFirstStage;

    EnemyManager enemyManager;
    EnemyController enemyController;

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

        dungeonList.Add(new Dungeon(1, "1�ܰ� ����", 10, 6, 3, 1.05f));
        dungeonList.Add(new Dungeon(2, "2�ܰ� ����", 10, 6, 3, 1.05f));
        dungeonList.Add(new Dungeon(3, "3�ܰ� ����", 10, 6, 3, 1.05f));

        enemyManager = FindObjectOfType<EnemyManager>();
        enemyController = FindObjectOfType<EnemyController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckClearStage()    // �������� Ŭ���� Ȯ��, ���� ���� ������ ����ϸ� �ɵ�
    {
        // ���ʹ� �ִ��� Ȯ��
        if (enemyManager.CheckEnemyExist()) // ���� �����ִٸ�
        {
            return;
        }
        else    // ���� ���ٸ�
        {
            ClearStage();
        }   
    }

    public void StartDungeon(int dungeonID)  // ���� ���۽� ������ �͵�
    {
        isFirstStage = true;
        currentDungeonID = dungeonID;   // dungeonID�� ��� �����;ߵɱ�
        StartStage();
    }

    public void StartStage()    // �������� ���۽� ������ �͵�
    {
        isClear = false;
        // ���� ���� �ø���
        if (!isFirstStage)  // ù ���������� �ƴ϶�� = ù ���������� �⺻ ü�¸� ������
        {
            enemyController.SetEnemyHealth(dungeonList.Find(d => d.ID == currentDungeonID).IncreaseStat);   // ID�� ���� ã�Ƽ� �� ������ IncreaseStat�� �����ͼ� SetEnemyHealth �����ϱ�
        }
        GameManager.instance.stageCount++;
        UIManager.instance.SetGame();
        isFirstStage = false;
    }

    public void ClearStage()    // �������� Ŭ����� ������ �͵�
    {
        Debug.Log("�������� Ŭ����");
        isClear = true;
        UIManager.instance.SetClearStage();
    }

    public void GameOver()  // ���ӿ����� ������ �͵�
    {
        UIManager.instance.SetGameOver();
    }

    public void ExitDungeon()   // �������� ���� �� ������ �͵�
    {
        // �÷��̾� ���� 1�� �����
        // �÷��̾� ����ġ 0���� �����
        PlayerManager.instance.ResetPlayer();
        // stageCount 0���� ���߱�
        GameManager.instance.stageCount = 0;
        // currentDungeonID 0���� ���߱�
        currentDungeonID = 0;
        // Ȩ ȭ�� �ҷ�����
        SceneController.instance.LoadMainScene();
    }

    public void ChangeStat()
    {
        currentDungeonID = 1;
        enemyController.SetEnemyHealth(dungeonList.Find(d => d.ID == currentDungeonID).IncreaseStat);
        currentDungeonID = 0;
    }
}
