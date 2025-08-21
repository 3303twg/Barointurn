using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public Animator animator;
    private SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

    private Vector3 previousPosition;

    private static readonly int IsRunHash = Animator.StringToHash("IsRun");
    private static readonly int IsDeadHash = Animator.StringToHash("IsDead");
    private static readonly int IsHitHash = Animator.StringToHash("IsHit");


    public void Init()
    {
        animator.SetBool(IsDeadHash, false);
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector3 currentPosition = transform.position;

        if (currentPosition.x > previousPosition.x)
        {
            spriteRenderer.flipX = false;
        }
        else if (currentPosition.x < previousPosition.x)
        {
            spriteRenderer.flipX = true;
        }

        if (rb.velocity.magnitude > 0.01f)
        {
            animator.SetBool(IsRunHash, true);
        }
        else
        {
            animator.SetBool(IsRunHash, false);
        }

        previousPosition = currentPosition;
    }

    public void Die()
    {
        animator.SetBool(IsDeadHash, true);
    }
    public void Hit()
    {
        animator.SetTrigger(IsHitHash);
    }




    //네이밍 좀 애매하네
    public void UnscaledMode()
    {
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    public void ResetMode()
    {
        animator.updateMode = AnimatorUpdateMode.Normal;
    }
}
