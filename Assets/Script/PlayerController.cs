 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float RunSpeed = 1;
    public float JumpSpeed = 5;
    public float DoubleJumpSpeed = 2;
    public float restoreTime;
    public float climbSpeed;


    private Rigidbody2D Myrigidbody2D;
    private Animator myAnim;
    private BoxCollider2D myFeet;
    private bool isGround;
    private bool CanDoubleJump;
    private bool isLadder;
    private bool isClimbing;//是否在爬
    private bool isJumping;//是否在跳
    private bool isFalling;//是否下落
    private bool isDoubleJumping;//是否在二段跳
    private bool isDoubleFalling;//是否在二段下落
    private float playerGravity;


    private bool IsOneWayPlatform;
    // Start is called before the first frame update
    void Start()
    {
        Myrigidbody2D = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
        playerGravity = Myrigidbody2D.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.IsGameAlive == true)
        {
            CheckAirStatus();//是否在空中
            Run();
            Filp();
            Jump();
            CheckGround();
            SwitchAnimation();
            OneWayPlatformCheck();//按下键，角色从单项平台上下落
            CheckLadder();//检测是否在爬梯子
            Climb();
        }
        
        //Attack();
    }

    void CheckGround()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
                   myFeet.IsTouchingLayers(LayerMask.GetMask("MoveingPlatform")) ||
                   myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));//获取是否和地面接触
        IsOneWayPlatform = myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));
    }

    void CheckLadder()
    {
        isLadder = myFeet.IsTouchingLayers(LayerMask.GetMask("Ladder"));
    }
    void Filp()
    {
        //Math.Abs取绝对值, Mathf.Epsilon：一个很小的浮点数值
        bool playerHasXAxisSpeed = Mathf.Abs(Myrigidbody2D.velocity.x) > Mathf.Epsilon;
        if (playerHasXAxisSpeed)
        {
            if (Myrigidbody2D.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);//返回一个旋转，它围绕 z 轴旋转 z 度、围绕 x 轴旋转 x 度、围绕 y 轴旋转 y 度
            }
            else if (Myrigidbody2D.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
    void Run()
    {
        float moveDir = Input.GetAxis("Horizontal");//Horizontal:获得水平方向。
        Vector2 playerVec = new Vector2(moveDir * RunSpeed, Myrigidbody2D.velocity.y);
        Myrigidbody2D.velocity = playerVec;//.velocity:给物体一个恒定的速度,将物体提升至该速度。
        bool playerHasXAxisSpeed = Mathf.Abs(Myrigidbody2D.velocity.x) > Mathf.Epsilon;
        myAnim.SetBool("IsRun", playerHasXAxisSpeed);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !Input.GetKey(KeyCode.S))
        {
            if (isGround)//如果和地面接触则可以跳
            {
                myAnim.SetBool("IsJump", true);
                Vector2 JumpVel = new Vector2(0.0f, JumpSpeed);
                Myrigidbody2D.velocity = Vector2.up * JumpVel;//Vector2.up方向为上，数值为1的单位向量
                CanDoubleJump = true;
            }
            else 
            {
                if (CanDoubleJump)
                {
                    myAnim.SetBool("IsDoubleJump", true);
                    Vector2 DoubleJumpVel = new Vector2(0.0f, DoubleJumpSpeed);
                    Myrigidbody2D.velocity = Vector2.up * DoubleJumpVel;
                    CanDoubleJump = false;//二段跳之后改为False
                }
            }
        }
    }

    void Climb()
    {
        if (isLadder)
        {
            float moveY = Input.GetAxis("Vertical");
            if (moveY > 0.5f || moveY < -0.5f)
            {
                myAnim.SetBool("IsClimbing", true);
                Myrigidbody2D.gravityScale = 0.0f;
                Myrigidbody2D.velocity = new Vector2(Myrigidbody2D.velocity.x, moveY * climbSpeed);

            }
            else 
            {
                if (isJumping || isFalling || isDoubleJumping || isDoubleFalling)
                {
                    myAnim.SetBool("IsClimbing", false);
                }
                else 
                {
                    myAnim.SetBool("IsClimbing", false);
                    Myrigidbody2D.velocity = new Vector2(Myrigidbody2D.velocity.x, 0.0f);
                }
            }
        }
        else
        {
            myAnim.SetBool("IsClimbing", false);
            Myrigidbody2D.gravityScale = playerGravity ;
        }
    }

    //void Attack()
    //{
    //    if (Input.GetButtonDown("Attack"))
    //    {
    //        myAnim.SetTrigger("Attack");
    //    }
    //}
    void SwitchAnimation()//切换动画
    {
        myAnim.SetBool("IsIdle",false);
        if (myAnim.GetBool("IsJump"))
        {
            if (Myrigidbody2D.velocity.y < 0.0f)//如果Y轴速度为负的话,就切换为下落
            {
                myAnim.SetBool("IsJump", false);
                myAnim.SetBool("IsFall", true);
            }
        }
        else if (isGround)//接触到了地面,则下落为False，Idle为True
        {
            myAnim.SetBool("IsFall", false);
            myAnim.SetBool("IsIdle", true);
        }

        if (myAnim.GetBool("IsDoubleJump"))
        {
            if (Myrigidbody2D.velocity.y < 0.0f)//如果Y轴速度为负的话,就切换为下落
            {
                myAnim.SetBool("IsDoubleJump", false);
                myAnim.SetBool("IsDoubleFall", true);
            }
        }
        else if (isGround)//接触到了地面,则下落为False，Idle为True
        {
            myAnim.SetBool("IsDoubleFall", false);
            myAnim.SetBool("IsIdle", true);
        }
    }

    void OneWayPlatformCheck()
    {
        float moveY = Input.GetAxis("Vertical");
        if (IsOneWayPlatform && moveY < -0.1f && Input.GetButtonDown("Jump"))   
        {
            gameObject.layer = LayerMask.NameToLayer("OneWayPlatform");
            Invoke("RestorePlayerLayer", restoreTime);
        }
        
    }

    void RestorePlayerLayer()
    {
        if (!isGround && gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }

    void CheckAirStatus()
    {
        isJumping = myAnim.GetBool("IsJump");
        isFalling = myAnim.GetBool("IsFall");
        isDoubleJumping = myAnim.GetBool("IsDoubleJump");
        isDoubleFalling = myAnim.GetBool("IsDoubleFall");
        isClimbing = myAnim.GetBool("IsClimbing");
    }
}


