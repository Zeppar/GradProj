using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitManger : MonoBehaviour
{
    public float timeScale;
    public float duration;
    private bool isShow;

    public void ShowHit() {
        if (isShow)
            return;
        isShow = true;
        Time.timeScale = timeScale;
        StartCoroutine(Reset());
    }

    IEnumerator Reset() {
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
        isShow = false;
    }
}
