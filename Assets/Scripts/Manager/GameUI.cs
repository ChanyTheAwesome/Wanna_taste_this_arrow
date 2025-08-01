using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    [SerializeField] private Button _menuButton;    // 메뉴 버튼
    [SerializeField] private Button _exitDungeonButton; // 던전 나가기 버튼
    [SerializeField] private Button _closeMenuButton;  // UI 닫기 버튼
    [SerializeField] private Image _menuImage; // 메뉴 눌렀을 때 띄울 UI 이미지
    [SerializeField] private GameObject _characterSelectUI;
    [SerializeField] private Button _firstCharacterIndex;
    [SerializeField] private Button _secondCharacterIndex;
    [SerializeField] private Button _thirdCharacterIndex;
    [SerializeField] private Button _fourthCharacterIndex;

    //테스트용
    [SerializeField] private Button _clearButton;
    //테스트용
    // 메뉴 버튼 눌렀을 때

    private void Awake()
    {
        //Init();
        _menuButton.onClick.AddListener(OnClickMenuButton);
        _exitDungeonButton.onClick.AddListener(OnClickExitDungeonButton);
        _closeMenuButton.onClick.AddListener(OnClickCloseMenuButton);
        _clearButton.onClick.AddListener(OnClickClearStage);
        _firstCharacterIndex.onClick.AddListener(OnClickFirstButton);
        _secondCharacterIndex.onClick.AddListener(OnClickSecondButton);
        _thirdCharacterIndex.onClick.AddListener(OnClickThirdButton);
        _fourthCharacterIndex.onClick.AddListener(OnClickFourthButton);
    }

    private void Start()
    {
        if (DungeonManager.Instance.IsFirstStage)
        {
            Debug.Log("true");
            SetCharacterSelect();
            DungeonManager.Instance.IsFirstStage = false;
        }
        else
        {
            Debug.Log("false");
            SetGame();
            PlayerManager.Instance.SetCharacter();  // 캐릭터 외형 변경
        }
    }

    //public override void Init()
    //{
    //    _menuButton.onClick.AddListener(OnClickMenuButton);
    //    _exitDungeonButton.onClick.AddListener(OnClickExitDungeon);
    //    _closeUIButton.onClick.AddListener(OnClickExitUI);
    //}

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }

    public void OnClickMenuButton()   // 메뉴 버튼 클릭
    {
        StopGame(); // 게임 정지
        SetMenu();  // 메뉴창 띄우기
    }

    public void OnClickExitDungeonButton()
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

    public void OnClickCloseMenuButton() // UI 닫기 버튼 클릭
    {
        // UI 비활성화
        SetGame();
        ResumeGame();
    }

    public void OnClickFirstButton()
    {
        ResumeGame();
        _characterSelectUI.SetActive(false);
        PlayerManager.Instance.SelectedIndex = 0;
        PlayerManager.Instance.SetCharacter();
        _menuButton.gameObject.SetActive(true);
        _menuImage.gameObject.SetActive(false);

    }
    public void OnClickSecondButton()
    {
        ResumeGame();
        _characterSelectUI.SetActive(false);
        PlayerManager.Instance.SelectedIndex = 1;
        PlayerManager.Instance.SetCharacter();
        _menuButton.gameObject.SetActive(true);
        _menuImage.gameObject.SetActive(false);
    }
    public void OnClickThirdButton()
    {
        ResumeGame();
        _characterSelectUI.SetActive(false);
        PlayerManager.Instance.SelectedIndex = 2;
        PlayerManager.Instance.SetCharacter();
        _menuButton.gameObject.SetActive(true);
        _menuImage.gameObject.SetActive(false);
    }
    public void OnClickFourthButton()
    {
        ResumeGame();
        _characterSelectUI.SetActive(false);
        PlayerManager.Instance.SelectedIndex = 3;
        PlayerManager.Instance.SetCharacter();
        _menuButton.gameObject.SetActive(true);
        _menuImage.gameObject.SetActive(false);
    }

    public void SetCharacterSelect()
    {
        StopGame();
        _characterSelectUI.SetActive(true);
        _menuButton.gameObject.SetActive(false);
        _menuImage.gameObject.SetActive(false);
    }

    public void SetGame()   // 게임중 UI 설정
    {
        // 메뉴 UI 띄우기
        _menuButton.interactable = true;
        _menuImage.gameObject.SetActive(false);
        _characterSelectUI.SetActive(false);
    }

    public void SetMenu()
    {
        _menuButton.interactable = false;
        _menuImage.gameObject.SetActive(true);
        _characterSelectUI.SetActive(false);
    }

    public void OnClickClearStage()    // 테스트용 메서드
    {
        Debug.Log("클리어 누름");
        DungeonManager.Instance.ClearStage();
        DungeonManager.Instance.gate.OpenGate();
    }
}
