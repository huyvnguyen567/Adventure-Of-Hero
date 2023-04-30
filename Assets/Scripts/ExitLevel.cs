using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private float winDelay, timeToExit;
    public void WinGame()
    {
        StartCoroutine(WinGameCorutine());
    }
    public IEnumerator WinGameCorutine()
    {
        yield return new WaitForSeconds(winDelay);
        winScreen.SetActive(true);
        AudioController.Instance.PlayerSFX(2);
        yield return new WaitForSeconds(timeToExit);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex-1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            WinGame();
            Destroy(collision.gameObject,1);
        }
    }
}
