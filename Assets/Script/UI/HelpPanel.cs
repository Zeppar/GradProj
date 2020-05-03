﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpPanel : MonoBehaviour
{
    public GameObject getItemTipGo;
    public GameObject ownItemTipGo;
    public TopTip topTip;

    public void ShowGetItemTip() {
        getItemTipGo.SetActive(true);
    }

    public void HideGetItemTip() {
        getItemTipGo.SetActive(false);
    }

    public void ShowOwnItemTip() {
        ownItemTipGo.SetActive(true);
    }

    public void HideOwnItemTip() {
        ownItemTipGo.SetActive(false);
    }

    

}
