using JetBrains.Annotations;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public  class EnemyInfo
{
    public int id;
    public string name;

    public int attack;
    public int attack_add;

    public int hp;
    public int hp_add;
    
}

public class EnemyManager
{
    public Dictionary<int, EnemyInfo> enemyDict = new Dictionary<int, EnemyInfo>();

    public void InitEmey()
    {
        JsonData enemyData = JsonMapper.ToObject(Resources.Load<TextAsset>("Enemy/EnemyData").text);
        for (int i = 0; i < enemyData.Count; i++)
        {
            EnemyInfo info = new EnemyInfo();
            info.id = (int)enemyData[i]["id"];
            info.name = enemyData[i]["name"].ToString();
            info.attack = (int)enemyData[i]["attack"];
            info.attack_add = (int)enemyData[i]["attack_add"];
            info.hp = (int)enemyData[i]["hp"];           
            info.hp_add = (int)enemyData[i]["hp_add"]; 
            
            enemyDict.Add((int)enemyData[i]["id"], info);
        }    
    }

    public EnemyInfo FindEnemyWithID(int _id)
    {
        if (!enemyDict.ContainsKey(_id))
        {
            return null;
        }
        return enemyDict[_id];
    }

    public int GetAttackInfo(int _id,int _level)
    {

        return FindEnemyWithID(_id).attack + (_level * FindEnemyWithID(_id).attack_add);
    }

    public int GetHPInfo(int _id, int _level)
    {

        return FindEnemyWithID(_id).hp + (_level * FindEnemyWithID(_id).hp_add);
    }
}
