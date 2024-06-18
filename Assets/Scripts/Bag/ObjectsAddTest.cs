using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//测试 增加死去的麻雀 按钮
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
