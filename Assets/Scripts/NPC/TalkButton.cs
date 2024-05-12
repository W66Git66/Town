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

    private int dialogueIndex;

    private bool isAnyActive=false;//�ж��Ƿ��жԻ���ť���ڻ״̬

    public GameObject npcOnTrigger;
    public string npcOnTriggerName;

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
        npcOnTriggerName=other.name;
        npcOnTrigger = other.gameObject;
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
        dialogueIndex=other.GetComponent<DialogueIndex>().dialogueIndex;
        dialogue = other.GetComponent<NPC>().dialogue[dialogueIndex];
    }

        private void OnTriggerExit2D(Collider2D other)
        {
            npcOnTrigger = null;
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