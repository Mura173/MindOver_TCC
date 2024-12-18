using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Novelo : MonoBehaviour
{
    public float jumpForce;
    public float speed;
    private Rigidbody2D rb;
    private bool isGrounded;
    public float jumpInterval = 1f;
    private float jumpTimer;

    public int damage;
    public CharacterHealth playerHealth;

    private AudioSource audioSource;
    public AudioClip boingClip;

    private Transform player;
    public float activationDistance = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpTimer = jumpInterval;
        audioSource = GetComponent<AudioSource>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);

        jumpTimer -= Time.deltaTime;

        // Calcula a distancia entre o jogador e o objeto "Novelo"
        float distanceToPlayer = Vector2.Distance(player.position, transform.position);

        if (isGrounded && jumpTimer <= 0f)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            // Toca o som apenas se o jogador estiver proximo
            if (distanceToPlayer <= activationDistance)
            {
                audioSource.PlayOneShot(boingClip);
            }

            // Resetando o temporizador
            jumpTimer = jumpInterval;
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ColliderNovelo"))
        {
            speed = -speed;

            if (speed > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0); // Olhar para a direita
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0); // Olhar para a esquerda
            }
        }
    }
}
