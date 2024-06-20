using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanchuangController : MonoBehaviour
{
    public GameObject Tanchuang;
    public void HideTanchuang()
    {
        Tanchuang.SetActive(false);
    }
}
