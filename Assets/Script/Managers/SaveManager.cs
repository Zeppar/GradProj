using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class Save
{
    public List<GoodInfo> saveGoodInfo;
}
public class SaveManager: ICloneable
{
    public void SaveGood()
    {
        Save data = new Save();
  //      data.saveGoodInfo = new List<GoodInfo>();
        data.saveGoodInfo = GameManager.instance.goodManager.goodInfoList;
        ES3.Save<Save>("Good", data);
     //   List<GoodInfo> deepCopyList = Clone<GoodInfo>(originalList);


     //   BinaryFormatter bf = new BinaryFormatter();
       
        //FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
  
     //   bf.Serialize(file, data);  
      //  file.Close();            

    }
    public void LoadGood()
    {
        var obj = (List<GoodInfo>)ES3.LoadInto<object>("Good");
        GameManager.instance.goodManager.goodInfoList = obj;
        GameManager.instance.goodManager.isDirty = true;
    }

    public object Clone()
    {
        throw new NotImplementedException();
    }
}
