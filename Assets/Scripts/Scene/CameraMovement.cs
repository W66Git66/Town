using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform minLimit; // 最小移动范围
    public Transform maxLimit; // 最大移动范围

    private void Update()
    {
        // 获取相机的位置
        Vector3 cameraPosition = transform.position;

        // 限制相机的 X 坐标在最小和最大移动范围内
        cameraPosition.x = Mathf.Clamp(cameraPosition.x, minLimit.position.x, maxLimit.position.x);

        // 限制相机的 Y 坐标在最小和最大移动范围内
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, minLimit.position.y, maxLimit.position.y);

        // 将相机的位置更新为受限制的位置
        transform.position = cameraPosition;
    }
}
