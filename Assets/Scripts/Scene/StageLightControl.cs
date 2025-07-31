using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class StageLightControl : MonoBehaviour
{
    public int StageCount = 1;

    [SerializeField]
    private Light directionalLight;
    private Light playerLight;
    private void Awake()
    {
        GameObject playerLightGameObject = GameObject.Find("PlayerLight");
        if (playerLightGameObject != null)
        {
            playerLight = playerLightGameObject.GetComponent<Light>();
        }

        directionalLight = GameObject.Find("DirectionalLight").GetComponent<Light>();
    }
    void Start()
    {
        directionalLight.intensity = 0.4f - StageCount * 0.04f;
        playerLight.range = 30f - StageCount * 1f;
        playerLight.intensity = 2f - StageCount * 0.15f;
    }
}
