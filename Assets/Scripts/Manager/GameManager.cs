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
    // 현재 진행중인 스테이지 넘버, 스테이지 시작시에 올라가게 설정

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

    public void StartGame() // 처음에 게임 실행했을 때 실행할 것들, 스테이지나 던전 시작 아님, 최초 1회만 실행됨 다시 돌아와도 실행안됨
    {
        CurrentState = UIState.Home;
        DungeonManager.Instance.CurrentDungeonID = 1;
    }
}
