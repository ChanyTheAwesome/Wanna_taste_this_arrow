using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _currentLv;
    [SerializeField] private Button _selectButton;

    private string _abilityName;
    private PlayerController _playerController;
    private AbilitySelectUI _ui;

    public void SetInfo(AbilityData data, PlayerController controller, AbilitySelectUI ui)
    {
        _name.text = data._abilityName.ToString();
        _description.text = data._description.ToString();

        if(data._currentLevel == 0) _currentLv.text = "New!";
        else _currentLv.text = data._currentLevel.ToString();

        _abilityName = data._abilityName;
        _playerController = controller;
        _ui = ui;

        _selectButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _playerController.ApplyAbility(_abilityName);
        _ui.Hide();
    }
}
