using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int currentCoin;
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private float gameOverDelay, timeToRestart;
    public int CurrentCoin { get { return currentCoin; } set { currentCoin = value; } }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
    public void GameOver()
    {
        StartCoroutine(GameOverCorutine());
    }
    public IEnumerator GameOverCorutine()
    {
        yield return new WaitForSeconds(gameOverDelay);
        gameOverScreen.SetActive(true);
        AudioController.Instance.PlayerSFX(2);
        yield return new WaitForSeconds(timeToRestart);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
