using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TalkButton : Singleton<TalkButton>
{
        private GameObject tipsButton;
        [Header("对话框")]
        public GameObject dialogBox;
        [NonSerialized]
        public Dialogue dialogue;//对话内容

    private bool isAnyActive=false;//判断是否有对话按钮处于活动状态

    //给脚本设置单例模式
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
        // 获取触发器事件对应的物体
        GameObject triggeredObject = other.gameObject;

        Transform childTransform = triggeredObject.transform.Find("对话提示");

        if (childTransform != null)
        {
            // 找到了指定名称的子物体
            tipsButton = childTransform.gameObject;
            Debug.Log("Found child object: " + tipsButton.name);

            // 执行操作
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
