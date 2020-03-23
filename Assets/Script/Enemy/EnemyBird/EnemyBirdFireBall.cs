using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class EnemyBirdFireBall : MonoBehaviour
{
    public float speed = 0.5f;
    public int attack = 35;
    public GameObject Boom;
    public Vector2 vector2_dir = new Vector2(1,0);
   
    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 pos = collision.contacts[0].point;
        if (collision.gameObject.CompareTag(Util.playerTag))
        {
            collision.gameObject.GetComponent<Player>().BeAttacked(attack);
        }
        var boom = Instantiate(Boom, pos, Quaternion.identity);
        Destroy(gameObject);
    }
    public void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(vector2_dir * speed);
   
    }

}
