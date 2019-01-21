using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform tank;

    void LateUpdate()
    {
        if (tank != null)
        {
            Vector3 tankPosition = new Vector3(tank.position.x, tank.position.y, transform.position.z);
            transform.position = tankPosition;
        }
    }
}
