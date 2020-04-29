﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSkillPanel : MonoBehaviour
{
    private void Update() {
        if (GameManager.instance.skillManager.isDirty) {
            GameManager.instance.skillManager.isDirty = false;
            UpdateItem();
        }
    }

    private void UpdateItem() {

        QuickSkillItem[] skillItems = transform.GetComponentsInChildren<QuickSkillItem>();
        for (int i = 0; i < skillItems.Length; i++) {
            skillItems[i].SetContent(GameManager.instance.skillManager.quickSkillList[i], i);
        }
    }
}
