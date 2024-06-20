using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XinShouYinDao : MonoBehaviour
{
    void Update()
    {
        if (gameObject.GetComponent<NPC>().isOver == true)
        {
            DataSaveManager.Instance.SetXinshouGuanYinDao();
        }
        if (DataSaveManager.Instance.isXinshouGuanYinDao)
        {
            Destroy(gameObject);
        }
    }
}
