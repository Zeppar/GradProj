using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class LevelInfo {
    public int id;
    public bool dashEnable;
    public bool bagEnable;
}

public class LevelManager
{
    public List<LevelInfo> list = new List<LevelInfo>();
    public LevelInfo currentInfo = null;

    public void Init() {
        var t = Resources.Load<TextAsset>("Level/Level");
        JsonData data = JsonMapper.ToObject(t.text);
        for(int i = 0; i < data.Count; i++) {
            LevelInfo info = new LevelInfo();
            info.id = (int)data[i]["id"];
            info.dashEnable = (bool)data[i]["DashEnable"];
            info.bagEnable = (bool)data[i]["BagEnable"];
            list.Add(info);
            if (i == 0)
                currentInfo = info;
        }
    }

    public void EnterNextLevel() {
        try {
            currentInfo = list[currentInfo.id];
        } catch {
            Debug.LogError("Can't find next level");
        }
    }
}
