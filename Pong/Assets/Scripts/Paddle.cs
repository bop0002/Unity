using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    protected Rigidbody2D rb;

    public float speed = 20.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}
