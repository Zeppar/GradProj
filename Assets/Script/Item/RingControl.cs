﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingControl : MonoBehaviour
{
    public SpriteRenderer arrow;
    public SpriteRenderer ring;
    public Animator anim;
    private float timer;
    private float involerTimer = 0.5f;

    private void Update() {

        if (GameManager.instance.player.dir == 1) {
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        } else if (GameManager.instance.player.dir == -1) {
            transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }

        if (MagicElf.instance.curType != ElfType.StrengthMovement)
            return;


        if (/*Input.GetMouseButtonDown(1)*/Input.GetKeyDown(KeyCode.P)) {
            ring.enabled = true;
            arrow.enabled = true;
            anim.SetBool("show", true);
        } else if(/*Input.GetMouseButtonUp(1)*/Input.GetKeyUp(KeyCode.P)) {
            ring.enabled = true;
            arrow.enabled = true;
            anim.SetBool("show", false);
            if(timer >= involerTimer) {
                MagicElf.instance.StartOperation(new Vector2(2.5f, 1.5f));
            }
            timer = 0;
        } else if(/*Input.GetMouseButton(1)*/Input.GetKey(KeyCode.P)) {
            timer += Time.deltaTime;
            Vector3 screenPos = Camera.main.WorldToScreenPoint(arrow.transform.position);
            float xDiff = Input.mousePosition.x - screenPos.x;
            float yDiff = Input.mousePosition.y - screenPos.y;
            float tanVal = yDiff / xDiff;
            float addAngle = 0;
            if(xDiff < 0) {
                if (tanVal > 0)
                    addAngle = -Mathf.PI;
                else
                    addAngle = Mathf.PI;
            }
            float angle = (Mathf.Atan(tanVal) + addAngle) / Mathf.PI * 180;
            ring.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        } else {
            ring.enabled = false;
            arrow.enabled = false;
        }
    }
}
