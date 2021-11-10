using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    [Header("Particles")]
    public ParticleSystem jumpParticle;
    public ParticleSystem dashParticle;
    public ParticleSystem stepParticle;
    public ParticleSystem landParticle;

    bool isJumping;
    float collisionNormal;

    public void HandleAllParticles()
    {
        HandleStepParticles();
        HandleJumpParticles();
        HandleDashParticles();
    }
    /// <summary>
    /// Handles the step particles.
    /// </summary>
    void HandleStepParticles()
    {
        if(playerMovement.rb.velocity.x != 0 && playerMovement.Grounded() && !stepParticle.isPlaying) stepParticle.Play();
        if(playerMovement.rb.velocity.x == 0 || !playerMovement.Grounded()) stepParticle.Stop();
    }
    /// <summary>
    /// Handles the jumping particles.
    /// </summary>
    void HandleJumpParticles()
    {
        if(playerMovement.playerInputs.jump == 1 && playerMovement.Grounded()) jumpParticle.Play();
    }
    /// <summary>
    /// Handles the dashing particles.
    /// </sumary>
    void HandleDashParticles()
    {
        if(playerMovement.rb.velocity.x >= 1) dashParticle.gameObject.transform.localScale = new Vector3(1, 1, 1);
        if(playerMovement.rb.velocity.x <= -1) dashParticle.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        if(playerMovement.dashing && !dashParticle.isPlaying) dashParticle.Play();
        
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if(collision2D.gameObject.layer == 6)
        {
            collisionNormal = collision2D.GetContact(0).normal.y;
            if(collisionNormal > 0.5f) landParticle.Play();
        }
    }
}