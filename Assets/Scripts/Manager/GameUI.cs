using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    [SerializeField] private Button exitDungeonButton; // ���� ������ ��ư
    [SerializeField] private Button closeMenuButton;  // UI �ݱ� ��ư
    [SerializeField] private Image menuImage; // �޴� ������ �� ��� UI �̹���

    // ĳ���� ���� ���� UI
    [SerializeField] private GameObject characterSelectUI;
    [SerializeField] private Button firstCharacterIndex;
    [SerializeField] private Button secondCharacterIndex;
    [SerializeField] private Button thirdCharacterIndex;
    [SerializeField] private Button fourthCharacterIndex;

    // �ɷ� ���� UI
    [SerializeField] private GameObject abilitySelectUI;

    // ���� ���� Ŭ���� UI
    [SerializeField] private Image achievementImage;
    [SerializeField] private Text achievementTitle;
    [SerializeField] private Text achievementDescription;

    private bool _isMenuOn = false;

    //�׽�Ʈ��
    [SerializeField] private Button getExpButton;
    [SerializeField] private Button stageClearButton;
    [SerializeField] private Button gameOverButton;
    //�׽�Ʈ��

    //������
    [SerializeField] private GameOverUI gameOverUI;

    private bool isActiveGameOverUI;
    private bool isActiveCharacterSelectUI;
    private bool isActiveAbilitySelectUI;
    //������

    private void Awake()
    {
        UIManager.Instance.GameUI = this;

        exitDungeonButton.onClick.AddListener(OnClickExitDungeonButton);
        closeMenuButton.onClick.AddListener(OnClickCloseMenuButton);
        firstCharacterIndex.onClick.AddListener(OnClickFirstButton);
        secondCharacterIndex.onClick.AddListener(OnClickSecondButton);
        thirdCharacterIndex.onClick.AddListener(OnClickThirdButton);
        fourthCharacterIndex.onClick.AddListener(OnClickFourthButton);

        //�׽�Ʈ��
        getExpButton.onClick.AddListener(OnClickGetExp);
        stageClearButton.onClick.AddListener(OnClickClearStage);
        gameOverButton.onClick.AddListener(OnClickGameOverStage);
        //�׽�Ʈ��
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
            PlayerManager.Instance.SetCharacter();  // ĳ���� ���� ����
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

    public void OpenMenu()   // �޴� ��ư Ŭ��
    {
        isActiveGameOverUI = gameOverUI.gameOverImage.gameObject.activeSelf;
        isActiveCharacterSelectUI = characterSelectUI.activeSelf;
        isActiveAbilitySelectUI = abilitySelectUI.activeSelf;
        _isMenuOn = true;
        StopGame(); // ���� ����
        SetMenu();  // �޴�â ����
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
        Time.timeScale = 1; // ������� �ð� �ٽ� ����
        // Ȩ �� �ҷ�����
        DungeonManager.Instance.ExitDungeon();
    }

    public void OnClickCloseMenuButton() // UI �ݱ� ��ư Ŭ��
    {
        // UI ��Ȱ��ȭ
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

    public void SetGame()   // ������ UI ����
    {
        // �޴� UI ����
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

    public void OnClickGetExp()    // �׽�Ʈ�� �޼���
    {
        PlayerManager.Instance.GetEXP(50);
    }

    public void OnClickClearStage()    // �׽�Ʈ�� �޼���
    {
        DungeonManager.Instance.ClearStage();
        DungeonManager.Instance.gate.OpenGate();
    }

    public void OnClickGameOverStage()  // �׽�Ʈ�� �޼���
    {
        DungeonManager.Instance.GameOver();
    }
}
