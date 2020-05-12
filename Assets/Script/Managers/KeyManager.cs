using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInfo
{
    public string id;
    public KeyCode key;
    public string motd;

    public KeyInfo(string id, KeyCode key, string motd)
    {
        this.id = id;
        this.key = key;
        this.motd = motd;
    }
        
}
public class KeyManager
{
   // public List<KeyInfo> keyInfos = new List<KeyInfo>();
    public Dictionary<string, KeyInfo> keyInfos = new Dictionary<string, KeyInfo>();
    public  void Init()
    {
        //   AddKey(Util.KeyCollection.Attack, KeyCode.J, "攻击");
        //  AddKey(Util.KeyCollection.Dash, KeyCode.K, "冲刺");
        //  AddKey(Util.KeyCollection.Jump, KeyCode.W, "跳跃");
        //  AddKey(Util.KeyCollection.OpenBag, KeyCode.B, "背包");
        InitKey();
    }
    public void InitKey()
    {
        JsonData KeyData = JsonMapper.ToObject(Resources.Load<TextAsset>("Key/KeyInfo").text);
        for (int i = 0; i < KeyData.Count; i++)
        {
            KeyInfo info = new KeyInfo(KeyData[i]["id"].ToString(),(KeyCode)((int)KeyData[i]["key"]),KeyData[i]["motd"].ToString());

            keyInfos.Add(info.id, info);
      
       }
    }

    public KeyInfo FindKey(string id)
    {
        return keyInfos[id];
        //foreach (var item in keyInfos)
        //{
         //   if(item.Value.id == id)
         //   {
         //       return item.Value;
         //   }
     //   }
      //  return null;
    }
    
}
