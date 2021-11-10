using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;

    public PlayerMovement playerMovement;

    bool walking {get; set;}
    bool falling {get; set;}
    bool jumping {get; set;}
    bool dashing {get; set;}
    bool grounded {get; set;}
    bool idle {get; set;}

    void Start()
    {
        dashing = false;
    }

    public void HandleAllAnimations()
    {
        AnimationUpdater();
    }

    private void AnimationUpdater()
    {
        walking = playerMovement.rb.velocity.x != 0;
        jumping = !playerMovement.Grounded() && playerMovement.rb.velocity.y > 0;
        falling = !playerMovement.Grounded() && playerMovement.rb.velocity.y < 0;
        grounded = playerMovement.Grounded();
        idle = playerMovement.rb.velocity == Vector2.zero;
        dashing = playerMovement.dashing;

        animator.SetBool("Walking", walking);
        animator.SetBool("Falling", falling);
        animator.SetBool("Jumping", jumping);
        animator.SetBool("Grounded", grounded);
        animator.SetBool("Idle", idle);
        animator.SetBool("Dashing", dashing);
    }
}
