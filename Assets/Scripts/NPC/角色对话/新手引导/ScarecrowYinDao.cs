using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowYinDao : MonoBehaviour
{
    void Update()
    {
        if (gameObject.GetComponent<NPC>().isOver == true)
        {
<<<<<<< HEAD
            DataSaveManager.Instance.SetScarecrowYinDao();            
=======
            DataSaveManager.Instance.isScareDes = true;
            DataSaveManager.Instance.SetScarecrowYinDao();
>>>>>>> 8fe6210937c01035acf5a4173b9b486a162648f8
        }
        if (DataSaveManager.Instance.isScarecrowYinDao)
        {
            Destroy(gameObject);
        }
    }
}
