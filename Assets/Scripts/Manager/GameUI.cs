using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    [SerializeField] private Button _menuButton;    // �޴� ��ư
    [SerializeField] private Button _exitDungeonButton; // ���� ������ ��ư
    [SerializeField] private Button _closeMenuButton;  // UI �ݱ� ��ư
    [SerializeField] private Image _menuImage; // �޴� ������ �� ��� UI �̹���
    [SerializeField] private GameObject _characterSelectUI;
    [SerializeField] private Button _firstCharacterIndex;
    [SerializeField] private Button _secondCharacterIndex;
    [SerializeField] private Button _thirdCharacterIndex;
    [SerializeField] private Button _fourthCharacterIndex;

    //�׽�Ʈ��
    [SerializeField] private Button _clearButton;
    //�׽�Ʈ��
    // �޴� ��ư ������ ��

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
            PlayerManager.Instance.SetCharacter();  // ĳ���� ���� ����
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

    public void OnClickMenuButton()   // �޴� ��ư Ŭ��
    {
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

    public void SetGame()   // ������ UI ����
    {
        // �޴� UI ����
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

    public void OnClickClearStage()    // �׽�Ʈ�� �޼���
    {
        Debug.Log("Ŭ���� ����");
        DungeonManager.Instance.ClearStage();
        DungeonManager.Instance.gate.OpenGate();
    }
}
