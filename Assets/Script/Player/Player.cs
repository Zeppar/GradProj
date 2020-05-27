using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public float maxHP;
    public int attack;
    public float range;
    public float attackInterval;
    public bool dead = false;
    public float speed;

    public float dashSpeed;
    private float dashTime;
    public float dashTotalTime;
    private float dashCreateCurTime;
    public float dashCreateTime;
    public float dashCD;
    private float dashCDRemain;

    public float jumpForce;
    public float fallForce;
    public Transform attackPoint;

    public Transform climbPos;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask groundLayer;
    public AttackChecker attackChecker;
    public PlayerAirAttack airAttack;
    public Light2D selfLight;

    public float HP {
        get {
            return _hp;
        }
        set {
            if (_hp > value) {
                SoundManager.instance.PlayerHitEffect();
                UIManager.instance.screenEffect.Show();
            }
            _hp = Mathf.Clamp(value, 0, maxHP);
            if (_hp <= 0) {
                Die();
            }
        }
    }
    private float _hp;

    private int attackCount = 0;
    private AnimatorStateInfo currentState;
    private Animator anim;
    private Rigidbody2D rb;

    private float moveInput;
    private bool isGrounded;
    private bool isJump = false;
    private bool isDash = false;

    private bool jumpPressed = false;

    public bool isHurt = false;
    public int dir = 1;
    public Transform elfPos;
    public BoxCollider2D feetCollider;


    void Start() {
        HP = maxHP;
        anim = GetComponent<Animator>();
        currentState = anim.GetCurrentAnimatorStateInfo(0);
        rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(ChangeEnergy());
        dashCDRemain = 0;
    }

    private IEnumerator ChangeEnergy() {
        while (true) {
            if (GameManager.instance.energyManager.inEnergyInCrease) {
                GameManager.instance.energyManager.IncreaseEnergy(0.5f);
                yield return null;
            } else {
                GameManager.instance.energyManager.ChangeEnergy(GameManager.instance.levelManager.currentInfo.lightDecrease);
                yield return new WaitForSecondsRealtime(GameManager.instance.levelManager.currentInfo.lightDecreaseTime);
            }
        }
    }

    void FixedUpdate() {
        if (!GameManager.instance.gameStart || GameManager.instance.disableInput)
            return;
        UpdateGroundState();
        Move();
        Jump();
        CheckAirAttack();
        Dash();
    }

    void Update() {
        if (!GameManager.instance.gameStart || GameManager.instance.disableInput)
            return;
        UpdateState();
        AirAttack();
        GroundAttack();
        CreateShadow();
        SwitchFall();
    }


    private void CheckAirAttack() {
        airAttack.gameObject.SetActive(attackCount == 6 && !isGrounded);
    }

    private void UpdateGroundState() {
        isGrounded = feetCollider.IsTouchingLayers(LayerMask.GetMask(Util.LayerCollection.groundLayer));
    }

    private void UpdateState() {
        currentState = anim.GetCurrentAnimatorStateInfo(0);
        if ((Input.GetKeyDown(GameManager.instance.keyManager.FindKey(Util.KeyCollection.Jump).keyCode) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded) {
            jumpPressed = true;
        }
        if (Input.GetKeyUp(GameManager.instance.keyManager.FindKey(Util.KeyCollection.Jump).keyCode) || Input.GetKeyUp(KeyCode.UpArrow)) {

            jumpPressed = false;
        }

        if (dashCDRemain > 0) {
            dashCDRemain -= Time.fixedDeltaTime;
            dashCDRemain = Mathf.Max(dashCDRemain, 0);
        }

        if (Input.GetKeyDown(GameManager.instance.keyManager.FindKey(Util.KeyCollection.Dash).keyCode) && !isDash
            && Mathf.Approximately(dashCDRemain, 0) && GameManager.instance.levelManager.currentInfo.dashEnable) {
            isDash = true;
            SoundManager.instance.PlayEffect(Util.ClipNameCollection.dash);
            GameManager.instance.effectManager.ShakeCamera();
        }
    }

    private void SwitchFall() {
        if (currentState.IsName(Util.PlayerAnimCollection.jump) && !isGrounded && rb.velocity.y < -0.5f) {
            anim.SetTrigger("Fall");
        }
    }

    private void CreateShadow() {
        if (Input.GetKeyDown(KeyCode.U)) {
            //GameManager.instance.skillParticleCreator.CreateFireball(attackPoint.position, new Vector2(dir, 0), 0.5f, Util.SkillCollection.playerFireBall,attack);
        }
    }

    void AirAttack() {
        if (isGrounded) {
            if (currentState.IsName(Util.PlayerAnimCollection.airAttack3)) {
                SetAttackVal(7);
                GameManager.instance.effectManager.ShakeCamera();
            } else if (currentState.IsName(Util.PlayerAnimCollection.airAttack2)) {
                SetAttackVal(0);
            }
            return;
        }
        if (currentState.IsName(Util.PlayerAnimCollection.airAttack1) && currentState.normalizedTime > 1.3f) {
            SetAttackVal(0);

        }
        if (currentState.IsName(Util.PlayerAnimCollection.airAttack2) && currentState.normalizedTime > 1.3f) {
            SetAttackVal(0);
        }

        if (Input.GetKeyDown(GameManager.instance.keyManager.FindKey("Attack").keyCode)) {
            if (currentState.IsName(Util.PlayerAnimCollection.jump) && attackCount == 0) {
                AddVertVelocity(7.5f);
                SetAttackVal(4);
            } else if (currentState.IsName(Util.PlayerAnimCollection.airAttack1) && currentState.normalizedTime > 0.3F) {
                AddVertVelocity(20.5f);
                SetAttackVal(5);
            } else if (currentState.IsName(Util.PlayerAnimCollection.airAttack2) && currentState.normalizedTime > 0.5F) {
                AddVertVelocity(-34.5f);
                SetAttackVal(6);
            }
        }

    }

    void GroundAttack() {
        if (!isGrounded)
            return;

        if (currentState.IsName(Util.PlayerAnimCollection.attack1) && currentState.normalizedTime > 1.3f) {
            SetAttackVal(0);
        }
        if (currentState.IsName(Util.PlayerAnimCollection.attack2) && currentState.normalizedTime > 1.3f) {
            SetAttackVal(0);
        }

        if (Input.GetKeyDown(GameManager.instance.keyManager.FindKey("Attack").keyCode)) {
            if ((currentState.IsName(Util.PlayerAnimCollection.idle) || currentState.IsName(Util.PlayerAnimCollection.walk)) && attackCount == 0) {
               // AddHoriVelocity(dir * -0.4f);
                SetAttackVal(1);
            } else if (currentState.IsName(Util.PlayerAnimCollection.attack1) && currentState.normalizedTime > 0.8f) {
               // AddHoriVelocity(dir * -0.8f);
                SetAttackVal(2);
            } else if (currentState.IsName(Util.PlayerAnimCollection.attack2) && currentState.normalizedTime > 0.5f) {
              //  AddHoriVelocity(dir * -1.5f);
                SetAttackVal(3);
            }
        }
    }

    private void SetAttackVal(int value) {
        attackCount = value;
        anim.SetInteger("Attack", value);
        if (value >= 1 && value <= 6) {
            SoundManager.instance.PlayEffect(Util.ClipNameCollection.attack0);
            attackChecker.CheckAttack((PlayerAttackType)value);
        }
    }

    public float GetDashCDPercent() {
        return 1.0f - dashCDRemain / dashCD;
    }

    private void Dash() {
        if (!isDash)
            return;
        if (dashTime <= dashTotalTime) {

            gameObject.layer = 14;
            AddVelocity(new Vector2(dir * dashSpeed, 0));
            dashTime += Time.deltaTime;
            dashCreateCurTime += Time.deltaTime;
            if (dashCreateCurTime >= dashCreateTime) {
                dashCreateCurTime = 0;
                var dash = ObjectPool.instance.GetItem(Util.ObjectItemNameCollection.playerDash);
                dash.transform.position = transform.position;
                dash.transform.localScale = new Vector2(dir * Mathf.Abs(dash.transform.localScale.x), Mathf.Abs(dash.transform.localScale.y));
            }
        } else {
            gameObject.layer = 13;
            dashTime = 0;
            isDash = false;
            dashCDRemain = dashCD;
        }
    }

    public void ResetDashCD() {
        // TODO

    }

    private void Move() {
        if (isHurt || GetComponent<Player>().attackCount > 0)
            return;

        moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput > 0) {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            dir = 1;
        } else if (moveInput < 0) {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            dir = -1;
        }

        if (!Mathf.Approximately(moveInput, 0)) {
            anim.SetBool("Walk", true);


        } else {
            anim.SetBool("Walk", false);
        }

        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

    }

    private void Jump() {
        anim.SetBool("IsGround", isGrounded);
        if (isGrounded) {
            if (jumpPressed) {
                SetAttackVal(0);
                anim.SetTrigger("Jump");
                isJump = true;
                rb.velocity = Vector2.up * jumpForce;
                SoundManager.instance.PlayEffect(Util.ClipNameCollection.jump);
            }
        } else if (rb.velocity.y < 0 || !jumpPressed) {
            rb.velocity += Vector2.down * fallForce;
        }
    }
    void Die() {
        dead = true;
        anim.SetBool("Dead", true);
        UIManager.instance.gameOverPanel.SetActive(true);
    }

    public void BeAttacked(float _attack) {
        SetAttackVal(0);
        HP -= _attack * GameManager.instance.energyManager.GetEnemyAttackRatio();
        if (HP > 0)
            anim.SetTrigger("Hurt");
    }

    void DestroySelf() {
        Destroy(gameObject);
    }

    public void ResetAttackCount() {
        attackCount = 0;
        anim.SetInteger("Attack", attackCount);
    }

    public void AddHoriVelocity(float val) {
        rb.velocity = new Vector2(val, 0);
    }

    private void AddVertVelocity(float val) {
        rb.velocity = new Vector2(0, val);
    }

    public void AddVelocity(Vector2 vec) {
        rb.velocity = vec;
    }

    public void BeAttackedAndBeatBack(int dir, float xForce, float yForce, float attackVal) {
        BeAttacked(attackVal);
        isHurt = true;
        AddVelocity(new Vector2(dir * xForce, yForce));
        Invoke("ResetHurtState", 0.4f);
    }

    public void BeAttackedAndBeatBackNormal(float xForce, float yForce, float attackVal) {
        BeAttackedAndBeatBack(-dir, xForce, yForce, attackVal);
    }

    private void ResetHurtState() {
        isHurt = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(Util.TagCollection.enemyTag) && !isGrounded && attackCount != 6) {
            BeAttackedAndBeatBackNormal(10, 3, collision.gameObject.GetComponent<Enemy>().attack);
        }
    }
}
