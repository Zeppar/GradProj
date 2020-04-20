using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillParticleCreator : MonoBehaviour
{
    public void CreateFireball(Vector2 pos, Vector2 dir, float speed, int attack, bool isPlayer) {
        string objectName = "";
        string targetTag = "";
        string fireBallExplosion = "";
        if(isPlayer) {
            objectName = Util.SkillCollection.playerFireBall;
            targetTag = Util.TagCollection.enemyTag;
            fireBallExplosion = Util.EffectCollection.playerFireBallExplosion;
        } else {
            objectName = Util.SkillCollection.enemyFireBall;
            targetTag = Util.TagCollection.playerTag;
            fireBallExplosion = Util.EffectCollection.enemyFireBallExplosion;
        }
        CreateFireball(pos, dir, speed, attack, objectName, targetTag, fireBallExplosion);
    }

    private void CreateFireball(Vector2 pos, Vector2 dir, float speed, int attack, string objectName, string targetTag, string fireBallExplosion) {
        Fireball fireBall = ObjectPool.instance.GetItem(objectName).GetComponent<Fireball>();
        fireBall.transform.position = pos;
        fireBall.SetContent(attack, targetTag, fireBallExplosion);
        fireBall.GetComponent<Rigidbody2D>().AddForce(dir * speed);
    }

    public void CreateShadow(Vector2 pos, int dir) {
        var shadow = ObjectPool.instance.GetItem(Util.ObjectItemNameCollection.playerShadow);
        shadow.transform.position = pos;
        shadow.transform.localScale = new Vector2(Mathf.Abs(shadow.transform.localScale.x) * dir, shadow.transform.localScale.y);
    }

    public void CreateEffect(Vector2 pos, string tag) {
        var effect = ObjectPool.instance.GetItem(tag);
        effect.transform.position = pos;
    }

    public void CreateBlood(Transform tran)
    {
        Vector3 posAdd = new Vector3(GameManager.instance.player.dir * 0.5f, 0.2f);
        GameObject blood = ObjectPool.instance.GetItem(Util.ObjectItemNameCollection.blood);
        blood.transform.position = tran.position + posAdd;
        blood.transform.localScale = new Vector2(-GameManager.instance.player.dir * 2.0f, 2.0f);
    }
}
