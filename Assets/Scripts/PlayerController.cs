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
    private enum State { Idle, Running, Jumping, Falling, Hurt, Climbing }

    private State state = State.Idle;

    //Ladder Variables
    [HideInInspector]

    public bool CanClimb = false;

    [HideInInspector]

    public bool BottomLadder = false;

    [HideInInspector]

    public bool TopLadder = false;

    public Ladder ladder;

    private float NaturalGravity;

    [SerializeField]

    float ClimbingSpeed = 3f;

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

    private AudioSource coins_10;

    [SerializeField]

    private AudioSource lives;

    [SerializeField]

    private AudioSource powerups;

    [SerializeField]

    private AudioSource points_1000;

    [SerializeField]

    private AudioSource points_5000;

    [SerializeField]

    private AudioSource footsteps;

    [SerializeField]

    private AudioSource hurt;

    [SerializeField]

    private AudioSource checkpoint;

    public bool AlreadyPlayed = false;

    private int interval = 100;

    private int ExtraLifeCounter = 1;

    public Vector3 RespawnPoint;

    public Level_Manager GameLevelManager;

    private void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();

        PlayerAnimation = GetComponent<Animator>();

        Colliders = GetComponent<Collider2D>();

        Timer = Time.time;

        LivesText.text = Lives.ToString();

        footsteps = GetComponent<AudioSource>();

        NaturalGravity = RigidBody.gravityScale;

        RespawnPoint = transform.position;

        GameLevelManager = FindObjectOfType<Level_Manager>();

    }

    private void Update()
    {
        if (state == State.Climbing)
        {
            Climb();
        }

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

            lives.Play();

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

        minutes = ((int)t / 60).ToString("0");

        seconds = (t % 60).ToString("00");

        TimerText.text = minutes + ":" + seconds;

        if (t == 59)
        {
            t -= 59;

            minutes = minutes + 1;

            
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

            coins_10.Play();

            Coins += 10;

            CoinsText.text = Coins.ToString();
        }

        if (collision.tag == "Lives")
        {
            Destroy(collision.gameObject);

            lives.Play();

            Lives += 1;

            LivesText.text = Lives.ToString();
        }

        if (collision.tag == "Powerups")
        {
            Destroy(collision.gameObject);

            powerups.Play();

            JumpingForce = 25f;

            GetComponent<SpriteRenderer>().color = Color.gray;

            StartCoroutine(ResetPowerup());
        }

        if (collision.tag == "1000_Points")
        {
            Destroy(collision.gameObject);

            points_1000.Play();

            Score += 1000;

            ScoreText.text = Score.ToString();
        }

        if (collision.tag == "5000_Points")
        {
            Destroy(collision.gameObject);

            points_5000.Play();

            Score += 5000;

            ScoreText.text = Score.ToString();
        }

        if (collision.tag == "Trap")
        {
            GameLevelManager.Respawn();

            Damage(1);
        }

        if (collision.tag == "Checkpoint")
        {
            RespawnPoint = collision.transform.position;

            if (!AlreadyPlayed)
            {
                checkpoint.Play();

                AlreadyPlayed = true;
            }
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

                    hurt.Play();
                }


                else
                {
                    //Enemy is to my left and I should be damage and move right

                    RigidBody.velocity = new Vector2(HurtfulForce, RigidBody.velocity.y);

                    hurt.Play();
                }
            }

        }

        if (other.gameObject.tag == "Obstruction")
        {
            state = State.Hurt;

            ObstructionScoreandHealthHandler();

            if (other.gameObject.transform.position.x > transform.position.x)
            {
                //Enemy is to my right and I should be damage and move left

                RigidBody.velocity = new Vector2(-HurtfulForce, RigidBody.velocity.y);

                hurt.Play();
            }

            else
            {
                //Enemy is to my left and I should be damage and move right

                RigidBody.velocity = new Vector2(HurtfulForce, RigidBody.velocity.y);

                hurt.Play();
            }
        }
    }

    private void ObstructionScoreandHealthHandler()
    {
        Score -= 300;

        ScoreText.text = Score.ToString();

        if (Score <= 0)
        {
            Lives -= 1;

            LivesText.text = Lives.ToString();

            transform.position = RespawnPoint;

            Score = 2000;

            ScoreText.text = Score.ToString();
        }

        if (Lives <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void ScoreandHealthHandler()
    {
        Score -= 500;

        ScoreText.text = Score.ToString();

        if (Score <= 0)
        {
            Lives -= 1;

            LivesText.text = Lives.ToString();

            transform.position = RespawnPoint;

            Score = 2000;

            ScoreText.text = Score.ToString();
        }

        if (Lives <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void Directions()
    {
        float HorizontalDirection = Input.GetAxis("Horizontal");

        if (CanClimb && Mathf.Abs(Input.GetAxis("Vertical")) > .1f)
        {
            state = State.Climbing;

            RigidBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

            transform.position = new Vector3(ladder.transform.position.x, RigidBody.position.y);

            RigidBody.gravityScale = 0f;
        }

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
        if (state == State.Climbing)
        {

        }

        else if (state == State.Jumping)
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

    private void Climb()
    {
        if (Input.GetButtonDown("Jump"))
        {
            RigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

            CanClimb = false;

            RigidBody.gravityScale = NaturalGravity;

            PlayerAnimation.speed = 1f;

            Jump();

            Score -= 100;

            ScoreText.text = Score.ToString();

            return;
        }

        float VerticalDirection = Input.GetAxis("Vertical");

        //Climbing Up
        if (VerticalDirection > .1f && !TopLadder)
        {
            RigidBody.velocity = new Vector2(0f, VerticalDirection * ClimbingSpeed);

            PlayerAnimation.speed = 1f;
        }

        //Climbing Down
        else if (VerticalDirection < -.1f && !BottomLadder)
        {

            RigidBody.velocity = new Vector2(0f, VerticalDirection * ClimbingSpeed);
        }

        //Still
        else
        {
            PlayerAnimation.speed = 0f;

            RigidBody.velocity = Vector2.zero;
        }
    }

    private void Death()
    {
        Debug.Log("Working");
    }

    public void Damage(int damage)
    {
        Lives -= damage;

        LivesText.text = Lives.ToString();

        transform.position = RespawnPoint;

        if (Lives <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    public IEnumerator Reaction(float ReactionDuration, float ReactionPower, Vector3 ReactionDirection)
    {
        float timer = 0;

        while (ReactionDuration > timer)
        {
            timer += Time.deltaTime;

            RigidBody.AddForce(new Vector3(ReactionDirection.x * -30, ReactionDirection.y * ReactionPower, transform.position.z));
        }

        yield return 0;
    }
}


