using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillParticleCreator : MonoBehaviour
{
    public PlayerShadow shadowPrefab;

    public void CreateFireball(Vector2 pos, Vector2 dir, float speed, string tag) {
        var fb = Resources.Load<GameObject>("Skill/" + tag);
        GameObject fireball = Instantiate(fb, pos, Quaternion.identity);
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
}
