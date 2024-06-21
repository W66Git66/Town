using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Dialog;

public class DialogueSysYinDao : MonoBehaviour
{
    [Header("对话框")]
    public GameObject dialogueBox;
    // [NonSerialized]
    public Dialogue dialogue;//对话内容

    private int dialogueIndex;

    public GameObject npcOnTrigger;
    public GameObject xinshouYindao;
    public GameObject cunzhangYindao;
    public GameObject heiyeYindao;
    public GameObject chumoScareYinDao;
    public GameObject ghostYinDao;

    private int index;//索引


    //对话内容框
    //TextMeshProUGUI dialogueContent;
    Text dialogueContent;
    //名称框
    //TextMeshProUGUI dialogueName;
    Text dialogueName;
    //头像框
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
        if (Input.GetKeyDown(KeyCode.Mouse0) && dialogue != null)
        {
            //对话播放完，关闭对话
            if (index == dialogue.dialogNodes.Length)
            {
                dialogueBox.SetActive(false);
                Time.timeScale = 1;
                index = 0;
                npcOnTrigger.GetComponent<NPC>().OverTalk();
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
