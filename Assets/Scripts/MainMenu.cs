using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string startScene;
    void Update()
    {
        ResetLevel();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(startScene);
    }
    public void OnApplicationQuit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void ResetLevel()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Level Reseted");    
        }
    }

}
