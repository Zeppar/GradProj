using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireBall : MonoBehaviour
{
    public int attack = 35;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 pos = collision.contacts[0].point;
        if (collision.gameObject.CompareTag(Util.TagCollection.enemyTag))
        {
            collision.gameObject.GetComponent<Enemy>().BeAttacked(attack);
        }
        GameManager.instance.skillParticleCreator.CreateEffect(pos, Util.EffectCollection.playerFireBallExplosion);
        Destroy(gameObject);
    }

}
