using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameSaveManger : MonoBehaviour
{
    public void SaveGame()
    {
        ES3.Save<List<GoodInfo>>("GoodInfoList", GameManager.instance.goodManager.goodInfoList);
        
        List<GoodInfo> bagItems = ES3.LoadInto <List<GoodInfo>>("GoodInfoList");
        int goodCount = 0;

        
        for (int i = 0; i < GameManager.instance.goodManager.goodInfoList.Count; i++)
        {
            GoodInfo b = GameManager.instance.goodManager.goodInfoList[i];
            if(b != null && b.skillInfo != null)
            {
                goodCount++;
            }
        }
        print(goodCount);
          
    }
        
     

    
    public void LoadGame()
    {
        ////增加物品时，记住增加各自中的物品数量个物品
        if (!ES3.KeyExists("GoodInfoList")) { 
        
            Debug.LogError("There hasn't GoodInfo List");
            return;
        }
        List<GoodInfo> goodItems = ES3.LoadInto<List<GoodInfo>>("GoodInfoList");       
        int goodCount = 0;
        for (int i = 0; i < goodItems.Count; i++)
        {
            if (goodItems[i] != null)
           {
                GoodInfo b = goodItems[i];
               GameManager.instance.goodManager.AddGoodInfo(b);
                goodCount++;
            }
        }
        print(goodCount);
    }
    


}
