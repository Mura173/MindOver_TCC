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

    [SerializeField]
    public float jumpPower = 5f;
    public float jumpTime = 0.35f;
    public float jumpTimeCounter;
    public bool isJumping;

    private Transform posStart;

    public ParticleSystem dust;

    [SerializeField]
    bool canJump = false;

    Rigidbody2D vivotia;

    Animator anim;

    public float kbForce;
    public float kbCount;
    public float kbTime;

    public bool isKnockRight;
    public bool hasTakenDamage = false;
    public bool isKnockedBack = false;

    public AudioSource audioSource;
    public AudioClip footstepClip, jumpingClip;

    public float footstepInterval = 0.2f;
    private float footstepTimer = 0.0f;

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

        Jump();

        AttackAnim();
    }

    void FixedUpdate()
    {
        if (!isKnockedBack)
        {
            Move();
            
        }

        if (hasTakenDamage)
        {
            KnockBack();
        }

        anim.SetFloat("xVelocity", Math.Abs(vivotia.velocity.x));
        anim.SetFloat("yVelocity", vivotia.velocity.y);
    }

    void Move()
    {
        if (isGrounded)
        {
            vivotia.velocity = new Vector2(horizontalInput * speed, vivotia.velocity.y);

            // Reproduz som de passos
            if (Mathf.Abs(horizontalInput) > 0.1f) // Certifica-se de que o personagem está se movendo
            {
                footstepTimer -= Time.deltaTime;

                if (footstepTimer <= 0)
                {
                    PlaySound(footstepClip);
                    footstepTimer = footstepInterval; // Reinicia o intervalo do som de passos
                }
            }
            else
            {
                // Reseta o timer quando não está andando
                footstepTimer = 0;
            }
        }
        else
        {
            vivotia.velocity = new Vector2(horizontalInput * speed * 0.9f, vivotia.velocity.y);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && canJump == true)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            vivotia.velocity = new Vector2(vivotia.velocity.x, jumpPower);
            PlaySound(jumpingClip);

            // Mantém a animação de ataque, se estiver atacando
            if (!isAttacking)
            {
                anim.SetBool("isJumping", true);
            }
        }

        if (Input.GetButton("Jump") && isJumping == true)
        {
            CreateDust();

            if (jumpTimeCounter > 0)
            {
                vivotia.velocity = new Vector2(vivotia.velocity.x, jumpPower);
                jumpTimeCounter -= Time.deltaTime;
                isGrounded = false;

                // Só atualiza a animação de pulo se não estiver atacando
                if (!isAttacking)
                {
                    anim.SetBool("isJumping", true);
                }
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
    }

    void KnockBack()
    {
        if (kbCount < 0)
        {
            Move();
        }

        else
        {
            if (isKnockRight)
            {
                vivotia.velocity = new Vector2(-kbForce, kbForce);
            }

            else
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
        if (Input.GetKeyDown(KeyCode.Z) && !isAttacking)
        {
            isAttacking = true;
            anim.SetBool("isAttacking", true);

            // Não desativa a animação de pulo, mas a prioriza visualmente
            anim.SetBool("isJumping", false);
            attackTimer = attackDuration;
        }

        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                isAttacking = false;
                anim.SetBool("isAttacking", false);
                // Reativa a animação de pulo se estiver no ar
                if (!isGrounded && isJumping)
                {
                    anim.SetBool("isJumping", true);
                }
            }
        }
    }

    void CreateDust()
    {
        dust.Play();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            canJump = true;
            isGrounded = true;
            anim.SetBool("isJumping", !isGrounded);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            canJump = false;
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}