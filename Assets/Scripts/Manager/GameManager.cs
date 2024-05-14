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


    public Transform createPoint;

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
        PlayerController.Instance.transform.position = createPoint.position;
        myCameraConfiner.m_BoundingShape2D = nightBoard;
    }

    public void TransToDay()
    {
        EventCenter.Broadcast(EventType.teleport, SceneNight, sceneDay);
        PlayerController.Instance.transform.position = Vector2.zero;
        myCameraConfiner.m_BoundingShape2D = dayBoard;
    }
}
