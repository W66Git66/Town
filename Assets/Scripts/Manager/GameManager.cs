using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
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

    public AudioClip uiClick;
    public AudioClip playerInjured;
    public AudioClip talkClick;
    public AudioClip gameFailed;
    public AudioClip lightFire;
    public AudioClip footBgm;
    public AudioClip chuMo;

    //设置UI界面
    public GameObject uiPanel;

    public List<Transform> checkPoints;
    private List<GameObject> gfirePoints=new List<GameObject>();
    public GameObject gfire;

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
        if(gfireNumber>=3)
        {
            Invoke("TransToDay", 1f);
        }
    }

    public void TransToNight()
    {
        DataSaveManager.Instance.UnShenSheBack();
        StartCoroutine(TransMove());
        EventCenter.Broadcast(EventType.teleport, sceneDay, SceneNight);
        PlayerController.Instance.transform.position = createNightPoint.position;
        myCameraConfiner.m_BoundingShape2D = nightBoard;
        PlayerController.Instance.speed = 10;
        StartCoroutine(MakePoints());
    }

    public void TransToDay()
    {
        StartCoroutine(TransMove());
        EventCenter.Broadcast(EventType.teleport, SceneNight, sceneDay);
        PlayerController.Instance.transform.position = createDayPoint.position;
        myCameraConfiner.m_BoundingShape2D = dayBoard;
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
        StartCoroutine(TransMove());
        EventCenter.Broadcast(EventType.teleport, sceneHouse, sceneDay);
        PlayerController.Instance.transform.position = createDayPoint.position;
        myCameraConfiner.m_BoundingShape2D = dayBoard;
        PlayerController.Instance.speed = 10;
    }

    IEnumerator TransMove()
    {
        PlayerController.Instance.TransMove(false);
        yield return new WaitForSeconds(1f);
        PlayerController.Instance.TransMove(true);
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

    IEnumerator MakePoints()
    {
        for (int i = 0; i < 3; i++)
        {
            int num = Random.Range(0, 8);
            var obj=Instantiate(gfire, checkPoints[num].position, Quaternion.identity, transform);
            gfirePoints.Add(obj);
        }
        yield return new WaitForSeconds(1f);
    }
}
