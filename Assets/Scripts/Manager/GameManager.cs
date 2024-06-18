using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : Singleton<GameManager>
{
    private int day;//��Ϸ����������

    [Header("Camera���")]
    public CinemachineConfiner2D myCameraConfiner;//����߽�
    public Collider2D dayBoard;//���������߽�
    public Collider2D nightBoard;//ҹ��


    public Transform createNightPoint;
    public Transform createDayPoint;

    public string sceneDay;
    public string SceneNight;

    //����UI����
    public GameObject uiPanel;

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
    }

    public void TransToDay()
    {
        StartCoroutine(TransMove());
        EventCenter.Broadcast(EventType.teleport, SceneNight, sceneDay);
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

    //������UI
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

    //�˳���Ϸ
    public void ExitGame()
    {
        Application.Quit();
    }

}
