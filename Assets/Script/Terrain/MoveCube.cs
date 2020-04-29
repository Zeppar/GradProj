using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public int moveVar;
    public float speed = 3;
    int dir = 1;
    int i_MoveVar;
    void Update()
    {
        transform.position = new Vector2(transform.position.x + (speed * Time.deltaTime*dir) ,transform.position.y );
        i_MoveVar++;
        if(i_MoveVar>= moveVar)
        {
            dir = -dir;
            i_MoveVar = 0;
        }
    }
}
