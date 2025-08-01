using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState // UI 상태 enum, 홈, 게임중, 게임종료 등으로 나눌 예정 < 필요한가 모르겠음
{
    Home,
    Game,
    GameOver
}

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance {  get { return instance; } }

    //public UIState currentState;

    HomeUI _homeUI;
    GameUI _gameUI;
    GameOverUI _gameOverUI;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _homeUI = GetComponentInChildren<HomeUI>(true);
        //_homeUI.Init(/*this*/);
        _gameUI = GetComponentInChildren<GameUI>(true);
        //_gameUI.Init(/*this*/);
        _gameOverUI = GetComponentInChildren<GameOverUI>(true);
        //_gameOverUI.Init(/*this*/);
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
        // 일시정지나 메뉴 UI 띄우기
        ChangeState(UIState.Game);
    }

    public void SetGameOver()   // 게임 오버시 UI 설정
    {
        // 일시정지 후 게임오버 UI 띄우기
        // 실패했다는 UI 출력(진행결과 등 포함?, 홈으로 돌아가기 버튼)
        ChangeState(UIState.GameOver);
    }

    public void SetClearStage() // 스테이지 클리어 시 UI 설정
    {
        // 스테이지 클리어했다는 문구라도 띄우기
    }

    public void ChangeState(UIState state)
    {
        GameManager.Instance.CurrentState = state;
        //_homeUI.SetActive(GameManager.Instance.CurrentState);
        //_gameUI.SetActive(GameManager.Instance.CurrentState);
        //_gameOverUI.SetActive(GameManager.Instance.CurrentState);
    }
}
