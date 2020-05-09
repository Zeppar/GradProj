﻿using System;
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
    private EnemyInfo enemyinfo;
    //玩家属性
    [Header("基础属性")]
    public int maxHP;
    private int _HP;
    public float speed;
    public int dir = -1;
    public int attack = 7;
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

    public EnemyType type;
    private bool dead = false;
    private bool isAttacking = false;
    private bool isHurt = false;


    [Header("巡逻属性")]
    //巡逻属性
    public float chaseDis = 2;
    public float attackRange = 1;

    public bool hasSlider = true;
    public Slider hpSlider;
    public AttackChecker attackChecker;

    public float attackInterval;
    public float originInterval;
    [HideInInspector]
    public float lastAttackTime = 0;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public Rigidbody2D rb;
    public Transform attackPoint;


    public virtual void Start() {

        maxHP = GameManager.instance.enemyManager.GetHPInfo(id,level);
        attack = GameManager.instance.enemyManager.GetAttackInfo(id,level);

        enemyinfo = GameManager.instance.enemyManager.FindEnemyWithID(id);
        speed = enemyinfo.speed;
        attackInterval = enemyinfo.attackInterval;
      

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        HP = maxHP;
        if (hasSlider)
            hpSlider.value = 1.0f;
        scaleMulti = Mathf.Abs(transform.localScale.x);
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
