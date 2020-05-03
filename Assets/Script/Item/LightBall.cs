using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBall : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag(Util.TagCollection.playerTag)) {
            ObjectPool.instance.ReturnToPool(gameObject);
            GameManager.instance.player.ResetDashCD();
        }
    }
}
