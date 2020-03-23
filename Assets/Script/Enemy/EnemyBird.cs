using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBird : Enemy
{
    [Header("敌人属性")]
    public float birdHight;


    private int dir = 1;
    public override void Seek()
    {
        if (Physics.Raycast(transform.position, new Vector3(0,-1,0), birdHight, LayerMask.GetMask("Ground")))
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime * dir, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
        }
    }
}
