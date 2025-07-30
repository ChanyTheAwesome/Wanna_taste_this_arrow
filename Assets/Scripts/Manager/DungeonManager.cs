using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager instance;

    public List<Dungeon> dungeonList;

    public bool isClear = false;    // �̰� ���� �ֳ�?

    public bool isEnd = false;  // ���� ������ �� �� ��Ҵ��� ����, true�� �ٲٸ� �ٷ� ���� �������� �̵�

    //public int stageCount = 0;

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

        dungeonList.Clear();
        dungeonList.Add(new Dungeon(1, "1�ܰ� ����", 10, 6, 3));
        dungeonList.Add(new Dungeon(2, "2�ܰ� ����", 10, 6, 3));
        dungeonList.Add(new Dungeon(3, "3�ܰ� ����", 10, 6, 3));
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckClearStage()    // �������� Ŭ����
    {
        // ���ʹ� �ִ��� Ȯ��

        // ���������� ���ư���
        // ������ Ŭ����
        ClearStage();
    }

    public void StartDungeon()  // ���� ���۽� ������ �͵�
    {
        StartStage();
    }

    public void StartStage()    // �������� ���۽� ������ �͵�
    {
        GameManager.instance.stageCount++;
        UIManager.instance.SetGame();
    }

    public void ClearStage()    // �������� Ŭ����� ������ �͵�
    {
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
        // Ȩ ȭ�� �ҷ�����
        SceneController.instance.LoadMainScene();
    }
}
