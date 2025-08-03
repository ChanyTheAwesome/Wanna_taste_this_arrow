using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonManager : MonoBehaviour
{
    private static DungeonManager instance;
    public static DungeonManager Instance => instance;

    public List<Dungeon> DungeonList = new List<Dungeon>();

    private bool _isClear = false;
    public bool IsClear => _isClear;    // �������� Ŭ���� �ߴ���

    private int _currentDungeonID = 1;
    public int CurrentDungeonID {
        get { return _currentDungeonID; }
        set { _currentDungeonID = value; }
    }
    
    private bool _isFirstStage;

    public bool IsFirstStage { get { return _isFirstStage; } set { _isFirstStage = value; } }

    private EnemyManager _enemyManager;
    public EnemyManager EnemyManager { get { return _enemyManager; } set { _enemyManager = value; } }
    private EnemyController _enemyController;

    public Gate gate;

    [SerializeField] private Material caveMaterial;
    public Material CaveMaterial { get { return caveMaterial; } }

    //Suggestion

    public Dictionary<int, Dungeon> DungeonDict = new Dictionary<int, Dungeon>();
    
    private void AddDungeonDict()   // �׽�Ʈ������ �������� 3���� ����, ���Ŀ� �ٽ� ���� �ʿ�
    {
        DungeonDict.Add(1, new Dungeon(1, "���ڴ� ������ ����^^", 10, 6, 3, 1.05f));
        DungeonDict.Add(2, new Dungeon(2, "��ȣ���� ���� ����^^", 10, 6, 3, 1.05f));
        DungeonDict.Add(3, new Dungeon(3, "�� ���� �� ������^^", 10, 6, 3, 1.05f));
    }
     
    //Or try to use Json/newtonJson to read json data.
     

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }
    private void Init()
    {
        AddDungeonDict();

        _enemyManager = FindObjectOfType<EnemyManager>();
        _enemyController = FindObjectOfType<EnemyController>();
        //These are too heavy to deal with, and some minor potential errors
    }

    public void CheckClearStage()    // �������� Ŭ���� Ȯ��, ���� ���� ������ ����ϸ� �ɵ�
    {
        // ���ʹ� �ִ��� Ȯ��
        if (_enemyManager.CheckEnemyExist()) // ���� �����ִٸ�
        {
            Debug.Log("Ŭ���� �ȵ�");    // �׽�Ʈ��
            return;
        }
        else    // ���� ���ٸ�
        {
            Debug.Log("Ŭ����");   // �׽�Ʈ��
            ClearStage();
        }   
    }

    public void StartDungeon()  // ���� ���۽� ������ �͵�
    {
        Debug.Log($"{CurrentDungeonID} ���� ����");   // �׽�Ʈ��
        _isFirstStage = true;
        StartStage();
    }

    public void StartStage()    // �������� ���۽� ������ �͵�
    {
        Debug.Log("�������� ����");   // �׽�Ʈ��
        _isClear = false;
        // ���� ���� �ø���
        //if (!_isFirstStage)  // ù ���������� �ƴ϶�� = ù ���������� �⺻ ü�¸� ������
        //{
        //    _enemyController.SetEnemyHealth(DungeonDict[_currentDungeonID].IncreaseStat);   // ID�� ���� ã�Ƽ� �� ������ IncreaseStat�� �����ͼ� SetEnemyHealth �����ϱ�
        //}
        GameManager.Instance.StageCount++;
        //UIManager.Instance.SetGame();   // ������ ���� �ε��ϴ� ������ �Ѿ�� UI�� �ʱ�ȭ �ɰ��� �� �ҷ����� �����ؾߵɵ�
        //_isFirstStage = false;
        //if(GameManager.Instance.StageCount < DungeonDict[CurrentDungeonID].MaxStageCount)   // ������ ���������� �ƴϸ� �Ϲ� �� �ҷ�����
        //{
        //    SceneController.Instance.LoadDungeonScene();
        //}
        //else    // ������ ���������� ���� �� �ҷ�����
        //{
        //    SceneController.Instance.LoadBossScene();
        //}
        if (CheckBossStage())   // ���� ���������� ���� �� �ҷ�����
        {
            SceneController.Instance.LoadBossScene();
        }
        else    // �Ϲ� ���������� �Ϲ� �� �ҷ�����
        {
            SceneController.Instance.LoadDungeonScene();
        }
    }

    public void ClearStage()    // �������� Ŭ����� ������ �͵�
    {
        Debug.Log("�������� Ŭ����");  // �׽�Ʈ��
        _isClear = true;
        gate.OpenGate();
        AchievementManager.Instance.OnStageClear(GameManager.Instance.StageCount);
        UIManager.Instance.SetClear();
    }

    public void GameOver()  // ���ӿ����� ������ �͵�
    {
        UIManager.Instance.SetGameOver();
    }

    public void ExitDungeon()   // �������� ���� �� ������ �͵�
    {
        // �÷��̾� ���� 1�� �����
        // �÷��̾� ����ġ 0���� �����
        PlayerManager.Instance.ResetPlayer();
        // stageCount 0���� ���߱�
        GameManager.Instance.StageCount = 0;
        // currentDungeonID 0���� ���߱�
        _currentDungeonID = 1;
        // �÷��̾� ���� �ʱ�ȭ�ϱ�
        PlayerManager.Instance.SelectedIndex = 0;
        PlayerManager.Instance.SetCharacter();
        UIManager.Instance.SetHome();
        // Ȩ ȭ�� �ҷ�����
        SceneController.Instance.LoadMainScene();
    }

    public bool CheckBossStage(int num = 0)    // ������ ���� ���������� true, �ƴϸ� false, num �Ű������� ��� �������� ����� ���� ����
    {
        if (GameManager.Instance.StageCount == (DungeonDict[CurrentDungeonID].MaxStageCount + num))
        {
            return true;
        }
        else return false;
    }

    public void ChangeStat()    // �׽�Ʈ�� �޼���
    {
        //_currentDungeonID = 1;//Meaning?
        //_enemyController.SetEnemyHealth(DungeonList.Find(d => d.ID == _currentDungeonID).IncreaseStat);
        _enemyController.SetEnemyHealth(DungeonDict[_currentDungeonID].IncreaseStat);
        //_currentDungeonID = 0;
    }
}