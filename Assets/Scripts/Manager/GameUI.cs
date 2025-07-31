using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    [SerializeField] private Button _menuButton;    // 메뉴 버튼
    [SerializeField] private Button _exitDungeonButton; // 던전 나가기 버튼
    [SerializeField] private Button _exitUIButton;  // UI 닫기 버튼
    [SerializeField] private Image _dummyImage; // 메뉴 눌렀을 때 띄울 UI의 테스트용 더미
    // 메뉴 버튼 눌렀을 때

    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        _menuButton.onClick.AddListener(OnClickMenuButton);
        _exitDungeonButton.onClick.AddListener(OnClickExitDungeon);
        _exitUIButton.onClick.AddListener(OnClickExitUI);
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }

    public void OnClickMenuButton()   // 메뉴 버튼 클릭
    {
        StopGame(); // 게임 정지
        SetMenu();  // 메뉴창 띄우기
    }

    public void OnClickExitDungeon()
    {
        Time.timeScale = 1; // 멈춰놨던 시간 다시 세팅
        // 홈 씬 불러오기
        DungeonManager.Instance.ExitDungeon();
    }

    //public void OnClickOption() // 옵션 버튼 클릭
    //{
    //    StopGame();
    //    // 옵션 UI 띄우기
    //}

    public void OnClickExitUI() // UI 닫기 버튼 클릭
    {
        // UI 비활성화
        SetGame();
        ResumeGame();
    }

    public void SetGame()   // 게임중 UI 설정
    {
        // 메뉴 UI 띄우기
        _menuButton.gameObject.SetActive(true);
        _dummyImage.gameObject.SetActive(false);
    }

    public void SetMenu()
    {
        _menuButton.gameObject.SetActive(false);
        _dummyImage.gameObject.SetActive(true);
    }
}
