using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillParticleCreator : MonoBehaviour
{
    public PlayerShadow shadowPrefab;
#warning 需要修改
    public GameObject Blood;

    public void CreateFireball(Vector2 pos, Vector2 dir, float speed, string tag,int attack,string targetTag,string fireBallExplosion) {
        var fb = Resources.Load<GameObject>("Skill/" + tag);
        GameObject fireball = Instantiate(fb, pos, Quaternion.identity);     
        fireball.GetComponent<Fireball>().attack = attack;      
        fireball.GetComponent<Fireball>().tag = targetTag;      
        fireball.GetComponent<Fireball>().fireBallExplosion = fireBallExplosion;      
        fireball.GetComponent<Rigidbody2D>().AddForce(dir * speed);
    }

    public void CreateShadow(Vector2 pos, int dir) {
        PlayerShadow shadow = Instantiate(shadowPrefab, pos, Quaternion.identity);
        shadow.transform.localScale = new Vector2(Mathf.Abs(shadow.transform.localScale.x) * dir, shadow.transform.localScale.y);
    }

    public void CreateEffect(Vector2 pos, string tag) {
        var effect = Resources.Load<GameObject>("Effect/" + tag);
        Instantiate(effect, pos, Quaternion.identity);
    }


    //TODO 
    public void CreateBood(Transform tran)
    {
        Instantiate(Blood, tran.position,tran.rotation).transform.localScale =new Vector2(GameManager.instance.player.dir, 1);
    }
}
