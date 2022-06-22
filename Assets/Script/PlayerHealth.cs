using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int Blinks;//闪烁的次数
    public float seconds;//闪烁的时间


    private Renderer MyRender;
    // Start is called before the first frame update
    void Start()
    {
        MyRender = GetComponent<Renderer>();
    }

    public void DamagePlayer(int Damage)//角色被攻击函数
    {
        health -= Damage;
        if (health <= 0)
        { 
            Destroy(gameObject);
        }
        BlinkPlayer(Blinks, seconds);

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
