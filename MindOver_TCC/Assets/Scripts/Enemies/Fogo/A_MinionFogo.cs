using System.Collections;
using UnityEngine;

public class A_MinionFogo : MonoBehaviour
{
    private Rigidbody2D rb;
    public ParticleSystem smokeParticle;
    public float speed;

    private bool isFacingRight = false;
    private GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        CheckPlayerPosition();
        Move();
    }

    void Move()
    {
        rb.velocity = new Vector2(speed, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Foguinho"))
        {
            Instantiate(smokeParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void CheckPlayerPosition()
    {
        if (player.transform.position.x < transform.position.x && isFacingRight)
        {
            FlipSprite();
        }
        else if (player.transform.position.x > transform.position.x && !isFacingRight)
        {
            FlipSprite();
        }
    }

    void FlipSprite()
    {
        isFacingRight = !isFacingRight;

        // Inverte a escala no eixo X para flipar o sprite
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
