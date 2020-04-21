using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboPanel : MonoBehaviour
{
    public Text comboText;
    public CanvasGroup cg;
    public Animator anim;
    public float multi;
    private Coroutine coroutine;
    private int comboValue = 0;
    public void Show() {
        gameObject.SetActive(true);
        if (coroutine != null)
            StopCoroutine(coroutine);
        cg.alpha = 1.0f;
        comboValue += 1;
        anim.SetTrigger("Shake");
        comboText.text = comboValue.ToString();
        coroutine = StartCoroutine(Hide());
    }

    private IEnumerator Hide() {
        yield return new WaitForSecondsRealtime(1.0f);
        while(true) {
            cg.alpha *= multi;
            if(cg.alpha < 0.01f) {
                gameObject.SetActive(false);
                comboValue = 0;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void ResetCombo() {
        comboValue = 0;
    }
}
