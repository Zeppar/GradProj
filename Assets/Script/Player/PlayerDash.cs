using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float removeTime;
    public float alphaMulti;

    private SpriteRenderer sr;
    private float dashStartTime;

    private void OnEnable() {
        dashStartTime = Time.time;
        if(sr == null)
            sr = GetComponent<SpriteRenderer>();
        sr.sprite = GameManager.instance.player.GetComponent<SpriteRenderer>().sprite;
        sr.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - dashStartTime > removeTime) {
            ObjectPool.instance.ReturnToPool(this);
            return;
        }
        float alpha = sr.color.a * alphaMulti;
        sr.color = new Color(1.0f, 1.0f, 1.0f, alpha);
    }
}
