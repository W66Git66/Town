using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    // ������һ���Ի��ڵ㡣
    [Serializable]
    public class DialogNode
    {
        [Header("��ɫ������")]
        public string name;
        [Header("��ɫ��ͷ��")]
        public Sprite sprite;

        [TextArea, Header("�Ի�������")]
        public string content;
    }

}
