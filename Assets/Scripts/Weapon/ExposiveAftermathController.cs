using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveAftermathController : MonoBehaviour
{
    private float _power;
    private float _size;
    private LayerMask _target;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public void Init(RangeWeaponHandler rangeWeaponHandler)
    {
        _size = rangeWeaponHandler.BulletSize;
        _power = rangeWeaponHandler.Power;
        _target = rangeWeaponHandler.target;
        spriteRenderer.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        transform.localScale = new Vector3(_size, _size);

        gameObject.SetActive(true);
        Destroy(this.transform.root.gameObject, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_target.value == (_target.value | (1 << collision.gameObject.layer)))
        {
            ResourceController resourceController = collision.GetComponent<ResourceController>();
            if(resourceController != null)
            {
                resourceController.ChangeHealth(_power);
            }
        }
    }
}