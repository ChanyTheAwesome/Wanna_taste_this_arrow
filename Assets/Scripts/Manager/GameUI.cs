using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    [SerializeField] private Button exitDungeonButton; // ���� ������ ��ư
    [SerializeField] private Button closeMenuButton;  // UI �ݱ� ��ư
    [SerializeField] private Image menuImage; // �޴� ������ �� ��� UI �̹���
    [SerializeField] private GameObject characterSelectUI;
    [SerializeField] private Button firstCharacterIndex;
    [SerializeField] private Button secondCharacterIndex;
    [SerializeField] private Button thirdCharacterIndex;
    [SerializeField] private Button fourthCharacterIndex;

    //�׽�Ʈ��
    [SerializeField] private Button clearButton;
    private bool _isMenuOn = false;
    //�׽�Ʈ��
    // �޴� ��ư ������ ��

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

    public void SetGame()   // ������ UI ����
    {
        // �޴� UI ����
        menuImage.gameObject.SetActive(false);
        characterSelectUI.SetActive(false);
    }

    public void SetMenu()
    {
        menuImage.gameObject.SetActive(true);
        characterSelectUI.SetActive(false);
    }

    public void OnClickClearStage()    // �׽�Ʈ�� �޼���
    {
        Debug.Log("Ŭ���� ����");
        DungeonManager.Instance.ClearStage();
        DungeonManager.Instance.gate.OpenGate();
    }
}
