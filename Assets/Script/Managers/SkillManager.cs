using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;


public class SkillInfo {
    public int id;
    public string name;
    public int value;
    public string describe;
    public Sprite iconSprite;
    public int cd;
    public string action;
    public SkillInfo() {
        this.id = -1;
    }
}

public class SkillManager
{
  
    public Dictionary<int, SkillInfo> skillDict = new Dictionary<int, SkillInfo>();
    public Dictionary<int, Sprite> skillIconDict = new Dictionary<int, Sprite>();
    // TODO Goodinfo skillInfo
    public List<GoodInfo> quickSkillList = new List<GoodInfo>();
    public bool isDirty = false;

    public void InitSkill(){

        JsonData skilldata = JsonMapper.ToObject(Resources.Load<TextAsset>("Skill/Skill").text);
        for (int i = 0; i < skilldata.Count; i++){
            SkillInfo info = new SkillInfo();
            info.id = (int)skilldata[i]["id"];
            info.name = skilldata[i]["title"].ToString();
            info.value = (int)skilldata[i]["value"];
            info.cd = (int)skilldata[i]["cd"];
            info.describe = skilldata[i]["describe"].ToString();
            string iconPath = skilldata[i]["iconPath"].ToString();
            info.iconSprite = Resources.Load<Sprite>(iconPath);
            info.action = skilldata[i]["action"].ToString();
            skillDict.Add((int)skilldata[i]["id"],info);
            skillIconDict.Add((int)skilldata[i]["id"], Resources.Load<Sprite>(iconPath));
        }


        // TODO 
        quickSkillList.Add(null);
        quickSkillList.Add(null);
        isDirty = true;
    }

    public SkillInfo FindSkillWithID(int _id)
    {
        if (!skillDict.ContainsKey(_id))
        {
            return null;
        }
        return skillDict[_id];
    }

    public void ChangeQuickSkill(int index, GoodInfo info) {
        quickSkillList[index] = info;
        isDirty = true;
    }

    public void ExchangeGoodInfo(int index1, int index2) {
        var goodInfo = quickSkillList[index1];
        quickSkillList[index1] = quickSkillList[index2];
        quickSkillList[index2] = goodInfo;
        isDirty = true;
    }

}


