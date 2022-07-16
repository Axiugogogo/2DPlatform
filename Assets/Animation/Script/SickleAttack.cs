using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickleAttack : MonoBehaviour
{
    public GameObject sickle;//生成一个回旋镖
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))//按U发送回旋镖
        {
            Instantiate(sickle, transform.position, transform.rotation);
        }
    }
    

     
}
