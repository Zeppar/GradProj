using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GoodItem : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerEnterHandler,IPointerExitHandler
{
   
    public int SlotInedx;
    public GameObject describePanel;

    public Image Mask;

    private void Awake()
    {
        describePanel = UIManger.instance.describePanel.gameObject;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        
            this.transform.SetParent(transform.parent.parent.parent.parent,false);
            this.transform.position = eventData.position;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
       
            this.transform.position = eventData.position;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       
        transform.SetParent(GameManager.instance.goodManger.goodInfoList[SlotInedx].transform,false);
        transform.position = transform.parent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       
       
        describePanel.SetActive(true);
        describePanel.transform.position = Input.mousePosition;
        describePanel.GetComponent<DescribePanel>().Title.text = GameManager.instance.goodManger.goodInfoList[SlotInedx].goodInfo.skill.Title;
        describePanel.GetComponent<DescribePanel>().Describe.text = GameManager.instance.goodManger.goodInfoList[SlotInedx].goodInfo.skill.Describe;
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        describePanel.SetActive(false);

    }

  
}
