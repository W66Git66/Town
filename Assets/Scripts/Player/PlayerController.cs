using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;
    public GameObject knife;

    public float speed;
    public GameObject GetToothTiShi;//获得假牙提示
    public GameObject ToothTanchuang;//第一次弹窗
    public GameObject enterHouseTiShi;//进入自己家提示
    public GameObject goToSleepTiShi;//入眠提示
    public CanvasGroup backselfUI;
    public CanvasGroup bebeatenUI;
    public float horizontalMove;
    private float verticalMove;

    private Vector2 direction;

    private bool isMove=true;
    public GameObject magicCircle;

    private float shootDur = 0;

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
        if (Input.GetKeyDown(KeyCode.J)&&DataSaveManager.Instance.isScareBeated && DataSaveManager.Instance.liveBird >= 1)
        {
                DataSaveManager.Instance.UseLiveBird();
                var obj=Instantiate(magicCircle, new Vector2(transform.position.x,transform.position.y+8), Quaternion.identity);
                Destroy(obj,7f);
        }
        if(DataSaveManager.Instance.isChumoZhu == true)
        {
            shootDur += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.K)&&shootDur>1f)
            {
                shootDur = 0;
                Instantiate(knife,transform.position, Quaternion.identity);
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
            StartCoroutine(LoseUI());
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
                StartCoroutine(WinUI());
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

    private IEnumerator WinUI()
    {
        yield return TransformSceneManager.Instance.FadeUI(backselfUI, 1, 1);
        //yield return new WaitForSeconds (2f);
        yield return TransformSceneManager.Instance.FadeUI(backselfUI, 1, 0);
        //backselfUI.gameObject.SetActive(true);
        //yield return new WaitForSeconds(2f);
        //backselfUI.gameObject.SetActive(false);
    }

    private IEnumerator LoseUI()
    {
        yield return TransformSceneManager.Instance.FadeUI(bebeatenUI, 1, 1);
        //yield return new WaitForSeconds (2f);
        yield return TransformSceneManager.Instance.FadeUI(bebeatenUI, 1, 0);
        //bebeatenUI.gameObject.SetActive(true);
        //yield return new WaitForSeconds(2f);
        //bebeatenUI.gameObject.SetActive(false);
    }
}
