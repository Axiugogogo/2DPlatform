using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtack : MonoBehaviour
{
    public float time; 
    public int damage;//伤害值
    public float Starttime;
    private Animator anim;//父对象动画
    private PolygonCollider2D coll2D;//自己本身的碰撞体 
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();//用来寻找父物体的组件
        coll2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()//主角的攻击
    {
        if (Input.GetButtonDown("Attack"))
        {
            
            anim.SetTrigger("Attack");
            StartCoroutine(StartAttack());
        }
    }

    IEnumerator StartAttack()//攻击开始
    { 
        yield return new WaitForSeconds(Starttime);//
        coll2D.enabled = true;//显示攻击表格框
        StartCoroutine(disableHitBox());//调用协程
    }

    IEnumerator disableHitBox()
    {
        yield return new WaitForSeconds(time);//实现等待time的时间，执行coll2D.enabled=false;
        coll2D.enabled=false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
