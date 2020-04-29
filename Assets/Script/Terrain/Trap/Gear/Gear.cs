using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public int attack = 3;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Util.TagCollection.playerTag))
        {
            collision.gameObject.GetComponent<Player>().HP -= attack;
        }
    }
}
