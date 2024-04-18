using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f; // 移动速度
    private Rigidbody2D rb; // Rigidbody2D 组件引用
    private Vector2 movement; // 移动方向

    void Start()
    {
        // 获取角色的 Rigidbody2D 组件
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 获取 WASD 键输入
        movement.x = Input.GetAxisRaw("Horizontal"); // 获取水平轴输入（A 和 D 键）
        movement.y = Input.GetAxisRaw("Vertical"); // 获取垂直轴输入（W 和 S 键）
    }

    void FixedUpdate()
    {
        // 根据输入移动角色
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
