using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 100.0f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        GameReset();
    }

    private void AddStartForce()
    {
        float x = Random.value < 0.5f ? -0.5f : 0.5f;
        float y = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) :
                                        Random.Range(0.5f, 1.0f);
       Vector2 direction = new Vector2(x, y);
        rb.AddForce(direction*this.speed);

    }

    public void AddForce(Vector2 normal)
    {
        rb.AddForce(normal);
    }

    public void GameReset()
    {
        rb.position = Vector3.zero;
        rb.velocity = Vector3.zero;
        AddStartForce();
    }




}
