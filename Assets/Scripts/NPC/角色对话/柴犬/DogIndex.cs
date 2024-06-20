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
        if(DataSaveManager.Instance.isJirouBack)
        {
            n = Random.Range(3, 6);//随机3-5个
            m = Random.Range(3, 6);
        }
        else
        {
            n = Random.Range(1, 4);//随机1-3个
            m = Random.Range(1, 4);
        }
        if (DataSaveManager.Instance.isProteinEverbeenFound)
        {
            GameObject.Find("柴犬").GetComponent<DialogueIndex>().AddToIndex(2);
            DataSaveManager.Instance.GetDeadBird(n);
            if (DataSaveManager.Instance.isShenSheBack)
            {
                DataSaveManager.Instance.GetDeadBird(m);
            }
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
            GameObject.Find("柴犬").GetComponent<DialogueIndex>().AddToIndex(4);
        }
    }
}
