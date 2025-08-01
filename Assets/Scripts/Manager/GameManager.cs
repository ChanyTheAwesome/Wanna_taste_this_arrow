using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    private EnemyManager _enemyManager;
    //private PlayerController _playerController;

    //public PlayerController PlayerController { get { return _playerController; } }

    [SerializeField] private GameObject[] characterPrefabs;
    private int selectedIndex = 0;

    private int stageCount = 0;
    public int StageCount
    {
        get { return stageCount; }
        set { stageCount = value; }
    }
    // ���� �������� �������� �ѹ�, �������� ���۽ÿ� �ö󰡰� ����

    private UIState _currentState;

    public UIState CurrentState { get { return _currentState; } set { _currentState = value; } }

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
        _enemyManager = FindObjectOfType<EnemyManager>();
        //_playerController = FindObjectOfType<PlayerController>();
        //DungeonManager.Instance.CurrentDungeonID = 1;

        CurrentState = UIState.Home;
    }
    private void Start()
    {
        DungeonManager.Instance.CurrentDungeonID = 1;
    }

    private void Update()
    {
        
    }

    public void StartGame() // ó���� ���� �������� �� ������ �͵�, ���������� ���� ���� �ƴ�, ���� 1ȸ�� ����� �ٽ� ���ƿ͵� ����ȵ�
    {
        
    }

    //public void UpStageCount()  // �׽�Ʈ��
    //{
    //    stageCount++;
    //    Debug.Log(stageCount);
    //}

    //public void DownStageCount()    // �׽�Ʈ��
    //{
    //    stageCount--;
    //    Debug.Log(stageCount);
    //}

    //public void CheckEnemy()    // �׽�Ʈ��
    //{
    //    DungeonManager.Instance.CheckClearStage();
    //}

    //public void SetPlayer() // �׽�Ʈ��
    //{
    //    _playerController = FindObjectOfType<PlayerController>();
    //}
}
