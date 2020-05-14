using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBegining : MonoBehaviour
{
    public List<string> Words;
    public Text text;
    public GameManager gameManager;
    void Start()
    {
             //print("set1");
            StartCoroutine(SetText());

    }

    IEnumerator SetText()
    {
        foreach (var item in Words)
        {
            text.CrossFadeAlpha(0, 0, true);
            text.text = item;
            text.CrossFadeAlpha(1, 1, true);
            yield return new WaitForSeconds(2);
            text.CrossFadeAlpha(0, 1, true);
            yield return new WaitForSeconds(1.5f);
        }
        gameManager.LevelUp();
    
    }



}
