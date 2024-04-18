using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;

    public float speed, jumpForce;
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
        rb.velocity = new Vector3(horizontalMove * speed, rb.velocity.y);

        if (horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }

    }
    void SwitchAnim()//¶¯»­ÇÐ»»
    {
        anim.SetFloat("running", Mathf.Abs(rb.velocity.x));
    }
}
