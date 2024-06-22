using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private int day;//游戏的天数流程
    private AudioSource audioSource;

    [Header("Camera相关")]
    public CinemachineConfiner2D myCameraConfiner;//相机边界
    public Collider2D dayBoard;//白天的相机边界
    public Collider2D nightBoard;//夜晚
    public Collider2D houseBoard;


    public Transform createNightPoint;
    public Transform createDayPoint;
    public Transform createHousePoint;

    public string sceneDay;
    public string SceneNight;
    public string sceneHouse;
    public string sceneXinshou;

    public AudioClip uiClick;
    public AudioClip playerInjured;
    public AudioClip talkClick;
    public AudioClip gameFailed;
    public AudioClip lightFire;
    public AudioClip footBgm;
    public AudioClip chuMo;

    //转换UI界面（被怪捶死/神社传回）
    public CanvasGroup beBeatenUI;
    public CanvasGroup sendSelfUI;

    //设置UI界面
    public GameObject uiPanel;

    public List<Transform> checkPoints;
    private List<GameObject> gfirePoints=new List<GameObject>();
    public GameObject gfire;
    public GameObject shenShe;

    public int n, m,p;//麻雀数量

    public GameObject TanchuangBird;
    public GameObject TanchuangZhu;
    public GameObject TanchuangScare;
    public GameObject TanchuangGou;
    public GameObject TanchuangGhost;

    public GameObject numBirdbox;
    public Text numBird;

    public int gfireNumber = 0;

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    private void Start()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
    private void Update()
    {
        if (DataSaveManager.Instance.isProteinEverbeenFound)
        {
            numBirdbox.SetActive(true);
        }
        {
            
        }
        if (DataSaveManager.Instance.liveBird > DataSaveManager.Instance.deadBird)
        {
            p=DataSaveManager.Instance.liveBird;
        }
        else
        {
            p = DataSaveManager.Instance.deadBird;
        }
        numBird.text = "当前剩余麻雀数： " + p.ToString();
    }

    public void TransToNight()
    {
        DataSaveManager.Instance.UnShenSheBack();
        DataSaveManager.Instance.YetGivenBird();
        StartCoroutine(TransMove());
        EventCenter.Broadcast(EventType.teleport, sceneDay, SceneNight);
        PlayerController.Instance.transform.position = createNightPoint.position;
        myCameraConfiner.m_BoundingShape2D = nightBoard;
        PlayerController.Instance.speed = 10;
        StartCoroutine(MakePoints(checkPoints));
    }

    public void TransHouseToNight()
    {
        DataSaveManager.Instance.UnShenSheBack();
        DataSaveManager.Instance.YetGivenBird();
        StartCoroutine(TransMove());
        EventCenter.Broadcast(EventType.teleport, sceneHouse, SceneNight);
        PlayerController.Instance.transform.position = createNightPoint.position;
        myCameraConfiner.m_BoundingShape2D = nightBoard;
        PlayerController.Instance.speed = 10;
        StartCoroutine(MakePoints(checkPoints));
    }

    public void TransToDay()
    {
        if (DataSaveManager.Instance.isJirouBack)
        {
            n = Random.Range(3, 6);//随机3-5个
            m = Random.Range(3, 6);
        }
        else
        {
            n = Random.Range(1, 4);//随机1-3个
            m = Random.Range(1, 4);
        }
        StartCoroutine(TransMove());
        EventCenter.Broadcast(EventType.teleport, SceneNight, sceneHouse);
        PlayerController.Instance.transform.position = createHousePoint.position;
        myCameraConfiner.m_BoundingShape2D = houseBoard;
        PlayerController.Instance.speed = 10;
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Arrow");
        foreach (GameObject obj in objects)
        {
            obj.GetComponent<Arrow>().DestroyArrow();
        }
        foreach(var item in gfirePoints)
        {
            Destroy(item);
        }
        gfireNumber = 0;
        DataSaveManager.Instance.NightReSet();
        var obj1 = PlayerController.Instance.transform.GetChild(2).gameObject;
        obj1.SetActive(false);
    }

    public void TransToDayHouse()
    {
        StartCoroutine(TransMove());
        EventCenter.Broadcast(EventType.teleport, sceneDay, sceneHouse);
        PlayerController.Instance.transform.position = createHousePoint.position;
        myCameraConfiner.m_BoundingShape2D = houseBoard;
        PlayerController.Instance.speed = 10;
    }

    public void TransToHouseDay()
    {
        if (DataSaveManager.Instance.isProteinEverbeenFound && !DataSaveManager.Instance.isGivenBird)
        {
            DataSaveManager.Instance.GetDeadBird(n);
            if (DataSaveManager.Instance.isShenSheBack)
            {
                DataSaveManager.Instance.GetDeadBird(m);
            }
            DataSaveManager.Instance.HaveGivenBird();
            if (!DataSaveManager.Instance.isFirstGiveBird)
            {
                TanchuangBird.SetActive(true);
                DataSaveManager.Instance.FirstGiveBird();
            }
        }
        StartCoroutine(TransMove());
        EventCenter.Broadcast(EventType.teleport, sceneHouse, sceneDay);
        PlayerController.Instance.transform.position = createDayPoint.position;
        myCameraConfiner.m_BoundingShape2D = dayBoard;
        PlayerController.Instance.speed = 10;
    }

    public void TransXinshouToHouse()
    {
        StartCoroutine(TransMove());
        EventCenter.Broadcast(EventType.teleport, sceneXinshou, sceneHouse);
        PlayerController.Instance.transform.position = createHousePoint.position;
        myCameraConfiner.m_BoundingShape2D = houseBoard;
        PlayerController.Instance.speed = 10;
        if (!TransformSceneManager.Instance.isFade1)
            {
                StartCoroutine(DieScene());
            }
        
    }
    IEnumerator TransMove()
    {
        PlayerController.Instance.TransMove(false);
        yield return new WaitForSeconds(1f);
        PlayerController.Instance.TransMove(true);
        var obj1 = PlayerController.Instance.transform.GetChild(2).gameObject;
        obj1.SetActive(false);
    }

    //打开设置UI
    public void UIPanel()
    {
        ChangeAudioClip(uiClick);
        PlaySound();
        if(uiPanel == null)
        {
            return;
        }

        if(uiPanel.activeSelf)
        {
            uiPanel.SetActive(false);
            Time.timeScale= 1f;
        }
        else
        {
            uiPanel.SetActive(true);
            Time.timeScale= 0f;
        }
    }

    //退出游戏
    public void ExitGame()
    {
        Application.Quit();
    }

    public void ChangeAudioClip(AudioClip clip)
    {
        audioSource.clip = clip;
    }

    public void PlaySound()
    {
        audioSource.Play();
    }

    public void TanChuangZhu()
    {
        StartCoroutine(TanChuangZhuYanShi());
        
    }
    IEnumerator TanChuangZhuYanShi()
    {
        yield return new WaitForSeconds(1.5f);
        TanchuangZhu.SetActive(true);
    }

    public void TanChuangScare()
    {
        TanchuangScare.SetActive(true);
    }

    public void TanChuangGou()
    {
        TanchuangGou.SetActive(true);
    }

    public void TanChuangGhost()
    {
        TanchuangGhost.SetActive(true);
    }

    IEnumerator DieScene()
    {
        //beBeatenUI.gameObject.SetActive(true);
        yield return TransformSceneManager.Instance.FadeUI(beBeatenUI, 1, 1);
        //yield return new WaitForSeconds (2f);
        yield return TransformSceneManager.Instance.FadeUI(beBeatenUI, 1, 0);
    }

    IEnumerator MakePoints(List<Transform> checkPoints1)
    {
        for (int i=0;i<3;i++)
        {
            var obj=Instantiate(gfire, checkPoints1[i].position, Quaternion.identity, transform);
            gfirePoints.Add(obj);
        }
        var obj1=Instantiate(shenShe, checkPoints1[3].position, Quaternion.identity, transform);
        gfirePoints.Add(obj1);
        yield return new WaitForSeconds(1f);
    }
}
