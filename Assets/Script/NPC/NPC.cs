using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject help;
    public DialogueAsset dialogueAsset;
    public bool isAcceptTask;
    public int TaskId;

    bool isTriger;

    private void Update()
    {
        if(isTriger && Input.GetKeyUp(KeyCode.E))
        {
            UIManager.instance.dialoguePanel.Show(dialogueAsset.dialogueInfos);
            if(isAcceptTask){
                GameManager.instance.taskManager.AcceptTask(TaskId);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        help.SetActive(true);
        isTriger = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        help.SetActive(false);
        isTriger = false;
    }
}
