using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour 
{
    public float speed;
    public float damage;
    public bool rocket;
    public float rotationSpeed;
    public float lifeTime;
    private Transform player;
    private Vector2 target;
    private float timer = 0;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MusicManager.instance.Play("Hit_1");
            collision.GetComponent<Tank>().life -= damage;
            DestroyBullet();
        }
        else if (collision.CompareTag("Shield"))
        {
            MusicManager.instance.Play("Hit_1");
            DestroyBullet();
        }
    }
    private void Move()
    {
        if (player != null)
        {
            if (rocket)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                Rotate();
                if (timer >= lifeTime)
                    DestroyBullet();
                else
                    timer += Time.deltaTime ;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
                if (transform.position.x == target.x && transform.position.y == target.y)
                {
                    DestroyBullet();
                }
            } 
        }
    }
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
    private void Rotate()
    {
        Vector2 vectorToTarget = player.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }
}
