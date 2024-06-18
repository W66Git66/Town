using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JiaoFu : MonoBehaviour
{
    public Text payMaque;
    private int live;
    private int dead;

    public GameObject yesButton;
    public GameObject noButton;
    void Start()
    {
        
    }

    void Update()
    {
        live = DataSaveManager.Instance.liveBird;
        dead = DataSaveManager.Instance.deadBird;
        if (live > dead)
        {
            payMaque.text = "�Ƿ�����һֻ��ȸ��ʣ������������ȸ���� " + live.ToString()+" )";
        }
        else
        {
            payMaque.text = "�Ƿ�����һֻ��ȸ��ʣ������������ȸ���� " + dead.ToString()+" )";
        }

    }

    public void ClickYes()
    {
        if(live > dead)
        {
            DataSaveManager.Instance.UseLiveBird();
        }
        else
        {
            DataSaveManager.Instance.UseDeadBird();
        }
        gameObject.SetActive(false);
    }

    public void ClickNo()
    {
        gameObject.SetActive(false);
    }
}
