using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerBehaviour : MonoBehaviour
{    
    public Animator anim;
    public Light2D lantern;
    public ShadowMeter shadowMeter;
    public float speed = 5f;
    public float maxLanternIntensity = 2;

    private Rigidbody2D rb;
    private Vector2 movement;
    private float holdStart;
    private float lightIntensity = 1;
    private bool kill = false;
    private bool dim = true;

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
        Light();
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

    public void Light()
    {
        shadowMeter.rate = lantern.intensity;

        if (Input.GetMouseButtonDown(0))
        {
            holdStart = Time.time;
        }

        if (Input.GetMouseButton(0))
        {
            float hold = Time.time - holdStart;
            if (lantern.intensity <= maxLanternIntensity)
            {
                lantern.intensity += 0.008f;
            }
            if (hold >= 2) kill = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            kill = false;
            dim = true;
        }

        if (dim)
        {
            if (lantern.intensity >= lightIntensity) lantern.intensity -= 0.008f;
            else
            {
                lantern.intensity = lightIntensity;
                dim = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Destructible") && kill)
        {
            Debug.Log("item drop");
            kill = false;
        }
    }
}
