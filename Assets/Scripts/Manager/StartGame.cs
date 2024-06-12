using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    //游戏开始时角色位置
    public Transform dayPoint;

    //场景名
    public string Day;
    public string Load;

    //开始游戏
    public void GameStart()
    {
        StartCoroutine(TransMove());
        EventCenter.Broadcast(EventType.teleport, Load, Day);
        PlayerController.Instance.transform.position = dayPoint.position;
    }

    IEnumerator TransMove()
    {
        PlayerController.Instance.TransMove(false);
        yield return new WaitForSeconds(1f);
        PlayerController.Instance.TransMove(true);
    }
}
