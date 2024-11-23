using System.Collections;
using UnityEngine;

public class MinionFogo : MonoBehaviour
{
    private Rigidbody2D rb;
    public ParticleSystem smokeParticle;
    public float speed;

    private bool isFacingRight = false;
    private GameObject player;

    private BossFogo bossFogo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        bossFogo = FindObjectOfType<BossFogo>();
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
        if (other.gameObject.CompareTag("ChefeFogo"))
        {
            bossFogo.height++;
            bossFogo.AumentarObjeto();
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Foguinho"))
        {
            Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
        }

        if (other.gameObject.layer == 10)
        {
            FlipSprite();
            speed = -speed;
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
