using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected AudioSource a;
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        a=GetComponent<AudioSource>();
    }
    public void JumpedOn()
    {
        anim.SetTrigger("Death");

    }
    private void Death()
    {   
        Destroy(this.gameObject);
    }   
    private void foot()
    {
        a.Play();
    }
}
