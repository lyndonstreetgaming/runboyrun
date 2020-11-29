using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D RigidBody;

    private Animator PlayerAnimation;

    private enum State { Idle, Running, Jumping, Falling }

    private State state = State.Idle;

    private Collider2D Colliders;

    [SerializeField]

    private LayerMask Ground;

    private void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();

        PlayerAnimation = GetComponent<Animator>();

        Colliders = GetComponent<Collider2D>();
    }

    private void Update()
    {
        float HorizontalDirection = Input.GetAxis("Horizontal");

        if (HorizontalDirection < 0)
        {
            RigidBody.velocity = new Vector2(-5, RigidBody.velocity.y);

            transform.localScale = new Vector2(-1, 1);

        }

       else if (HorizontalDirection > 0)
        {
            RigidBody.velocity = new Vector2(5, RigidBody.velocity.y);

            transform.localScale = new Vector2(1, 1);

        }

        else
        {

        }

        if (Input.GetButtonDown("Jump") && Colliders.IsTouchingLayers(Ground))
        {
            RigidBody.velocity = new Vector2(RigidBody.velocity.x, 18f);

            state = State.Jumping;
        }

        VelocityState();

        PlayerAnimation.SetInteger("state", (int)state);
    }

    private void VelocityState()
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
