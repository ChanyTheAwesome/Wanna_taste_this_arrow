using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonEnterButton : MonoBehaviour
{
    [SerializeField] private int _dungeonID;

    public Button dungeonEnterButton;

    private void Start()
    {
        dungeonEnterButton.onClick.AddListener(() => DungeonManager.Instance.StartDungeon(_dungeonID));
    }
}
