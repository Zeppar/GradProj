using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SlotData : MonoBehaviour,IDropHandler
{
    public int SlotID;
    private SkillBagManger bag;
    public void OnDrop(PointerEventData eventData)
    {
        SkillData dropedSkill = eventData.pointerDrag.GetComponent<SkillData>();
        if(bag.skills[SlotID].id == -1)
        {
            bag.skills[dropedSkill.SlotInedx] = new SkillInfo();
            dropedSkill.SlotInedx = SlotID;
            bag.skills[SlotID] = dropedSkill.skill;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
