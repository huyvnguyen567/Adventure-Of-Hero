using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int currentCoin;
    [SerializeField] private TMP_Text coinText;
    public int CurrentCoin { get { return currentCoin; } set { currentCoin = value; } }


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentCoin = SaveManager.Instance.activeSave.currentCoin;
        UpdateCoins();
    }

    void Update()
    {
        SaveManager.Instance.activeSave.currentCoin = GameManager.Instance.CurrentCoin;
        UpdateCoins();
    }

    public void GetCoins(int coinsToGive)
    {
        currentCoin += coinsToGive;
    }

    public void UpdateCoins()
    {
        coinText.text = currentCoin.ToString();
    }
}
