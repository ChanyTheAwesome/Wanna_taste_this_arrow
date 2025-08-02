using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    [SerializeField] private Button exitDungeonButton; // 던전 나가기 버튼
    [SerializeField] private Button closeMenuButton;  // UI 닫기 버튼
    [SerializeField] private Image menuImage; // 메뉴 눌렀을 때 띄울 UI 이미지
    [SerializeField] private GameObject characterSelectUI;
    [SerializeField] private Button firstCharacterIndex;
    [SerializeField] private Button secondCharacterIndex;
    [SerializeField] private Button thirdCharacterIndex;
    [SerializeField] private Button fourthCharacterIndex;

    //테스트용
    [SerializeField] private Button clearButton;
    private bool _isMenuOn = false;
    //테스트용
    // 메뉴 버튼 눌렀을 때

    private void Awake()
    {
        //Init();
        exitDungeonButton.onClick.AddListener(OnClickExitDungeonButton);
        closeMenuButton.onClick.AddListener(OnClickCloseMenuButton);
        clearButton.onClick.AddListener(OnClickClearStage);
        firstCharacterIndex.onClick.AddListener(OnClickFirstButton);
        secondCharacterIndex.onClick.AddListener(OnClickSecondButton);
        thirdCharacterIndex.onClick.AddListener(OnClickThirdButton);
        fourthCharacterIndex.onClick.AddListener(OnClickFourthButton);
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CheckMenu();
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

    public void OpenMenu()   // 메뉴 버튼 클릭
    {
        _isMenuOn = true;
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

    public void CheckMenu()
    {
        if (!_isMenuOn)
        {
            OpenMenu();
        }
        else if (_isMenuOn)
        {
            OnClickCloseMenuButton();
        }
    }

    public void OnClickFirstButton()
    {
        ResumeGame();
        characterSelectUI.SetActive(false);
        PlayerManager.Instance.SelectedIndex = 0;
        PlayerManager.Instance.SetCharacter();
        menuImage.gameObject.SetActive(false);

    }
    public void OnClickSecondButton()
    {
        ResumeGame();
        characterSelectUI.SetActive(false);
        PlayerManager.Instance.SelectedIndex = 1;
        PlayerManager.Instance.SetCharacter();
        menuImage.gameObject.SetActive(false);
    }
    public void OnClickThirdButton()
    {
        ResumeGame();
        characterSelectUI.SetActive(false);
        PlayerManager.Instance.SelectedIndex = 2;
        PlayerManager.Instance.SetCharacter();
        menuImage.gameObject.SetActive(false);
    }
    public void OnClickFourthButton()
    {
        ResumeGame();
        characterSelectUI.SetActive(false);
        PlayerManager.Instance.SelectedIndex = 3;
        PlayerManager.Instance.SetCharacter();
        menuImage.gameObject.SetActive(false);
    }

    public void SetCharacterSelect()
    {
        StopGame();
        characterSelectUI.SetActive(true);
        menuImage.gameObject.SetActive(false);
    }

    public void SetGame()   // 게임중 UI 설정
    {
        // 메뉴 UI 띄우기
        menuImage.gameObject.SetActive(false);
        characterSelectUI.SetActive(false);
    }

    public void SetMenu()
    {
        menuImage.gameObject.SetActive(true);
        characterSelectUI.SetActive(false);
    }

    public void OnClickClearStage()    // 테스트용 메서드
    {
        Debug.Log("클리어 누름");
        DungeonManager.Instance.ClearStage();
        DungeonManager.Instance.gate.OpenGate();
    }
}
