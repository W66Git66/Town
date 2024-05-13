using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueIndex : MonoBehaviour
{
    public int dialogueIndex=0;//NPC对话段落索引,默认初始为0

    private GameObject canvasBox;//获取Canvas
    private Transform dialogueBox;//获取Canvas下的对话框组件

    private string npcName;
    public bool isFakeToothFind;
    public bool isAlbumenPowderFind;
    public void Start()
    {
        canvasBox = GameObject.Find("Canvas");
        dialogueBox = canvasBox.transform.Find("对话框");
        npcName = gameObject.name;
    }

    public void Update()
    {
        isFakeToothFind = GameObject.Find("DataSaveManager").GetComponent<DataSaveManager>().isFakeToothFind;

        switch (npcName)
        {
            case "柴犬":
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
