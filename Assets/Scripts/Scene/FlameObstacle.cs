using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameObstacle : MonoBehaviour
{
    public ResourceController player;
    [SerializeField]
    private float obstacleDamage = -1f;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        player = collision.GetComponent<ResourceController>();
    //        player.ChangeHealth(obstacleDamage);
    //    }
    //}
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            Debug.Log("¥Í¿Ω");
            player = collision.GetComponent<ResourceController>();
            player.ChangeHealth(obstacleDamage);
        }
    }
}
