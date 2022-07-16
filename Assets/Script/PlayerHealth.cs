using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int Blinks;//闪烁的次数
    public float seconds;//闪烁的时间
    public float dieTime;
    public float HitBoxCDTime;


    private Renderer MyRender;
    private Animator anim;
    private ScreenFlash sf;
    private Rigidbody2D rb2d;

    private PolygonCollider2D polygonCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        MyRender = GetComponent<Renderer>();
        anim = GetComponent<Animator>();
        HealthBar.Maxhealth = health;
        HealthBar.HealthCurrent = health;
        sf = GetComponent<ScreenFlash>();
        rb2d = GetComponent<Rigidbody2D>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    public void DamagePlayer(int Damage)//角色被攻击函数
    {

        sf.FlashScreen();
        health -= Damage;
        if (health < 0)
        {
            health = 0;
        }
        HealthBar.HealthCurrent = health;
        if (health <= 0)
        {
            rb2d.velocity = new Vector2(0, 0);
            GameController.IsGameAlive = false;
            anim.SetTrigger("Die");
            Invoke("KillPlayer", dieTime);
        }
        BlinkPlayer(Blinks, seconds);
        polygonCollider2D.enabled = false;
        StartCoroutine(ShowPlayerHitBox());
    }

    IEnumerator ShowPlayerHitBox()
    {
        yield return new WaitForSeconds(HitBoxCDTime);
        polygonCollider2D.enabled = true;
    }
    void KillPlayer()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void BlinkPlayer(int numLinks, float seconds)//角色闪烁
    {
        StartCoroutine(DoBlinks(numLinks,seconds));//调用协程
    }

    IEnumerator DoBlinks(int numLinks, float seconds)
    {
        for (int i = 0; i < numLinks * 2; i++)
        {
            MyRender.enabled = !MyRender.enabled;
            yield return  new  WaitForSeconds(seconds);// WaitForSeconds会在等second的时间后在执行
        }
        MyRender.enabled = true;
    }
}
