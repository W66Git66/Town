using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JiaoFu : MonoBehaviour
{
    public Text payMaque;
    private int live;
    private int dead;

    public GameObject yesButton;
    public GameObject noButton;
    public scarecrow scarecrow_;
    void Start()
    {
        
    }

    void Update()
    {
        live = DataSaveManager.Instance.liveBird;
        dead = DataSaveManager.Instance.deadBird;
        if (live > dead)
        {
            payMaque.text = "是否交予他一只麻雀（剩余活蹦乱跳的麻雀数： " + live.ToString()+" )";
        }
        else
        {
            payMaque.text = "是否交予他一只麻雀（剩余半死不活的麻雀数： " + dead.ToString()+" )";
        }

    }

    public void ClickYes()
    {
        if(live > dead)
        {
            scarecrow_.GetComponent<scarecrow>().ChuMoScare();
            DataSaveManager.Instance.UseLiveBird();
        }
        else
        {
            scarecrow_.GetComponent<scarecrow>().ChuMoScare();
            DataSaveManager.Instance.UseDeadBird();
        }
        gameObject.SetActive(false);
    }

    public void ClickNo()
    {
        gameObject.SetActive(false);
    }
}
