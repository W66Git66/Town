using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostyinDao : MonoBehaviour
{
    void Update()
    {
        if (gameObject.GetComponent<NPC>().isOver)
        {
            DataSaveManager.Instance.isGhostYinDao=true;
        }
        if (DataSaveManager.Instance.isGhostYinDao)
        {
            Destroy(gameObject);
        }
    }
}
