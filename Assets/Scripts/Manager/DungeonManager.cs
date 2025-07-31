using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    private static DungeonManager instance;
    public static DungeonManager Instance => instance;

    public List<Dungeon> DungeonList = new List<Dungeon>();

    private bool _isClear = false;
    public bool IsClear => _isClear;    // �������� Ŭ���� �ߴ���

    private int _currentDungeonID = 0;
    public int CurrentDungeonID {
        get { return _currentDungeonID; }
        set { _currentDungeonID = value; }
    }
    
    private bool _isFirstStage;

    private EnemyManager _enemyManager;
    private EnemyController _enemyController;

    /*Suggestion
     * 
     * public Dictionary<int, Dungeon> DungeonDict = new Dictionary<int, Dungeon>;
     * 
     * private void AddDungeonDict(){
     * DungeonDict.Add(1, new Dungeon(1, "1�ܰ� ����", 10, 6, 3, 1.05f));
        DungeonDict.Add(2, new Dungeon(2, "2�ܰ� ����", 10, 6, 3, 1.05f));
        DungeonDict.Add(3, new Dungeon(3, "3�ܰ� ����", 10, 6, 3, 1.05f));
     * }
     * 
     * //Or try to use Json/newtonJson to read json data.
     */

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

        Init();
    }
    private void Init()
    {
        AddDungeonList();

        _enemyManager = FindObjectOfType<EnemyManager>();
        _enemyController = FindObjectOfType<EnemyController>();
        //These are too heavy to deal with, and some minor potential errors
    }
    private void AddDungeonList()
    {
        DungeonList.Add(new Dungeon(1, "1�ܰ� ����", 10, 6, 3, 1.05f));
        DungeonList.Add(new Dungeon(2, "2�ܰ� ����", 10, 6, 3, 1.05f));
        DungeonList.Add(new Dungeon(3, "3�ܰ� ����", 10, 6, 3, 1.05f));
    }

    public void CheckClearStage()    // �������� Ŭ���� Ȯ��, ���� ���� ������ ����ϸ� �ɵ�
    {
        // ���ʹ� �ִ��� Ȯ��
        if (_enemyManager.CheckEnemyExist()) // ���� �����ִٸ�
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
        _isFirstStage = true;
        _currentDungeonID = dungeonID;   // dungeonID�� ��� �����;ߵɱ�
        StartStage();
    }

    public void StartStage()    // �������� ���۽� ������ �͵�
    {
        _isClear = false;
        // ���� ���� �ø���
        if (!_isFirstStage)  // ù ���������� �ƴ϶�� = ù ���������� �⺻ ü�¸� ������
        {
            _enemyController.SetEnemyHealth(DungeonList.Find(d => d.ID == _currentDungeonID).IncreaseStat);   // ID�� ���� ã�Ƽ� �� ������ IncreaseStat�� �����ͼ� SetEnemyHealth �����ϱ�
        }
        GameManager.Instance.StageCount++;
        UIManager.Instance.SetGame();
        _isFirstStage = false; //<- Does this variable need to be here? If not, consider moving it to the top. 
    }

    public void ClearStage()    // �������� Ŭ����� ������ �͵�
    {
        Debug.Log("�������� Ŭ����");
        _isClear = true;
        UIManager.Instance.SetClearStage();
    }

    public void GameOver()  // ���ӿ����� ������ �͵�
    {
        UIManager.Instance.SetGameOver();
    }

    public void ExitDungeon()   // �������� ���� �� ������ �͵�
    {
        // �÷��̾� ���� 1�� �����
        // �÷��̾� ����ġ 0���� �����
        PlayerManager.instance.ResetPlayer();
        // stageCount 0���� ���߱�
        GameManager.Instance.StageCount = 0;
        // currentDungeonID 0���� ���߱�
        _currentDungeonID = 0;
        // Ȩ ȭ�� �ҷ�����
        SceneController.Instance.LoadMainScene();
    }

    public void ChangeStat()
    {
        _currentDungeonID = 1;//Meaning?
        _enemyController.SetEnemyHealth(DungeonList.Find(d => d.ID == _currentDungeonID).IncreaseStat);
        _currentDungeonID = 0;
    }
}