using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager
{
    public void Save()
    {
      //  SaveGood();
        SavePlayerData();
    }
    public void Load()
    {
     //   LoadGood();
        LoadPlayerData();
    }
    public void SavePlayerData()
    {
        PlayerData data= new PlayerData();

        data.pos= GameManager.instance.player.transform.position;
        data.goodInfos = GameManager.instance.goodManager.goodInfoList;
        data.playerHp = GameManager.instance.player.HP;
        data.energy = GameManager.instance.energyManager.curEnemgy;

        ES3.Save<PlayerData>("PlayerData", data);
    }
    public void LoadPlayerData()
    {

        PlayerData loaddata = ES3.LoadInto<PlayerData>("PlayerData");

        GameManager.instance.player.transform.position = loaddata.pos;
        GameManager.instance.goodManager.goodInfoList = loaddata.goodInfos;
        GameManager.instance.player.HP = loaddata.playerHp;
        GameManager.instance.energyManager.curEnemgy =  loaddata.energy;
        GameManager.instance.goodManager.isDirty = true;
    }

}
public class PlayerData
{
    public Vector2 pos;
    public List<GoodInfo> goodInfos;
    public float playerHp;
    public float energy;
   // GameManager.instance.energyManager.GetPercentValue();
}
