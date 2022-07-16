using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    private bool canOpen;//现在是否能打开
    private bool isOpened;//现在是否是一个打开的状态
    private Animator anim;

    public float delayTime;//
    public GameObject coin;//宝箱掉落物品
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isOpened = false;//一开始箱子没有打开
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.I))//按下I打开宝箱
        {
            if (canOpen && !isOpened)
            {
                anim.SetTrigger("Opened");
                isOpened = true;
                Invoke("GenCoin", delayTime);
            }
        }
    }

    void GenCoin()
    {
        Instantiate(coin, transform.position, Quaternion.identity);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        print(1234);
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            canOpen = true;//接触到宝箱，可以打开

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            canOpen = false;

        }
    }
}
