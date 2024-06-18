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
            n = Random.Range(3, 6);//随机3-5个
        }
        else
        {
            n = Random.Range(1, 4);//随机1-3个
        }
        if (DataSaveManager.Instance.isProteinEverbeenFound)
        {
            GameObject.Find("柴犬").GetComponent<DialogueIndex>().AddToIndex(2);
            DataSaveManager.Instance.GetDeadBird(n);
            animator.SetTrigger("IsFull");
        }

        if(DataSaveManager.Instance.isProteinEverbeenFound && DataSaveManager.Instance.isJirouBack)
        {

            GameObject.Find("柴犬").GetComponent<DialogueIndex>().AddToIndex(4);
        }
    }
}
