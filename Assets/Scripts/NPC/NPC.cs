using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
        [Header("�Ի�����")]
        public Dialogue[] dialogue;

        public bool isOver = false;

    public void OverTalk()
    {
        isOver = true;
    }
}
