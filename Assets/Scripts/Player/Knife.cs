using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public float speed;  //子弹速度
    private Vector2 direction;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Destroy(gameObject, 7f);  //7s后销毁自身
        if (PlayerController.Instance.horizontalMove == 0)
        {
            direction = new Vector2(0, -1);
        }
        else
        {
            direction = new Vector2(PlayerController.Instance.horizontalMove, 0);
        }
        if (direction.x < 0)//左
        {
            sr.flipX = false;
        }
        if (direction.x > 0)//右
        {
            sr.flipX = true;
        }
    }

    void Update()
    {
        transform.Translate(direction.normalized * speed);//子弹位移     
    }
}
