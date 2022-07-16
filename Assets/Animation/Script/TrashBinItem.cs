using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBinItem : MonoBehaviour
{

    private bool IsPlayerInTrashBin;//是否在垃圾里框中
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (IsPlayerInTrashBin)
            {
                if (CoinUI.CurrentCoin > 0) //当前的金币数量  > 0
                {
                    SoundManage.PlayThrowCoin();//播放投币声音
                    TrashBinCoin.coinCurrent++;
                    CoinUI.CurrentCoin--;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            IsPlayerInTrashBin= true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IsPlayerInTrashBin = false;
        
    }
}
