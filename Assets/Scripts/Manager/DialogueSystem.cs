using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Dialog;

public class DialogueSystem : MonoBehaviour
{
        private Dialogue dialogue;//对话内容
                                  //索引
        private int index;

        //对话内容框
        //TextMeshProUGUI dialogueContent;
        Text dialogueContent;
        //名称框
        //TextMeshProUGUI dialogueName;
        Text dialogueName;
        //头像框
        Image dialogueImage;

        private void Awake()
        {
            //gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            dialogue = TalkButton.instance.dialogue;
            dialogueContent = transform.Find("内容").GetComponent<Text>();
            dialogueName = transform.Find("名字").GetComponent<Text>();
            dialogueImage = transform.Find("头像").GetComponent<Image>();

            //设置人物头像保持宽高比，防止压缩变形
            dialogueImage.preserveAspect = true;

            index = 0;
            Play();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && dialogue != null)
            {
                //对话播放完，关闭对话
                if (index == dialogue.dialogNodes.Length)
                {
                    gameObject.SetActive(false);
                    index = 0;
                }
                else
                {
                    //开始对话
                    Play();
                }
            }
        }

        // Play 函数用于开始播放对话。
        private void Play()
        {
            // 获取当前对话节点，并更新索引值。
            DialogNode node = dialogue.dialogNodes[index++];

            // 设置对话内容、角色名称和头像
            dialogueContent.text = node.content;
            dialogueName.text = node.name;
            dialogueImage.sprite = node.sprite;
        }
}
