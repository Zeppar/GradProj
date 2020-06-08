using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject help;
    public DialogueAsset dialogueAsset;

    bool isTriger;

    private void Update()
    {
        if(isTriger && Input.GetKeyUp(KeyCode.E))
        {
            UIManager.instance.dialoguePanel.Show(dialogueAsset.dialogueInfos);
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
