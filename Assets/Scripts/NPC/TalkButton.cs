using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TalkButton : Singleton<TalkButton>
{
        public GameObject[] tipsButtons;//�Ի���ʾ��ť
        [Header("�Ի���")]
        public GameObject dialogBox;
        [NonSerialized]
        public Dialogue dialogue;//�Ի�����

    private bool isAnyActive=false;//�ж��Ƿ��жԻ���ť���ڻ״̬

    //���ű����õ���ģʽ
    public static TalkButton instance;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    private void Start()
    {
        tipsButtons = new GameObject[6];//��������ʾ���·�tag�б�˳������
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("�峤"))
        {
            tipsButtons[0].SetActive(true);
        }
        if (other.CompareTag("��Ȯ"))
        {
            tipsButtons[1].SetActive(true);
        }
        if (other.CompareTag("������"))
        {
            tipsButtons[2].SetActive(true);
        }
        if (other.CompareTag("�÷�"))
        {
            tipsButtons[3].SetActive(true);
        }
        if (other.CompareTag("����"))
        {
            tipsButtons[4].SetActive(true);
        }
        if (other.CompareTag("������"))
        {
            tipsButtons[5].SetActive(true);
        }
        //tipsButton.SetActive(true);
        dialogue = other.GetComponent<NPC>().dialogue;
    }

        private void OnTriggerExit2D(Collider2D other)
        {
            foreach(GameObject obj in tipsButtons)
            {
              obj.SetActive(false);
            }
            //tipsButton.SetActive(false);
            dialogBox.SetActive(false);
        }

        private void Update()
        {
            foreach(GameObject obj in tipsButtons)
            {
               if(obj != null&&obj.activeSelf)
               {
                 isAnyActive = true;
                 break;
               }
            }
            if (tipsButtons != null && isAnyActive && Input.GetKeyDown(KeyCode.E))
            {
               dialogBox.SetActive(true);
            }
        }

}
