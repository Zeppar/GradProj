using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public int maxHP;
    public int attack;
    public float range;
    public float attackInterval;
    public bool dead = false;
    public float speed;
    public float jumpForce;
    public GameObject Target;
    public Transform AttackPoint;

    public Transform ClimbPos;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask groundLayer;

    //public ParticleSystem moveParticle;
    //public ParticleSystem jumpParticle;

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

    //计时器
    private float jumpTimeCounter;
    public float jumpTime;

    void Start()
    {
        HP = maxHP;
        anim = GetComponent<Animator>();
        currentState = anim.GetCurrentAnimatorStateInfo(0);
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        Move();
    }

    void Update()
    {
        currentState = anim.GetCurrentAnimatorStateInfo(0);
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, groundLayer);
        AirAttack();
        GroundAttack();
        SkillAttack();
        //Climb();
        Jump();
    }
    void SkillAttack() {
        if (Input.GetKeyDown(KeyCode.K)) {
            GameManger.instance.skillParticleCreator.CreateFireball(AttackPoint.position, new Vector2(transform.GetComponent<Player>().dir, 0));
        }
    }
    void AirAttack() {
        if (isGrounded) {
            if(currentState.IsName("AirAttack3")) {
                anim.SetInteger("Attack", 4);
            }
            return;
        }

        //Debug.Log(currentState.IsName("AirAttack1") + " - " + currentState.normalizedTime + " - " + currentState.IsName("Jump") + " - " + currentState.IsName("Idle"));
        if (currentState.IsName("AirAttack1") && currentState.normalizedTime > 4.0f) {
            anim.SetInteger("Attack", 0);
            attackCount = 0;
        }
        if (currentState.IsName("AirAttack2") && currentState.normalizedTime > 4.0f) {
            anim.SetInteger("Attack", 0);
            attackCount = 0;
        }

        if (Input.GetKeyDown(KeyCode.J)) {
            if (currentState.IsName("Jump") && attackCount == 0) {
                rb.velocity = new Vector2(0, 7.5f);
                anim.SetInteger("Attack", 1);
                attackCount = 1;
            } else if (currentState.IsName("AirAttack1") && attackCount == 1 && currentState.normalizedTime > 0.5F) {
                rb.velocity = new Vector2(0, 12.5f);
                anim.SetInteger("Attack", 2);
                attackCount = 2;
            } else if (currentState.IsName("AirAttack2") && attackCount == 2 && currentState.normalizedTime > 0.5F) {
                anim.SetInteger("Attack", 3);
                rb.velocity = new Vector2(0, -20.5f);
                attackCount = 3;
            }
            Collider2D coll = Physics2D.OverlapCircle(AttackPoint.position, range);

            if (coll != null && coll.CompareTag("Enemy") && !coll.GetComponent<Enemy>().dead) {
                coll.GetComponent<Enemy>().BeAttacked(attack);
            }
        }

    }
    void GroundAttack() {
        if (!isGrounded)
            return;

        if (currentState.IsName("Attack1") && currentState.normalizedTime > 2.0f) {
            anim.SetInteger("Attack", 0);
            attackCount = 0;
        }
        if (currentState.IsName("Attack2") && currentState.normalizedTime > 2.0f) {
            anim.SetInteger("Attack", 0);
            attackCount = 0;
        }

        if (Input.GetKeyDown(KeyCode.J)) {
            if ((currentState.IsName("Idle") || currentState.IsName("Walk")) && attackCount == 0) {
                rb.velocity = new Vector2(rb.velocity.x * 0.5f, rb.velocity.y);
                anim.SetInteger("Attack", 1);
                attackCount = 1;
            } else if (currentState.IsName("Attack1") && attackCount == 1 && currentState.normalizedTime > 0.5F && currentState.normalizedTime < 1.5f) {
                anim.SetInteger("Attack", 2);
                attackCount = 2;
            } else if (currentState.IsName("Attack2") && attackCount == 2 && currentState.normalizedTime > 0.5F && currentState.normalizedTime < 1.0f) {
                anim.SetInteger("Attack", 3);
                attackCount = 3;
            }
            Collider2D coll = Physics2D.OverlapCircle(AttackPoint.position, range);
            if (coll != null && coll.CompareTag("Enemy") && !coll.GetComponent<Enemy>().dead) {
                coll.GetComponent<Enemy>().BeAttacked(attack);
            }
        }

    }

    private void Move() {
        if (isClimb)
            return;
        if (GetComponent<Player>().attackCount > 0)
            return;
        moveInput = Input.GetAxisRaw("Horizontal");
        //moveParticle.gameObject.SetActive(isGrounded);
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (moveInput != 0) {
            anim.SetBool("Walk", true);
        } else if (isJump == false) {
            anim.SetBool("Walk", false);
        }
        if (transform.position.y <= -40)//玩家过低销毁玩家
        {
            Destroy(gameObject);
        }

    }
    private void Climb() {
        if (Input.GetKeyDown(KeyCode.F)) {
            if (isClimb) {
                anim.SetBool("Climb", false);
                anim.SetBool("ClimbDown", false);
                isClimb = false;
            }
            if (!isClimb && Physics2D.OverlapCircle(ClimbPos.position, checkRadius, groundLayer)) {
                anim.SetBool("ClimbDown", true);
                isClimb = true;
            }
        }
        if (isClimb) {
            bool canClimb = Physics2D.OverlapCircle(ClimbPos.position, checkRadius, groundLayer);
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
        if (isClimb) { return; }
        //jumpParticle.gameObject.SetActive(!isGrounded);
        anim.SetBool("IsGround", isGrounded);
        if (moveInput > 0) {
            //transform.eulerAngles = new Vector2(0, 0);
            transform.rotation = new Quaternion(0, 0, 0, 0);
            dir = 1;
        } else if (moveInput < 0) {
            //transform.eulerAngles = new Vector2(0, 180);
            transform.rotation = new Quaternion(0, 180, 0, 0);
            dir = -1;
        }

        if (isGrounded == true) {

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                anim.SetTrigger("Jump");
                isJump = true;
                jumpTimeCounter = jumpTime;
                rb.velocity = Vector2.up * jumpForce;

                //source.PlayOneShot(Jump);
            }
        } else {
            isJump = false;
        }

        //if (isJump == true) {
            
        //    if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
        //        if (jumpTimeCounter > 0) {
        //            rb.velocity = Vector2.up * jumpForce;
        //            jumpTimeCounter -= Time.deltaTime;
        //        } else {

        //            isJump = false;
        //        }
        //    }
        //}
        //if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)) {

        //    isJump = false;
        //}

    }

    void Die()
    {
        dead = true;
        anim.SetBool("Dead", true);
    }

    public void BeAttacked(int _attack)
    {
        HP -= _attack;
        if(HP > 0)
            anim.SetTrigger("Hurt");
    }


    void DestroySelf() {
        Destroy(gameObject);
    }

    public void ResetAttackCount() {
        attackCount = 0;
        anim.SetInteger("Attack", attackCount);
    }
}
