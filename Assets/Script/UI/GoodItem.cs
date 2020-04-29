using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 对于背包来说 index 赋值的是背包的index
// 对于技能来说 index 赋值的是技能的index
public enum GoodItemType {
    Skill = 0,
    Normal
}

public class GoodItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler {
    //public int SlotInedx;
    public int index;
    public Image maskImage;
    public Image goodImage;
    public GoodItemType itemType;

    public GoodInfo goodInfo;
    public float percentValue;
    private Transform parent;

    

    public void SetContent(GoodInfo info, int idx, GoodItemType type) {
        goodInfo = info;
        index = idx;
        itemType = type;
        if (info == null)
            return;
        goodImage.sprite = info.skillInfo.iconSprite;
        transform.name = info.skillInfo.name;
    }

    private void Update() {
        percentValue = 1 - (Time.time - GameManager.instance.skillActionManager.cdDict[goodInfo.skillInfo.id]) / goodInfo.skillInfo.cd;

        if (percentValue < 0) {
            percentValue = 0;
        }
        maskImage.fillAmount = percentValue;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        parent = transform.parent;
        transform.SetParent(UIManager.instance.goodItemMidParent, false);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        transform.SetParent(parent, false);
        transform.SetAsFirstSibling();
        transform.position = transform.parent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        UIManager.instance.describeAlert.Show(goodInfo.skillInfo.name, goodInfo.skillInfo.describe, Input.mousePosition);
    }

    public void OnPointerExit(PointerEventData eventData) {
        UIManager.instance.describeAlert.Hide();
    }
}
