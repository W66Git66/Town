using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//���� ������ȥ����ȸ ��ť
public class ObjectsAddTest : MonoBehaviour
{
    private Button addDeadBird;
    private DataSaveManager dataSaveManager;
    private void Start()
    {
        dataSaveManager = GameObject.Find("DataSaveManager").GetComponent<DataSaveManager>();
        addDeadBird= gameObject.GetComponent<Button>();
        addDeadBird.onClick.AddListener(AddDeadBird);
    }

    private void AddDeadBird()
    {
        dataSaveManager.GetDeadBird(1);
        Debug.Log("+1");
    }
}
