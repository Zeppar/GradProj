using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashFlag : MonoBehaviour
{
    public Image sliderImg;
    public Material skingMat;

    public void SetValue(float val) {
        float value = Mathf.Clamp(val, 0, 1.0f);
        sliderImg.fillAmount = value;
        if (Mathf.Approximately(value, 1.0f) && sliderImg.material != skingMat) {
            sliderImg.material = skingMat;
        } else if (value < 0.5f && sliderImg.material == skingMat) {
            sliderImg.color = Color.white;
            sliderImg.material = null;
        }
    }
}
