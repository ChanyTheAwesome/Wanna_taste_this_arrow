using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class StageLightControl : MonoBehaviour
{
    public int stageCount = 1;

    public Light directionalLight;
    public Light playerLight;
    private void Awake()
    {
        directionalLight = GameObject.Find("DirectionalLight").GetComponent<Light>();
        playerLight = GameObject.Find("PlayerLight").GetComponent<Light>();
    }
    void Start()
    {
        directionalLight.intensity = 0.4f - stageCount * 0.04f;
        playerLight.range = 30f - stageCount * 1f;
        playerLight.intensity = 2f - stageCount * 0.15f;
    }
}
