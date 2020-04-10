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
        contactFilter.SetLayerMask(LayerMask.GetMask("Ground"));
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
        return Vector2.Distance(GameManager.instance.player.transform.position, transform.position) <= attackRange;
    }

    public void CheckAttackPlayer() {
        Collider2D[] collArr = Physics2D.OverlapBoxAll(attackPoint.position, new Vector2(checkAttackRange, checkAttackRange), 0);
        if (collArr.Length == 0) { return; }

        foreach (var coll in collArr) {
            if (coll.CompareTag(Util.TagCollection.playerTag)) {
                GameManager.instance.player.BeAttacked(10);
                iTween.MoveBy(GameManager.instance.player.gameObject, iTween.Hash("x", dir * 2, "y", 1, "looktime", 0.5f));
                break;
            }
        }
    }

    public override void BeAttacked(int IntCount) {
        if (HP > 0) {
            base.BeAttacked(IntCount);
            anim.SetTrigger("Hurt");
            lastAttackTime = Time.time;//打断攻击
        }
    }

    public override void Seek() {//巡逻
        
        anim.SetBool("Walk", true);
        int count = rd.Cast(new Vector2(dir * 5, 0), contactFilter, resultArr, 5 + 0.01f);

        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(dir * 5, 0), new Vector2(0, -1), 2);

        if (count > 0 || hit.collider == null) {
            dir *= -1; ;
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
