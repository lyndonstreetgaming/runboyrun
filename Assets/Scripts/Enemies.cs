using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    protected Animator SeekerBotAnimation;

    protected Rigidbody2D RigidBody;

    protected AudioSource death;

    protected virtual void Start()
    {
        SeekerBotAnimation = GetComponent<Animator>();

        RigidBody = GetComponent<Rigidbody2D>();

        death = GetComponent<AudioSource>();
    }

    public void JumpedOn()
    {
            SeekerBotAnimation.SetTrigger("Death");

            death.Play();

            RigidBody.velocity = new Vector2(0, 0);

            RigidBody.bodyType = RigidbodyType2D.Kinematic;

            GetComponent<Collider2D>().enabled = false;
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
}
