using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;


public class SkillManager
{
  
    public Dictionary<int, SkillInfo> skill_Dic = new Dictionary<int, SkillInfo>();
    public List<SkillInfo> currentSkillList = new List<SkillInfo>();
    public List<Sprite> Icon_List = new List<Sprite>();

    public void InitSkill(){

        JsonData skilldata = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Json/Skill.json"));
        for (int i = 0; i < skilldata.Count; i++){
            SkillInfo info = new SkillInfo();
            info.ID = (int)skilldata[i]["id"];
            info.Title = skilldata[i]["title"].ToString();
            info.Value = (int)skilldata[i]["value"];
            info.Range = (int)skilldata[i]["range"];
            info.Describe = skilldata[i]["describe"].ToString();
            info.Icon = skilldata[i]["icon"].ToString();
            info.Action = skilldata[i]["action"].ToString();
            skill_Dic.Add((int)skilldata[i]["id"],info);
            Icon_List.Add(Resources.Load<Sprite>(info.Icon));
            //加载图标
        }
        Debug.Log("已完成技能加载，共加载到了 " + skill_Dic.Count + " 个技能");
    }

    public SkillInfo FindSkillWithID(int _id)
    {
        if (!skill_Dic.ContainsKey(_id))
        {
            return null;
        }
        return skill_Dic[_id];
    }

    public void AddSkill(SkillInfo skill)
    {
        if (currentSkillList.Count == 2)
        {
            currentSkillList.RemoveAt(0);
            Debug.Log("技能过多，移除技能");
        }
        currentSkillList.Add(skill);

        Debug.Log("增加技能 ID:" + skill.ID + "技能 标题:" + skill.Title);
       

    }
}


