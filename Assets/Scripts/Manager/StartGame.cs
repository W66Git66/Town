using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    //��Ϸ��ʼʱ��ɫλ��
    public Transform dayPoint;

    //������
    public string Day;
    public string Load;

    //��ʼ��Ϸ
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
