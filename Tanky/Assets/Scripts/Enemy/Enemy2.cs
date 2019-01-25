using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float life;
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public float fireTime;
    public GameObject bullet;

    private float timer = 0;
    private Transform player;

    void Awake()
    {
        if(GameObject.FindWithTag("Player")!=null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(player != null)
        {
            CheckIfAlive();

            float distance = Vector2.Distance(transform.position, player.position);

            if (distance > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else if (distance < stoppingDistance && distance > retreatDistance)
            {
                transform.position = this.transform.position;
            }
            else if (distance < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            }

            if (timer >= fireTime)
            {
                MusicManager.instance.Play("Lazer");
                Instantiate(bullet, transform.position, Quaternion.identity);
                timer = 0f;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }        
    }

    private void CheckIfAlive()
    {
        if (life <= 0)
        {
            WaveSpawner.instance.wave.enemysAlive -= 1;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Tank>().kills += 1;
            ScoreInfo.instance.Score += 1;
            Destroy(gameObject);
        }
    }
}
