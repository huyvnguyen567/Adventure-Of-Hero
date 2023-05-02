using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    [SerializeField] private int startIndexScene;
    public void SceneToLoad()
    {
        SceneManager.LoadScene(startIndexScene);
    }
}
