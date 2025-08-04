using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState // UI 상태
{
    Home,
    Game,
    GameOver,
    Clear
}

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance {  get { return instance; } }

    HomeUI _homeUI;
    public HomeUI HomeUI { get { return _homeUI; } set { _homeUI = value; } }

    GameUI _gameUI;
    public GameUI GameUI { get { return _gameUI; } set { _gameUI = value; } }

    GameOverUI _gameOverUI;
    public GameOverUI GameOverUI { get { return _gameOverUI; } set{ _gameOverUI = value; } }

    ClearUI _clearUI;
    public ClearUI ClearUI { get { return _clearUI; } set { _clearUI = value; } }

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
        SetHome();
    }

    public void SetHome()
    {
        ChangeState(UIState.Home);
    }

    public void SetGame()   // 게임중 UI 설정
    {
        ChangeState(UIState.Game);
    }

    public void SetClear()
    {
        _clearUI.SetClearUI();
        ChangeState(UIState.Clear);
    }

    public void SetGameOver()   // 게임 오버시 UI 설정
    {
        // 일시정지 후 게임오버 UI 띄우기
        _gameUI.SetActiveGameUI(false);
        _clearUI.SetActiveClearUI(false);
        _gameOverUI.SetGameOverUI();
        // 실패했다는 UI 출력(진행결과 등 포함?, 홈으로 돌아가기 버튼)
        ChangeState(UIState.GameOver);
    }

    public void SetAchievement(string name, string description)
    {
        StartCoroutine(_gameUI.SetAchievementUI(name, description));
    }

    public void ChangeState(UIState state)
    {
        GameManager.Instance.CurrentState = state;
    }
}
