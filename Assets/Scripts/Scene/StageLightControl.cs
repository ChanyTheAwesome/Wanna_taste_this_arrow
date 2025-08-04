using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class StageLightControl : MonoBehaviour
{
    private int stageCount;

    [SerializeField]
    private Light directionalLight;
    private Light playerLight;
    void Start()
    {
        GameObject playerLightGameObject = GameObject.Find("PlayerLight");
        if (playerLightGameObject != null)
        {
            playerLight = playerLightGameObject.GetComponent<Light>();
        }
        directionalLight = GameObject.Find("DirectionalLight").GetComponent<Light>();

        stageCount = GameManager.Instance.StageCount;

        directionalLight.intensity = 0.4f - stageCount * 0.04f;
        playerLight.range = 30f - stageCount * 1f;
        playerLight.intensity = 2f - stageCount * 0.15f;
    }
}
