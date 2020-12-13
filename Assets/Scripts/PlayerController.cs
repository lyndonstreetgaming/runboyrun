using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Start() Variables
    private Rigidbody2D RigidBody;

    private Collider2D Colliders;

    private Animator PlayerAnimation;

    //FSM
    private enum State { Idle, Running, Jumping, Falling, Hurt }

    private State state = State.Idle;

    //All these are inspector Variables
    [SerializeField]

    private LayerMask Ground;

    [SerializeField]

    private float Speed = 5f;

    [SerializeField]

    private float JumpingForce = 18f;

    [SerializeField]

    private int Coins = 0;

    [SerializeField]

    private TextMeshProUGUI CoinsText;

    [SerializeField]

    private float Timer;

    [SerializeField]

    private TextMeshProUGUI TimerText;

    [SerializeField]

    private int Score = 2000;

    [SerializeField]

    private TextMeshProUGUI ScoreText;

    [SerializeField]

    private float HurtfulForce = 10f;

    [SerializeField]

    private int Lives;

    [SerializeField]

    private TextMeshProUGUI LivesText;

    [SerializeField]

    private float t;

    [SerializeField]

    private string minutes;

    [SerializeField]

    private string seconds;

    [SerializeField]

    private AudioSource coins;

    [SerializeField]

    private AudioSource footsteps;

    private int interval = 100;

    private int ExtraLifeCounter = 1;

    private void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();

        PlayerAnimation = GetComponent<Animator>();

        Colliders = GetComponent<Collider2D>();

        Timer = Time.time;

        LivesText.text = Lives.ToString();

        footsteps = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (state != State.Hurt)
        {
            Directions();
        }

        AnimationState();

        //Set animation based on Enumerator state and thing.
        PlayerAnimation.SetInteger("state", (int)state);

        InGameTimer();

        ExtraLives();

    }

    private void ExtraLives()
    {
        if (Coins >= interval * ExtraLifeCounter)
        {
            ++Lives;

            ++ExtraLifeCounter;

            LivesText.text = Lives.ToString();

            if (Coins >= 99)
            {
                Coins -= 99;

                CoinsText.text = Coins.ToString();
            }
        }
    }

    private void InGameTimer()
    { 
        t = Time.time - Timer;

        minutes = ((int)t / 60).ToString();

        seconds = (t % 60).ToString("f0");

        TimerText.text = minutes + ":" + seconds;

        if (t >= 60)
        {
            minutes = minutes + 1;

            t -= 60;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coins")
        {
            coins.Play();

            Destroy(collision.gameObject);

            Coins += 1;

            CoinsText.text = Coins.ToString();
        }

        if (collision.tag == "10_Coins")
        {
            Destroy(collision.gameObject);

            Coins += 10;

            CoinsText.text = Coins.ToString();
        }

        if (collision.tag == "Lives")
        {
                Destroy(collision.gameObject);

                Lives += 1;

                LivesText.text = Lives.ToString();
            }

        if (collision.tag == "Powerups")
        {
            Destroy(collision.gameObject);

            JumpingForce = 25f;

            GetComponent<SpriteRenderer>().color = Color.gray;

            StartCoroutine(ResetPowerup());
        }

        if (collision.tag == "1000_Points")
        {
            Destroy(collision.gameObject);

            Score += 1000;

            ScoreText.text = Score.ToString();
        }

        if (collision.tag == "5000_Points")
        {
            Destroy(collision.gameObject);

            Score += 5000;

            ScoreText.text = Score.ToString();
        }
       
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemies enemies = other.gameObject.GetComponent<Enemies>();

            if (state == State.Falling)
            {
                enemies.JumpedOn();

                Jump();

                Score += 500;

                ScoreText.text = Score.ToString();
            }

            else
            {
                state = State.Hurt;

                ScoreandHealthHandler();

                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    //Enemy is to my right and I should be damage and move left

                    RigidBody.velocity = new Vector2(-HurtfulForce, RigidBody.velocity.y);

                }


                else
                {
                    //Enemy is to my left and I should be damage and move right

                    RigidBody.velocity = new Vector2(HurtfulForce, RigidBody.velocity.y);

                }
            }

        }
    }

    private void ScoreandHealthHandler()
    {
        Score -= 100;

        ScoreText.text = Score.ToString();

        if (Score <= 0)
        {
            Lives -= 1;

            LivesText.text = Lives.ToString();
        } 

        if (Lives <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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
            Jump();
        }


    }

    private void Jump()
    {
        RigidBody.velocity = new Vector2(RigidBody.velocity.x, JumpingForce);

        state = State.Jumping;
    }

    private void AnimationState()
    {
        if (state == State.Jumping)
        {
            if (RigidBody.velocity.y < .1f)
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

        else if (state == State.Hurt)
        {
            if (Mathf.Abs(RigidBody.velocity.x) < .1f)
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

    private IEnumerator ResetPowerup()
    {
        yield return new WaitForSeconds(10);

        JumpingForce = 18;

        GetComponent<SpriteRenderer>().color = Color.white;

    }

    private void Footsteps()
    {
        footsteps.Play();
    }
}

