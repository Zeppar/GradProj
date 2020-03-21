using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodInfo//GoodInfo 数据类，必须无执行方法
{
    
    public enum GoodType{//物品类型
        Item,
        Skill,
        }
    public SkillInfo skill;//技能实例


    public ConsumablesInfo Consumables;//消耗品实例
   
    public int count = 1;//数量
    public GoodType goodType;//物品类型实例
    
}
