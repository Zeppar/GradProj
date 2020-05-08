using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanTanBall : MonoBehaviour
{
    Rigidbody2D rd;
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        int a = Random.Range(-10, -30);
        rd.AddForce(new Vector2(a, 10));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int a = Random.Range(-10, -30);
        rd.AddForce(new Vector2(a, 10));
    }
}
