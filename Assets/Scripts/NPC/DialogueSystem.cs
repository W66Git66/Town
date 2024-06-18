using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Dialog;

public class DialogueSystem : MonoBehaviour
{
        private Dialogue dialogue;//对话内容
                                  
        private int index;//索引

        public GameObject dialogueBox;//获取对话框


        //对话内容框
        //TextMeshProUGUI dialogueContent;
        Text dialogueContent;
        //名称框
        //TextMeshProUGUI dialogueName;
        Text dialogueName;
        //头像框
        Image dialogueImage;

        private string npcOnTalkingName; //正与之对话或即将对话的npc

        private void Awake()
        {
            dialogueBox.SetActive(false);
        }

    public void SetDialogueBox()
    {
        dialogue = TalkButton.Instance.dialogue;
        dialogueContent = dialogueBox.transform.Find("内容").GetComponent<Text>();
        dialogueName = dialogueBox.transform.Find("名字").GetComponent<Text>();
        dialogueImage = dialogueBox.transform.Find("头像").GetComponent<Image>();

        //设置人物头像保持宽高比，防止压缩变形
        dialogueImage.preserveAspect = true;

        index = 0;

        Play();

    }
    private void Update()
        {
            npcOnTalkingName= GameObject.Find("FollowPlayer").GetComponent<TalkButton>().npcOnTriggerName;
            //npcOnTalking = GameObject.Find("Player").GetComponent<TalkButton>().npcOnTrigger;//获取正触发trigger的npc
            if (Input.GetKeyDown(KeyCode.Mouse0) && dialogue != null)
            {
                //对话播放完，关闭对话
                if (index == dialogue.dialogNodes.Length)
                {
                    dialogueBox.SetActive(false);
                    index = 0;
                    switch (npcOnTalkingName)
                    {
                       case "柴犬":
                        //柴犬对话的三个一次性
                        if (GameObject.Find("柴犬").GetComponent<DialogueIndex>().dialogueIndex == 0 || GameObject.Find("柴犬").GetComponent<DialogueIndex>().dialogueIndex == 2 || GameObject.Find("柴犬").GetComponent<DialogueIndex>().dialogueIndex == 4)
                        {
                            GameObject.Find("柴犬").GetComponent<DialogueIndex>().AddIndex();
                        }
                        //柴犬的两个任务（在DogIndex里）
                        break;

                        case "村长":
                        break;

                        case "老奶奶":
                        //老奶奶对话的两个一次性
                        if(GameObject.Find("老奶奶").GetComponent<DialogueIndex>().dialogueIndex==0|| GameObject.Find("老奶奶").GetComponent<DialogueIndex>().dialogueIndex == 2)
                        {
                            GameObject.Find("老奶奶").GetComponent<DialogueIndex>().AddIndex();
                        }
                        //老奶奶的一个任务
                        if(GameObject.Find("老奶奶").GetComponent<DialogueIndex>().dialogueIndex == 1 && DataSaveManager.Instance.isFakeToothFind)
                        {
                            GameObject.Find("老奶奶").GetComponent<DialogueIndex>().AddIndex();
                        }
                        break;

                        case "裁缝":
                        //裁缝对话的两个一次性
                        if(GameObject.Find("裁缝").GetComponent<DialogueIndex>().dialogueIndex==0|| GameObject.Find("裁缝").GetComponent<DialogueIndex>().dialogueIndex == 1)
                        {
                            GameObject.Find("裁缝").GetComponent<DialogueIndex>().AddIndex();
                        }
                        break;

                        case "屠夫":
                        //屠夫对话的一个一次性
                        if(GameObject.Find("屠夫").GetComponent<DialogueIndex>().dialogueIndex == 0)
                        {
                            GameObject.Find("屠夫").GetComponent<DialogueIndex>().AddIndex();
                        }
                        //屠夫的一个任务
                        if (GameObject.Find("屠夫").GetComponent<DialogueIndex>().dialogueIndex == 1 && DataSaveManager.Instance.isKnifeOn)
                        {
                            GameObject.Find("屠夫").GetComponent<DialogueIndex>().AddIndex();
                        }
                        break;

                        case "稻草人":
                        //稻草人对话的两个一次性
                        if(GameObject.Find("稻草人").GetComponent<DialogueIndex>().dialogueIndex == 0|| GameObject.Find("稻草人").GetComponent<DialogueIndex>().dialogueIndex == 2)
                        {
                            GameObject.Find("稻草人").GetComponent<DialogueIndex>().AddIndex();
                        }
                        //稻草人的一个任务(在ScarecrowIndex里）
                        break;
                    }

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
