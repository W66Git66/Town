using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterChase : MonoBehaviour
{
    public GameObject dieImage;
    void Start()
    {
        dieImage.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dieImage.SetActive(true);
        }
    }

}
