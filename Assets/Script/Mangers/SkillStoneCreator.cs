using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStoneCreator : MonoBehaviour {
    public SkillStone skillStonePerfab;

    public void CreateSkillStone(int _id, Vector2 vector2) {
        SkillInfo info = GameManager.instance.skillManager.FindSkillWithID(_id);
        if (info == null) {
            Debug.LogError("Skill Info Is Null");
            return;
            
        }
        SkillStone ss = Instantiate<SkillStone>(skillStonePerfab, vector2, transform.rotation);
        ss.skillInfo = info;
    }
}
