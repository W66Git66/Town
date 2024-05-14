using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform minLimit; // ��С�ƶ���Χ
    public Transform maxLimit; // ����ƶ���Χ

    private void Update()
    {
        // ��ȡ�����λ��
        Vector3 cameraPosition = transform.position;

        // ��������� X ��������С������ƶ���Χ��
        cameraPosition.x = Mathf.Clamp(cameraPosition.x, minLimit.position.x, maxLimit.position.x);

        // ��������� Y ��������С������ƶ���Χ��
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, minLimit.position.y, maxLimit.position.y);

        // �������λ�ø���Ϊ�����Ƶ�λ��
        transform.position = cameraPosition;
    }
}
