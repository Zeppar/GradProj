using JetBrains.Annotations;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public  class EnemyInfo
{
    public int id;
    public string name;

    public int attack;
    public int attackAdd;

    public int hp;
    public int hpAdd;

    public int speed;
    public float attackInterval;

}

public class EnemyManager
{
    public Dictionary<int, EnemyInfo> enemyDict = new Dictionary<int, EnemyInfo>();
    private const int defaultHp = 100;
    private const int defaultAttack = 8;

    public void InitEmey()
    {
        JsonData enemyData = JsonMapper.ToObject(Resources.Load<TextAsset>("Enemy/EnemyData").text);
        for (int i = 0; i < enemyData.Count; i++)
        {
            EnemyInfo info = new EnemyInfo();
            info.id = (int)enemyData[i]["id"];
            info.name = enemyData[i]["name"].ToString();
            info.attack = (int)enemyData[i]["attack"];
            info.attackAdd = (int)enemyData[i]["attack_add"];
            info.hp = (int)enemyData[i]["hp"];           
            info.hpAdd = (int)enemyData[i]["hp_add"]; 
            info.speed = (int)enemyData[i]["speed"]; 
            info.attackInterval= Convert.ToSingle(enemyData[i]["attackInterval"].ToString());
                                                                           
            enemyDict.Add((int)enemyData[i]["id"], info);

        }    
    }

    public EnemyInfo FindEnemyWithID(int id)
    {
        if (!enemyDict.ContainsKey(id))
        {
            return null;
        }
        return enemyDict[id];
    }

    public int GetAttackInfo(int id,int level)
    {
        var info = FindEnemyWithID(id);
        if (info == null)
            return defaultAttack;
        return info.attack + (level * info.attackAdd);
    }

    public int GetHPInfo(int id, int level)
    {
        var info = FindEnemyWithID(id);
        if (info == null)
            return defaultHp;
        return info.hp + (level * info.hpAdd);
    }
}
