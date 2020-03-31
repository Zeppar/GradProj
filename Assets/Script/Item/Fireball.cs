﻿using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Fireball: MonoBehaviour
{
    public int attack;
    public string tag;
    public string fireBallExplosion;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 pos = collision.contacts[0].point;
        if (collision.gameObject.CompareTag(tag))
        {
            if (collision.gameObject.GetComponent<Player>() != null)
            {
                collision.gameObject.GetComponent<Player>().BeAttacked(attack);
            }
            else
            {
                collision.gameObject.GetComponent<Enemy>().BeAttacked(attack);
            }
        }
        GameManager.instance.skillParticleCreator.CreateEffect(pos, fireBallExplosion);
        Destroy(gameObject);
    }

}
