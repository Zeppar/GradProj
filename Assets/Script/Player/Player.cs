using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.Experimental.Rendering.Universal;

public class Player : MonoBehaviour {
    public int maxHP;
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

    public int HP {
        get {
            return _hp;
        }
        set {
            _hp = Mathf.Clamp(value, 0, maxHP);
            if (_hp <= 0) {
                Die();
            }
        }
    }
    private int _hp;

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
    public BoxCollider2D feetCollider;


    void Start() {
        HP = maxHP;
        anim = GetComponent<Animator>();
        currentState = anim.GetCurrentAnimatorStateInfo(0);
        rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(ChangeEnergy());
    }

    private IEnumerator ChangeEnergy() {
        while(true) {
            if (GameManager.instance.energyManager.inEnergyInCrease) {
                yield return null;
                GameManager.instance.energyManager.IncreaseEnergy(0.1f);
            } else {
                yield return new WaitForSecondsRealtime(0.1f);
                GameManager.instance.energyManager.ChangeEnergy(-0.1f);
            }
        }
    }

    void FixedUpdate() {
        UpdateGroundState();
        Move();
        Jump();
        CheckAirAttack();
        Dash();
    }

    void Update() {
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
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded) {
            jumpPressed = true;
        }
        if((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))) {
            jumpPressed = false;
        }
        if (Input.GetKeyDown(KeyCode.H) && !isDash) {
            isDash = true;
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
            } else if(currentState.IsName(Util.PlayerAnimCollection.airAttack2)) {
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

        if (Input.GetKeyDown(KeyCode.J)) {
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

        if (Input.GetKeyDown(KeyCode.J)) {
            if ((currentState.IsName(Util.PlayerAnimCollection.idle) || currentState.IsName(Util.PlayerAnimCollection.walk)) && attackCount == 0) {
                AddHoriVelocity(dir * 0.6f);
                SetAttackVal(1);
            } else if (currentState.IsName(Util.PlayerAnimCollection.attack1) && currentState.normalizedTime > 0.8f) {
                AddHoriVelocity(dir * 1.0f);
                SetAttackVal(2);
            } else if (currentState.IsName(Util.PlayerAnimCollection.attack2) && currentState.normalizedTime > 0.5f) {
                AddHoriVelocity(dir * 2.0f);
                SetAttackVal(3);
            }
        }
    }

    private void SetAttackVal(int value) {
        attackCount = value;
        anim.SetInteger("Attack", value);
        if (value >= 1 && value <= 6) {
            SoundManager.instance.PlayEffect(Util.ClipNameCollection.attack);
            attackChecker.CheckAttack((PlayerAttackType)value);
        }
    }

    private void Dash() {
        if (!isDash)
            return;
        if (dashTime <= dashTotalTime) {
            AddVelocity(new Vector2(dir * dashSpeed, rb.velocity.y));
            dashTime += Time.deltaTime;
            dashCreateCurTime += Time.deltaTime;
            if (dashCreateCurTime >= dashCreateTime) {
                dashCreateCurTime = 0;
                var dash = ObjectPool.instance.GetItem(Util.ObjectItemNameCollection.playerDash);
                dash.transform.position = transform.position;
            }
        } else {
            dashTime = 0;
            isDash = false;
        }
    }

    private void Move() {
        if (isHurt || GetComponent<Player>().attackCount > 0)
            return;

        if (transform.position.y < -160.0f) {
            HP -= 100000;
            return;
        }

        moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput > 0) {
            transform.localScale = new Vector2(Math.Abs(transform.localScale.x),transform.localScale.y);
            dir = 1;
        } else if (moveInput < 0) {
            transform.localScale = new Vector2(-Math.Abs(transform.localScale.x), transform.localScale.y);
            dir = -1;
        }

        if (!Mathf.Approximately(moveInput, 0)) {
            anim.SetBool("Walk", true);
        } else {
            anim.SetBool("Walk", false);
        }
        //  添加摩擦力避免与墙碰撞 射线影响性能
        //if (!Mathf.Approximately(moveInput, 0)) {
        //    anim.SetBool("Walk", true);
        //    RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(moveInput * 1.2f, 0, 0), new Vector2(moveInput, 0), 0.1f);
        //    if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer(Util.LayerCollection.groundLayer)) {
        //        rb.velocity = new Vector2(0, rb.velocity.y);
        //        return;
        //    }
        //} else {
        //    anim.SetBool("Walk", false);
        //}

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

    public void BeAttacked(int _attack) {
        SetAttackVal(0);
        HP -= _attack;
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

    private void AddHoriVelocity(float val) {
        rb.velocity = new Vector2(val, 0);
    }

    private void AddVertVelocity(float val) {
        rb.velocity = new Vector2(0, val);
    }

    public void AddVelocity(Vector2 vec) {
        rb.velocity = vec;
    }

    public void BeAttackedAndBeatBack(int dir, float xForce, float yForce, int attackVal) {
        BeAttacked(attackVal);
        isHurt = true;
        AddVelocity(new Vector2(dir * xForce, yForce));
        Invoke("ResetHurtState", 0.4f);
    }

    private void ResetHurtState() {
        isHurt = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(Util.TagCollection.enemyTag) && !isGrounded && attackCount != 6) {
            BeAttackedAndBeatBack(-dir, 10, 3, collision.gameObject.GetComponent<Enemy>().attack);
        }
    }
}
