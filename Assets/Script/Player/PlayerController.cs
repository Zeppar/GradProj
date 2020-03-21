using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private float moveInput;

    private bool isGrounded;
    public bool isJump = false;
    public  bool isClimb = false;
    public bool isMove = false;

    public Transform ClimbPos;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

   
    public int dir = 1;
    //计时器
    private float jumpTimeCounter;//实时更新的值
    public float jumpTime;//设置一个计时值      
    public Animator anim;

    private AnimatorStateInfo currentState;



    // Start is called before the first frame update
    void Start()
    {        
        rb = gameObject.GetComponent<Rigidbody2D>();       
        anim = gameObject.GetComponent<Animator>();
              
    }
    void FixedUpdate()
    {
        currentState = anim.GetCurrentAnimatorStateInfo(0);
        Move();
    }
    private void Update()
    {
        Climb();
        Jump();
        if (Input.GetKey(KeyCode.JoystickButton0))
        {
            print("0");
        }
       
       
    }
    private void Move()
    {       
        if (isClimb) { return; }
        if (!currentState.IsName("Idle") && !currentState.IsName("Walk") &&  !currentState.IsName("Jump")) { isMove = false;  return; }
        moveInput = Input.GetAxisRaw("Horizontal");
       // dust_ps.gameObject.SetActive(isGrounded);
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if(Mathf.Abs(moveInput) > 0.1)
        {
            anim.SetBool("Walk", true);
            isMove = true;
            //transform.localScale =  transform.localScale * new Vector2(moveInput, 0);
        }
            
        if (moveInput > 0)
         {
                
                transform.rotation = new Quaternion(0, 0, 0, 0);
                dir = 1;
         }
        else if (moveInput < 0)
         {
               
                transform.rotation = new Quaternion(0, 180, 0, 0);
                dir = -1;
         }
        
        else if (isJump == false)
        {
            anim.SetBool("Walk", false);
            isMove = false;
         
   
            // audioSource.Stop();
        }
       
        if (transform.position.y <= -40)//玩家过低销毁玩家
        {
            Destroy(gameObject);
        }

    }
    private void Climb()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isClimb)
            {
                anim.SetBool("Climb", false);
                anim.SetBool("ClimbDown", false);
                isClimb = false;
            }
            if (!isClimb && Physics2D.OverlapCircle(ClimbPos.position, checkRadius, whatIsGround))
            {
                anim.SetBool("ClimbDown", true);
                isClimb = true;
            }
        }
        if (isClimb)
        {
            bool canClimb = Physics2D.OverlapCircle(ClimbPos.position, checkRadius, whatIsGround);
            if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.UpArrow))
            {
                if (!canClimb)
                {
                    anim.SetBool("Climb", false);
                    anim.SetBool("ClimbDown", false);
                    isClimb = false;
                    return;
                }
                rb.velocity = new Vector2(0,10);
                anim.SetBool("Climb", true);
                anim.SetBool("ClimbDown", false);
            }
            else
            {
                anim.SetBool("Climb", false);
                anim.SetBool("ClimbDown", true);
            }
          
        }
    }
    private void Jump()
    {
        if (isClimb) { return; }
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

    

        if (isGrounded == true)
        {

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                isJump = true;
                jumpTimeCounter = jumpTime;
                rb.velocity = Vector2.up * jumpForce;

                //source.PlayOneShot(Jump);
            }
        }

        if (isJump == true)
        {
            anim.SetTrigger("Jump");
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)||Input.GetKey(KeyCode.JoystickButton0))
            {
                if (jumpTimeCounter > 0)
                {
                    rb.velocity = Vector2.up * jumpForce;
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    
                    isJump = false;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.JoystickButton0))
        {
            
            isJump = false;
        }
       
    }

    public void fall()
    {
        //anim.SetTrigger("Fall");
    }
  

}
