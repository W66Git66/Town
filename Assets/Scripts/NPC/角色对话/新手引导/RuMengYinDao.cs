using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuMengYinDao : MonoBehaviour
{
    void Update()
    {
        if (gameObject.GetComponent<NPC>().isOver == true)
        {
            DataSaveManager.Instance.SetRuMengYinDao();
        }
        if (DataSaveManager.Instance.isRuMengYinDao)
        {
            Destroy(gameObject);
        }
    }
}
