using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO
// 修改待定
public class SkillParticleCreator : MonoBehaviour
{
    public void CreateFireball(Vector2 pos, Vector2 dir, float speed, int attack, Util.FireBallType type) {
        string objectName = "";
        string targetTag = "";
        string fireBallExplosion = "";
        if(type == Util.FireBallType.Player) {
            objectName = Util.ObjectItemNameCollection.playerFireBall;
            targetTag = Util.TagCollection.enemyTag;
            fireBallExplosion = Util.ObjectItemNameCollection.playerFireBallExplosion;
        } else if (type == Util.FireBallType.Enemy) {
            objectName = Util.ObjectItemNameCollection.enemyFireBall;
            targetTag = Util.TagCollection.playerTag;
            fireBallExplosion = Util.ObjectItemNameCollection.enemyFireBallExplosion;
        } else if (type == Util.FireBallType.Boss) {
            objectName = Util.ObjectItemNameCollection.bossFireBall;
            targetTag = Util.TagCollection.playerTag;
            fireBallExplosion = Util.ObjectItemNameCollection.bossFireBallExplosion;
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

    public void CreateLightBall(Vector2 pos, Vector2 dir, float speed, float remain) {
        var lightBall = ObjectPool.instance.GetItem(Util.ObjectItemNameCollection.lightBall);
        lightBall.transform.position = pos + new Vector2(dir.x * 2.0f, dir.y);
        lightBall.GetComponent<Rigidbody2D>().AddForce(dir * speed);
        //lightBall.GetComponent<ObjectPoolItem>().destoryTime = remain;
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
