using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;
    
    private int stageCount = 0;
    public int StageCount
    {
        get { return stageCount; }
        set { stageCount = value; }
    }
    // ���� �������� �������� �ѹ�, �������� ���۽ÿ� �ö󰡰� ����

    private UIState _currentState;

    public UIState CurrentState { get { return _currentState; } set { _currentState = value; } }

    public GameObject player;

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
    }
    private void Start()
    {
        StartGame();
    }

    public void StartGame() // ó���� ���� �������� �� ������ �͵�, ���������� ���� ���� �ƴ�, ���� 1ȸ�� ����� �ٽ� ���ƿ͵� ����ȵ�
    {
        CurrentState = UIState.Home;
        DungeonManager.Instance.CurrentDungeonID = 1;
    }
}
