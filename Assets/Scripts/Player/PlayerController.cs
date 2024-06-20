using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;

    public float speed;
    public GameObject GetToothTiShi;//获得假牙提示
    public GameObject ToothTanchuang;//第一次弹窗
    public GameObject enterHouseTiShi;//进入自己家提示
    public GameObject goToSleepTiShi;//入眠提示
    private float horizontalMove;
    private float verticalMove;

    private Vector2 direction;

    private bool isMove=true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

        GetToothTiShiShut();
    }

    private void Update()
    {
        if (isMove == true)
        {
            GroundMovement();
            SwitchAnim();
            
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        if(enterHouseTiShi!=null&&enterHouseTiShi.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameManager.Instance.TransToDayHouse();
            }
        }

        if (goToSleepTiShi != null && goToSleepTiShi.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameManager.Instance.TransHouseToNight();
            }
        }
    }

    private void FixedUpdate()
    {
        
    }

    void GroundMovement()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");//只返回-1，0，1
        verticalMove = Input.GetAxisRaw("Vertical");
        direction = new Vector2(horizontalMove, verticalMove);       
        rb.velocity = direction*speed;
    }
    void SwitchAnim()//动画切换
    {
        anim.SetFloat("Xmove", horizontalMove);
        anim.SetFloat("Ymove", verticalMove);
        anim.SetFloat("speed", direction.magnitude);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("board"))
        {
            GameManager.Instance.TransToNight();
        }
        if (collision.CompareTag("board1"))
        {
            GameManager.Instance.TransToHouseDay();
        }
        if (collision.CompareTag("enterHouse"))
        {
            enterHouseTiShi.SetActive(true);
        }
        if(collision.CompareTag("monster"))
        {
            GameManager.Instance.TransToDay();
        }
        if (collision.CompareTag("鬼火"))
        {
            collision.GetComponent<Gfire>().ChangeSprite();
        }
        if (collision.gameObject.CompareTag("假牙"))
        {
            if (!DataSaveManager.Instance.isFakeToothFind)
            {
                ToothTanchuang.SetActive(true);
            }
            else
            {
                GetToothTiShi.SetActive(true);
            }
            DataSaveManager.Instance.isFakeToothFind = true;
            Destroy(collision.gameObject);
            Invoke("GetToothTiShiShut", 2f);
        }

        if (collision.CompareTag("XinshouMonster"))
        {
            GameManager.Instance.TransXinshouToHouse();
        }
        if (collision.CompareTag("神社"))
        {
            if(GameManager.Instance.gfireNumber>=3)
            {
                GameManager.Instance.TransToDay();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bed"))
        {
            goToSleepTiShi.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bed"))
        {
            goToSleepTiShi.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("enterHouse"))
        {
            enterHouseTiShi.SetActive(false);
        }
    }

    public void TransMove(bool ismove)
    {
        isMove = ismove;
    }

    public void GetToothTiShiShut()
    {
        GetToothTiShi.SetActive(false);
    }
}
