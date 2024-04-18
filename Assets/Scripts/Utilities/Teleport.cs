using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public string day;
    public string night;
    private void Update()
    {
       // EventCenter.Broadcast(EventType.teleport,day,night);
    }
}
