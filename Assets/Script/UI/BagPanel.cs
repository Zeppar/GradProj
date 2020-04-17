using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagPanel : MonoBehaviour {

    public BagItem bagItemPrefab;//将要创建的每个格子
    public Transform bagItemParent;//背包父物体

    public void Init()
    {
        for (int i = 0; i < GameManager.instance.goodManager.bagCount; i++) {
            BagItem bagItem = Instantiate(bagItemPrefab);//生成物体
            bagItem.transform.SetParent(bagItemParent, false);//设置层次
            bagItem.index = i;
            bagItem.name = "Slot(" + i + ")";
        }
        gameObject.SetActive(false);
    }

    private void Update() {
        if(GameManager.instance.goodManager.isDirty) {
            GameManager.instance.goodManager.isDirty = false;
            UpdateItem();
        }
    }

    private void UpdateItem() {
        BagItem[] bagItems = bagItemParent.GetComponentsInChildren<BagItem>();
        for(int i = 0; i < bagItems.Length;i++) {
            bagItems[i].SetContent(GameManager.instance.goodManager.goodInfoList[i]);
        }
    }
}





