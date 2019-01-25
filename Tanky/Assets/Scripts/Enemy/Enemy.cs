using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float damage;
    public float speed;
    public float life;
    public float stopTime;
    public float timeBetweenHits = 0.5f;
    public float enemyHitTimer = 0;

    private bool hit = false;
    private float hitTimer = 0f;

    void Awake()
    {
        if (GameObject.FindWithTag("Player")!=null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if(player!= null)
        {
            CheckIfAlive();

            //timer for hits
            enemyHitTimer += Time.deltaTime;

            if (!hit)
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            else
            {
                //if hit then dont move the enemy
                if (hitTimer >= stopTime)
                {
                    hit = false;
                    hitTimer = 0;
                }
                else
                    hitTimer += Time.deltaTime;
            }
        }
    }

    public void Hit(float damage)
    {
        life -= damage;
        hit = true;
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && enemyHitTimer>=timeBetweenHits)
        {
            collision.gameObject.GetComponent<Tank>().life -= damage;
            enemyHitTimer = 0;
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
