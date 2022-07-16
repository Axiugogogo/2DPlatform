﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Text healthText;
    public static int HealthCurrent;
    public static int Maxhealth;

    private Image healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();//获取血条图像
       // HealthCurrent = Maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float)HealthCurrent / Maxhealth;
        healthText.text = HealthCurrent.ToString() + "/" + Maxhealth.ToString();
    }
}
