using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStone : MonoBehaviour {
    public SkillInfo skillInfo;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag(Util.TagCollection.playerTag)) {
            GameManager.instance.skillManager.AddSkill(skillInfo);
            Destroy(gameObject);
        }
    }
}
