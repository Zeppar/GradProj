using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackScreenEffect : MonoBehaviour
{
    public Image selfImage;
    public float originAlpha;
    public float multi;
    private Coroutine coroutine;
    private bool isShow = false;

    public void Show() {
        if (isShow && selfImage.color.a > 0.5f)
            return;
        isShow = true;
        gameObject.SetActive(true);
        if (coroutine != null)
            StopCoroutine(coroutine);
        selfImage.color = new Color(selfImage.color.r, selfImage.color.g, selfImage.color.b, originAlpha);
        coroutine = StartCoroutine(Hide());
    }

    private IEnumerator Hide() {
        while(true) {
            yield return new WaitForEndOfFrame();
            float alpha = selfImage.color.a * multi;
            selfImage.color = new Color(selfImage.color.r, selfImage.color.g, selfImage.color.b, alpha);
            if(alpha < 0.01f) {
                isShow = false;
                gameObject.SetActive(false);
            }
        }
    }


}
