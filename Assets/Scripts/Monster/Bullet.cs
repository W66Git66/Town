using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;  //�ӵ��ٶ�
    private Vector2 direction;

    void Start()
    {
        Destroy(gameObject, 7f);  //7s����������
        direction=PlayerController.Instance.transform.position-transform.position;
    }

    void Update()
    {
        transform.Translate(direction.normalized * speed);//�ӵ�λ��     
    }

}
