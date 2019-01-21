using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform tank;
    void Awake()
    {
        if (tank == null)
            Debug.LogWarning("Tank property in CameraFollow is null/empty");
    }
    void LateUpdate()
    {
        if (tank != null)
        {
            Vector3 tankPosition = new Vector3(tank.position.x, tank.position.y, transform.position.z);
            transform.position = tankPosition;
        }
    }
}
