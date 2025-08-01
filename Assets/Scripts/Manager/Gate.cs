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
        closedGate.gameObject.SetActive(!DungeonManager.Instance.IsClear);
        openedGate.gameObject.SetActive(DungeonManager.Instance.IsClear);
    }
}
