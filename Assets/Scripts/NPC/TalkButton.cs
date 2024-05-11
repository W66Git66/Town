using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TalkButton : Singleton<TalkButton>
{
        private GameObject tipsButton;
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
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ��ȡ�������¼���Ӧ������
        GameObject triggeredObject = other.gameObject;

        Transform childTransform = triggeredObject.transform.Find("�Ի���ʾ");

        if (childTransform != null)
        {
            // �ҵ���ָ�����Ƶ�������
            tipsButton = childTransform.gameObject;
            Debug.Log("Found child object: " + tipsButton.name);

            // ִ�в���
            tipsButton.SetActive(true);
        }
        else
        {
            Debug.Log("Child object not found.");
        }

        dialogue = other.GetComponent<NPC>().dialogue;
    }

        private void OnTriggerExit2D(Collider2D other)
        {
            tipsButton.SetActive(false);
            dialogBox.SetActive(false);
        }

        private void Update()
        {
           if(tipsButton != null&&tipsButton.activeSelf)
        {
            isAnyActive = true;
        }
           if (tipsButton != null && isAnyActive && Input.GetKeyDown(KeyCode.E))
            {
               dialogBox.SetActive(true);
            }
        }

}
