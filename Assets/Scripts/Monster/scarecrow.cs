using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scarecrow : MonoBehaviour
{
    private EnemyStates curState;

    [Header("Ŀ��")]
    public Transform player;

    [Header("����")]
    [HideInInspector] public float distance;
    public LayerMask playerLayer;//��ʾ���ͼ��

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
        enemyCollider = GetComponent<Collider2D>();//��ײ�����
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
