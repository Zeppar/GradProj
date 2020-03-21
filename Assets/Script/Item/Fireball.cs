using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 0.5f;
    public int attack = 35;
    public GameObject Boom;
    // Start is called before the first frame update
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Util.Enemy))
        {
            collision.gameObject.GetComponent<Enemy>().BeAttacked(35);
        }
        var boom = Instantiate(Boom, transform.position,transform.rotation);
        Destroy(gameObject);
    }
}
