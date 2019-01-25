using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float speed;
    public Transform shield1;
    public Transform shield2;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        transform.position = player.transform.position;
        shield1.transform.RotateAround(transform.position, Vector3.forward, speed * Time.deltaTime);
        shield2.transform.RotateAround(transform.position, Vector3.forward, speed * Time.deltaTime);
    }
}
