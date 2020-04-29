using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform pointA;
    public Transform pointB;
    public MoveState moveState = MoveState.Forward;
    public float speed = 3;

    Transform player;

    i_State i_state = i_State.A;
    private enum i_State
    {
        A,
        B
    }  public enum MoveState
    {
        Up,
        Forward
    }

    // Update is called once per frame
    void Update()
    {
        if (moveState == MoveState.Up)
        {
            if (i_state == i_State.B)
            {
                if (transform.position.y <= pointA.position.y)
                {
                    i_state = i_State.A;
                }
                transform.position = new Vector2(transform.position.x, transform.position.y-speed);
            }
            else if (i_state == i_State.A)
            {
                if (transform.position.y >= pointB.position.y)
                {
                    i_state = i_State.B;
                }
                transform.position = new Vector2(transform.position.x, transform.position.y+speed);
            }

        }
        else
        {
            if (i_state == i_State.A)
            {
                if (transform.position.x <= pointA.position.x)
                {
                    i_state = i_State.B;
                }
                transform.position = new Vector2(transform.position.x - speed, transform.position.y);
                if (player != null)
                {
                    player.position = new Vector2(player.position.x - speed, player.position.y);
                }
            }
            else if (i_state == i_State.B)
            {
                if (transform.position.x >= pointB.position.x)
                {
                    i_state = i_State.A;
                }
                transform.position = new Vector2(transform.position.x + speed, transform.position.y);
                if (player != null)
                {
                    player.position = new Vector2(player.position.x + speed, player.position.y);
                }
            }
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Util.TagCollection.playerTag))
        {
            player = collision.gameObject.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Util.TagCollection.playerTag))
        {
            player = null;
        }

    }
}
