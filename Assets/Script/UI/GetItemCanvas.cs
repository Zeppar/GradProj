using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetItemCanvas : MonoBehaviour
{
    public Text showText;
    public float alphaMulti;
    public CanvasGroup cg;

    public void ShowGetItemUI(int value) {
        showText.text = "+ " + value.ToString() + "<size=24>LX</size>";
        cg.alpha = 1.0f;

    }

    public void Update() {
        cg.alpha *= alphaMulti;
        transform.localPosition += new Vector3(0, 0.4f, 0);
        if (cg.alpha <= 0.01f) {
            gameObject.SetActive(false);
            ObjectPool.instance.ReturnToPool(gameObject);
        }
    }
}
