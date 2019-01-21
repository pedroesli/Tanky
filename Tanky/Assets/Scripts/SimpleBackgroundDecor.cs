using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBackgroundDecor : MonoBehaviour
{
    public float rotation;
    public float moveSpeedMin;
    public float moveSpeedMax;
    private Vector2[] directions = { Vector2.up, Vector2.right, Vector2.down, Vector2.left };
    private float moveSpeed;
    private Vector2 direction;
    void Start()
    {
        //setup
        rotation = Random.Range(0, 1) == 0 ? -rotation : rotation;
        direction = directions[Random.Range(0, 3)];
        moveSpeed = Random.Range(moveSpeedMin, moveSpeedMax);
        //to ocupy less space
        directions = null;
    }
    void Update()
    {
        transform.Rotate(0f, 0f, rotation * Time.deltaTime);
        transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            direction = Vector2.Reflect(transform.position.normalized, collision.transform.position.normalized);
            rotation = -rotation;
        }
    }
}
