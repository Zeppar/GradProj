using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : EnemyGround
{
    private int attackCount = 0;
    private int attackType = 1;

    public override void Attack() {
        if (Time.time - lastAttackTime < attackInterval) {
            return;
        }
        base.Attack();
        // choose attack
        SetAttackCount(attackType);
        attackType = Random.Range(1, 3);
        //Debug.Log("set : " + attackType);
        lastAttackTime = Time.time;
        attackInterval = Random.Range(originInterval * 0.9f, originInterval * 1.1f);
    }

    public override bool ShouldChase() {
        return base.ShouldChase();
    }

    public override void CheckAttackPlayer() {
        base.CheckAttackPlayer();
        attackChecker.CheckAttack((EnemyAttackType)attackType, this);
    }

    public override bool CanAttack() {
        if (attackType == 3)
            return true;
        float yDiff = Mathf.Abs(GameManager.instance.player.transform.position.y - transform.position.y);
        float xDiff = Mathf.Abs(GameManager.instance.player.transform.position.x - transform.position.x);
        return Vector2.Distance(GameManager.instance.player.transform.position, transform.position) <= attackRange
            || (yDiff > 1.0f && yDiff < 5.0f && xDiff < 1.0f);
    }

    public override void BeAttacked(int IntCount) {
        if (HP > 0) {
            base.BeAttacked(IntCount);
            // do nothing
        }
    }

    private void SetAttackCount(int val) {
        attackCount = val;
        anim.SetInteger("Attack", attackCount);
    }

    public override void ResetAttackState() {
        SetAttackCount(0);
        base.ResetAttackState();
    }

    public void Defend() {

    }
    
}
