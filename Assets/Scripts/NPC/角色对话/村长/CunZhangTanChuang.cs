using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CunZhangTanChuang : MonoBehaviour
{
    public GameObject Tishi;
    void Start()
    {
        Tishi.SetActive(false);
    }

    void Update()
    {
        
    }

    public void ShowTishi()
    {
        Tishi.SetActive(true);
    }
}
