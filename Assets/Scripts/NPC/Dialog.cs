using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    // 代表了一个对话节点。
    [Serializable]
    public class DialogNode
    {
        [Header("角色的名字")]
        public string name;
        [Header("角色的头像")]
        public Sprite sprite;

        [TextArea, Header("对话的内容")]
        public string content;
    }

}
