using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Dialog;

    // ��ʾһ�ζԻ�
[CreateAssetMenu(menuName = "�����Ի�", fileName = "�Ի�")]
public class Dialogue : ScriptableObject
{
        // �Ի��ڵ�
    public DialogNode[] dialogNodes;
}
