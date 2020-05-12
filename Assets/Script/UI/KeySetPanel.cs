using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySetPanel : MonoBehaviour
{
    public KeyItem itemPrefab;
    public Transform InitTran;
    void Start()
    {
        Init();
    }
    void Init()
    {
        foreach (var kv in GameManager.instance.keyManager.keyInfos)
        {
            KeyItem keyItem =  Instantiate(itemPrefab);
            keyItem.transform.SetParent(InitTran);
            keyItem.SetContent(kv.Value);
        }
    }
}
