using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManage : MonoBehaviour
{
    public static AudioSource audioScr;//音源
    public static AudioClip pickCoin;//声音片
    public static AudioClip throwCoin;
    
    // Start is called before the first frame update
    void Start()
    {
       audioScr = GetComponent<AudioSource>();
       pickCoin = Resources.Load<AudioClip>("Assets_Resources_PickCoin"); 
        throwCoin = Resources.Load<AudioClip>("Assets_Resources_ThrowCoin");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlayPickCoin()
    {
        audioScr.PlayOneShot(pickCoin);
    }

    public static void PlayThrowCoin()
    {
        audioScr.PlayOneShot(throwCoin);
    }
}
