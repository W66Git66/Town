using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanchuangController : MonoBehaviour
{
    public GameObject Tanchuang;
    public GameObject Tanchuang1;
    public GameObject Tanchuang2;
    public GameObject Tanchuang3;
    public GameObject Tanchuang4;
    public void HideTanchuang()
    {
        Tanchuang.SetActive(false);
        Tanchuang1.SetActive(false);
        Tanchuang2.SetActive(false);
        Tanchuang3.SetActive(false);
        Tanchuang4.SetActive(false);
    }
}
