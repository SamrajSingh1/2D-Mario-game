using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private enum State { idle,running,jumping,falling,hurt}
    private State state = State.idle;
    private Collider2D coll;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jump = 10f;
    private AudioSource footstep;
    [SerializeField] private float hurtForce = 10f;
    private  int cherries = 0;
    [SerializeField] private Text no;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        footstep = GetComponent<AudioSource>();
    }
    private void Update()
    {   if(state!=State.hurt)
        Movement();
        AnimationState();
        anim.SetInteger("state", (int)state);
        no.text = cherries.ToString();
        if (cherries >= 10)
        {
            speed = 15f;
        }
        if (cherries >= 20)
        {
            speed = 18f;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "collectable")
        {
            Destroy(collision.gameObject);
            cherries += 1;
        }
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        Enemy f = collision.gameObject.GetComponent<Enemy>();
        if (collision.gameObject.tag == "Enemy") {
            if (state == State.falling)
            {
                f.JumpedOn();
                rb.velocity = new Vector2(rb.velocity.x, jump);
                state = State.jumping;

            }
            else
            {
                state = State.hurt;
                if (collision.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(-hurtForce, transform.position.y);
                }
                else
                {
                    rb.velocity = new Vector2(hurtForce, transform.position.y);
                }
            }
        }
        if (collision.gameObject.tag == "Powerups")
        {
            Destroy(collision.gameObject);
            cherries += 10;
        }
    }
    private void Movement()
    {
        float h = Input.GetAxis("Horizontal");
        if (h < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            //anim.SetBool("running", true);
        }
        else if (h > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
            //anim.SetBool("running", true);
        }
        else
        {
            //anim.SetBool("running", false);
        }
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            state = State.jumping;
       
        }
    }
    private void AnimationState()
    {
        if (rb.velocity.y < .1f && !coll.IsTouchingLayers(ground))
        {
            state = State.falling;
        }
        if (state == State.jumping)
        {
            if (rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground) && rb.velocity.y==0)
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > .1f)
        {
            state = State.running;

        }
        else
        {
            
            state = State.idle;
           
        }
    }
    private void Foot()
    {
        footstep.Play();
    }

}
