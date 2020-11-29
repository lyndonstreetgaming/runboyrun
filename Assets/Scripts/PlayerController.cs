using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Start() Variables
    private Rigidbody2D RigidBody;

    private Collider2D Colliders;

    private Animator PlayerAnimation;
    
    //FSM
    private enum State { Idle, Running, Jumping, Falling }

    private State state = State.Idle;
   
    //All these are inspector Variables
    [SerializeField]

    private LayerMask Ground;

    [SerializeField]

    //Yuh wah Speed? 
    private float Speed = 5f;

    [SerializeField]

    //Jump boy, JUMP!
    private float JumpingForce = 18f;

    private void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();

        PlayerAnimation = GetComponent<Animator>();

        Colliders = GetComponent<Collider2D>();
    }

    private void Update()
    {
        Directions();

        AnimationState();

        //Set animation based on Enumerator state and thing.
        PlayerAnimation.SetInteger("state", (int)state);
    }

    private void Directions()
    {
        float HorizontalDirection = Input.GetAxis("Horizontal");

        //This is to make it move left.

        if (HorizontalDirection < 0)
        {
            RigidBody.velocity = new Vector2(-Speed, RigidBody.velocity.y);

            transform.localScale = new Vector2(-1, 1);

        }

        //This is to make it move the opposite of left which is right. 

        else if (HorizontalDirection > 0)
        {
            RigidBody.velocity = new Vector2(Speed, RigidBody.velocity.y);

            transform.localScale = new Vector2(1, 1);

        }

        //This is to make it jump.

        if (Input.GetButtonDown("Jump") && Colliders.IsTouchingLayers(Ground))
        {
            RigidBody.velocity = new Vector2(RigidBody.velocity.x, JumpingForce);

            state = State.Jumping;
        }
    }

    private void AnimationState()
    {
        if (state == State.Jumping)
        {
            if (RigidBody.velocity.y < .1f )
            {
                state = State.Falling;
            }
        }

        else if (state == State.Falling)
        {
            if (Colliders.IsTouchingLayers(Ground))
            {
                state = State.Idle;
            }
   
        }

        else if (Mathf.Abs(RigidBody.velocity.x) > 2f)
        {
            //Moving at either direction

            state = State.Running;
        }

        else
        {
            state = State.Idle;
        }

    }
}
