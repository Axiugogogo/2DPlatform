using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;//代表Player的位置
    public float smoothing;//平滑因子

    // Start is called before the first frame update
    void Start()
    {
        GameController.camShake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();
    }

    void LateUpdate()//要等所有的UPdate执行后才执行
    {
        if (target != null)
        {
            if (transform.position != target.position)
            {
                Vector3 targetPos = target.position;
                transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);//每次返回的值都是从a到b的某段距离,如：smoothing = 0.1 也就是返回其a到b距离中的10分之1,造成拖拉移动缓动的效果
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
