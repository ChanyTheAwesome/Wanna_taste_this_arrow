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

    //�׽�Ʈ��
    [SerializeField] private Button clearButton;
    [SerializeField] private Button gameOverButton;
    private bool _isMenuOn = false;
    //�׽�Ʈ��
    // �޴� ��ư ������ ��

    private void Awake()
    {
        //Init();
        UIManager.Instance.GameUI = this;

        exitDungeonButton.onClick.AddListener(OnClickExitDungeonButton);
        closeMenuButton.onClick.AddListener(OnClickCloseMenuButton);
        firstCharacterIndex.onClick.AddListener(OnClickFirstButton);
        secondCharacterIndex.onClick.AddListener(OnClickSecondButton);
        thirdCharacterIndex.onClick.AddListener(OnClickThirdButton);
        fourthCharacterIndex.onClick.AddListener(OnClickFourthButton);

        //�׽�Ʈ��
        clearButton.onClick.AddListener(OnClickClearStage);
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
            Debug.Log("UI ����");
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

    public void OpenMenu()   // �޴� ��ư Ŭ��
    {
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

    //public void OnClickOption() // �ɼ� ��ư Ŭ��
    //{
    //    StopGame();
    //    // �ɼ� UI ����
    //}

    public void OnClickCloseMenuButton() // UI �ݱ� ��ư Ŭ��
    {
        // UI ��Ȱ��ȭ
        SetGame();
        ResumeGame();
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
        Debug.Log("SetGame");
        menuImage.gameObject.SetActive(false);
        characterSelectUI.SetActive(false);
        abilitySelectUI.SetActive(false);
        Debug.Log("�������� ���ߵǴµ�");
        achievementImage.gameObject.SetActive(false);
    }

    public void SetMenu()
    {
        menuImage.gameObject.SetActive(true);
        characterSelectUI.SetActive(false);
    }

    public IEnumerator SetAchievementUI(string name, string description)
    {
        achievementImage.gameObject.SetActive(true);
        achievementTitle.text = name;
        achievementDescription.text = description;
        yield return new WaitForSeconds(3);
        achievementImage.gameObject.SetActive(false);
    }

    public void OnClickClearStage()    // �׽�Ʈ�� �޼���
    {
        Debug.Log("Ŭ���� ����");
        DungeonManager.Instance.ClearStage();
        DungeonManager.Instance.gate.OpenGate();
    }

    public void OnClickGameOverStage()  // �׽�Ʈ�� �޼���
    {
        Debug.Log("���ӿ��� ����");
        DungeonManager.Instance.GameOver();
    }

    public void OnClickGetExp()    // �׽�Ʈ��
    {
        PlayerManager.Instance.GetEXP(50);
    }
}
