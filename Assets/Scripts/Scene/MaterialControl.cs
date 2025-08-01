using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialControl : MonoBehaviour
{
    [SerializeField]
    private Vector2 scrollSpeed = new Vector2(0f, 0.05f); // Y축으로 흐르게 설정

    private Renderer rend;
    private Material mat;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        mat = rend.material;
    }

    private void Update()
    {
        Vector2 offset = Time.time * scrollSpeed;
        mat.mainTextureOffset = offset;
    }
}
