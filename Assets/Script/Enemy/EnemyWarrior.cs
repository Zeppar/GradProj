using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyWarrior : EnemyGround {

    public override void Attack() {
        if (Time.time - lastAttackTime < attackInterval) {
            return;
        }
        base.Attack();
        anim.SetTrigger("Attack");
        lastAttackTime = Time.time;
    }

    public override void CheckAttackPlayer() {
        base.CheckAttackPlayer();
        attackChecker.CheckAttack(EnemyAttackType.NormalAttack1, this);
    }

    public override bool CanAttack() {
        float yDiff = Mathf.Abs(GameManager.instance.player.transform.position.y - transform.position.y);
        float xDiff = Mathf.Abs(GameManager.instance.player.transform.position.x - transform.position.x);
        return (yDiff < 1.0f
            && Vector2.Distance(GameManager.instance.player.transform.position, transform.position) <= attackRange)
            || (yDiff > 1.0f && yDiff < 3.0f && xDiff < 1.0f);
    }

    public override void BeAttacked(int IntCount) {
        if (HP > 0) {
            base.BeAttacked(IntCount);
            //iTween.MoveBy(GameManager.instance.player.gameObject, iTween.Hash("x", dir * 2, "y", 1, "looktime", 0.5f));
            AddVelocity(new Vector2(GameManager.instance.player.dir * 3, 1f));
            anim.SetTrigger("Hurt");
            lastAttackTime = Time.time;//打断攻击
        }
    }
}
