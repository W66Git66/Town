using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public float speed;  //�ӵ��ٶ�
    private Vector2 direction;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Destroy(gameObject, 7f);  //7s����������
        if (PlayerController.Instance.horizontalMove == 0)
        {
            direction = new Vector2(0, -1);
        }
        else
        {
            direction = new Vector2(PlayerController.Instance.horizontalMove, 0);
        }
        if (direction.x < 0)//��
        {
            sr.flipX = false;
        }
        if (direction.x > 0)//��
        {
            sr.flipX = true;
        }
    }

    void Update()
    {
        transform.Translate(direction.normalized * speed);//�ӵ�λ��     
    }
}
