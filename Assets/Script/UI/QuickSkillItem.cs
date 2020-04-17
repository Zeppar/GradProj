using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuickSkillItem : MonoBehaviour, IDropHandler {
    public GoodInfo goodInfo;
    public GoodItem goodItemPrefab;
    public int index;
    private GoodItem goodItem = null;
    public KeyCode curKeyCode;

    public void SetContent(GoodInfo info, int idx) {
        goodInfo = info;
        index = idx;
        if (goodInfo == null) {
            if (goodItem != null)
                goodItem.gameObject.SetActive(false);
            return;
        }

        if (goodItem == null) {
            goodItem = Instantiate(goodItemPrefab);
            goodItem.transform.SetParent(transform, false);
        }
        goodItem.SetContent(info, idx, GoodItemType.Skill);
        goodItem.gameObject.SetActive(true);
    }

    public void OnDrop(PointerEventData eventData)//当作为目标
    {
        GoodItem dropedItem = eventData.pointerDrag.GetComponent<GoodItem>();
        if (dropedItem == null)
            return;
        if (!Mathf.Approximately(dropedItem.percentValue, 0)) {
            Debug.Log("技能还未冷却，不能移动技能");
            return;
        }
        
        if (dropedItem.itemType == GoodItemType.Normal) {
            var info = dropedItem.goodInfo;
            GameManager.instance.goodManager.ChangeGoodInfo(dropedItem.index, goodInfo);
            GameManager.instance.skillManager.ChangeQuickSkill(index, info);
            GameManager.instance.skillActionManager.SetKeyCode(curKeyCode, info);
        } else {
            GameManager.instance.skillManager.ExchangeGoodInfo(index, dropedItem.index);
        }
    }
}
