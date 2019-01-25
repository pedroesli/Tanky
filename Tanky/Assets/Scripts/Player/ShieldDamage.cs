using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDamage : MonoBehaviour
{
    public float damage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            MusicManager.instance.Play("Impact_2");
            if (collision.collider.GetComponent<Enemy>()!=null)
            {
                collision.collider.GetComponent<Enemy>().Hit(damage);
            }
            else
            {
                collision.collider.GetComponent<Enemy2>().life -= damage;
            }
        }
    }
}
