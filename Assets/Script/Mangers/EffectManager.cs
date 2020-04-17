using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EffectManager : MonoBehaviour
{
    public float timeScale;
    public float duration;
    public CinemachineCollisionImpulseSource cinemaInpulse;
    private bool isShow;

    public void ShowHitEffect() {
        if (isShow)
            return;
        isShow = true;
        Time.timeScale = timeScale;
        StartCoroutine(Reset());
    }

    public void ShakeCamera() {
        cinemaInpulse.GenerateImpulse();
    }

    IEnumerator Reset() {
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
        isShow = false;
    }
}
