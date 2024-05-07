using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Dialog;

public class DialogueSystem : MonoBehaviour
{
        private Dialogue dialogue;//�Ի�����
                                  //����
        private int index;

        //�Ի����ݿ�
        //TextMeshProUGUI dialogueContent;
        Text dialogueContent;
        //���ƿ�
        //TextMeshProUGUI dialogueName;
        Text dialogueName;
        //ͷ���
        Image dialogueImage;

        private void Awake()
        {
            //gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            dialogue = TalkButton.instance.dialogue;
            dialogueContent = transform.Find("����").GetComponent<Text>();
            dialogueName = transform.Find("����").GetComponent<Text>();
            dialogueImage = transform.Find("ͷ��").GetComponent<Image>();

            //��������ͷ�񱣳ֿ�߱ȣ���ֹѹ������
            dialogueImage.preserveAspect = true;

            index = 0;
            Play();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && dialogue != null)
            {
                //�Ի������꣬�رնԻ�
                if (index == dialogue.dialogNodes.Length)
                {
                    gameObject.SetActive(false);
                    index = 0;
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
