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
    private bool inStandSkill = false;

    public override void Attack() {
        if (Time.time - lastAttackTime < attackInterval) {
            return;
        }
        base.Attack();
        // choose attack
        SetAttackType(attackType);
        attackType = (BossAttackType)Random.Range(3, 4);
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
                inStandSkill = true;
                StartCoroutine(FireBallRain());
                break;
            case BossAttackType.Charge:
                break;
        }
    }

    private RaycastHit2D[] results = new RaycastHit2D[1];
    private IEnumerator FireBallRain() {
        for(int i = 0; i < 5; i++) {
            int count = Physics2D.Raycast(GameManager.instance.player.transform.position, Vector2.down, contactFilter, results);
            if(count > 0) {
                GameManager.instance.skillParticleCreator.CreateEffect(results[0].point, Util.ObjectItemNameCollection.bossFireBallRain);
                yield return new WaitForSecondsRealtime(0.4f);
                GameManager.instance.skillParticleCreator.CreateFireball(results[0].point + new Vector2(0, 20.0f), Vector2.down, 0.05f, attack, Util.FireBallType.Boss);
                yield return new WaitForSecondsRealtime(0.3f);
            } else {
                yield return new WaitForSecondsRealtime(0.1f);
            }   
        }
        inStandSkill = false;
        ResetAttackState();
    }

    public override void BehaviourOperation() {
        if (inStandSkill)
            return;
        base.BehaviourOperation();
    }

    public override void ResetAttackState() {
        base.ResetAttackState();
        anim.SetInteger("Attack", 0);
    }

    public void Defend() {

    }
    
}
