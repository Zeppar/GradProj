using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyPanel : MonoBehaviour
{

    public Transform lightTransform;
    public Image lightSilder;

    private void Update() {
        //energyText.text = (GameManager.instance.energyManager.GetPercentValue() * 100).ToString("0.0") + "%";
        //energySlider.value = GameManager.instance.energyManager.GetPercentValue();
        lightSilder.fillAmount = GameManager.instance.energyManager.GetPercentValue();
    }
}
