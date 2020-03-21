﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UIManger : MonoBehaviour
{
   
    [Header("快捷技能")]
    public Image quickSkill1;
    public Image quickSkill2;

    [Header("主角血条")]

    public List<Image> heart_list;

    public Sprite heartFull;
    public Sprite heartHelf;
    public Sprite heartNull;

    [Header("道具背包")]
    public BagPanel bagPanel;
    public GameObject describePanel;

    

    [Header("面板物体")]
    public GameObject BagPanel_Obj;
    public GameObject Cheat_Obj;


    public static UIManger instance;


    private void Awake()
    {
        instance = this;
        bagPanel.InitSlot();
    }
    private void Start()
    {
      
    }
    private void Update()
    {
       //UpdataSkillIcon(GameManger.instance.skillManager.currentSkillList);
       UpdateHpBar(GameManger.instance.playerScript.HP);
    }

    //技能图标更新
    public void UpdataSkillIcon(List<SkillInfo> currentSkillList)
    {
        /////////////
        ///暂停使用
        ////////////
        for (int i = 0; i < currentSkillList.Count; i++)
        {
            if(i == 0)
            {

               // SkillIcon1.sprite = Resources.Load<Sprite>(currentSkillList[i].Icon);
            }
            else if(i == 1)
            {              
             //   SkillIcon2.sprite = Resources.Load<Sprite>(currentSkillList[i].Icon);
                print("1");
            }
            else
            {
                Debug.LogError("同志，你有多少技能啊");
            }
                       
        }
    }
   
    //血条更新
    public void UpdateHpBar(int hp)
    {

        if(hp % 2 == 0)
        {

            for (int i = 0; i < heart_list.Count; i++)
            {
                if(i+1 <= hp / 2)
                {
                    heart_list[i].sprite = heartFull;
                }
                else
                {
                    heart_list[i].sprite = heartNull;
                }
            }
        }
        else
        {
        
            for (int i = 0; i < heart_list.Count; i++)
            {
                
                if (i+1 <= hp / 2 )
                {
                   
                    heart_list[i].sprite = heartFull;                   
                }               
                else if(i+1 == (hp / 2) + 1)
                {
                 
                    
                    heart_list[i].sprite = heartHelf;
                }
                else 
                {
                   
                    heart_list[i].sprite = heartNull;

                }

            }
        }
  
    }

    public void LoadLevel(int index)
    {
        
    }

  
}
