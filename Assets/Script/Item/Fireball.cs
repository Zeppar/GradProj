using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Fireball: MonoBehaviour
{
    private int attack;
    private string tag;
    private string fireBallExplosion;

    public void SetContent(int _attack, string _tag, string _fireBallExplosion) {
        attack = _attack;
        tag = _tag;
        fireBallExplosion = _fireBallExplosion;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        var pos = transform.position;
        if (collision.gameObject.CompareTag(tag)) {
            if (collision.gameObject.GetComponent<Player>() != null) {
                collision.gameObject.GetComponent<Player>().BeAttacked(attack);
            } else {
                collision.gameObject.GetComponent<Enemy>().BeAttacked(attack);
            }
        }
        GameManager.instance.skillParticleCreator.CreateEffect(pos, fireBallExplosion);
        ObjectPool.instance.ReturnToPool(gameObject);
    }

}
