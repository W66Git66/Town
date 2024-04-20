using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;

    public float speed;
    private float horizontalMove;
    private float verticalMove;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        GroundMovement();

        SwitchAnim();
    }

    void GroundMovement()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");//Ö»·µ»Ø-1£¬0£¬1
        verticalMove = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector3(horizontalMove,verticalMove,1)*speed;
    }
    void SwitchAnim()//¶¯»­ÇÐ»»
    {
        anim.SetFloat("Xmove", horizontalMove);
        anim.SetFloat("Ymove", verticalMove);
    }
}
