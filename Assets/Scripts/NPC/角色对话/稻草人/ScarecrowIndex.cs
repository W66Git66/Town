using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowIndex : MonoBehaviour
{
    void Start()
    {
        if (DataSaveManager.Instance.isScareBeated)
        {
            GameObject.Find("µæ≤›»À").GetComponent<DialogueIndex>().AddToIndex(2);
        }
    }
}
