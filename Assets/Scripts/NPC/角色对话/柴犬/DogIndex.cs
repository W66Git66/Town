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
        if(DataSaveManager.Instance.isJirouBack)
        {
            n = Random.Range(3, 6);//���3-5��
        }
        else
        {
            n = Random.Range(1, 4);//���1-3��
        }
        if (DataSaveManager.Instance.isProteinEverbeenFound)
        {
            GameObject.Find("��Ȯ").GetComponent<DialogueIndex>().AddToIndex(2);
            DataSaveManager.Instance.GetDeadBird(n);
            animator.SetTrigger("IsFull");
        }

        if(DataSaveManager.Instance.isProteinEverbeenFound && DataSaveManager.Instance.isJirouBack)
        {

            GameObject.Find("��Ȯ").GetComponent<DialogueIndex>().AddToIndex(4);
        }
    }
}
