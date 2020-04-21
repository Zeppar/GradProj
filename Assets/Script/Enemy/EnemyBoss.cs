using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : EnemyGround
{
    enum BossAttackType {
        Normal = 1,
        FireBall,
        FireBallRain,
        Charge
    }

    private BossAttackType attackType = BossAttackType.FireBall;

    public override void Attack() {
        if (Time.time - lastAttackTime < attackInterval) {
            return;
        }
        base.Attack();
        // choose attack
        SetAttackType(attackType);
        attackType = (BossAttackType)Random.Range(1, 3);
        lastAttackTime = Time.time;
        attackInterval = Random.Range(originInterval * 0.9f, originInterval * 1.1f);
    }

    public override bool ShouldChase() {
        if (attackType == BossAttackType.FireBall || attackType == BossAttackType.FireBallRain)
            return true;
        if (attackType == BossAttackType.Normal && Time.time - lastAttackTime > attackInterval * 2.0f) {
            attackType = BossAttackType.FireBall;
            return true;
        }
        return base.ShouldChase();
    }

    // fixed Update 和 update 的帧率差异
    public override void CheckAttackPlayer() {
        if (attackType != BossAttackType.Normal)
            return;
        base.CheckAttackPlayer();
        attackChecker.CheckAttack((int)attackType, this);
    }

    public override bool CanAttack() {
        if (attackType == BossAttackType.FireBall || attackType == BossAttackType.FireBallRain)
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

    private void SetAttackType(BossAttackType val) {
        anim.SetInteger("Attack", (int)val);
        switch (attackType) {
            case BossAttackType.Normal:
                AddVelocity(new Vector2(dir * 0.6f, 0));
                break;
            case BossAttackType.FireBall:
                GameManager.instance.StartCoroutine(Util.DelayExecute(0.1f, () => {
                    GameManager.instance.skillParticleCreator.CreateFireball(attackPoint.position, new Vector2(dir, 0), 0.25f, attack, Util.FireBallType.Boss);
                }));
                break;
            case BossAttackType.FireBallRain:
                break;
            case BossAttackType.Charge:
                break;
        }
    }

    public override void ResetAttackState() {
        base.ResetAttackState();
        anim.SetInteger("Attack", 0);
    }

    public void Defend() {

    }
    
}
