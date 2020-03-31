using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : MonoBehaviour {
    public int maxHP;
    public int attack;
    public float range;
    public float attackInterval;
    public bool dead = false;
    public float speed;
    public float jumpForce;
    public Transform attackPoint;

    public Transform climbPos;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask groundLayer;

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
    private bool isClimb = false;

    public int dir = 1;
    public BoxCollider2D feetCollider;

    [Header("音频")]
    private AudioSource audioSource;
    public AudioClip attackAudio;

    void Start() {
        HP = maxHP;
        anim = GetComponent<Animator>();
        currentState = anim.GetCurrentAnimatorStateInfo(0);
        rb = gameObject.GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate() {
        Move();
    }

    void Update() {
        UpdateState();
        AirAttack();
        GroundAttack();
        SkillAttack();
        //Climb();
        Jump();
        SwitchFall();
        CreateShadow();
    }

    private void UpdateState() {
        currentState = anim.GetCurrentAnimatorStateInfo(0);
        isGrounded = feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    private void SwitchFall() {
        if (currentState.IsName("Jump") && !isGrounded && rb.velocity.y < -0.1f) {
            anim.SetTrigger("Fall");
        }
    }

    private void CreateShadow() {
        return;
        if (Input.GetKeyDown(KeyCode.L)) {
            //GameManager.instance.skillParticleCreator.CreateFireball(attackPoint.position, new Vector2(dir, 0), 0.5f, Util.SkillCollection.playerFireBall,attack);
        }
    }

    void SkillAttack() {
        if (Input.GetKeyDown(KeyCode.I)) {
            GameManager.instance.skillParticleCreator.CreateShadow(transform.position, dir);
        }
    }

    void AirAttack() {
        if (isGrounded) {
            if (currentState.IsName("AirAttack3")) {
                SetAttackVal(7);
            }
            return;
        }
        if (currentState.IsName("AirAttack1") && currentState.normalizedTime > 1.0f) {
            SetAttackVal(0);
        }
        if (currentState.IsName("AirAttack2") && currentState.normalizedTime > 1.0f) {
            SetAttackVal(0);
        }

        if (Input.GetKeyDown(KeyCode.J)) {
            if (currentState.IsName("Jump") && attackCount == 0) {
                AddVertVelocity(7.5f);
                SetAttackVal(4);
            } else if (currentState.IsName("AirAttack1") && currentState.normalizedTime > 0.5F) {
                AddVertVelocity(20.5f);
                SetAttackVal(5);
            } else if (currentState.IsName("AirAttack2") && currentState.normalizedTime > 0.5F) {
                AddVertVelocity(-34.5f);
                SetAttackVal(6);
            }
            Collider2D coll = Physics2D.OverlapCircle(attackPoint.position, range);

            if (coll != null && coll.CompareTag("Enemy") && !coll.GetComponent<Enemy>().dead) {
                coll.GetComponent<Enemy>().BeAttacked(attack);
            }
        }

    }

    void GroundAttack() {
        if (!isGrounded)
            return;

        if (currentState.IsName("Attack1") && currentState.normalizedTime > 1.0f) {
            SetAttackVal(0);
        }
        if (currentState.IsName("Attack2") && currentState.normalizedTime > 1.0f) {
            SetAttackVal(0);
        }

        if (Input.GetKeyDown(KeyCode.J)) {
            if ((currentState.IsName("Idle") || currentState.IsName("Walk")) && attackCount == 0) {
                AddHoriVelocity(dir * 2.0f);
                SetAttackVal(1);
            } else if (currentState.IsName("Attack1") && currentState.normalizedTime > 0.5f) {
                AddHoriVelocity(dir * 3.0f);
                SetAttackVal(2);
            } else if (currentState.IsName("Attack2") && currentState.normalizedTime > 0.5f) {
                AddHoriVelocity(dir * 5.0f);
                SetAttackVal(3);
            }
            Collider2D coll = Physics2D.OverlapCircle(attackPoint.position, range);
            if (coll != null && coll.CompareTag("Enemy") && !coll.GetComponent<Enemy>().dead) {
                coll.GetComponent<Enemy>().BeAttacked(attack);
            }
        }

    }

    private void SetAttackVal(int value) {
        attackCount = value;
        anim.SetInteger("Attack", value);

    }

    RaycastHit2D[] results = new RaycastHit2D[1];
    private void Move() {
        if (isClimb)
            return;
        if (GetComponent<Player>().attackCount > 0)
            return;

        if (transform.position.y < -40.0f) {
            Destroy(gameObject);
            return;
        }

        moveInput = Input.GetAxisRaw("Horizontal");
        if (moveInput > 0) {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            dir = 1;
        } else if (moveInput < 0) {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            dir = -1;
        }

        if (!Mathf.Approximately(moveInput, 0)) {
            anim.SetBool("Walk", true);
        } else {
            anim.SetBool("Walk", false);
        }


        if (!Mathf.Approximately(moveInput, 0)) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(moveInput * 1.2f, 0, 0), new Vector2(moveInput, 0), 0.1f);
            if (hit.collider != null && !hit.collider.isTrigger) {
                rb.velocity = new Vector2(0, rb.velocity.y);
                return;
            }
        }

        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

    }
    private void Climb() {
        if (Input.GetKeyDown(KeyCode.F)) {
            if (isClimb) {
                anim.SetBool("Climb", false);
                anim.SetBool("ClimbDown", false);
                isClimb = false;
            }
            if (!isClimb && Physics2D.OverlapCircle(climbPos.position, checkRadius, groundLayer)) {
                anim.SetBool("ClimbDown", true);
                isClimb = true;
            }
        }
        if (isClimb) {
            bool canClimb = Physics2D.OverlapCircle(climbPos.position, checkRadius, groundLayer);
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
                if (!canClimb) {
                    anim.SetBool("Climb", false);
                    anim.SetBool("ClimbDown", false);
                    isClimb = false;
                    return;
                }
                rb.velocity = new Vector2(0, 10);
                anim.SetBool("Climb", true);
                anim.SetBool("ClimbDown", false);
            } else {
                anim.SetBool("Climb", false);
                anim.SetBool("ClimbDown", true);
            }

        }
    }
    private void Jump() {
        if (isClimb) {
            return;
        }
        //jumpParticle.gameObject.SetActive(!isGrounded);
        anim.SetBool("IsGround", isGrounded);
        if (isGrounded == true) {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                anim.SetTrigger("Jump");
                attackCount = 0;
                anim.SetInteger("Attack", attackCount);
                isJump = true;
                rb.velocity = Vector2.up * jumpForce;
                //source.PlayOneShot(Jump);
            }
        }

    }
    void Die() {
        dead = true;
        anim.SetBool("Dead", true);
        UIManger.instance.gameOverPanel.SetActive(true);
    }

    public void BeAttacked(int _attack) {
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
}
