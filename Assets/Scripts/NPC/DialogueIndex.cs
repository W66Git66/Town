using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueIndex : MonoBehaviour
{
    public int dialogueIndex=0;//NPC�Ի���������,Ĭ�ϳ�ʼΪ0

    private GameObject canvasBox;//��ȡCanvas
    private Transform dialogueBox;//��ȡCanvas�µĶԻ������

    private string npcName;
    public bool isFakeToothFind;
    public bool isAlbumenPowderFind;
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

    public void AddIndex()
    {
        dialogueIndex++;
    }

    private void ChaiQuan()
    {
        
    }

}
