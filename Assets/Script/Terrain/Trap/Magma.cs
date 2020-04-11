using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magma : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(Util.TagCollection.playerTag))
        {
            
            GameManager.instance.player.HP -= 1;
        }
        if (collision.CompareTag(Util.TagCollection.enemyTag))
        {
            collision.GetComponent<Enemy>().HP -= 1;
        }
    }
}
