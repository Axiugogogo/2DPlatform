 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float RunSpeed = 1;
    public float JumpSpeed = 5;
    public float DoubleJumpSpeed = 2;
    private Rigidbody2D Myrigidbody2D;
    private Animator myAnim;
    private BoxCollider2D myFeet;
    private bool isGround;
    private bool CanDoubleJump;
    // Start is called before the first frame update
    void Start()
    {
        Myrigidbody2D = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Filp();
        Jump();
        CheckGround();
        SwitchAnimation();
        //Attack();
    }

    void CheckGround()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));//获取是否和地面接触
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
        if (Input.GetButtonDown("Jump"))
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
}
