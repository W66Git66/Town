using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public string day;
    public string night;
    private bool isday=true;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EventCenter.Broadcast(EventType.teleport, day, night, isday);
            isday = !isday;
        }

    }
}
