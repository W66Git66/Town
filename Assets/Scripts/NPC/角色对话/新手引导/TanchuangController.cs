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
    public GameObject Tanchuang5;
    public void HideTanchuang()
    {
        Tanchuang.SetActive(false);
        if (Tanchuang1 != null)
        {
            Tanchuang1.SetActive(false);
        }
        if (Tanchuang2 != null)
        {
            Tanchuang2.SetActive(false);
        }
        if (Tanchuang3 != null)
        {
            Tanchuang3.SetActive(false);
        }
        if (Tanchuang4 != null)
        {
            Tanchuang4.SetActive(false);
        }
        if (Tanchuang5 != null)
        {
            Tanchuang5.SetActive(false);
        }
    }
}
