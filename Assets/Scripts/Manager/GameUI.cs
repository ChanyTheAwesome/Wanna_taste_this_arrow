using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    [SerializeField] private Button exitDungeonButton; // 던전 나가기 버튼
    [SerializeField] private Button closeMenuButton;  // UI 닫기 버튼
    [SerializeField] private Image menuImage; // 메뉴 눌렀을 때 띄울 UI 이미지

    // 캐릭터 외형 선택 UI
    [SerializeField] private GameObject characterSelectUI;
    [SerializeField] private Button firstCharacterIndex;
    [SerializeField] private Button secondCharacterIndex;
    [SerializeField] private Button thirdCharacterIndex;
    [SerializeField] private Button fourthCharacterIndex;

    // 능력 선택 UI
    [SerializeField] private GameObject abilitySelectUI;

    // 도전 과제 클리어 UI
    [SerializeField] private Image achievementImage;
    [SerializeField] private Text achievementTitle;
    [SerializeField] private Text achievementDescription;

    private bool _isMenuOn = false;

    //테스트용
    [SerializeField] private Button getExpButton;
    [SerializeField] private Button stageClearButton;
    [SerializeField] private Button gameOverButton;
    //테스트용

    //수정중
    [SerializeField] private GameOverUI gameOverUI;

    private bool isActiveGameOverUI;
    private bool isActiveCharacterSelectUI;
    private bool isActiveAbilitySelectUI;
    //수정중

    private void Awake()
    {
        UIManager.Instance.GameUI = this;

        exitDungeonButton.onClick.AddListener(OnClickExitDungeonButton);
        closeMenuButton.onClick.AddListener(OnClickCloseMenuButton);
        firstCharacterIndex.onClick.AddListener(OnClickFirstButton);
        secondCharacterIndex.onClick.AddListener(OnClickSecondButton);
        thirdCharacterIndex.onClick.AddListener(OnClickThirdButton);
        fourthCharacterIndex.onClick.AddListener(OnClickFourthButton);

        //테스트용
        getExpButton.onClick.AddListener(OnClickGetExp);
        stageClearButton.onClick.AddListener(OnClickClearStage);
        gameOverButton.onClick.AddListener(OnClickGameOverStage);
        //테스트용
    }

    private void Start()
    {
        if (DungeonManager.Instance.IsFirstStage)
        {
            SetCharacterSelect();
            DungeonManager.Instance.IsFirstStage = false;
        }
        else
        {
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

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }

    public void OpenMenu()   // 메뉴 버튼 클릭
    {
        isActiveGameOverUI = gameOverUI.gameOverImage.gameObject.activeSelf;
        isActiveCharacterSelectUI = characterSelectUI.activeSelf;
        isActiveAbilitySelectUI = abilitySelectUI.activeSelf;
        _isMenuOn = true;
        StopGame(); // 게임 정지
        SetMenu();  // 메뉴창 띄우기
    }

    public void SetActiveGameUI(bool b)
    {
        menuImage.gameObject.SetActive(b);
        characterSelectUI.SetActive(b);
        abilitySelectUI.SetActive(b);
        achievementImage.gameObject.SetActive(b);
    }

    public void OnClickExitDungeonButton()
    {
        Time.timeScale = 1; // 멈춰놨던 시간 다시 세팅
        // 홈 씬 불러오기
        DungeonManager.Instance.ExitDungeon();
    }

    public void OnClickCloseMenuButton() // UI 닫기 버튼 클릭
    {
        // UI 비활성화
        SetGame();
        if(isActiveGameOverUI || isActiveCharacterSelectUI || isActiveAbilitySelectUI)
        {
            gameOverUI.gameOverImage.gameObject.SetActive(isActiveGameOverUI);
            characterSelectUI.SetActive(isActiveCharacterSelectUI);
            abilitySelectUI.SetActive(isActiveAbilitySelectUI);
        }
        else ResumeGame();
    }

    public void CheckMenu()
    {
        if (!_isMenuOn)
        {
            OpenMenu();
            _isMenuOn = true;
        }
        else if (_isMenuOn)
        {
            OnClickCloseMenuButton();
            _isMenuOn= false;
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
        abilitySelectUI.SetActive(false);
        achievementImage.gameObject.SetActive(false);
        secondCharacterIndex.interactable = AchievementManager.Instance.GetAchievementClear()[0];
        thirdCharacterIndex.interactable = AchievementManager.Instance.GetAchievementClear()[1];
        fourthCharacterIndex.interactable = AchievementManager.Instance.GetAchievementClear()[2];
    }

    public void SetGame()   // 게임중 UI 설정
    {
        // 메뉴 UI 띄우기
        menuImage.gameObject.SetActive(false);
        characterSelectUI.SetActive(false);
        abilitySelectUI.SetActive(false);
        achievementImage.gameObject.SetActive(false);
    }

    public void SetMenu()
    {
        menuImage.gameObject.SetActive(true);
        characterSelectUI.SetActive(false);
        abilitySelectUI.SetActive(false);
        gameOverUI.gameOverImage.gameObject.SetActive(false);
    }

    public IEnumerator SetAchievementUI(string name, string description)
    {
        achievementImage.gameObject.SetActive(true);
        achievementTitle.text = name;
        achievementDescription.text = description;
        yield return new WaitForSeconds(3);
        achievementImage.gameObject.SetActive(false);
    }

    public void OnClickGetExp()    // 테스트용 메서드
    {
        PlayerManager.Instance.GetEXP(50);
    }

    public void OnClickClearStage()    // 테스트용 메서드
    {
        DungeonManager.Instance.ClearStage();
        DungeonManager.Instance.gate.OpenGate();
    }

    public void OnClickGameOverStage()  // 테스트용 메서드
    {
        DungeonManager.Instance.GameOver();
    }
}
