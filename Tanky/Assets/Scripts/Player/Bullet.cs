using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float damage;
    public float bulletTorque;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if bullet hits wall
        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Enemy"))
        {
            if (collision.gameObject.name.Equals("Enemy(Clone)"))
            {
                MusicManager.instance.Play("Hit_1");
                collision.GetComponent<Enemy>().Hit(damage);
                collision.GetComponent<Rigidbody2D>().AddTorque(bulletTorque, ForceMode2D.Impulse);
                Destroy(gameObject);
            }
            else
            {
                MusicManager.instance.Play("Hit_1");
                collision.GetComponent<Enemy2>().life -= damage;
                Destroy(gameObject);
            }
        }
    }

}
