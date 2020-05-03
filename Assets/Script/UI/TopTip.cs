using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopTip : MonoBehaviour
{
    public Text ui_text;
    public void ShowTopTip(string text,int size = 40)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            ui_text.text = text;
            ui_text.fontSize = size;
        }
        else
        {
            ui_text.text = text;
            ui_text.fontSize = size;
            gameObject.GetComponent<Animator>().Play("TopTip",0,0);
        }
    }
    public void HideTopTip()
    {
        gameObject.SetActive(false);
    }
}
