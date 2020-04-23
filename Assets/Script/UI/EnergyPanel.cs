using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyPanel : MonoBehaviour
{
    public Slider energySlider;
    public Text energyText;
    public Transform lightTransform;

    private void Update() {
        energyText.text = (GameManager.instance.energyManager.GetPercentValue() * 100).ToString("0.0") + "%";
        energySlider.value = GameManager.instance.energyManager.GetPercentValue();
    }
}
