using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D Playermove;
    SpriteRenderer PlayerFlip;
    Animator PLayerAnime;
    BoxCollider2D PlayerCollider;

    public AudioSource JumpSFX;
    public float horizontal;
    public LayerMask PlayerMask;
    public float MoveSpeed = 12.5f;
    public float JumpSpeed = 12.5f;
    float MLR;
    int MaxJump = 1; // Max jumps available 
    enum PlayerAnimeState { PlayerIdle, PlayerWalking, PlayerJump, PlayerFalling }

    void Start()
    {
        Playermove = GetComponent<Rigidbody2D>();
        PLayerAnime = GetComponent<Animator>();
        PlayerFlip = GetComponent<SpriteRenderer>();
        PlayerCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Playermove.velocity = new Vector2(horizontal * MoveSpeed, Playermove.velocity.y); // Move player horizontally

        if (Input.GetButtonDown("Jump") && MaxJump > 0) // Check if jump button is pressed and if max jumps > 0
        {
            // Playermove.velocity = new Vector2(Playermove.velocity.x, JumpSpeed);
            // JumpSFX.Play(); 
            // MaxJump--; 
        }

        if (IsGrounded()) // Check if the player is grounded
        {
            MaxJump = 1; // Reset jumps to 1 when grounded
        }

        PlayerAnimation(); // Update player animations based on movement state
    }

    void PlayerAnimation()
    {
        PlayerAnimeState state;
        if (horizontal > 0)
        {
            state = PlayerAnimeState.PlayerWalking;
            PlayerFlip.flipX = false; // Flip player to face right
        }
        else if (horizontal < 0)
        {
            state = PlayerAnimeState.PlayerWalking;
            PlayerFlip.flipX = true; // Flip player to face left
        }
        else
        {
            state = PlayerAnimeState.PlayerIdle; 
        }

        if (Playermove.velocity.y > 0.1f) // Check if moving upward 
        {
            state = PlayerAnimeState.PlayerJump; // Set jump animation
        }

        if (Playermove.velocity.y < -0.1f) // Check if moving downward 
        {
            state = PlayerAnimeState.PlayerFalling; // Set falling animation
        }

        PLayerAnime.SetInteger("stateAnime", (int)state); // Update animation state
    }

    bool IsGrounded()
    {
        return Physics2D.BoxCast(PlayerCollider.bounds.center, PlayerCollider.bounds.size, 0f, Vector2.down, 0.1f, PlayerMask); // Check for ground collision
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x; // Get horizontal input value
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (MaxJump > 0) // Allow jump if max jumps available
        {
            Playermove.velocity = new Vector2(Playermove.velocity.x, JumpSpeed); // Apply vertical jump speed
            JumpSFX.Play(); // Play jump sound effect
            MaxJump--; // Decrease available jumps
        }
    }
}
