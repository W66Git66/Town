using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    //��Ϸ��ʼʱ��ɫλ��
    public Transform nightPoint;

    //������
    public string xinshouGuan;
    public string Load;

    //��ʼ��Ϸ
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
