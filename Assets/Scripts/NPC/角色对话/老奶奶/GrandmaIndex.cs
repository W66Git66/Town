using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandmaIndex : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        if (DataSaveManager.Instance.isFakeToothFind)
        {
            GameObject.Find("������").GetComponent<DialogueIndex>().AddToIndex(2);
            animator.SetTrigger("IsTooth");
            DataSaveManager.Instance.CanJirouBack();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DataSaveManager.Instance.isJirouBack)
        {
            GameObject.Find("������").GetComponent<DialogueIndex>().AddToIndex(3);
        }
    }
}
