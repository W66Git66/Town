using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogIndex : MonoBehaviour
{
    public int n,m;
    public GameObject jiRouKang;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        if (DataSaveManager.Instance.isProteinEverbeenFound)
        {
            GameObject.Find("≤Ò»Æ").GetComponent<DialogueIndex>().AddToIndex(2);
            animator.SetTrigger("IsFull");
        }
        if (DataSaveManager.Instance.canJirouBack)
        {
            jiRouKang.SetActive(true);
            DataSaveManager.Instance.JiRouBack();
        }
        
    }

    public void Update()
    {
        if (DataSaveManager.Instance.isProteinEverbeenFound && DataSaveManager.Instance.isJirouBack)
        {
            gameObject.transform.position = new Vector3(3.98f, -4.63f, 0);
            GameObject.Find("≤Ò»Æ").GetComponent<DialogueIndex>().AddToIndex(4);
        }
    }
}
