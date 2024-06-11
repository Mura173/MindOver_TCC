using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    float horizontalInput;
    bool isFacingRight = false;
    public float speed = 5f;
    public float jumpPower = 5f;
    bool isGrounded = false;

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
            vivotia.velocity = new Vector2(vivotia.velocity.x, jumpPower);
            isGrounded = false;
            anim.SetBool("isJumping", !isGrounded);
        }
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
