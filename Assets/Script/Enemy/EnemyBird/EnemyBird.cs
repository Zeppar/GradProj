using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBird : Enemy {
    [Header("敌人属性")]
    public float birdHight;
    public float birdFly;
    public GameObject Fireball;
    public int dir = 1;

    [Header("攻击属性")]
    public float attackInterval;
    private float lastAttackTime = 0;
    public Transform AttackPoint;
    public override void Seek() {
        //转头

        transform.GetComponent<SpriteRenderer>().color = Color.white;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(dir * -0.5f, -1), birdHight, LayerMask.GetMask("Ground"));

        if (hit.collider != null) {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime * dir, transform.position.y);
        } else//飞行过高降低
          {
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
        }
        //飞行过升高
        RaycastHit2D hight_hit = Physics2D.Raycast(transform.position, new Vector2(-0.5f * dir, -1), birdFly, LayerMask.GetMask("Ground"));
        if (hight_hit.collider != null) {
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
        }
        RaycastHit2D move_hit = Physics2D.Raycast(transform.localPosition, new Vector2(dir, 0), 5, LayerMask.GetMask("Ground"));
        if (move_hit.collider != null) {
            dir = -dir;
            transform.localScale = new Vector2(dir * 7, transform.localScale.y);
            print("砖头");
        }
    }

    public override bool ShouldChase() {
        return true;
    }

    public override void Chase() {

        transform.GetComponent<SpriteRenderer>().color = Color.red;
        //转向
        if (transform.position.x >= GameManger.instance.player.transform.position.x) {
            dir = -1;

        } else {
            dir = 1;
        }
        transform.localScale = new Vector2(dir * 7, transform.localScale.y);

        //高度
        if (transform.position.y > GameManger.instance.player.transform.position.y) {

            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
        } else {
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
        }

        Attack();
    }
    public override void Attack() {
        if (Time.time - lastAttackTime < attackInterval) {
            return;
        }
        base.Attack();
        GameObject fireball = Instantiate(Fireball);
        fireball.transform.position = AttackPoint.position;
        fireball.GetComponent<EnemyBirdFireBall>().vector2_dir = new Vector2(dir, 0);
        fireball.GetComponent<EnemyBirdFireBall>().attack = attack;
        lastAttackTime = Time.time;
        ResetAttackState();
    }
    public override void BeAttacked(int IntCount) {

        base.BeAttacked(IntCount);
        isHurt = false;
    }
    public override void Die() {
        base.Die();
        Destroy(gameObject);
    }
}
