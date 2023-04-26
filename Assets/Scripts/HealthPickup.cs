using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healthToGive;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Player.Instance.RestoreHealth(healthToGive);
            AudioController.Instance.UiSFX(3);
            Destroy(gameObject);
        }
    }
}
