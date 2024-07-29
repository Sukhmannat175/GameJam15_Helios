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
    public LayerMask layerMask;
    public float speed = 5f;
    public float maxLanternIntensity = 2;
    public float baseRate = 1;
    public float lightInt = 1;

    private Rigidbody2D rb;
    private Vector2 movement;
    private List<GameObject> destructibles;
    private float lightIntensity = 1;
    private bool dim = true;
    private int worldNum;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        destructibles = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        ShineLight();
        ChangeLights();
        ChangeWorldState();
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

    public void ShineLight()
    {
        shadowMeter.rate = baseRate * lantern.intensity * lightInt;

        if (Input.GetMouseButton(0))
        {
            if (lantern.intensity <= maxLanternIntensity)
            {
                lantern.intensity += 0.008f;
            }
            if (lantern.intensity >= maxLanternIntensity)
            {
                foreach (GameObject go in destructibles)
                {
                    go.GetComponent<SpriteRenderer>().sprite = null;
                    go.transform.localScale = new Vector3(0, 0, 10);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
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

    private void ChangeLights()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            lantern.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            lantern.color = new Color32(255, 97, 0, 255);
            lantern.intensity = 1;
            worldNum = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            lantern.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            lantern.color = new Color32(3, 108, 0, 255); ;
            worldNum = 2;
        }
    }

    private void ChangeWorldState()
    {
        if (Input.GetMouseButton(1))
        {
            if (lantern.intensity <= maxLanternIntensity)
            {
                lantern.intensity += 0.008f;
            }
            if (lantern.intensity >= maxLanternIntensity)
            {
                GameController.Instance.ChangeWorlds(worldNum);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
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
        if (other.gameObject.CompareTag("Destructible"))
        {
            destructibles.Add(other.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Light"))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (other.gameObject.transform.position - transform.position), 10, layerMask);
            Debug.DrawRay(transform.position, (other.gameObject.transform.position - transform.position), Color.green);
            if (hit.collider != null && hit.collider == other.GetComponentInChildren<BoxCollider2D>())
            {
                lightInt = 2.1f - (0.1f * Vector2.Distance(gameObject.transform.position, other.GetComponentInChildren<BoxCollider2D>().gameObject.transform.position));
            }
            else
            {
                lightInt = 1;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Destructible"))
        {
            destructibles.Remove(other.gameObject);
            if (other.GetComponent<SpriteRenderer>().sprite == null) Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Light"))
        {
            lightInt = 1;
        }
    }
}
