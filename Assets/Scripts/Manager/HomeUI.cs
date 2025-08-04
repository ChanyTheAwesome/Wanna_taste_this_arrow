using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : BaseUI
{
    [SerializeField] private Button _menuButton;
    [SerializeField] private Button _menuCloseButton;
    [SerializeField] private Button _previousDungeonButton;
    [SerializeField] private Button _nextDungeonButton;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitGameButton;

    [SerializeField] private Text _currentDungeonNameText;
    [SerializeField] List<Image> _dungeonImages;
    [SerializeField] private Image _menuImage;

    private List<Button> _otherButtons = new(); // �޴� ������ �� ��Ȱ��ȭ��ų ��ư ����Ʈ

    private void Awake()
    {
        // ��ư Ŭ�� �޼��� ����
        _menuButton.onClick.AddListener(OnClickMenuButton);
        _menuCloseButton.onClick.AddListener(OnClickCloseMenuButton);
        _previousDungeonButton.onClick.AddListener(OnClickPreviousDungeonButton);
        _nextDungeonButton.onClick.AddListener(OnClickNextDungeonButton);
        _startButton.onClick.AddListener(OnClickStartButton);
        _exitGameButton.onClick.AddListener(OnClickExitGameButton);

        AddOtherButtons();
    }
    private void Start()
    {
        _currentDungeonNameText.text = DungeonManager.Instance.DungeonDict[DungeonManager.Instance.CurrentDungeonID].Name;
        SetDungeonImageActive();
    }

    public void OnClickMenuButton()
    {
        _menuImage.gameObject.SetActive(true);
        SetOtherButtonsInteractableState(false);
    }

    public void OnClickCloseMenuButton()
    {
        _menuImage.gameObject.SetActive(false);
        SetOtherButtonsInteractableState(true);
    }

    public void OnClickPreviousDungeonButton()
    {
        if (DungeonManager.Instance.CurrentDungeonID > 1)
        {
            DungeonManager.Instance.CurrentDungeonID--;
            // ���� �̹����� �̸��� �ٲٱ�
            _currentDungeonNameText.text = DungeonManager.Instance.DungeonDict[DungeonManager.Instance.CurrentDungeonID].Name;
            SetDungeonImageActive();
        }
        else return;
    }

    public void OnClickNextDungeonButton()
    {
        if (DungeonManager.Instance.CurrentDungeonID < DungeonManager.Instance.DungeonDict.Count)
        {
            DungeonManager.Instance.CurrentDungeonID++;
            // ���� �̹����� �̸��� �ٲٱ�
            _currentDungeonNameText.text = DungeonManager.Instance.DungeonDict[DungeonManager.Instance.CurrentDungeonID].Name;
            SetDungeonImageActive();
        }
        else return;
    }

    public void OnClickStartButton()
    {
        DungeonManager.Instance.StartDungeon();
    }

    public void OnClickExitGameButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    protected override UIState GetUIState()
    {
        return UIState.Home;
    }

    private void AddOtherButtons()
    {
        _otherButtons.Add(_previousDungeonButton);
        _otherButtons.Add(_nextDungeonButton);
        _otherButtons.Add(_startButton);
    }

    private void SetOtherButtonsInteractableState(bool isInteractable)
    {
        foreach(var button in _otherButtons)
        {
            button.interactable = isInteractable;
        }
    }

    private void SetDungeonImageActive()
    {
        for(int i = 0; i < _dungeonImages.Count; i++)
        {
            _dungeonImages[i].gameObject.SetActive(DungeonManager.Instance.CurrentDungeonID == (i + 1));
        }
    }

    // ���� �׽�Ʈ��

    public void StartBoss()
    {
        GameManager.Instance.StageCount = 10;
        OnClickStartButton();
    }
}
