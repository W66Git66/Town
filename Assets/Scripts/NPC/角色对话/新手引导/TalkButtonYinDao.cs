using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkButtonYinDao : Singleton<TalkButtonYinDao>
{
    [Header("对话框")]
    public GameObject dialogBox;
    // [NonSerialized]
    public Dialogue dialogue;//对话内容

    private int dialogueIndex;

    public GameObject npcOnTrigger;
    public GameObject xinshouYindao;
    public GameObject cunzhangYindao;

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
        Invoke("XinShouYinDaoVar", 1.5f);
        Invoke("CunZhangYinDaoVar", 1.5f);
    }

    public void XinShouYinDaoVar()
    {
        dialogBox.SetActive(true);
        if (xinshouYindao!=null)
        {
            dialogueIndex = xinshouYindao.GetComponent<DialogueIndex>().dialogueIndex;
            dialogue = xinshouYindao.GetComponent<NPC>().dialogue[dialogueIndex];
            Time.timeScale = 0;
            gameObject.GetComponent<DialogueSysYinDao>().SetDialogueBox();
            dialogBox.SetActive(true);
        }
        else
        {
            return;
        }
    }

    public void CunZhangYinDaoVar()
    {
        dialogBox.SetActive(true);
        if (cunzhangYindao != null)
        {
            dialogueIndex = cunzhangYindao.GetComponent<DialogueIndex>().dialogueIndex;
            dialogue = cunzhangYindao.GetComponent<NPC>().dialogue[dialogueIndex];
            Time.timeScale = 0;
            gameObject.GetComponent<DialogueSysYinDao>().SetDialogueBox();
            dialogBox.SetActive(true);
        }
        else
        {
            return;
        }
    }

    private void Update()
    {
       
    }
}
