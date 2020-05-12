using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInfo
{
    public string id;
    public KeyCode keyCode;
    public string des;

    public KeyInfo(string id, KeyCode keyCode, string des)
    {
        this.id = id;
        this.keyCode = keyCode;
        this.des = des;
    }
        
}
public class KeyManager {
    public Dictionary<string, KeyInfo> keyInfos = new Dictionary<string, KeyInfo>();
    public void Init() {
        InitKey();
    }

    public void InitKey() {
        JsonData KeyData = JsonMapper.ToObject(Resources.Load<TextAsset>("Key/KeyInfo").text);
        for (int i = 0; i < KeyData.Count; i++) {
            KeyInfo info = new KeyInfo(KeyData[i]["id"].ToString(), (KeyCode)((int)KeyData[i]["key"]), KeyData[i]["motd"].ToString());
            keyInfos.Add(info.id, info);
        }
    }

    public KeyInfo FindKey(string id) {
        if (!keyInfos.ContainsKey(id)) {
            Debug.LogError("Missing Key");
            return null;
        }
        return keyInfos[id];
    }

    public void SetKeyData(string id, KeyCode keyCode, KeyCode originKeyCode, Util.NoParmsCallBack callBack) {
        bool find = false;
        foreach(var kv in keyInfos) {
            if(kv.Value.keyCode == keyCode) {
                find = true;
                break;
            }
        }
        if (find) {
            GameManager.instance.keyManager.keyInfos[id].keyCode = originKeyCode;
        } else {
            GameManager.instance.keyManager.keyInfos[id].keyCode = keyCode;
        }
        callBack?.Invoke();
    }
}
