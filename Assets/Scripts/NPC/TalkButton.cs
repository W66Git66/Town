using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkButton : MonoBehaviour
{
        public GameObject tipsButton;//�Ի���ʾ��ť
        [Header("�Ի���")]
        public GameObject dialogBox;
        [NonSerialized]
        public Dialogue dialogue;//�Ի�����

    //���ű����õ���ģʽ
    public static TalkButton instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
        {
            dialogue = other.GetComponent<NPC>().dialogue;
            tipsButton.SetActive(true);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            tipsButton.SetActive(false);
            dialogBox.SetActive(false);
        }

        private void Update()
        {
            if (tipsButton != null && tipsButton.activeSelf && Input.GetKeyDown(KeyCode.E))
            {
                dialogBox.SetActive(true);
            }
        }

}
