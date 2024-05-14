using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TalkButton : Singleton<TalkButton>
{
        private GameObject tipsButton;
        [Header("对话框")]
        public GameObject dialogBox=GameObject.Find("对话框");
       // [NonSerialized]
        public Dialogue dialogue;//对话内容

    private int dialogueIndex;

    public GameObject npcOnTrigger;
    public string npcOnTriggerName;

    //给脚本设置单例模式
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
        // 获取触发器事件对应的物体
        GameObject triggeredObject = other.gameObject;

        Transform childTransform = triggeredObject.transform.Find("对话提示");

        dialogueIndex = other.GetComponent<DialogueIndex>().dialogueIndex;
        dialogue = other.GetComponent<NPC>().dialogue[dialogueIndex];

        if (childTransform != null)
        {
            // 找到了指定名称的子物体
            tipsButton = childTransform.gameObject;
            Debug.Log("Found child object: " + tipsButton.name);

            // 执行操作
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