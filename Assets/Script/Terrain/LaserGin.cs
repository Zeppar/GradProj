using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LaserGin : MonoBehaviour {
    public Transform startTrans;
    public float distance;
    public LineRenderer lineRenderer;
    public Gradient attackLineColor;
    public Gradient normalLineColor;

    public Transform leftPos;
    public Transform rightPos;
    public float speed;
    public Light2D topLight;
    public Light2D detectLight;
    public Color attackLightColor;
    public Color normalLightColor;

    public ContactFilter2D contaceFilter2D;
    private Vector2 targetPos;
    private RaycastHit2D[] raycastResults = new RaycastHit2D[1];

    public void Start() {
        targetPos = FindRandomPosition();
    }

    private Vector2 FindRandomPosition() {
        Vector2 pos = new Vector2(Random.Range(leftPos.position.x, rightPos.position.x), Random.Range(leftPos.position.y, rightPos.position.y));
        return pos;
    }

    // Update is called once per frame
    private void Update() {
        LaserDetect();
    }

    private void FixedUpdate() {
        Move();
    }

    private void LaserDetect() {
        int count = Physics2D.Raycast(startTrans.position, (-transform.up), contaceFilter2D, raycastResults, distance);
        if (count > 0) {
            var hit = raycastResults[0];
            if (hit.transform.CompareTag(Util.TagCollection.groundTag)) {
                //Debug.DrawLine(startTrans.position, hit.point, Color.blue);
                lineRenderer.SetPosition(1, hit.point);
                lineRenderer.colorGradient = normalLineColor;
                topLight.color = normalLightColor;
                detectLight.color = normalLightColor;
            } else if (hit.transform.CompareTag(Util.TagCollection.playerTag)) {
                //Debug.DrawLine(startTrans.position, hit.point, Color.red);
                lineRenderer.SetPosition(1, hit.point);
                lineRenderer.colorGradient = attackLineColor;
                topLight.color = attackLightColor;
                detectLight.color = attackLightColor;
            }
            lineRenderer.SetPosition(0, startTrans.position);
        }
    }

    private void Move() {
        if (Vector2.Distance(transform.position, targetPos) < 0.1f) {
            targetPos = FindRandomPosition();
        } else {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }

    }
}
