using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyWarrior : Enemy {
    //射线属性
    [Header("射线属性")]
    private ContactFilter2D contactFilter;
    private RaycastHit2D[] resultArr = new RaycastHit2D[16];

    //攻击
    [Header("攻击属性")]
    public float attackInterval;
    private float lastAttackTime = 0;
    public Transform attackPoint;
    public float checkAttackRange;//攻击范围

    //敌人属性
   // public int attack = 10;

    [Header("地面监测")]
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public bool isGrounded =true;

    public override void Begin() {
        contactFilter.useTriggers = false;
        contactFilter.useLayerMask = true;
        contactFilter.SetLayerMask(LayerMask.GetMask(Util.LayerCollection.groundLayer));
    }

    public override void Attack() {
        if (Time.time - lastAttackTime < attackInterval) {
            return;
        }
        base.Attack();
        anim.SetTrigger("Attack");
        lastAttackTime = Time.time;
    }

    public override bool CanAttack() {
        float yDiff = Mathf.Abs(GameManager.instance.player.transform.position.y - transform.position.y);
        float xDiff = Mathf.Abs(GameManager.instance.player.transform.position.x - transform.position.x);
        return (yDiff < 1.0f
            && Vector2.Distance(GameManager.instance.player.transform.position, transform.position) <= attackRange)
            || (yDiff > 1.0f && yDiff < 3.0f && xDiff < 1.0f);
    }

    public void CheckAttackPlayer() {
        AddVelocity(new Vector2(dir * 0.8f, 0));
        attackChecker.CheckAttack(WarriorAttackType.NormalAttack, this);
    }

    public override void BeAttacked(int IntCount) {
        if (HP > 0) {
            base.BeAttacked(IntCount);
            iTween.MoveBy(GameManager.instance.player.gameObject, iTween.Hash("x", dir * 2, "y", 1, "looktime", 0.5f));
            anim.SetTrigger("Hurt");
            lastAttackTime = Time.time;//打断攻击
        }
    }

    public override void Seek() {//巡逻
        
        anim.SetBool("Walk", true);
        int count = rb.Cast(new Vector2(dir * 5, 0), contactFilter, resultArr, 5 + 0.01f);

        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(dir * 5, 0), new Vector2(0, -1), 2);

        if (count > 0 || hit.collider == null) {
            dir *= -1;
        }
        transform.localScale = new Vector2(dir * 10, transform.localScale.y);
        transform.position = new Vector2(transform.position.x + speed * Time.deltaTime * dir, transform.position.y);
    }

    public override void Chase() {//追逐
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);//同步是否在地面
        if (!isGrounded)
        {        return; }
        anim.SetBool("Walk", true);
        if(GameManager.instance.player.transform.position.x > transform.position.x + 0.5f) {
            dir = 1;
        } else if(GameManager.instance.player.transform.position.x < transform.position.x - 0.5f) {
            dir = -1;
        }
        transform.position = new Vector2(transform.position.x + speed * Time.deltaTime * dir, transform.position.y);
        transform.localScale = new Vector2(dir * 10, transform.localScale.y);
    }

    public override void Die() {
        base.Die();
        anim.SetBool("Dead", true);
    }

    public void DestorySelf() {
        Destroy(gameObject);
    }
}
