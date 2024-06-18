using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gfire : MonoBehaviour
{
    public Sprite lightFire;
    private SpriteRenderer sr;
    public GameObject light;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void ChangeSprite()
    {
        light.SetActive(true);
        sr.sprite = lightFire;
        GameManager.Instance.gfireNumber += 1;
    }
}
