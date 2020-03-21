using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBagManger : MonoBehaviour
{

    public GameObject slot;
    public GameObject item;

    public List<GameObject> slots = new List<GameObject>();
    public List<SkillInfo> skills = new List<SkillInfo>();
    public uint bugCount = 16;

    GameObject skillPanel;

    SkillManager skillManager;
    public void InitSkillPanel()
    {
        skillManager = GameManger.instance.skillManager;
        skillPanel = GameObject.Find("Slot Panel");

        for (int i = 0; i < bugCount; i++)
        {
            slots.Add(Instantiate(slot));
            slots[i].transform.SetParent(skillPanel.transform);
            slots[i].GetComponent<SlotData>().SlotID = i;
            skills.Add(new SkillInfo());
        }
        AddSkillToSkillPanel(0);
        AddSkillToSkillPanel(1);
        AddSkillToSkillPanel(2);
    }

    public void AddSkillToSkillPanel(int _id)
    {
        SkillInfo skillToAdd = skillManager.FindSkillWithID(_id);
        for (int i = 0; i < skills.Count; i++)
        {
            if (skills[i].ID == -1)
            {
                skills[i] = skillToAdd;
                GameObject skillObj = Instantiate(item);
                skillObj.transform.SetParent(slots[i].transform);
                skillObj.transform.position = Vector2.zero;
                skillObj.name = skillToAdd.Title;
               // skillObj.GetComponent<Image>().sprite = Resources.Load<Sprite>(skills[i].Icon);
                skillObj.GetComponent<SkillData>().skill = skillToAdd;
                skillObj.GetComponent<SkillData>().SlotInedx = i;
                break;
            }

        }
    }
    
}
