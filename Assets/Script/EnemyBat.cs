using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : Enemy
{
    public float BatSpeed;
    public float StartWaitTime;

    private float WaitTime;

    public Transform MovePos;
    public Transform LeftDwonPos;
    public Transform RightUpPos;

    

    // Start is called before the first frame update
    public void Start()
    {
        base.Start();//初始化要先调用父类的方法
        WaitTime = StartWaitTime;
        MovePos.position = GetRandomPos();
    }

    // Update is called once per frame
    public void Update()
    {
       base.Update();

        transform.position = Vector2.MoveTowards(transform.position, MovePos.position, BatSpeed * Time.deltaTime);//Unity自己的移动方法

        if (Vector2.Distance(transform.position, MovePos.position) < 0.1f)//当前位置和目标位置是否到达
        {
            if (WaitTime <= 0)
            {
                MovePos.position = GetRandomPos();
                WaitTime = StartWaitTime;
            }
            else 
            {
                WaitTime -= Time.deltaTime;
            }
        }
    }

    //定义一个方法
    Vector2 GetRandomPos()//获取一个随机位置
    { 
        Vector2 RndPos = new Vector2(Random.Range(LeftDwonPos.position.x,RightUpPos.position.x), Random.Range(LeftDwonPos.position.y, RightUpPos.position.y));
        return RndPos;
    }
}
