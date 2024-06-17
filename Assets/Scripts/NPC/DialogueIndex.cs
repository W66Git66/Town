using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueIndex : MonoBehaviour
{
    public int dialogueIndex=0;//NPC对话段落索引,默认初始为0

    public void AddIndex()
    {
        dialogueIndex++;
    }

    public void AddToIndex(int index)
    {
        dialogueIndex=index;
    }
}
