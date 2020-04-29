using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stab : MonoBehaviour
{
    // Start is called before the first frame update
    public float attack = 10;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Util.TagCollection.playerTag))
        {
            GameManager.instance.player.BeAttackedAndBeatBack(-GameManager.instance.player.dir, 5, 7, attack);
            GameManager.instance.player.BeAttacked(attack);
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(Util.TagCollection.playerTag)) {
            GameManager.instance.player.BeAttackedAndBeatBack(-GameManager.instance.player.dir, 5, 7, attack);
            GameManager.instance.player.BeAttacked(attack);
        }
    }
}
