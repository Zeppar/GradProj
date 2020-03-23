using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillParticleCreator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject fireBallPerfab;
    public PlayerShadow shadowPrefab;
   
    public void CreateFireball(Vector2 vector2_pos,Vector2 vector2_dir)
    {        
        GameObject fireball = Instantiate(fireBallPerfab, vector2_pos, Quaternion.identity);
        fireball.GetComponent<Rigidbody2D>().AddForce(vector2_dir*fireball.GetComponent<Fireball>().speed);
    }

    public void CreateShadow(Vector2 vector2_pos) {
        Instantiate(shadowPrefab, vector2_pos, Quaternion.identity);
    }
}
