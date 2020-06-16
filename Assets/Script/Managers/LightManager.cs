using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class LightManager : MonoBehaviour
{
    public Light2D globalLight;

    public void Start() {
        StartCoroutine(DetectLightEnergy());
    }
    private IEnumerator DetectLightEnergy() {
        while (true)
        {
            if (GameManager.instance.player != null)
            {

                yield return new WaitForSecondsRealtime(1.0f);
                //  计算公式  0 - 100   全局光照  0.15 - 0.5   自身光照  pointLightOuterRadius  3 - 8
                globalLight.intensity = GameManager.instance.levelManager.currentInfo.minLight + GameManager.instance.energyManager.GetPercentValue() * GameManager.instance.levelManager.currentInfo.maxAddLight;
                GameManager.instance.player.selfLight.pointLightOuterRadius = 3.0f + GameManager.instance.energyManager.GetPercentValue() * 5.0f;
            }
        }
    }
}
