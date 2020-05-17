using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wormhole : MonoBehaviour
{
    public Transform toTran;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Util.TagCollection.playerTag))
        {
            GameManager.instance.player.transform.position = toTran.position;
        }
    }
}
