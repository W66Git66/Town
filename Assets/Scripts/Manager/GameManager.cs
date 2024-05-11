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

    public GameObject monster;

    public string sceneDay;
    public string SceneNight;

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
        EventCenter.Broadcast(EventType.teleport, sceneDay, SceneNight);
        PlayerController.Instance.transform.position = Vector2.zero;
        myCameraConfiner.m_BoundingShape2D = nightBoard;
        monster.SetActive(true);
    }

    public void TransToDay()
    {
        EventCenter.Broadcast(EventType.teleport, SceneNight, sceneDay);
        PlayerController.Instance.transform.position = Vector2.zero;
        myCameraConfiner.m_BoundingShape2D = dayBoard;
        monster.SetActive(false);
    }
}
