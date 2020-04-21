using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStoneCreator : MonoBehaviour {
    public SkillStone skillStonePerfab;

    public void CreateSkillStone(int id, Vector2 pos) {
        SkillInfo info = GameManager.instance.skillManager.FindSkillWithID(id);
        if (info == null) {
            Debug.LogError("Skill Info Is Null");
            return;
            
        }
        SkillStone ss = Instantiate(skillStonePerfab, pos, transform.rotation);
        ss.skillInfo = info;
    }
}
