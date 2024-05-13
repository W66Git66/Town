using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scarecrow : MonoBehaviour
{
    private EnemyStates curState;

    [Header("目标")]
    public Transform player;

    [Header("攻击")]
    [HideInInspector] public float distance;
    public LayerMask playerLayer;//表示玩家图层

    [HideInInspector] public SpriteRenderer sr;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Collider2D enemyCollider;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();//碰撞器组件
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
