using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag(Util.TagCollection.playerTag)) {
            collision.GetComponent<Player>().HP = 0;
        } else if (collision.gameObject.CompareTag(Util.TagCollection.enemyTag)) {
            collision.GetComponent<Enemy>().HP = 0;
        }
    }
}
