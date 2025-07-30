using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class StageLightControl : MonoBehaviour
{
    public int StageCount = 1;

    public Light directionalLight;
    public Light playerLight; //Too Much public Variants?
    private void Awake()
    {//It is Really dangerous to use GetComponent() in Awake()!!!
        directionalLight = GameObject.Find("DirectionalLight").GetComponent<Light>();
        playerLight = GameObject.Find("PlayerLight").GetComponent<Light>();
    }
    void Start()
    {
        directionalLight.intensity = 0.4f - StageCount * 0.04f;
        playerLight.range = 30f - StageCount * 1f;
        playerLight.intensity = 2f - StageCount * 0.15f;
    }
}
