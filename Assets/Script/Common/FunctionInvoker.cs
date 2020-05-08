using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionInvoker : MonoBehaviour
{
    public int id;
    private void OnTriggerEnter2D(Collider2D collision) {
        switch(id) {
            case 0:
                GameManager.instance.levelManager.currentInfo.dashEnable = true;
                break;
            default:
                break;
        }
    }
}
