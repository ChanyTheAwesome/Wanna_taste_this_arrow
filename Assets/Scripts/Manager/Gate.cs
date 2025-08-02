using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private GameObject closedGate;
    [SerializeField] private GameObject openedGate;

    private void Awake()
    {
        DungeonManager.Instance.gate = this;
    }

    public void OpenGate()
    {
        if(DungeonManager.Instance.CurrentDungeonID == 2)
        {
            closedGate.gameObject.SetActive(!DungeonManager.Instance.IsClear);
        }
        else
        {
            closedGate.gameObject.SetActive(!DungeonManager.Instance.IsClear);
            openedGate.gameObject.SetActive(DungeonManager.Instance.IsClear);
        }   
    }
}
