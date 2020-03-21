using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SkillData : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    // Start is called before the first frame upd

    public SkillInfo skill;
    public int SlotInedx;

    public SkillBagManger bag;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (skill != null)
        {
            this.transform.SetParent(transform.parent.parent);
            this.transform.position = eventData.position;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (skill != null)
        {
            this.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(bag.slots[SlotInedx].transform);
        transform.position = transform.parent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    void Start()
    {
        
      
          
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
