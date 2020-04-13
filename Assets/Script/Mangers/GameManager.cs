﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;//实例

    public SkillManager skillManager = new SkillManager();//技能管理器实例
    public GoodManger goodManger = new GoodManger();//物品管理器实例

    public Player player;//玩家脚本

    public SkillStoneCreator skillStoneCreator;//技能石创建器
    public SkillParticleCreator skillParticleCreator;//技能特效创建器
    public SkillActionManger skillActionManger;
    public EffectManger effectManager;
   
    
    void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else if(instance != this) {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        skillManager.InitSkill();//初始化技能

        //goodManger.AddItemToPanel(GoodInfo.GoodType.Skill, 0);//测试！！！  创建两个物品用于测试
        // goodManger.AddItemToPanel(GoodInfo.GoodType.Skill, 2);
        // goodManger.AddItemToPanel(GoodInfo.GoodType.Skill, 1);
        UIInit();
    }
    void UIInit()
    {
        UIManger ui = Instantiate(Resources.Load("UI/UIController") as GameObject).GetComponent<UIManger>();
        ui.Init();

    }

}
