using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopTipTrigger : MonoBehaviour
{
    public string text = "文本未输入";
    public int size = 40;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Util.TagCollection.playerTag))
        {
            UIManager.instance.helpPanel.topTip.ShowTopTip(text,size);
        }
    }
}
