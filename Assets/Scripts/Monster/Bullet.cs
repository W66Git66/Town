using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;  //子弹速度
    private Vector2 direction;

    void Start()
    {
        Destroy(gameObject, 7f);  //7s后销毁自身
        direction=PlayerController.Instance.transform.position-transform.position;
    }

    void Update()
    {
        transform.Translate(direction.normalized * speed);//子弹位移     
    }

}
