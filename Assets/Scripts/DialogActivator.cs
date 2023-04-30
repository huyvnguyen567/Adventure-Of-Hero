using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    [SerializeField] private string[] lines;
    [SerializeField] private bool canActive;

    void Update()
    {
        if(canActive && Input.GetMouseButtonUp(1) && !DialogManager.Instance.dialogPanel.activeInHierarchy)
        {
            DialogManager.Instance.ShowDialog(lines);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canActive = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canActive = false;
        }
    }
}
