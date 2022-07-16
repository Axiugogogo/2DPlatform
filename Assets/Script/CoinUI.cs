using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    public int startCoinNum;//初始金币数量
    public Text CoinQuantity;//显示金币数量

    public static int CurrentCoin;//当前的金币数量

    // Start is called before the first frame update
    void Start()
    {
        CurrentCoin = startCoinNum;//最开始为初始的金币数量
    }

    // Update is called once per frame
    void Update()
    {
        CoinQuantity.text = CurrentCoin.ToString();
    }
}
