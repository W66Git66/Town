using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TalkButton : Singleton<TalkButton>
{
        public GameObject[] tipsButtons;//对话提示按钮
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
        tipsButtons = new GameObject[6];//这六个提示按下方tag列表顺序排列
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("村长"))
        {
            tipsButtons[0].SetActive(true);
        }
        if (other.CompareTag("柴犬"))
        {
            tipsButtons[1].SetActive(true);
        }
        if (other.CompareTag("老奶奶"))
        {
            tipsButtons[2].SetActive(true);
        }
        if (other.CompareTag("裁缝"))
        {
            tipsButtons[3].SetActive(true);
        }
        if (other.CompareTag("屠夫"))
        {
            tipsButtons[4].SetActive(true);
        }
        if (other.CompareTag("稻草人"))
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
