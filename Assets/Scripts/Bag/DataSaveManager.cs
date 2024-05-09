using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerBag
{
    public int deadBird = 0;
    public int liveBird = 0;
    public bool isKnifeOn = false;

}
public class DataSaveManager : MonoBehaviour
{
    public PlayerBag bag = new PlayerBag();

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
