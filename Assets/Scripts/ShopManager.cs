using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public void HealthUpgrade50()
    {
        if (GameManager.Instance.CurrentCoin >= 100)
        {
            Player.Instance.MaxHealth += 50;
            Player.Instance.CurrentHealth += 50;
            GameManager.Instance.CurrentCoin -= 100;
            GameManager.Instance.UpdateCoins();
            SaveManager.Instance.activeSave.maxHealth = Player.Instance.MaxHealth;
            Player.Instance.UpdateHealth();
            AudioController.Instance.UiSFX(6);
        }
    }
    public void MagicUpgrade5()
    {
        if (GameManager.Instance.CurrentCoin >= 200)
        {
            Player.Instance.MaxMagic += 5;
            Player.Instance.CurrentMagic += 5;
            GameManager.Instance.CurrentCoin -= 200;
            GameManager.Instance.UpdateCoins();
            SaveManager.Instance.activeSave.maxMagic = Player.Instance.MaxMagic;
            Player.Instance.UpdateMagic();
            AudioController.Instance.UiSFX(6);
        }
    }
    public void HealingSpeedUpgrade()
    {
        if (GameManager.Instance.CurrentCoin >= 500)
        {
            Player.Instance.HealingSpeed += 1;
            GameManager.Instance.CurrentCoin -= 500;
            GameManager.Instance.UpdateCoins();
            SaveManager.Instance.activeSave.healingSpeed = Player.Instance.HealingSpeed;
            AudioController.Instance.UiSFX(6);
        }
    }
    public void MagicRefillSpeedUpdate()
    {
        if (GameManager.Instance.CurrentCoin >= 500)
        {
            Player.Instance.MagicRefillSpeed += 1;
            GameManager.Instance.CurrentCoin -= 500;
            GameManager.Instance.UpdateCoins();
            SaveManager.Instance.activeSave.magicRefillSpeed = Player.Instance.MagicRefillSpeed;
            AudioController.Instance.UiSFX(6);
        }
    }
    public void AttackDamageUpdate()
    {
        if (GameManager.Instance.CurrentCoin >= 300)
        {
            Player.Instance.AttackDamage += 5;
            GameManager.Instance.CurrentCoin -= 300;
            GameManager.Instance.UpdateCoins();
            SaveManager.Instance.activeSave.attackDamage = Player.Instance.AttackDamage;
            AudioController.Instance.UiSFX(6);
        }
    }
}
