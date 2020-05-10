using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingWheel : MonoBehaviour
{
    public SpriteRenderer wheelSr;
    public SpriteRenderer signalSr;
    public Sprite[] wheelSignalSps;
    public TextMesh showText;

    private float totalTime;
    private int signalIndex = 0;
    private Util.NoParmsCallBack successCallback;

    private void Hide() {
        wheelSr.gameObject.SetActiveFast(false);
        signalSr.gameObject.SetActiveFast(false);
        showText.gameObject.SetActiveFast(false);
    }

    private void Start() {
        Hide();
    }

    public void Stop() {
        StopCoroutine("ChangeSignal");
        Hide();
    }

    public void StartSaving(float time = 1.0f, Util.NoParmsCallBack callback = null) {
        wheelSr.gameObject.SetActiveFast(true);
        signalSr.gameObject.SetActiveFast(true);
        showText.gameObject.SetActiveFast(true);
        signalSr.sprite = wheelSignalSps[0];
        signalIndex = 1;
        totalTime = time;
        showText.text = "正在存档.";
        successCallback = callback;
        StopCoroutine("ChangeSignal");
        StartCoroutine("ChangeSignal");
    }

    public IEnumerator ChangeSignal() {
        while(true) {
            yield return new WaitForSecondsRealtime(totalTime / 3.0f);
            if (signalIndex == 3) {
                showText.text = "存档成功";
                successCallback?.Invoke();
                yield return new WaitForSecondsRealtime(1f);
                Hide();
                break;
            } else {
                showText.text += ".";
                signalSr.sprite = wheelSignalSps[signalIndex++];
            }
        }
    }
}
