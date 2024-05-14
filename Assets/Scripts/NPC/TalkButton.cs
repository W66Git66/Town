using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TalkButton : Singleton<TalkButton>
{
        private GameObject tipsButton;
        [Header("�Ի���")]
        public GameObject dialogBox=GameObject.Find("�Ի���");
       // [NonSerialized]
        public Dialogue dialogue;//�Ի�����

    private int dialogueIndex;

    public GameObject npcOnTrigger;
    public string npcOnTriggerName;

    //���ű����õ���ģʽ
    //public static TalkButton instance;

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

        dialogueIndex = other.GetComponent<DialogueIndex>().dialogueIndex;
        dialogue = other.GetComponent<NPC>().dialogue[dialogueIndex];

        if (childTransform != null)
        {
            // �ҵ���ָ�����Ƶ�������
            tipsButton = childTransform.gameObject;
            Debug.Log("Found child object: " + tipsButton.name);

            // ִ�в���
            tipsButton.SetActive(true);
            GameObject.Find("DataSaveManager").GetComponent<DialogueSystem>().SetDialogueBox();
        }
        else
        {
            Debug.Log("Child object not found.");
        }
    }

        private void OnTriggerExit2D(Collider2D other)
        {
            npcOnTrigger = null;
            tipsButton.SetActive(false);
            dialogBox.SetActive(false);
        }

        private void Update()
        {
           if (tipsButton != null && tipsButton.activeSelf && Input.GetKeyDown(KeyCode.E))
            {
               dialogBox.SetActive(true);
            }
        }
}