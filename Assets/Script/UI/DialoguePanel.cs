using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour 
{
    public Image speakerIcon;
    public Text speakerName;
    public Text text;
    public Button nextBtn;

    bool isInDialogue = false;
    List<DialogueInfo> curinfos;
    int speakIndex = 0;

    void Start()
    {
        nextBtn.onClick.AddListener(Next);

    }
    public void Show(List<DialogueInfo> dialogueInfos)
    {
        isInDialogue = true;
        gameObject.SetActive(true);
        curinfos = dialogueInfos;
        Next();
    }
    public void ShowInfo(DialogueInfo info)
    {
        speakerIcon.sprite = info.Icon;
        speakerName.text = info.Name;
        text.text = info.Text;
    }
    public void Next()
    {
        if (isInDialogue)
        {
            if (curinfos.Count <= speakIndex)
            {
                isInDialogue = false;
                gameObject.SetActive(false);
                return;
            }
           ShowInfo(curinfos[speakIndex]);
           speakIndex++;
        }
    }
}




