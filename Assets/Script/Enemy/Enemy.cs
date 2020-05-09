using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public enum EnemyType {
    OnGround = 0,
    InSky
}
public class Enemy : MonoBehaviour {
    public int id;
    public int level = 1;

    private EnemyInfo enemyInfo;

    [Header("基础属性")]
    private int maxHP;
    private int _HP;
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public int dir = -1;
    [HideInInspector]
    public int attack = 7;
    [HideInInspector]
    public float scaleMulti;
    public int HP {
        get { return _HP; }
        set {
            _HP = Mathf.Clamp(value, 0, maxHP);
            if (_HP <= 0) {
                Die();
            }
        }
    }

    
    private bool dead = false;
    private bool isAttacking = false;
    private bool isHurt = false;

    [HideInInspector]
    public EnemyType type;

    [Header("巡逻属性")]
    public Slider hpSlider;
    public AttackChecker attackChecker;
    public Transform attackPoint;

    [HideInInspector]
    public bool hasSlider = true;
    [HideInInspector]
    public float chaseDis = 2;
    [HideInInspector]
    public float attackRange = 1;
    [HideInInspector]
    public float attackInterval;
    [HideInInspector]
    public float originInterval;
    [HideInInspector]
    public float lastAttackTime = 0;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public Rigidbody2D rb;
    


    public virtual void Start() {
        ParseEnemyInfo();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (hasSlider)
            hpSlider.value = 1.0f;
        scaleMulti = Mathf.Abs(transform.localScale.x);
    }

    private void ParseEnemyInfo() {
        enemyInfo = GameManager.instance.enemyManager.GetEnemyInfoWithID(id);

        maxHP = enemyInfo.GetHPByLevel(level);
        attack = enemyInfo.GetAttackByLevel(level);
        speed = enemyInfo.speed;
        attackInterval = enemyInfo.attackInterval;
        chaseDis = enemyInfo.chaseDis;
        attackRange = enemyInfo.attackRange;
        hasSlider = enemyInfo.hasSlider;
        type = enemyInfo.type;
        HP = maxHP;
    }

    public virtual void Begin() {

    }

    private void Update() {
        hpSlider.transform.localScale = new Vector2(-dir, 1);
        if (dead)
            return;
        UpdateState();
        if (isHurt)
            return;
        if (isAttacking)
            return;
        BehaviourOperation();
    }


    public virtual void UpdateState() {
        if (hasSlider)
            hpSlider.transform.localScale = new Vector2(-dir, 1.0f);

    }

    public virtual bool ShouldChase() {
        return Vector2.Distance(GameManager.instance.player.transform.position, transform.position) < chaseDis;
    }

    public virtual bool CanAttack() {
        return true;
    }

    public virtual void BehaviourOperation() {
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
        if (HP > 0) {
            isHurt = true;
            UpdateHP(IntCount);
            ResetAttackState();
            Invoke("ResetHurtState", 0.2f);
        }
    }

    private void UpdateHP(int IntCount) {
        var hpMins = UnityEngine.Random.Range(IntCount - 2, IntCount + 2); ;
        HP -= hpMins;
        if (hasSlider)
            hpSlider.value = (float)HP / maxHP;
        UIManager.instance.ShowHPUI(this, hpMins);
    }

    public virtual void Seek() {

    }

    public virtual void Chase() {

    }

    public virtual void Die() {
        dead = true;
        int skillID = UnityEngine.Random.Range(0, GameManager.instance.skillManager.skillDict.Count);
        GameManager.instance.skillStoneCreator.CreateSkillStone(skillID, transform.position);
        GameManager.instance.skillParticleCreator.CreateEffect(transform.position, Util.ObjectItemNameCollection.enemyDie);
    }


    public virtual void ResetAttackState() {
        StartCoroutine(ResetAttackFlag());
    }

    IEnumerator ResetAttackFlag() {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        isAttacking = false;
    }

    public void ResetHurtState() {
        isHurt = false;
    }

    public void AddVelocity(Vector2 vec) {
        rb.velocity = vec;
    }

    public void BeAttackedAndBeatBack(float xForce, float yForce, int attack) {
        isHurt = true;
        AddVelocity(new Vector2(dir * xForce, yForce));
        UpdateHP(attack);
        anim.SetTrigger("BeatBack");
        Invoke("ResetHurtState", 1.0f);
        ResetAttackState();
    }
}
