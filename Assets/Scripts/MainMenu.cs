using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string startScene;
    public void StartGame()
    {
        SceneManager.LoadScene(startScene);
    }
    public void OnApplicationQuit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}
