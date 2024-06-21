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
            DataSaveManager.Instance.isScareDes = true;
        }
        if (DataSaveManager.Instance.isScarecrowYinDao)
        {
            Destroy(gameObject);
        }
    }
}
