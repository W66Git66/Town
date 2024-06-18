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
            GameObject.Find("析通通").GetComponent<DialogueIndex>().AddToIndex(2);
            animator.SetTrigger("IsTooth");
        }
        if (DataSaveManager.Instance.isJirouBack)
        {
            GameObject.Find("析通通").GetComponent<DialogueIndex>().AddToIndex(3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
