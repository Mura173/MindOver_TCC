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

    private GameObject audioSourceObject;
    private AudioSource audioSource;

    public AudioClip tookDamageClip;
    public AudioClip lastDamageSound;

    private MusicLooper musicLooper;

    private bool isChefe;

    public GameObject colChefe;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        musicLooper = FindAnyObjectByType<MusicLooper>();

        audioSourceObject = GameObject.Find("AudioManagerEnemyDamage");

        audioSource = audioSourceObject.GetComponent<AudioSource>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D não encontrado no inimigo!");
        }

        if (CompareTag("Chefe"))
        {
            isChefe = true;
        }
    }

    public void ReceberDano(Vector2 knockbackDirection)
    {
        this.life--;

        audioSource.PlayOneShot(tookDamageClip);

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

            if (isChefe)
            {
                musicLooper.audioSource.Stop();
                audioSource.PlayOneShot(lastDamageSound);
                Instantiate(colChefe, transform.position, Quaternion.identity);
            }
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
