using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag(Util.TagCollection.enemyTag)) {
            collision.gameObject.GetComponent<Enemy>().BeAttackedAndBeatBack(-10.0f, 3f);
            GameManager.instance.playerHitManager.ShowHit();
        }
    }
}
