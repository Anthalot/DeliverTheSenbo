using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerAnimator playerAnimator;
    public PlayerInputs playerInputs;
    public ParticleManager particleManager;

    void Update()
    {
        playerInputs.HandleAllInputs();
        playerAnimator.HandleAllAnimations();
        particleManager.HandleAllParticles();
    }

    void FixedUpdate()
    {
        playerMovement.HandleAllMovement();
    }
}
