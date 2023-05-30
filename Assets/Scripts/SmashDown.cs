using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class SmashDown : MonoBehaviour
{
    // BEHOLD, THE WORST CODE EVER WRITTEN!!
    [SerializeField] private GameObject waypoint;
    [SerializeField] private float MoveUpSpeed = 1f;
    [SerializeField] private float SmashIntervalSpeed = 2f; 

    private bool Fallen;
    private bool FallNotScheduled;

    private Animator anim;
    private AudioSource smashSound;
    private Rigidbody2D rb;

    
    // Start is called before the first frame update
    void Start()
    {
        smashSound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        FallNotScheduled = true;
        Fall();

    }

    private void Update()
    {
        if (Fallen)
        {
            anim.SetBool("Smashing", true);


            transform.position = Vector2.MoveTowards(transform.position, waypoint.transform.position, Time.deltaTime * MoveUpSpeed);
           
            if (transform.position ==  waypoint.transform.position)
            {
                Fallen = false;
                FallNotScheduled |= true;
                anim.SetBool("Smashing", false);

            }
        }
        else
        {
            // getting called way too many times
            if (FallNotScheduled) 
            { 
                Invoke("Fall", SmashIntervalSpeed);
                FallNotScheduled = false;
            }
        }
    }

    private void Fall()
    {
        rb.bodyType = RigidbodyType2D.Dynamic; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.bodyType = RigidbodyType2D.Static;
        Fallen = true;
        smashSound.Play();
        
    }
}
