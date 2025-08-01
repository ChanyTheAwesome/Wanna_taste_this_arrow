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
    [SerializeField] private Text _currentDungeonNameText;
    [SerializeField] List<Image> _dungeonImages;
    [SerializeField] private Image _menuImage;
    //[SerializeField] private Slider _bgmVolumeSlider;
    //[SerializeField] private Slider _sfxVolumeSlider;

    private List<Button> _otherButtons = new(); // 메뉴 열었을 때 비활성화시킬 버튼 리스트

    private void Awake()
    {  
        // 버튼 클릭 메서드 연결
        _menuButton.onClick.AddListener(OnClickMenuButton);
        _menuCloseButton.onClick.AddListener(OnClickCloseMenuButton);
        _previousDungeonButton.onClick.AddListener(OnClickPreviousDungeonButton);
        _nextDungeonButton.onClick.AddListener(OnClickNextDungeonButton);
        _startButton.onClick.AddListener(OnClickStartButton);

        AddOtherButtons();
    }
    private void Start()
    {
        //_currentDungeonIDText.text = DungeonManager.Instance.CurrentDungeonID.ToString();
        _currentDungeonNameText.text = DungeonManager.Instance.DungeonDict[DungeonManager.Instance.CurrentDungeonID].Name;
        SetDungeonImageActive();
    }
    //public override void Init(/*UIManager uiManager*/)
    //{
    //    //base.Init(uiManager);

    //    _shiftLeftButton.onClick.AddListener(OnClickShiftLeftButton);
    //    _shiftRightButton.onClick.AddListener(OnClickShiftRightButton);
    //    _enterDungeonButton.onClick.AddListener(OnClickEnterDungeonButton);
    //}

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
            // 던전 이미지나 이름도 바꾸기
            //_currentDungeonNameText.text = DungeonManager.Instance.CurrentDungeonID.ToString();
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
            // 던전 이미지나 이름도 바꾸기
            //_currentDungeonNameText.text = DungeonManager.Instance.CurrentDungeonID.ToString();
            _currentDungeonNameText.text = DungeonManager.Instance.DungeonDict[DungeonManager.Instance.CurrentDungeonID].Name;
            SetDungeonImageActive();
        }
        else return;
    }

    public void OnClickStartButton()
    {
        DungeonManager.Instance.StartDungeon();
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
}
