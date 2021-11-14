using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerAnimator playerAnimator;
    public PlayerInputs playerInputs;
    public ParticleManager particleManager;
    public Rigidbody2D rb;
    public float knockbackForce;
    public float yForce;

    Vector2 hitDirection;
    bool hit;

    void Update()
    {
        playerInputs.HandleAllInputs();
        playerAnimator.HandleAllAnimations();
        particleManager.HandleAllParticles();
        if(!hit) playerMovement.HandleAllMovement();
    }

    void FixedUpdate()
    {

    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if(collision2D.gameObject.tag == "Enemy")
        {
            Debug.Log(collision2D.GetContact(0).normal.y);
            if(collision2D.GetContact(0).normal.y == 1)
            {
                rb.velocity = Vector2.zero;
                if(playerInputs.jump == 1f || playerMovement.boosting) rb.AddForce(Vector2.up * yForce * 5f, ForceMode2D.Impulse);
                else rb.AddForce(Vector2.up * yForce, ForceMode2D.Impulse);
            }
            else
            {
                hitDirection = collision2D.GetContact(0).normal;
                StartCoroutine("Hit");
            } 
        }
    }

    IEnumerator Hit()
    {
        hit = true;
        rb.AddForce(hitDirection * knockbackForce, ForceMode2D.Impulse);
        rb.AddForce(transform.up * yForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        hit = false;
        StopCoroutine(Hit());
    }
}
