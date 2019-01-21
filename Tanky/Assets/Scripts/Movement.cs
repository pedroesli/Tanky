using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public float dashSpeed;
    public float startDashTime;
    public GameObject ParticleObject;
    private float horizontal;
    private float vertical;
    private Rigidbody2D rb;
    private float dashTime;
    private int direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Dash();
        if (!IsDashing())
        {
            rb.velocity = Vector2.zero;
            transform.position += new Vector3(horizontal, vertical) * Time.deltaTime * speed;
        }


    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsDashing())
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Wall"))
                MusicManager.instance.Play("Impact");
    }
    private void Dash()
    {
        if (direction == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Input.GetKey(KeyCode.A))
                {
                    direction = 1;//left
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    direction = 2;//right
                }
                else if (Input.GetKey(KeyCode.W))
                {
                    direction = 3;//up
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    direction = 4;//down
                }
                if (direction != 0)
                {
                    Instantiate(ParticleObject, transform.position, Quaternion.identity);
                    MusicManager.instance.Play("Dash");
                }
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;
                switch (direction)
                {
                    case 1:
                        rb.velocity = Vector2.left * dashSpeed;
                        break;
                    case 2:
                        rb.velocity = Vector2.right * dashSpeed;
                        break;
                    case 3:
                        rb.velocity = Vector2.up * dashSpeed;
                        break;
                    case 4:
                        rb.velocity = Vector2.down * dashSpeed;
                        break;
                }
            }
        }
    }
    public bool IsDashing()
    {
        return dashTime != startDashTime ? true : false;
    }
}
