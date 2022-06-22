using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour//定义为敌人抽象类,这是一个父类，由其他敌人自己去继承以实现敌人的基本问题
{
    public int damage;
    public int health;//血量、伤害
    public float flashTime;

    private SpriteRenderer sr;
    private Color OriginalColor;//记录原始Color

    private PlayerHealth playerHealth;

    public GameObject BloodEffect;//血液特效

    // Start is called before the first frame update
    public void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();//获取Player
        sr = GetComponent<SpriteRenderer>();//获取SpriteRenderer组件用来更改敌人受击的颜色
        OriginalColor = sr.color;
    }

    // Update is called once per frame
    public void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        FlashColor(flashTime);
        Instantiate(BloodEffect, transform.position, Quaternion.identity);//
        GameController.camShake.Shake();
    }

    void FlashColor(float time)//收到攻击改变颜色
    {
        sr.color = Color.red;
        Invoke("ResetColor", time);//延迟调用函数
    }

    void ResetColor()//变色之后将颜色变回
    {
        sr.color = OriginalColor;
    }

    private void OnTriggerEnter2D(Collider2D other)//
    {
        
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if (playerHealth != null)
            {
                playerHealth.DamagePlayer(damage);
            }
            
        }
    }
}
