using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    protected Animator SeekerBotAnimation;

    protected Rigidbody2D RigidBody;

    protected virtual void Start()
    {
        SeekerBotAnimation = GetComponent<Animator>();

        RigidBody = GetComponent<Rigidbody2D>();
    }

    public void JumpedOn()
    {
        SeekerBotAnimation.SetTrigger("Death");

        RigidBody.velocity = new Vector2(0, 0);
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
}
