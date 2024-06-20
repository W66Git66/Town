using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CunZhangYinDao : MonoBehaviour
{
    public GameObject Tanchuang;
    private void Start()
    {
        if (DataSaveManager.Instance.isCunZhangYinDao)
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (gameObject.GetComponent<NPC>().isOver)
        {
            Tanchuang.SetActive(true);
            DataSaveManager.Instance.SetCunZhangYinDao();
        }
        if (DataSaveManager.Instance.isCunZhangYinDao && Tanchuang.activeSelf)
        {
            Destroy(gameObject);
        }
        if(DataSaveManager.Instance.isCunZhangYinDao && !gameObject.GetComponent<NPC>().isOver)
        {
            Destroy(gameObject);
        }
    }
}
