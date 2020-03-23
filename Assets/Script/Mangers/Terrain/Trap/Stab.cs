using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stab : MonoBehaviour
{
    // Start is called before the first frame update
    public int attack = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            iTween.MoveBy(collision.gameObject, iTween.Hash("x", collision.gameObject.GetComponent<Player>().dir* -3, "y", 4, "looktime", 0.5f));
            collision.gameObject.GetComponent<Player>().BeAttacked(attack);
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            iTween.MoveBy(collision.gameObject, iTween.Hash("x", collision.gameObject.GetComponent<Player>().dir * -3, "y", 4, "looktime", 0.5f));
            collision.gameObject.GetComponent<Player>().BeAttacked(attack);
        }
    }
}
