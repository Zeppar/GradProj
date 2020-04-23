using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemLightEffect : MonoBehaviour
{
    private float time = 0;
    private void OnEnable() {
        time = 0;
    }

    private void Update() {
        time += 0.02f;
        var targetPos = Camera.main.ScreenToWorldPoint(UIManager.instance.energyPanel.lightTransform.position);
        Vector2 midPos = (targetPos + transform.position) * 0.5f;
        var curPos = GetBezierPoint(transform.position, midPos, targetPos, time);
        transform.position = curPos;
        if (time > 0.9f) {
            ObjectPool.instance.ReturnToPool(gameObject);
            GameManager.instance.energyManager.StartIncreate(20.0f);
        }
    }

    // 获取贝塞尔曲线坐标点
    private Vector2 GetBezierPoint(Vector2 p0, Vector2 p1, Vector2 p2, float t) {
        return (1 - t) * (1 - t) * p0
            + 2 * t * (1 - t) * p1
            + t * t * p2;
    }


}
