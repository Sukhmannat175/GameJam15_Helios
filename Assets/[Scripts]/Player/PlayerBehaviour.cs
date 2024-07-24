using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{    
    public float speed = 5f;
    public Animator anim;

    private Rigidbody2D rb;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void Movement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);

        if(movement.x == 1)
        {
            anim.SetBool("Left", false);
            anim.SetBool("Right", true);
            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
        }
        if (movement.x == -1)
        {
            anim.SetBool("Left", true);
            anim.SetBool("Right", false);
            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
        }
        if (movement.y == 1)
        {
            anim.SetBool("Left", false);
            anim.SetBool("Right", false);
            anim.SetBool("Up", true);
            anim.SetBool("Down", false);
        }
        if (movement.y == -1)
        {
            anim.SetBool("Left", false);
            anim.SetBool("Right", false);
            anim.SetBool("Up", false);
            anim.SetBool("Down", true);
        }
    }
}
