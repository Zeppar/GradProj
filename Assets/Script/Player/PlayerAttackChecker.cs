using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType {
    NormalAttack1 = 0,
    NormalAttack2,
    NormalAttack3,
    AirAttack1,
    AirAttack2,
    AirAttack3,
    AirAttack4
}

public class PlayerAttackChecker : MonoBehaviour
{
    public BoxCollider2D[] colliders;
    public float[] colliderHideTimes;

    private void Start() {
        for(int i = 0; i < colliders.Length; i++) {
            colliders[i].enabled = false;
        }
    }

    public void CheckAttack(AttackType type) {
        int idx = (int)type;
        colliders[idx].enabled = true;
        StartCoroutine(HideCollider(idx));
    }

    private IEnumerator HideCollider(int idx) {
        yield return new WaitForSecondsRealtime(colliderHideTimes[idx]);
        colliders[idx].enabled = false;
    }
}
