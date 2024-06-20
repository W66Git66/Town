using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    //游戏开始时角色位置
    public Transform nightPoint;

    //场景名
    public string xinshouGuan;
    public string Load;

    //开始游戏
    public void GameStart()
    {
        StartCoroutine(TransMove());
        EventCenter.Broadcast(EventType.teleport, Load, xinshouGuan);
        PlayerController.Instance.transform.position = nightPoint.position;
    }

    public void GameEnd()
    {
        Application.Quit();
    }

    IEnumerator TransMove()
    {
        PlayerController.Instance.TransMove(false);
        yield return new WaitForSeconds(0.5f);
        PlayerController.Instance.TransMove(true);
    }
}
