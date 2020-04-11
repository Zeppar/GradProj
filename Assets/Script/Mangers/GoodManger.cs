using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodManger
{
    public int bagCount = 16;
   
 
    public List<BagItem> goodInfoList = new List<BagItem>();
   
    public bool isDirty = false;
   

   

    public void AddItemToPanel(GoodInfo.GoodType _goodType,int id)
    {
        if (UIManger.instance.GotItemHelpText != null)
        {
            UIManger.instance.GotItemHelpText.SetActive(true);
        }
        for (int i = 0; i < goodInfoList.Count; i++) //还未检查检查检查检查检查
        {
            if(goodInfoList[i].goodInfo!= null &&
                goodInfoList[i].goodInfo.goodType == GoodInfo.GoodType.Skill &&
                GameManager.instance.skillManager.FindSkillWithID(id) == goodInfoList[i].goodInfo.skill)
            {               
                goodInfoList[i].goodInfo.count++;

                UIManger.instance.bagPanel.UpdataItem();
                return;
            }
            else if (goodInfoList[i].goodInfo == null)
            {
                goodInfoList[i].goodInfo = new GoodInfo();
                goodInfoList[i].goodInfo.goodType = _goodType;
                if(_goodType == GoodInfo.GoodType.Skill)
                {
                    goodInfoList[i].goodInfo.skill = GameManager.instance.skillManager.FindSkillWithID(id);
                  
                   UIManger.instance.bagPanel.UpdataItem();
                    return;
                   
                }
                else
                {
                    Debug.LogError("道具还没有做");
                    return;
                }                                
            }
        }
        isDirty = true;
        UIManger.instance.bagPanel.UpdataItem();
    }

    public void AddItemToPanel(GoodInfo.GoodType _goodType, int id,int count)
    {
        for (int i = 0; i < count; i++)
        {
            AddItemToPanel(_goodType, id);
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
