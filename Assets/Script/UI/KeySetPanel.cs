using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySetPanel : MonoBehaviour
{
    public GameObject keyCount;
    public Transform InitTran;
    void Start()
    {
        Init();
    }
    void Init()
    {
        foreach (var item in GameManager.instance.keyManager.keyInfos)
        {
            GameObject count =  Instantiate(keyCount);
            count.transform.SetParent(InitTran);
            count.GetComponent<KeyCount>().SetCount(item.Value.key, item.Value.motd,item.Value.id);
        }
    }
}
