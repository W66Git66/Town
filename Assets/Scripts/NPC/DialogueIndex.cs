using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueIndex : MonoBehaviour
{
    public int dialogueIndex=0;//NPC�Ի���������,Ĭ�ϳ�ʼΪ0
    private bool isFirstTime;//�Ƿ�Ϊ���ζԻ�
    private GameObject canvasBox;//��ȡCanvas
    private Transform dialogueBox;//��ȡCanvas�µĶԻ������

    private string npcName;
    private bool isFakeToothFind;
    public void Start()
    {
        canvasBox = GameObject.Find("Canvas");
        dialogueBox = canvasBox.transform.Find("�Ի���");
        npcName = gameObject.name;
    }

    public void Update()
    {
        isFakeToothFind = GameObject.Find("DataSaveManager").GetComponent<DataSaveManager>().isFakeToothFind;
        switch (npcName)
        {
            case "��Ȯ":
                ChaiQuan();
                break;
        }
    }

    private void ChaiQuan()
    {
        bool isAlbumenPowderFind = false;
        isFirstTime = dialogueBox.GetComponent<DialogueSystem>().isFirstTime;
        if(!isFirstTime&&isAlbumenPowderFind)
        {
            dialogueIndex = 3;
        }
        else if (isFirstTime)
        {

        }


    }

}
