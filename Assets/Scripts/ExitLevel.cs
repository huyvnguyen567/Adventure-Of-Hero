using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private float winDelay, timeToExit;
    [SerializeField] private int nextSceneLoad;
    private Animator anim;

    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
        anim = GetComponent<Animator>();
    }
    public void WinGame()
    {
        StartCoroutine(WinGameCorutine());
    }
    public IEnumerator WinGameCorutine()
    {
        yield return new WaitForSeconds(winDelay);
        winScreen.SetActive(true);
        AudioController.Instance.PlayerSFX(13);
        yield return new WaitForSeconds(timeToExit);
        SceneManager.LoadScene(nextSceneLoad);
        if(nextSceneLoad > PlayerPrefs.GetInt("LevelAt"))
        {
            PlayerPrefs.SetInt("LevelAt", nextSceneLoad);
        }    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            WinGame();
            anim.SetBool("Open", true);
        }
    }
}
