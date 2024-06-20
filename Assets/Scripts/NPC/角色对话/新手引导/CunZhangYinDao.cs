using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CunZhangYinDao : MonoBehaviour
{
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
            DataSaveManager.Instance.SetCunZhangYinDao();
        }
        if (DataSaveManager.Instance.isCunZhangYinDao)
        {
            Destroy(gameObject);
        }
    }
}
