using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D playerRidgidbody;
    private SpriteRenderer playerRenderer;
    private Animator playerAnimator;

    private bool facingRight = true;
    private bool canMove = true;
    //Move
    public float maxSpeed;

    //Jump
    private bool grounded = false;
    float groundCheckRadius = 0.2f;

    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpPower;

    // Use this for initialization
    void Start()
    {
        playerRidgidbody = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Hoppa
        if (canMove && grounded && Input.GetAxis("Jump") > 0)
        {
            playerAnimator.SetBool("IsGrounded", false);
            playerRidgidbody.velocity = new Vector2(playerRidgidbody.velocity.x, 0f);
            playerRidgidbody.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            grounded = false;
        }

        //OverlapCircle skapar cirkel vid fötterna och kollar om fötterna är nere vid marken. 
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        playerAnimator.SetBool("IsGrounded", grounded);

        float move = Input.GetAxis("Horizontal");


        if (canMove)
        {
            if (move > 0 && !facingRight)
                Flip();
            else if (move < 0 && facingRight)
                Flip();


            playerRidgidbody.velocity = new Vector2(move * maxSpeed, playerRidgidbody.velocity.y);

            playerAnimator.SetFloat("MoveSpeed", Mathf.Abs(move));
        }
        else
        {
            playerRidgidbody.velocity = new Vector2(0, playerRidgidbody.velocity.y);

            playerAnimator.SetFloat("MoveSpeed", 0);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;

        playerRenderer.flipX = !playerRenderer.flipX;
    }

    public void ToggleCanMove()
    {
        canMove = !canMove;
    }
}
