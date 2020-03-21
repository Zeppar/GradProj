using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BagItem : MonoBehaviour, IDropHandler
{
    public GoodInfo goodInfo;//每个BagItem，都有一个GoodInfo
    public int index;
    public ItemType itemType = ItemType.Slot;
    //public GameObject skillAction = null;
    public void OnDrop(PointerEventData eventData)//但作为目标
    {
        
        GoodItem dropedItem = eventData.pointerDrag.GetComponent<GoodItem>();
        
        if (goodInfo == null) //需要修改修改修改修改
        {
            goodInfo = new GoodInfo();//先将自己的GoodInfo赋值           
            goodInfo.goodType = GameManger.instance.goodManger.goodInfoList[dropedItem.SlotInedx].goodInfo.goodType;//同步物品类型        
            goodInfo.skill = GameManger.instance.goodManger.goodInfoList[dropedItem.SlotInedx].goodInfo.skill;//同步skill
            goodInfo.Consumables = GameManger.instance.goodManger.goodInfoList[dropedItem.SlotInedx].goodInfo.Consumables;//同步skill
            goodInfo.count = GameManger.instance.goodManger.goodInfoList[dropedItem.SlotInedx].goodInfo.count;//同步数量
            GameManger.instance.goodManger.goodInfoList[dropedItem.SlotInedx].goodInfo = null;//设置原格子的goodinfo为空
            dropedItem.SlotInedx = index;//设置物品的格子索引为自己         
            UIManger.instance.bagPanel.UpdataItem();
        }
        else if(goodInfo == GameManger.instance.goodManger.goodInfoList[dropedItem.SlotInedx].goodInfo) {
           // Debug.LogError("...");
               
        }        
        else if(goodInfo.skill.ID == GameManger.instance.goodManger.goodInfoList[dropedItem.SlotInedx].goodInfo.skill.ID && goodInfo != GameManger.instance.goodManger.goodInfoList[dropedItem.SlotInedx].goodInfo) { //相同物品可叠加
            
            goodInfo.count++;
            GameManger.instance.goodManger.goodInfoList[dropedItem.SlotInedx].goodInfo = null;
            UIManger.instance.bagPanel.UpdataItem();
          
        }
        else if(goodInfo != null)//如果自己有物体，这交换物体数据
        {
                
            GoodInfo Todrop = goodInfo;//预替换组件
            GoodInfo Tome = GameManger.instance.goodManger.goodInfoList[dropedItem.SlotInedx].goodInfo;//预替换组件

            print("ToDrop Count:" + Todrop.count);
            print("Tome Count:" + Tome.count);

            

            goodInfo = Tome;//更改
            goodInfo.count = Tome.count;

            GameManger.instance.goodManger.goodInfoList[dropedItem.SlotInedx].goodInfo = Todrop;
            GameManger.instance.goodManger.goodInfoList[dropedItem.SlotInedx].goodInfo.count = Todrop.count;

            GameManger.instance.goodManger.goodInfoList[index].index = dropedItem.SlotInedx;
           //IManger.instance.bagPanel.goodItem_List[index].SlotInedx = dropedItem.SlotInedx;
            dropedItem.SlotInedx = index;



            dropedItem.OnEndDrag(eventData);
            UIManger.instance.bagPanel.UpdataItem();

            print("ToDrop Count1:" + Todrop.count);
            print("Tome Count1:" + Tome.count);

           
          
            
        }
        else
        {
            return;
        }
      // if(skillAction!=null && GameManger.instance.goodManger.goodInfoList[index].)
      //  {

     //   }
        
    }

    public enum ItemType
    {
        Slot,
        Quikly,
    }
}
