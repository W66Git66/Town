using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowYinDao : MonoBehaviour
{
    void Update()
    {
        if (gameObject.GetComponent<NPC>().isOver == true)
        {
            DataSaveManager.Instance.SetScarecrowYinDao();
        }
        if (DataSaveManager.Instance.isScarecrowYinDao)
        {
            Destroy(gameObject);
        }
    }
}
