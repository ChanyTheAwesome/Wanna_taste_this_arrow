using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class CaveLightControl : MonoBehaviour
{
    public int stageCount = 1;

    public Light directionalLight;
    public Light pointLight;
    private void Awake()
    {
    }
    void Start()
    {
        directionalLight.intensity = 0.4f - stageCount * 0.02f;
        pointLight.intensity = 2f - stageCount * 0.1f;
    }
}
