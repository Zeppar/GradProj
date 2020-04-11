using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameSaveManger : MonoBehaviour
{
    public void SaveGame()
    {
        ES3.Save<List<BagItem>>("GoodInfoList", GameManager.instance.goodManger.goodInfoList);

        List<BagItem> bagItems = ES3.LoadInto <List<BagItem>>("GoodInfoList");
        int goodCount = 0;

        
        for (int i = 0; i < GameManager.instance.goodManger.goodInfoList.Count; i++)
        {
            BagItem b = GameManager.instance.goodManger.goodInfoList[i];
            if(b.goodInfo != null && b.goodInfo.skill != null)
            {
                goodCount++;
            }
        }
        print(goodCount);
          
    }
        
     

    
    public void LoadGame()
    {
        //增加物品时，记住增加各自中的物品数量个物品
        if (!ES3.KeyExists("GoodInfoList"))
        {
            Debug.LogError("There hasn't GoodInfo List");
            return;
        }
        //List<BagItem> bagItems = ES3.LoadInto<List<BagItem>>("GoodInfoList");
        List<BagItem> bagItems = GameManager.instance.goodManger.goodInfoList;
        int goodCount = 0;
        for (int i = 0; i < bagItems.Count; i++)
        {
            if (bagItems[i].goodInfo != null)
            {
                BagItem b = bagItems[i];
                GameManager.instance.goodManger.AddItemToPanel(b.goodInfo.goodType,b.goodInfo.skill.ID,b.goodInfo.count);
                goodCount++;
            }
        }
        print(goodCount);
    }

    public void SerializeObject(object toSerializeObject)
    {
        FileStream fs = new FileStream("demo.bin", FileMode.OpenOrCreate);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, toSerializeObject);
        fs.Close();
        Debug.LogError("write done");
    }
    public object UnSerializeObject<T>()
    {
        FileStream fs = new FileStream("demo.bin", FileMode.Open);
        BinaryFormatter bf = new BinaryFormatter();
        object toUnObject = bf.Deserialize(fs) as object;
        fs.Close();
       return toUnObject;

    }
}
