using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodInfo//GoodInfo 数据类
{
    public SkillInfo skillInfo;//技能实例
    public int count = 1;//数量

    public GoodInfo(SkillInfo info, int _count) {
        skillInfo = info;
        count = _count;
    }
    public GoodInfo()
    {

    }
}

public class GoodManager
{
    public int bagCount = 16;
    public List<GoodInfo> goodInfoList = new List<GoodInfo>();
    public bool isDirty = false;

    public void InitGoods() {
        foreach(var kv in GameManager.instance.skillManager.skillDict) {
            goodInfoList.Add(new GoodInfo(kv.Value, 1));
        }
        int curIndex = goodInfoList.Count;
        for(int i = curIndex; i < bagCount; i++) {
            goodInfoList.Add(null);
        }
        isDirty = true;
    }

    public void AddGoodInfo(GoodInfo goodInfo)
    {
        bool find = false;
        for (int i = 0; i < goodInfoList.Count; i++) //还未检查检查检查检查检查
        {
            if (goodInfoList[i] == null)
                continue;
            if(goodInfo.skillInfo.id == goodInfoList[i].skillInfo.id)
            {
                goodInfoList[i].count += 1;
                find = true;
                break;
            }
        }
        if(!find) {
            goodInfoList.Add(goodInfo);
        }
        isDirty = true;
    }

    public void ExchangeGoodInfo(int index1, int index2) {
        // 值类型 引用类型
        var goodInfo = goodInfoList[index1];
        goodInfoList[index1] = goodInfoList[index2];
        goodInfoList[index2] = goodInfo;
        isDirty = true;
    }

    public void ChangeGoodInfo(int index, GoodInfo info) {
        goodInfoList[index] = info;
        isDirty = true;
    }
}
