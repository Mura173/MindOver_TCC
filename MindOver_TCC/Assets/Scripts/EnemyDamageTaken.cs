using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageTaken : MonoBehaviour
{
    public int life;
    public ParticleSystem smoke;
    public float knockbackForce = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public Color damageColor = Color.red;
    public float damageDuration = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D não encontrado no inimigo!");
        }
    }

    public void ReceberDano(Vector2 knockbackDirection)
    {
        this.life--;

        // Aplicar knockback
        if (rb != null)
        {
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }

        if (spriteRenderer != null)
        {
            StartCoroutine(ChangeColorTemporarily());
        }

        if (this.life <= 0)
        {
            Instantiate(smoke, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    private IEnumerator ChangeColorTemporarily()
    {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = damageColor;

        yield return new WaitForSeconds(damageDuration);

        spriteRenderer.color = originalColor;
    }
}
