using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashFlag : MonoBehaviour
{
    public Animator dashAnim;
    public Image sliderImg;

    public void SetValue(float val) {
        sliderImg.fillAmount = val;
        if(Mathf.Approximately(val, 1.0f) && !dashAnim.enabled) {
            dashAnim.enabled = true;
        } else if(Mathf.Approximately(val, 0) && dashAnim.enabled) {
            sliderImg.color = Color.white;
            dashAnim.enabled = false;
        }
    }
}
