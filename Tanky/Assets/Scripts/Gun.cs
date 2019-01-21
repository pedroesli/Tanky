using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform tank;
    public float rotationSpeed;
    public GameObject bullet;
    public float fireTime;
    private Vector3 mousePosition;
    private float timer = 0f;
    void Start()
    {
    }

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Rotation();
        Shoot();
    }

    private void Rotation()
    {
        Vector2 vectorToTarget = mousePosition - tank.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        tank.rotation = Quaternion.Slerp(tank.rotation, rotation, Time.deltaTime * rotationSpeed);
    }
    private void Shoot()
    {
        if(Input.GetKey(KeyCode.Mouse0) && timer >= fireTime)
        {
            MusicManager.instance.Play("Cannon");
            Instantiate(bullet,transform.position,tank.transform.rotation);
            timer = 0f;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
