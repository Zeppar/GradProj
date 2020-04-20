using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAttackType {
    NormalAttack1 = 1,
    NormalAttack2,
    NormalAttack3,
    AirAttack1,
    AirAttack2,
    AirAttack3
}

public enum EnemyAttackType {
    NormalAttack1 = 1,
    NormalAttack2
}


public class AttackChecker : MonoBehaviour {
    public PolygonCollider2D[] colliders;
    public float[] colliderHideTimes;
    private bool isPlayer;
    private Enemy enemy;
    private bool attackDetected;

    private void Start() {
        for (int i = 0; i < colliders.Length; i++) {
            colliders[i].enabled = false;
        }
    }

    public void CheckAttack(PlayerAttackType type) {
        isPlayer = true;
        int idx = (int)type;
        colliders[idx - 1].enabled = true;
        attackDetected = true;
        StartCoroutine(HideCollider(idx - 1));
    }

    public void CheckAttack(EnemyAttackType type, Enemy _enemy) {
        isPlayer = false;
        enemy = _enemy;
        int idx = (int)type;
        colliders[idx - 1].enabled = true;
        StartCoroutine(HideCollider(idx - 1));
    }

    private IEnumerator HideCollider(int idx) {
        yield return new WaitForSecondsRealtime(colliderHideTimes[idx]);
        colliders[idx].enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag(Util.TagCollection.enemyTag)) {
            //if(attackDetected) {
            //    attackDetected = false;
            //    GameManager.instance.effectManager.ShowHitEffect();
            //}
            collision.GetComponent<Enemy>().BeAttacked(GameManager.instance.player.attack);
        }
        if (collision.gameObject.CompareTag(Util.TagCollection.playerTag)) {
            UIManager.instance.screenEffect.Show();
            collision.GetComponent<Player>().BeAttackedAndBeatBack(enemy.dir, 5, 7, enemy.attack); 
        }
    }
}