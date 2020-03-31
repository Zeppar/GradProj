using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class EnemyBirdFireBall : MonoBehaviour
{
    public int attack;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 pos = collision.contacts[0].point;
        if (collision.gameObject.CompareTag(Util.TagCollection.playerTag))
        {
            collision.gameObject.GetComponent<Player>().BeAttacked(attack);
        }
        GameManager.instance.skillParticleCreator.CreateEffect(pos, Util.EffectCollection.enemyFireBallExplosion);
        Destroy(gameObject);
    }

}
