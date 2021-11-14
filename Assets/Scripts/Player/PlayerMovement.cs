using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInputs playerInputs;
    public SpriteRenderer spriteRenderer;

    [Header("Movement Settings")]
    public Rigidbody2D rb;
    public float speed;
    [Header("Ground Check Settings")]
    public float radius;
    public LayerMask groundLayer;
    public Transform feetPos;
    [Header("Jump Settings")]
    public float jumpForce;
    public float jumpTimer;
    [Header("Dash Settings")]
    public float dashForce;
    public float dashTimer;
    public float dashCooldown;
    [Header("Boost Settings")]
    public Transform fishyPos;
    public float boostCooldown;
    public float boostForce;
    public GameObject boostCam;
    public float boostTimer;
    public float onBoost;
    public GameObject arrow;
    [Header("Others")]
    public bool boosting;
    public bool dashing;
    public bool isJumping;
    float currentBoostTimer;
    bool canBoost;
    Vector2 boostDirection;
    Vector2 dashDirection;
    float onBoostInternal;
    float boostInternalCooldown;
    float dashInternalCooldown;
    float currentDashTimer;
    bool canDash;
    float internalJumpTimer;
    int direction;
    int lookDirection;
    float collisionNormal;

    void Start()
    {
        canDash = true;
        internalJumpTimer = jumpTimer;
        boostInternalCooldown = 0f;
        onBoostInternal = onBoost;
    }
    /// <summary>
    /// Handles all movement.
    /// </summary>
    public void HandleAllMovement()
    {
        HandleMovement();
        HandleDirection();
    }
    /// <summary>
    /// Handles the looking direction of the sprite.
    /// </summary>
    private void HandleDirection()
    {
        if(playerInputs.horizontalInput != 0f && !dashing) lookDirection = ((int)playerInputs.horizontalInput);
        if(rb.velocity.x > 0) spriteRenderer.flipX = false;
        else if(rb.velocity.x < 0) spriteRenderer.flipX = true;

        dashDirection = transform.right * lookDirection * dashForce;
    }
    /// <summary>
    /// Handles the movement of the player (horizontal movement, jump and dash).
    /// </summary>
    private void HandleMovement()
    {
        Walk();
        Jump();
        if(!canBoost)Dash();
        if(!dashing)Boost();
        
        Debug.DrawRay(transform.position, boostDirection, Color.yellow);
    }

    private void Walk()
    {
        direction = ((int)playerInputs.horizontalInput);
        if(!dashing) rb.velocity = new Vector2(direction * speed , rb.velocity.y);
    }

    private void Jump()
    {
        if(Grounded() && playerInputs.jump == 1f && !isJumping && !dashing)
        {
            isJumping = true;
            jumpTimer = internalJumpTimer;
        }

        if(playerInputs.jump == 1f && isJumping && !dashing)
        {
            if(jumpTimer > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, Vector2.up.y * jumpForce);
                jumpTimer -= Time.deltaTime;
            }
        }

        if(playerInputs.jump == 0) isJumping = false;
    }

    private void Dash()
    {
        if(Grounded() && dashInternalCooldown <= 0f && playerInputs.dash == 0f) canDash = true;
        if(dashInternalCooldown > 0f) dashInternalCooldown -= Time.deltaTime;

        if(playerInputs.dash == 1f && dashInternalCooldown <= 0f && canDash)
        {
            jumpTimer = 0;
            isJumping = true;
            canDash = false;
            dashInternalCooldown = dashCooldown;
            dashing = true;
            currentDashTimer = dashTimer;
            rb.velocity = Vector2.zero;
        }

        if(dashing)
        {
            rb.velocity = dashDirection;
            currentDashTimer -= Time.deltaTime;
            if(currentDashTimer <= 0f) dashing = false;
        }
    }

    private void Boost()
    {
        if(boostInternalCooldown <= 0f)
        {
            if(playerInputs.boost == 1f)
            {
                if(onBoostInternal > 0f){
                    arrow.SetActive(true);
                    Vector2 direction = new Vector2(
                        fishyPos.position.x - arrow.transform.position.x,
                        fishyPos.position.y - arrow.transform.position.y
                    );
                    arrow.transform.up = direction;
                    rb.velocity = Vector2.zero;

                    canBoost = true;
                    currentBoostTimer = boostTimer;

                    boostCam.SetActive(true);
                    Time.timeScale = 0f;
                    onBoostInternal -= Time.unscaledDeltaTime;
                }
                else
                {
                    if(currentBoostTimer > 0)boosting = true;
                    boostCam.SetActive(false);
                    Time.timeScale = 1f;
                }
            }
            if(playerInputs.boost == 0f && canBoost && currentBoostTimer > 0)
            {
                onBoostInternal = onBoost;
                boosting = true;
                canBoost = false;

                boostCam.SetActive(false);
                Time.timeScale = 1f;
            }
        }

        if(boosting)
        {
            arrow.SetActive(false);
            boostInternalCooldown = boostCooldown;
            rb.velocity = arrow.transform.up * boostForce;
            currentBoostTimer -= Time.unscaledDeltaTime;
            if(currentBoostTimer <= 0f) boosting = false;
        }

        if(!boosting && playerInputs.boost == 0f)
        {
            onBoostInternal = onBoost;
            canBoost = false;
        }

        if(boostInternalCooldown > 0f) boostInternalCooldown -= Time.unscaledDeltaTime;
    }
    /// <summary>
    /// Returns true if the player is touching an object with the ground layer on it.
    /// </summary>
    public bool Grounded()
    {
        return Physics2D.OverlapCircle(feetPos.position, radius, groundLayer);
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if(collision2D.gameObject.layer == 6)
        {
            collisionNormal = collision2D.GetContact(0).normal.y;
            if(collisionNormal < 0f) jumpTimer = 0f;
        }
    }
}
