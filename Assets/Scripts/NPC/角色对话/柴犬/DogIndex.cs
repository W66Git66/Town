using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogIndex : MonoBehaviour
{
    void Start()
    {
        if (DataSaveManager.Instance.isProteinEverbeenFound)
        {
            GameObject.Find("��Ȯ").GetComponent<DialogueIndex>().AddToIndex(2);
        }

        if(DataSaveManager.Instance.isProteinEverbeenFound && DataSaveManager.Instance.isFakeToothFind)
        {
            GameObject.Find("��Ȯ").GetComponent<DialogueIndex>().AddToIndex(4);
        }
    }
}
