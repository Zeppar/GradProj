using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpCanvas : MonoBehaviour
{
    public Text hpText;
    public float alphaMulti;
    public CanvasGroup cg;

    public void ShowHPUI(int hp) {
        hpText.text = hp.ToString();
        cg.alpha = 1.0f;
    }

    public void Update() {
        cg.alpha *= alphaMulti;
        transform.localPosition += new Vector3(0, 0.2f, 0);
        if (cg.alpha <= 0.01f) {
            gameObject.SetActive(false);
            ObjectPool.instance.ReturnToPool(gameObject);
        }
    }
}
