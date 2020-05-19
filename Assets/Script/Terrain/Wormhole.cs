using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wormhole : MonoBehaviour
{
    public Transform toTran;
    public WheelTip wheelTip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Util.TagCollection.playerTag))
        {
            wheelTip.StartSaving(1.0f, () =>
            {
                GameManager.instance.player.transform.position = toTran.position;
            });
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Util.TagCollection.playerTag))
        {
            wheelTip.Stop();
        }
    }
}
