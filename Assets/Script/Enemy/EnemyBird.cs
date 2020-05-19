using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBird : Enemy {

    public float fallSpeed;
    public float chaseHeight;

    // 飞行位置
    public Transform leftPos;
    public Transform rightPos;
    private Vector2 targetPos;

    public RaycastHit2D[] resultArr = new RaycastHit2D[16];
    public ContactFilter2D contactFilter;
    public override void Start() {
        base.Start();
        targetPos = FindRandomPosition();
        contactFilter.SetLayerMask(LayerMask.GetMask(Util.LayerCollection.groundLayer));
    }

    private Vector2 FindRandomPosition() {
        Vector2 pos = new Vector2(Random.Range(leftPos.position.x, rightPos.position.x), Random.Range(leftPos.position.y, rightPos.position.y));
        return pos;
    }

    public override void Seek() {
        if (targetPos.x > transform.position.x && dir < 0) {
            dir = 1;
        } else if (targetPos.x < transform.position.x && dir > 0) {
            dir = -1;
        }
        transform.localScale = new Vector2(dir * scaleMulti, transform.localScale.y);

        if (Vector2.Distance(transform.position, targetPos) < 0.1f) {
            targetPos = FindRandomPosition();
            transform.localScale = new Vector2(dir * scaleMulti, transform.localScale.y);
        } else {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }

    }

    public override bool CanAttack() {
        if (Mathf.Abs(transform.position.y - GameManager.instance.player.transform.position.y) <= chaseHeight
            && Vector2.Distance(GameManager.instance.player.transform.position, transform.position) <= attackRange) {
            return true;
        }
        return false;
    }
    public override bool ShouldChase()
    {

        RaycastHit2D hit  =  Physics2D.Raycast(transform.position, GameManager.instance.player.transform.position - transform.position, Vector2.Distance(transform.position,GameManager.instance.player.transform.position),LayerMask.GetMask("Ground"));
        
        if (hit.transform == null && base.ShouldChase())//TODO
        {
            print("true");
            return true;
        }
        else
        {
            print("flase");
            return false;
        }
 

    }

    private float tempX, tempY;
    public override void Chase() {
        if (transform.position.x >= GameManager.instance.player.transform.position.x) {
            dir = -1;
        } else {
            dir = 1;
        }
        transform.localScale = new Vector2(dir * scaleMulti, transform.localScale.y);

        if(transform.position.x > GameManager.instance.player.transform.position.x) {
            tempX = transform.position.x - speed * Time.deltaTime;
        } else {
            tempX = transform.position.x + speed * Time.deltaTime;
        }

        if (transform.position.y > GameManager.instance.player.transform.position.y + chaseHeight) {
            tempY = transform.position.y - fallSpeed * Time.deltaTime;
        } else if(transform.position.y < GameManager.instance.player.transform.position.y - chaseHeight) {
            tempY = transform.position.y + fallSpeed * Time.deltaTime;
        } else {
            tempY = transform.position.y;
        }

        transform.position = new Vector2(tempX, tempY);
    }

    public override void BeAttacked(int IntCount) {
        if (HP > 0) {
            base.BeAttacked(IntCount);
            anim.SetTrigger("Hurt");
            lastAttackTime = Time.time;//打断攻击
        }
    }

    public override void Attack() {
        if (Time.time - lastAttackTime < attackInterval) {
            return;
        }
        base.Attack();
        anim.SetTrigger("Attack");
        lastAttackTime = Time.time;
        GameManager.instance.skillParticleCreator.CreateFireball(attackPoint.position, new Vector2(dir, 0), 0.15f, attack, Util.FireBallType.Enemy);
        attackInterval = Random.Range(originInterval * 0.9f, originInterval * 1.1f);
    }


    public override void Die() {
        base.Die();
        Destroy(gameObject);
    }

}
