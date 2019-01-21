using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform tank;
    public Transform waveSpawner;
    public float damage;
    public float speed;
    public float life;
    public float stopTime;

    public float timeBetweenHits = 0.5f;
    public float timer = 0;

    private bool hit = false;
    private float hitTimer = 0f;

    void Awake()
    {
        tank = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (!hit && tank!=null)
            transform.position = Vector2.MoveTowards(transform.position, tank.position, speed * Time.deltaTime);
        else
        {
            if (hitTimer >= stopTime)
            {
                hit = false;
                hitTimer = 0;
            }
            else
                hitTimer += Time.deltaTime;
        }
        if (life <= 0)
        {
            WaveSpawner.instance.wave.enemysAlive -= 1;
            Destroy(gameObject);
        }

    }

    public void Hit(float damage)
    {
        life -= damage;
        hit = true;
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        timer += Time.deltaTime;
        if (collision.gameObject.CompareTag("Player") && timer>=timeBetweenHits)
        {
            collision.gameObject.GetComponent<Tank>().life -= damage;
            timer = 0;
        }
    }
}
