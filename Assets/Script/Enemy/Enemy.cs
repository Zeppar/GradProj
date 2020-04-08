using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyType {
    OnGround = 0,
    InSky
}
public class Enemy : MonoBehaviour {
    //玩家属性
    [Header("基础属性")]
    public int maxHP;
    private int _HP;
    public int speed;
    public int dir = -1;
    public int attack = 7;
    public int HP {
        get { return _HP; }
        set {
            _HP = Mathf.Clamp(value, 0, maxHP);
            if (_HP <= 0) {
                Die();
            }
        }
    }

    public Animator anim;
    public Rigidbody2D rb;
    public EnemyType type;

    public bool dead = false;
    private bool isAttacking = false;
    private bool isHurt = false;


    [Header("巡逻属性")]
    //巡逻属性
    public float chaseDis = 2;
    public int attackRange = 1;

    public int skillID;
    public Slider hpSlider;
    public AttackChecker attackChecker;

    public virtual void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        HP = maxHP;
        hpSlider.value = 1.0f;
        Begin();

    }
    public virtual void Begin() {

    }

    private void FixedUpdate() {
        if (dead)
            return;
        UpdateState();
        if (isHurt)
            return;
        if (isAttacking)
            return;
        BehaviourOperation();
    }

    private void UpdateState() {
        hpSlider.transform.localScale = new Vector2(-dir, 1.0f);
    }

    private bool ShouldChase() {
        return Vector2.Distance(GameManager.instance.player.transform.position, transform.position) < chaseDis;
    }

    public virtual bool CanAttack() {
        return true;
    }

    private void BehaviourOperation() {
        if (GameManager.instance.player != null && ShouldChase()) {
            if (CanAttack()) {
                if (type == EnemyType.OnGround) {
                    anim.SetBool("Walk", false);
                }
                Attack();
            } else {
                if (type == EnemyType.OnGround) {
                    anim.SetBool("Walk", true);
                }
                Chase();
            }
        } else {
            if (type == EnemyType.OnGround) {
                anim.SetBool("Walk", true);
            }
            Seek();
        }
    }

    public virtual void Attack() {
        isAttacking = true;
    }

    public virtual void BeAttacked(int IntCount) {
        if (HP > 0)
        {
            isHurt = true;
            HP -= IntCount;
            hpSlider.value = (float)HP / maxHP; 
            ResetAttackState();
        }
    }

    public virtual void Seek() {

    }

    public virtual void Chase() {

    }

    public virtual void Die() {
        dead = true;
        GameManager.instance.skillStoneCreator.CreateSkillStone(skillID, transform.position);
    }


    public void ResetAttackState() {
        isAttacking = false;
    }

    public void ResetHurtState() {
        isHurt = false;
    }

    public void AddVelocity(Vector2 vec) {
        rb.velocity = vec;
    }

    public void BeAttackedAndBeatBack(float xForce, float yForce) {
        isHurt = true;
        AddVelocity(new Vector2(dir * xForce, yForce));
        anim.SetTrigger("BeatBack");
        Invoke("ResetHurtState", 1.0f);
    }
}
