using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerCheck : MonoBehaviour
{
    private static LayerCheck instance;
    public static LayerCheck Instance;

    public bool targetLayerCheck(LayerMask targetLayer, GameObject go)
    {
        return (targetLayer.value & (1 << go.layer)) != 0;
    }
    public bool targetLayerCheck(LayerMask targetLayer, Collider2D collision)
    {
        return targetLayer.value == (targetLayer.value | (1 << collision.gameObject.layer));
    }
}