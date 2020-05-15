using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionInvoker : MonoBehaviour
{
    public int id;
    public string text;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag(Util.TagCollection.playerTag)) {
            switch (id) {
                case 0:
                    GameManager.instance.levelManager.currentInfo.dashEnable = true;
                    break;
                case 1:
                    UIManager.instance.ShowTopTip(text, 20);
                    break;
                default:
                    break;
            }
        }
    }
}
