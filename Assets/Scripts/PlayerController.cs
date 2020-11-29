using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D RigidBody;

    public Animator PlayerAnimation;

    private enum State { Idle, Running, Jumping }

    private State state = State.Idle;

    private void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();

        PlayerAnimation = GetComponent<Animator>();
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

        if (Input.GetButtonDown("Jump"))
        {
            RigidBody.velocity = new Vector2(RigidBody.velocity.x, 10f);

            state = State.Jumping;
        }

        VelocityState();

        PlayerAnimation.SetInteger("state", (int)state);
    }

    private void VelocityState()
    {
        if (state == State.Jumping)
        {
            // Going Right
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
