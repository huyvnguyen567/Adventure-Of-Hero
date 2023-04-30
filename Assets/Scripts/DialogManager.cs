using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance { get; private set; }
    public GameObject dialogPanel;
    public TMP_Text dialogText;
    [SerializeField] private string[] sentences;
    [SerializeField] private int currentSentence;
    [SerializeField] private bool justStarted;
    void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        if (dialogPanel.activeInHierarchy)
        {
            if (Input.GetMouseButtonUp(1))
            {
                if (!justStarted)
                {
                    currentSentence++;
                    if(currentSentence >= sentences.Length)
                    {
                        dialogPanel.SetActive(false);
                    }
                    else
                    {
                        dialogText.text = sentences[currentSentence];
                    }
                }
                else
                {
                    justStarted = false;
                }    
            }
        }
    }
    public void ShowDialog(string[] newLines)
    {
        sentences = newLines;
        currentSentence = 0;
        dialogText.text = sentences[currentSentence];
        dialogPanel.SetActive(true);
        justStarted = true;
    }

}
