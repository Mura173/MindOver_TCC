using UnityEngine;

public class FogoPulando : MonoBehaviour
{
    public float jumpForce = 5f;
    public float jumpInterval = 2f;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating(nameof(Jump), jumpInterval, jumpInterval); // Faz o inimigo pular repetidamente
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void Update()
    {
        // Detecta se está tocando o chao
        isGrounded = Physics2D.OverlapCircle(transform.position, 0.1f, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Jogador atingido!");
        }
    }
}
