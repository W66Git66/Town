using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Dialog;

public class DialogueSysYinDao : MonoBehaviour
{
    [Header("�Ի���")]
    public GameObject dialogueBox;
    // [NonSerialized]
    public Dialogue dialogue;//�Ի�����

    private int dialogueIndex;

    public GameObject npcOnTrigger;
    public GameObject xinshouYindao;
    public GameObject cunzhangYindao;
    public GameObject heiyeYindao;
    public GameObject chumoScareYinDao;
    public GameObject ghostYinDao;

    private int index;//����


    //�Ի����ݿ�
    //TextMeshProUGUI dialogueContent;
    Text dialogueContent;
    //���ƿ�
    //TextMeshProUGUI dialogueName;
    Text dialogueName;
    //ͷ���
    Image dialogueImage;

    private void Start()
    {
        Invoke("XinShouYinDaoVar", 1.5f);
        Invoke("CunZhangYinDaoVar", 1.5f);
        Invoke("HeiYeYinDaoVar", 1.5f);
    }

    public void XinShouYinDaoVar()
    {
        if (xinshouYindao != null)
        {
            npcOnTrigger = xinshouYindao;
            dialogueIndex = xinshouYindao.GetComponent<DialogueIndex>().dialogueIndex;
            dialogue = xinshouYindao.GetComponent<NPC>().dialogue[dialogueIndex];
            Time.timeScale = 0;
            SetDialogueBox();
            dialogueBox.SetActive(true);
        }
        else
        {
            return;
        }
    }

    public void CunZhangYinDaoVar()
    {
        if (cunzhangYindao != null)
        {
            npcOnTrigger = cunzhangYindao;
            dialogueIndex = cunzhangYindao.GetComponent<DialogueIndex>().dialogueIndex;
            dialogue = cunzhangYindao.GetComponent<NPC>().dialogue[dialogueIndex];
            Time.timeScale = 0;
            SetDialogueBox();
            dialogueBox.SetActive(true);
        }
        else
        {
            return;
        }
    }

    public void HeiYeYinDaoVar()
    {
        if (heiyeYindao != null)
        {
            npcOnTrigger = heiyeYindao;
            dialogueIndex = heiyeYindao.GetComponent<DialogueIndex>().dialogueIndex;
            dialogue = heiyeYindao.GetComponent<NPC>().dialogue[dialogueIndex];
            Time.timeScale = 0;
            SetDialogueBox();
            dialogueBox.SetActive(true);
        }
        else
        {
            return;
        }
    }

    public void ChuMoScareYinDaoVar()
    {
        if (chumoScareYinDao != null)
        {
            npcOnTrigger = chumoScareYinDao;
            dialogueIndex = chumoScareYinDao.GetComponent<DialogueIndex>().dialogueIndex;
            dialogue = chumoScareYinDao.GetComponent<NPC>().dialogue[dialogueIndex];
            Time.timeScale = 0;
            SetDialogueBox();
            dialogueBox.SetActive(true);
        }
        else
        {
            return;
        }
    }

    public void GhostYinDao()
    {
        if (ghostYinDao != null)
        {
            npcOnTrigger = ghostYinDao;
            dialogueIndex = ghostYinDao.GetComponent<DialogueIndex>().dialogueIndex;
            dialogue = ghostYinDao.GetComponent<NPC>().dialogue[dialogueIndex];
            Time.timeScale = 0;
            SetDialogueBox();
            dialogueBox.SetActive(true);
        }
        else
        {
            return;
        }
    }

    public void SetDialogueBox()
    {
        dialogueContent = dialogueBox.transform.Find("����").GetComponent<Text>();
        dialogueName = dialogueBox.transform.Find("����").GetComponent<Text>();
        dialogueImage = dialogueBox.transform.Find("ͷ��").GetComponent<Image>();

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
                dialogueBox.SetActive(false);
                Time.timeScale = 1;
                index = 0;
                npcOnTrigger.GetComponent<NPC>().OverTalk();
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
