using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag(Util.TagCollection.enemyTag)) {
            float xForce = 6.0f;
            if (collision.transform.position.x < transform.position.x)
                xForce = -6.0f;
            collision.gameObject.GetComponent<Enemy>().BeAttackedAndBeatBack(xForce, 3f);
            GameManager.instance.playerHitManager.ShowHit();
        }
    }
}
