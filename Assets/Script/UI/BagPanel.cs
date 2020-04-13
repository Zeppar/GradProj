using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagPanel : MonoBehaviour {

    public GameObject slot;//将要创建的每个格子
    public GameObject item;//将要创建的每个物品
    public GameObject count_Slot;//数量标签

    GoodManger manger;//物品管理器
    GameObject goodPanel;//背包

    public List<GameObject> GoodWasAdded = new List<GameObject>();//记录了在场景中的所有物品的GameObject
    public List<GoodItem> goodItem_List = new List<GoodItem>();


    private void Awake() {

      
    }
    public void Init()
    {
        manger = GameManager.instance.goodManger;//初始化物品管理器内容
        goodPanel = GameObject.Find("Slot Panel");//初始化背包面板内容
        InitPanel();
      
    }
    void InitPanel()
    {   
        InitSlot();
    }
   void InitQuitBag()//初始化快捷背包
    {
       
        BagItem skill1 = UIManger.instance.quickSkill1.GetComponent<BagItem>();
        BagItem skill2 = UIManger.instance.quickSkill2.GetComponent<BagItem>();
        skill1.index = manger.goodInfoList.Count;
        //  skill1.goodInfo.count = 0;
        manger.goodInfoList.Add(skill1);
        skill2.index = manger.goodInfoList.Count;
        //  skill2.goodInfo.count = 0;
        manger.goodInfoList.Add(skill2);
    }
   void InitSlot()//初始化背包格子
    {
        manger = GameManager.instance.goodManger;//初始化物品管理器内容
        goodPanel = GameObject.Find("Slot Panel");//初始化背包面板内容
        for (int i = 0; i < manger.bagCount; i++) {
            GameObject slotToAdd = Instantiate(slot);//生成物体
            slotToAdd.transform.SetParent(goodPanel.transform);//设置层次
            slotToAdd.transform.position = Vector2.zero;//物品居中
            slotToAdd.GetComponent<BagItem>().index = i;//设置格子的索引
            slotToAdd.name = "Slot(" + i + ")";//改个名字

            manger.goodInfoList.Add(slotToAdd.GetComponent<BagItem>());

        }
        InitQuitBag();
        UIManger.instance.BagPanel_Obj.SetActive(false);

    }
    public void UpdataItem() {
        for (int i = 0; i < GoodWasAdded.Count; i++)//清空装备
        {
            Destroy(GoodWasAdded[i]);//在场景中清楚物体

        }
        GoodWasAdded.Clear();
        goodItem_List.Clear();
        for (int i = 0; i < manger.goodInfoList.Count; i++)//生成装备
        {
            if (manger.goodInfoList[i].goodInfo != null) {
                GameObject skillToAdd = Instantiate(item);//生成物体
                GoodWasAdded.Add(skillToAdd);
                goodItem_List.Add(skillToAdd.GetComponent<GoodItem>());
                try {
                    skillToAdd.GetComponent<Image>().sprite = GameManager.instance.skillManager.Icon_List[manger.goodInfoList[i].goodInfo.skill.ID];
                } catch (System.Exception) {
                    Debug.LogError("Sprite:" + skillToAdd.GetComponent<Image>().sprite);
                    Debug.LogError("Index:" + manger.goodInfoList[i].goodInfo.skill.ID);
                    Debug.LogError("List:" + GameManager.instance.skillManager.Icon_List);

                    Debug.LogError("ToAdd:" + GameManager.instance.skillManager.Icon_List[manger.goodInfoList[i].goodInfo.skill.ID]);

                }

                //   Resources.Load<Sprite>(manger.goodInfoList[i].goodInfo.skill.Icon);//加载图标
                skillToAdd.transform.SetParent(manger.goodInfoList[i].transform);//设置层级                                
                skillToAdd.name = manger.goodInfoList[i].goodInfo.skill.Title;//设置Unity内物品标题             
                skillToAdd.GetComponent<GoodItem>().SlotInedx = i;//设置他对应的格子
                skillToAdd.transform.localPosition = Vector3.zero;//设置位置居中             

                if (manger.goodInfoList[i].goodInfo.count > 1) {
                    // print("发现叠加");
                    GameObject CountToAdd = Instantiate(count_Slot);
                    CountToAdd.transform.SetParent(skillToAdd.transform);
                    CountToAdd.GetComponent<Text>().text = manger.goodInfoList[i].goodInfo.count.ToString();
                    CountToAdd.transform.localPosition = new Vector2(10, -35);
                }

            }
        }

    }
}





