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

    bool isAttacking = false;
    public float attackDuration = 0.7f;
    float attackTimer = 0.0f;

    public float jumpPower = 5f;
    public float jumpTime = 0.35f;
    public float jumpTimeCounter;
    public bool isJumping;

    private Transform posStart;

    public ParticleSystem dust;

    [SerializeField]
    bool canJump = false;

    [SerializeField]
    Rigidbody2D vivotia;

    Animator anim;

    public float kbForce;
    public float kbCount;
    public float kbTime;

    public bool isKnockRight;

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
            CreateDust();

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
        KnockBack();       

        anim.SetFloat("xVelocity", Math.Abs(vivotia.velocity.x));
        anim.SetFloat("yVelocity", vivotia.velocity.y);
    }

    void Move()
    {
        if (isGrounded)
        {
            vivotia.velocity = new Vector2(horizontalInput * speed, vivotia.velocity.y);
        }
        else
        {
            vivotia.velocity = new Vector2(horizontalInput * speed * 0.9f, vivotia.velocity.y);
        }
    }

    void KnockBack()
    {
        if (kbCount < 0)
        {
            Move();
        }
        else
        {
            if (isKnockRight == true)
            {
                vivotia.velocity = new Vector2(-kbForce, kbForce);
            }

            if (isKnockRight == false)
            {
                vivotia.velocity = new Vector2(kbForce, kbForce);
            }
        }

        kbCount -= Time.deltaTime;
    }

    void FlipSprite()
    {
        if (isFacingRight == true && horizontalInput > 0f || !isFacingRight && horizontalInput < 0f)
        {
            CreateDust();
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1;
            transform.localScale = ls;
        }
    }

    private void AttackAnim()
    {
        if (Input.GetKeyDown(KeyCode.M) && !isAttacking)
        {
            isAttacking = true;
            anim.SetBool("isAttacking", true);
            attackTimer = attackDuration;
        }

        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                isAttacking = false;
                anim.SetBool("isAttacking", false);
            }
        }
    }

    void CreateDust()
    {
        dust.Play();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            canJump = true;
            isGrounded = true;
            anim.SetBool("isJumping", !isGrounded);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            canJump = false;
        }
    }
}
