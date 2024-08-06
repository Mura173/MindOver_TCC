using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    float horizontalInput;
    bool isFacingRight = false;
    public float speed = 5f;
    bool isGrounded = false;

    public float jumpPower = 5f;
    public float jumpTime = 0.35f;
    public float jumpTimeCounter;
    public bool isJumping;

    private Transform posStart;

    [SerializeField]
    bool canJump = false;

    [SerializeField]
    Rigidbody2D vivotia;

    Animator anim;
    void Start()
    {
        vivotia = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        posStart = GameObject.Find("posStart").transform;

        vivotia.transform.position = posStart.position;
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        FlipSprite();

        if (Input.GetButtonDown("Jump") && canJump == true)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            vivotia.velocity = new Vector2(vivotia.velocity.x, jumpPower);
        }

        if (Input.GetButton("Jump") && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                vivotia.velocity = new Vector2(vivotia.velocity.x, jumpPower);
                jumpTimeCounter -= Time.deltaTime;
                isGrounded = false;
                anim.SetBool("isJumping", !isGrounded);
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        AttackAnim();
    }

    void FixedUpdate()
    {
        vivotia.velocity = new Vector2(horizontalInput * speed, vivotia.velocity.y);
        anim.SetFloat("xVelocity", Math.Abs(vivotia.velocity.x));
        anim.SetFloat("yVelocity", vivotia.velocity.y);
    }

    void FlipSprite()
    {
        if(isFacingRight == true && horizontalInput > 0f || !isFacingRight && horizontalInput < 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private void AttackAnim()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            anim.SetBool("isAttacking", true);
        }

        if (!Input.GetKeyDown(KeyCode.M))
        {
            anim.SetBool("isAttacking", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        canJump = true;
        isGrounded = true;
        anim.SetBool("isJumping", !isGrounded);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        canJump = false;
    }
}
