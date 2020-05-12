using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rotater : MonoBehaviour
{
    public float speed = 30;
    public float startZ;
    public float endZ;
    public bool isLerp;
    private bool moveToStart = true;


    // Update is called once per frame
    private void FixedUpdate() {
        if (isLerp) {
            Quaternion target = moveToStart ? Quaternion.Euler(new Vector3(0, 0, startZ)) : Quaternion.Euler(new Vector3(0, 0, endZ));
            transform.localRotation = Quaternion.Lerp(transform.localRotation, target, speed * Time.fixedDeltaTime);
            if (Mathf.Abs(target.eulerAngles.z - transform.localRotation.eulerAngles.z) < 5f) {
                moveToStart = !moveToStart;
            }
        } else {
            Quaternion target = moveToStart ? Quaternion.Euler(new Vector3(0, 0, startZ)) : Quaternion.Euler(new Vector3(0, 0, endZ));
            int dir = moveToStart ? 1 : -1;
            transform.Rotate(0, 0, speed * dir);
            if (Mathf.Abs(target.eulerAngles.z - transform.localRotation.eulerAngles.z) < 5f) {
                moveToStart = !moveToStart;
            }
        }
    }
}
