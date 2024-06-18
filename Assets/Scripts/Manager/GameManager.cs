using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : Singleton<GameManager>
{
    private int day;//游戏的天数流程

    [Header("Camera相关")]
    public CinemachineConfiner2D myCameraConfiner;//相机边界
    public Collider2D dayBoard;//白天的相机边界
    public Collider2D nightBoard;//夜晚


    public Transform createNightPoint;
    public Transform createDayPoint;

    public string sceneDay;
    public string SceneNight;

    //设置UI界面
    public GameObject uiPanel;

    public List<Transform> checkPoints;
    private List<GameObject> gfirePoints=new List<GameObject>();
    public GameObject gfire;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
    private void Update()
    {
    }

    public void TransToNight()
    {
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
        foreach(var item in gfirePoints)
        {
            Destroy(item);
        }

        DataSaveManager.Instance.NightReSet();
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
