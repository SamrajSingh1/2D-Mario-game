using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : Enemy
{
    [SerializeField] private float LeftCap;
    [SerializeField] private float RightCap;
    [SerializeField] private float jumpLength=2f;
    [SerializeField] private float jumpHeight=2f;
    [SerializeField] private LayerMask ground;
    
    private bool facingLeft = true;
    
 
    private Collider2D coll;
    private Rigidbody2D rb;
    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

    }
    public void Update()
    {
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < .1f)
            {
                anim.SetBool("falling", true);
                anim.SetBool("jumping", false);
            }
        }
        if(coll.IsTouchingLayers(ground) && anim.GetBool("falling"))
        {
            anim.SetBool("falling", false);
        }
    }
    private void Move()
    {
        if (facingLeft)
        {

            if (transform.position.x > LeftCap)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                    anim.SetBool("jumping", true);
                }
            }
            else
            {
                facingLeft = false;
            }
        }
        else
        {

            if (transform.position.x < RightCap)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                    anim.SetBool("jumping", true);
                }
            }
            else
            {
                facingLeft = true;
            }
        }
    }

    
}
