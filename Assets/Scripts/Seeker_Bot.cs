﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker_Bot : Enemies
{
    [SerializeField]

    private float LeftCapture;

    [SerializeField]

    private float RightCapture;

    [SerializeField]

    private float JumpingLength = 10f;

    [SerializeField]

    private float JumpingHeight = 15f;

    [SerializeField]

    private LayerMask Ground;

    private Collider2D Colliders;

    private Rigidbody2D rb;

    private bool FacingLeft = true;

    protected override void Start()
    {
            base.Start();

            Colliders = GetComponent<Collider2D>();

            rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!Pause_Menu.IsPaused)

        { 
            //Transition from Fall to Idle
            if (Colliders.IsTouchingLayers(Ground) && SeekerBotAnimation.GetBool("Falling"))
            {
                SeekerBotAnimation.SetBool("Falling", false);
            }
        }

        //Transition from Jump to Fall.
        if (SeekerBotAnimation.GetBool("Jumping"))
        {
            if (rb.velocity.y < .1)
            {
                SeekerBotAnimation.SetBool("Falling", true);

                SeekerBotAnimation.SetBool("Jumping", false);
            }
        }
    }

    private void Movement()
    {
        if (!Pause_Menu.IsPaused)
        {
            if (FacingLeft)
            {
                //Test to see if we are beyond the Left Capture.
                if (transform.position.x > LeftCapture)
                {
                    //Make sure sprite is facing the right location, and if it is not, then face the right direction.
                    if (transform.localScale.x != 1)
                    {
                        transform.localScale = new Vector3(1, 1);
                    }

                    //Test to see if Seeker Bot is on the ground, if so jump.
                    if (Colliders.IsTouchingLayers(Ground))
                    {
                        //Jump, duh!
                        rb.velocity = new Vector2(-JumpingLength, JumpingHeight);

                        SeekerBotAnimation.SetBool("Jumping", true);
                    }
                }

                else
                {
                    FacingLeft = false;
                }
            }

            else
            {
                if (transform.position.x < RightCapture)
                {
                    //Make sure sprite is facing the right location, and if it is not, then face the right direction.
                    if (transform.localScale.x != 1)
                    {
                        transform.localScale = new Vector3(1, 1);
                    }

                    //Test to see if Seeker Bot is on the ground, if so jump.
                    if (Colliders.IsTouchingLayers(Ground))
                    {
                        //Jump, duh!
                        rb.velocity = new Vector2(-JumpingLength, JumpingHeight);

                        SeekerBotAnimation.SetBool("Jumping", true);
                    }
                }

                else
                {
                    FacingLeft = true;
                }
            }
        }
    }
           
}  
