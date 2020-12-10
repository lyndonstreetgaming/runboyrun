using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    protected Animator SeekerBotAnimation;

    protected virtual void Start()
    {
        SeekerBotAnimation = GetComponent<Animator>();
    }

    public void JumpedOn()
    {
        SeekerBotAnimation.SetTrigger("Death");
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
}
