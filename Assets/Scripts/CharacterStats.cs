using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharacterStats : MonoBehaviour
{
    [SerializeField] private TMP_Text hp, mp, ad, hpr, mpr, speed;
    void Update()
    {
        UpdateStats();
    }
    public void UpdateStats()
    {
        hp.text = Player.Instance.MaxHealth + "";
        mp.text = Player.Instance.MaxMagic + "";
        ad.text = Player.Instance.AttackDamage + "";
        hpr.text = Player.Instance.HealingSpeed + "";
        mpr.text = Player.Instance.MagicRefillSpeed + "";
        speed.text = Player.Instance.RunSpeed + "";
    }
}
