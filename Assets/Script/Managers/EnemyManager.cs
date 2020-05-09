using JetBrains.Annotations;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnemyInfo
{
    public int id;
    public string name;
    public int attack;
    public int attackAdd;
    public int hp;
    public int hpAdd;
    public float speed;
    public float attackInterval;
    public float chaseDis;
    public float attackRange;
    public bool hasSlider;
    public EnemyType type;

    public int GetAttackByLevel(int level) {
        return attack + (level * attackAdd);
    }

    public int GetHPByLevel(int level) {
        return hp + (level * hpAdd);
    }
}

public class EnemyManager {
    public Dictionary<int, EnemyInfo> enemyDict = new Dictionary<int, EnemyInfo>();

    public void InitEnemy() {
        JsonData enemyData = JsonMapper.ToObject(Resources.Load<TextAsset>("Enemy/EnemyData").text);
        for (int i = 0; i < enemyData.Count; i++) {
            EnemyInfo info = new EnemyInfo();
            info.id = (int)enemyData[i]["id"];
            info.name = enemyData[i]["name"].ToString();
            info.attack = (int)enemyData[i]["attack"];
            info.attackAdd = (int)enemyData[i]["attack_add"];
            info.hp = (int)enemyData[i]["hp"];
            info.hpAdd = (int)enemyData[i]["hp_add"];
            info.speed = (float)enemyData[i]["speed"];
            info.attackInterval = (float)enemyData[i]["attackInterval"];
            info.chaseDis = (float)enemyData[i]["chaseDis"];
            info.attackRange = (float)enemyData[i]["attackRange"];
            info.hasSlider = (bool)enemyData[i]["hasSlider"];
            info.type = (EnemyType)(int)enemyData[i]["type"];

            enemyDict.Add(info.id, info);
        }
    }

    public EnemyInfo GetEnemyInfoWithID(int id) {
        if (!enemyDict.ContainsKey(id)) {
            return null;
        }
        return enemyDict[id];
    }
}
