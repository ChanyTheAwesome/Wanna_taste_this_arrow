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
    {
        directionalLight = GameObject.Find("DirectionalLight").GetComponent<Light>();//This might cause potential nullexception errors.
        //Instead, consider this.
        //GameObject directionalLightGameObject = GameObject.Find("DirectionalLight");
        //if (directionalLightGameObject != null)
        //{
        //    directionalLight = directionalLightGameObject.GetComponent<Light>();
        //}

        playerLight = GameObject.Find("PlayerLight").GetComponent<Light>();//This also.
    }
    void Start()
    {
        directionalLight.intensity = 0.4f - StageCount * 0.04f;
        playerLight.range = 30f - StageCount * 1f;
        playerLight.intensity = 2f - StageCount * 0.15f;
    }
}
