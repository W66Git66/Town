using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f; // �ƶ��ٶ�
    private Rigidbody2D rb; // Rigidbody2D �������
    private Vector2 movement; // �ƶ�����

    void Start()
    {
        // ��ȡ��ɫ�� Rigidbody2D ���
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ��ȡ WASD ������
        movement.x = Input.GetAxisRaw("Horizontal"); // ��ȡˮƽ�����루A �� D ����
        movement.y = Input.GetAxisRaw("Vertical"); // ��ȡ��ֱ�����루W �� S ����
    }

    void FixedUpdate()
    {
        // ���������ƶ���ɫ
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
