using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public Vector2 startspeed;//初始速度
    public float DelayBoomTime;//等待爆炸时间
    public float destoryBombTime;//摧毁爆炸时间
    public float hitBoxTime;
    public GameObject explosionRange;

    private Rigidbody2D rb2d;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rb2d.velocity = transform.right * startspeed.x + transform.up * startspeed.y;//炸弹初始速度
        Invoke("Explode", DelayBoomTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Explode()
    {
        anim.SetTrigger("Explode");
        Invoke("DestroyThisBomb", destoryBombTime);
        Invoke("GenExplosionRange", hitBoxTime);
       }

    void GenExplosionRange()
    {
        Instantiate(explosionRange, transform.position, Quaternion.identity);
    }
    void DestroyThisBomb()
    {
        Destroy(gameObject);
    }
}
