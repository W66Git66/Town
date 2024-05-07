using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkButton : Singleton<TalkButton>
{
        public GameObject tipsButton;//对话提示按钮
        [Header("对话框")]
        public GameObject dialogBox;
        [NonSerialized]
        public Dialogue dialogue;//对话内容

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        dialogue = other.GetComponent<NPC>().dialogue;
        tipsButton.SetActive(true);
    }

        private void OnTriggerExit2D(Collider2D other)
        {
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
