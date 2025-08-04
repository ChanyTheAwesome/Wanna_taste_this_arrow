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
        if(closedGate != null)
        {
            closedGate.gameObject.SetActive(!DungeonManager.Instance.IsClear);
        }
        if(openedGate != null)
        {
            openedGate.gameObject.SetActive(DungeonManager.Instance.IsClear);
        }
    }
}
