using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPanel : MonoBehaviour
{
    public GameObject getItemTipGo;
    public GameObject ownItemTipGo;

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
