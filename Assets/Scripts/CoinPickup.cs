using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private int coinsToGive;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.Instance.GetCoins(coinsToGive);
            AudioController.Instance.UiSFX(1);
            Destroy(gameObject);
        }
    }
}
