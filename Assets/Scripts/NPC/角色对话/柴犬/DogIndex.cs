using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogIndex : MonoBehaviour
{
    public int n;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        n = Random.Range(1, 4);//Ëæ»ú1-3¸ö
        if (DataSaveManager.Instance.isProteinEverbeenFound)
        {
            GameObject.Find("²ñÈ®").GetComponent<DialogueIndex>().AddToIndex(2);
            DataSaveManager.Instance.GetDeadBird(n);
            animator.SetTrigger("IsFull");
        }

        if(DataSaveManager.Instance.isProteinEverbeenFound && DataSaveManager.Instance.isFakeToothFind)
        {
            GameObject.Find("²ñÈ®").GetComponent<DialogueIndex>().AddToIndex(4);
        }
    }
}
