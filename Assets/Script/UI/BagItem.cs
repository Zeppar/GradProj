using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagItem : MonoBehaviour, IDropHandler {

    public GoodInfo goodInfo;//每个BagItem，都有一个GoodInfo
    public int index;
    public Text countText;

    public GoodItem goodItemPrefab;//将要创建的每个物品 
    private GoodItem goodItem = null;


    // TODO  优化这个函数
    public void SetContent(GoodInfo info) {
        //Debug.Log("SetContent : " + info);
        goodInfo = info;
        if (goodInfo == null) {
            if (goodItem != null)
                goodItem.gameObject.SetActive(false);
            countText.gameObject.SetActive(false);

            return;
        }

        if (goodItem == null) {
            goodItem = Instantiate(goodItemPrefab);
            goodItem.transform.SetParent(transform, false);
        }
        goodItem.SetContent(info, index, GoodItemType.Normal);
        goodItem.transform.SetAsFirstSibling();
        goodItem.gameObject.SetActive(true);

        countText.text = info.count.ToString();
        countText.gameObject.SetActive(info.count > 0);
    }

    // TODO OnDrop 先执行 OnEndDrag
    // 不拖动物体 放下来是否触发 
    //public GameObject skillAction = null;
    public void OnDrop(PointerEventData eventData)//当作为目标
    {
        GoodItem dropedItem = eventData.pointerDrag.GetComponent<GoodItem>();
        if (dropedItem == null)
            return;
        if (!Mathf.Approximately(dropedItem.percentValue,0)) {
            Debug.Log("技能还未冷却，不能移动技能");
            return;
        }

        if (dropedItem.itemType == GoodItemType.Normal) {
            GameManager.instance.goodManager.ExchangeGoodInfo(index, dropedItem.index);
        } else {
            var info = GameManager.instance.goodManager.goodInfoList[index];
            GameManager.instance.goodManager.ChangeGoodInfo(index, dropedItem.goodInfo);
            GameManager.instance.skillManager.ChangeQuickSkill(dropedItem.index, info);
        }
    }
}
