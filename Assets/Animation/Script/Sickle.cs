using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sickle : MonoBehaviour
{
    public float flyspeed;//回旋镖飞行速度
    public int damage;//回旋镖飞行伤害
    public float rotateSpeed;//旋转速度
    public float tuning;//微调

    private Rigidbody2D rb2d;
    private Transform PlayerTrasform;
    private Transform sickleTransform;
    private Vector2 startSpeed;
    private CameraShake camShake;//相机震动   
    // Start is called before the first frame update
    void Start()
    {
        rb2d=GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.right * flyspeed;
        PlayerTrasform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        startSpeed = rb2d.velocity; //记录初始速度
        sickleTransform = GetComponent<Transform>();
        camShake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed);
        float y = Mathf.Lerp(transform.position.y, PlayerTrasform.position.y, tuning);//用差值的方法让飞镖跟着角色
        transform.position = new Vector3(transform.position.x, y, 0.0f);
        rb2d.velocity = rb2d.velocity - startSpeed * Time.deltaTime;

        if (Mathf.Abs(transform.position.x - PlayerTrasform.position.x )< 0.5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
