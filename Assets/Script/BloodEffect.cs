﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    public float timeToDestory;//从出生到销毁时间
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeToDestory); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}