using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Dialog;

public class DialogueSystem : MonoBehaviour
{
        private Dialogue dialogue;//�Ի�����
                                  
        private int index;//����

        public GameObject dialogueBox;//��ȡ�Ի���


        //�Ի����ݿ�
        //TextMeshProUGUI dialogueContent;
        Text dialogueContent;
        //���ƿ�
        //TextMeshProUGUI dialogueName;
        Text dialogueName;
        //ͷ���
        Image dialogueImage;

        private string npcOnTalkingName;
        private GameObject npcOnTalking;   //����֮�Ի��򼴽��Ի���npc

        private void Awake()
        {
            dialogueBox.SetActive(false);
        }

        private void OnEnable()
        {
            
        }

    public void SetDialogueBox()
    {
        dialogue = TalkButton.Instance.dialogue;
        dialogueContent = dialogueBox.transform.Find("����").GetComponent<Text>();
        dialogueName = dialogueBox.transform.Find("����").GetComponent<Text>();
        dialogueImage = dialogueBox.transform.Find("ͷ��").GetComponent<Image>();

        //��������ͷ�񱣳ֿ��߱ȣ���ֹѹ������
        dialogueImage.preserveAspect = true;

        index = 0;

        Play();

    }
    private void Update()
        {
            npcOnTalkingName= GameObject.Find("Player").GetComponent<TalkButton>().npcOnTriggerName;
            //npcOnTalking = GameObject.Find("Player").GetComponent<TalkButton>().npcOnTrigger;//��ȡ������trigger��npc
            if (Input.GetKeyDown(KeyCode.Mouse0) && dialogue != null)
            {
                //�Ի������꣬�رնԻ�
                if (index == dialogue.dialogNodes.Length)
                {
                    dialogueBox.SetActive(false);
                    index = 0;
                    switch (npcOnTalkingName)
                    {
                       case "��Ȯ":
                        //��Ȯ�Ի�������һ����
                        if (GameObject.Find("��Ȯ").GetComponent<DialogueIndex>().dialogueIndex == 0|| GameObject.Find("��Ȯ").GetComponent<DialogueIndex>().dialogueIndex == 2|| GameObject.Find("��Ȯ").GetComponent<DialogueIndex>().dialogueIndex == 4)
                        {
                            GameObject.Find("��Ȯ").GetComponent<DialogueIndex>().AddIndex();
                        }
                        //��Ȯ����������
                        if((GameObject.Find("��Ȯ").GetComponent<DialogueIndex>().dialogueIndex ==1&&GameObject.Find("��Ȯ").GetComponent<DialogueIndex>().isAlbumenPowderFind)||(GameObject.Find("��Ȯ").GetComponent<DialogueIndex>().dialogueIndex == 3 && GameObject.Find("DataSaveManager").GetComponent<DataSaveManager>().isFakeToothFind))
                        {
                            GameObject.Find("��Ȯ").GetComponent<DialogueIndex>().AddIndex();
                        }
                        break;

                        case "�峤":
                        break;

                        case "������":
                        //�����̶Ի�������һ����
                        if(GameObject.Find("������").GetComponent<DialogueIndex>().dialogueIndex==0|| GameObject.Find("������").GetComponent<DialogueIndex>().dialogueIndex == 2)
                        {
                            GameObject.Find("������").GetComponent<DialogueIndex>().AddIndex();
                        }
                        //�����̵�һ������
                        if(GameObject.Find("������").GetComponent<DialogueIndex>().dialogueIndex == 1 && GameObject.Find("DataSaveManager").GetComponent<DataSaveManager>().isFakeToothFind)
                        {
                            GameObject.Find("������").GetComponent<DialogueIndex>().AddIndex();
                        }
                        break;

                        case "�÷�":
                        //�÷�Ի�������һ����
                        if(GameObject.Find("�÷�").GetComponent<DialogueIndex>().dialogueIndex==0|| GameObject.Find("�÷�").GetComponent<DialogueIndex>().dialogueIndex == 1)
                        {
                            GameObject.Find("�÷�").GetComponent<DialogueIndex>().AddIndex();
                        }
                        break;

                        case "����":
                        //����Ի���һ��һ����
                        if(GameObject.Find("����").GetComponent<DialogueIndex>().dialogueIndex == 0)
                        {
                            GameObject.Find("����").GetComponent<DialogueIndex>().AddIndex();
                        }
                        //�����һ������
                        if (GameObject.Find("����").GetComponent<DialogueIndex>().dialogueIndex == 1 && GameObject.Find("DataSaveManager").GetComponent<DataSaveManager>().isKnifeOn)
                        {
                            GameObject.Find("����").GetComponent<DialogueIndex>().AddIndex();
                        }
                        break;

                        case "������":
                        //�����˶Ի�������һ����
                        if(GameObject.Find("������").GetComponent<DialogueIndex>().dialogueIndex == 0|| GameObject.Find("������").GetComponent<DialogueIndex>().dialogueIndex == 2)
                        {
                            GameObject.Find("������").GetComponent<DialogueIndex>().AddIndex();
                        }
                        //�����˵�һ������
                        if(GameObject.Find("������").GetComponent<DialogueIndex>().dialogueIndex == 1 && GameObject.Find("DataSaveManager").GetComponent<DataSaveManager>().liveBird > 0)
                        {
                            GameObject.Find("������").GetComponent<DialogueIndex>().AddIndex();
                        }
                        break;
                    }

                }
                else
                {
                    //��ʼ�Ի�
                    Play();
            }
            }
        }

        // Play �������ڿ�ʼ���ŶԻ���
        private void Play()
        {
        // ��ȡ��ǰ�Ի��ڵ㣬����������ֵ��
        DialogNode node = dialogue.dialogNodes[index++];
        
            // ���öԻ����ݡ���ɫ���ƺ�ͷ��
            dialogueContent.text = node.content;
            dialogueName.text = node.name;
            dialogueImage.sprite = node.sprite;
        }
}