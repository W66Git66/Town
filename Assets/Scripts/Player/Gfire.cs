using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gfire : MonoBehaviour
{
    public Sprite lightFire;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void ChangeSprite()
    {
        sr.sprite = lightFire;
        GameManager.Instance.gfireNumber += 1;
    }
}
