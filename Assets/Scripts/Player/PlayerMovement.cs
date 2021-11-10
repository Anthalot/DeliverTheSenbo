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
    float dashInternalCooldown;
    float currentDashTimer;
    public bool dashing;
    bool canDash;

    float internalJumpTimer;
    public bool isJumping;
    int direction;
    int lookDirection;
    float collisionNormal;

    void Start()
    {
        canDash = true;
        internalJumpTimer = jumpTimer;
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
        if(playerInputs.horizontalInput != 0 && !dashing) lookDirection = ((int)playerInputs.horizontalInput);
        if(lookDirection == 1) spriteRenderer.flipX = false;
        else if(lookDirection == -1) spriteRenderer.flipX = true;
    }
    /// <summary>
    /// Handles the movement of the player (horizontal movement, jump and dash).
    /// </summary>
    private void HandleMovement()
    {
        #region Walking
        direction = ((int)playerInputs.horizontalInput);
        if(!dashing) rb.velocity = new Vector2(direction * speed , rb.velocity.y);
        #endregion
        #region Jumping
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
        #endregion
        #region Dash
        if(Grounded() && dashInternalCooldown <= 0f && playerInputs.dash == 0f) canDash = true;
        if(dashInternalCooldown > 0f) dashInternalCooldown -= Time.deltaTime;
        if(dashing){
            rb.velocity = transform.right * lookDirection * dashForce;
            currentDashTimer -= Time.deltaTime;
            
            if(currentDashTimer <= 0f) dashing = false;
        }

        if(playerInputs.dash == 1 && dashInternalCooldown <= 0f && canDash){
            jumpTimer = 0;
            isJumping = true;
            canDash = false;
            dashInternalCooldown = dashCooldown;
            dashing = true;
            currentDashTimer = dashTimer;
            rb.velocity = Vector2.zero;
        }
        #endregion
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
            if(collisionNormal < 0) jumpTimer = 0;
            Debug.Log(collisionNormal);
        }
    }
}
