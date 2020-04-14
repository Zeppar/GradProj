using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGin : MonoBehaviour {
    public Transform startTrans;
    public float distance;
    public LineRenderer lineRenderer;
    public Gradient attackColor;
    public Gradient normalColor;

    public Transform leftPos;
    public Transform rightPos;
    public float speed;

    private Vector2 targetPos;

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
        RaycastHit2D hit = Physics2D.Raycast(startTrans.position, -transform.up, distance);
        if (hit.collider != null) {
            Debug.Log(hit.collider.name);
            if (hit.collider.CompareTag(Util.TagCollection.groundTag)) {
                //Debug.DrawLine(startTrans.position, hit.point, Color.blue);
                lineRenderer.SetPosition(1, hit.point);
                lineRenderer.colorGradient = normalColor;
            } else if (hit.collider.tag == Util.TagCollection.playerTag) {
                //Debug.DrawLine(startTrans.position, hit.point, Color.red);
                lineRenderer.SetPosition(1, hit.point);
                lineRenderer.colorGradient = attackColor;
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
