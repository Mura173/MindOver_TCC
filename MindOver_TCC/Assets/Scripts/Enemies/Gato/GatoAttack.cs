using System.Collections;
using UnityEngine;

public class GatoAttack : MonoBehaviour
{
    [SerializeField]
    private Transform pontoAtaque;

    [SerializeField]
    private float raioAtaque;

    [SerializeField]
    private LayerMask layersAtaque;

    private bool canAttack;
    private bool isAttacking = false;
    private Animator anim;

    private GameObject player;
    public float speed;

    public int damage;
    public CharacterHealth playerHealth;

    private bool isFacingRight = true;

    private GameObject placaDeAtencao;
    public Animator doorAnim;

    public AudioSource audioSource;
    public AudioClip swordClip, swordSlashClip, swordSlashClip1;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        placaDeAtencao = GameObject.Find("Atencao");

        placaDeAtencao.SetActive(false);
    }

    void Update()
    {
        CheckPlayerPosition();

        Collider2D colliderPlayer = Physics2D.OverlapCircle(this.pontoAtaque.position, this.raioAtaque, this.layersAtaque);
        if (colliderPlayer != null && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    void AtkCurto()
    {
        Collider2D colliderPlayer = Physics2D.OverlapCircle(this.pontoAtaque.position, this.raioAtaque, this.layersAtaque);

        if (colliderPlayer != null)
        {
            playerHealth = colliderPlayer.GetComponent<CharacterHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    void AtkLongo()
    {
        Collider2D gatoCollider = GetComponent<Collider2D>();

        if (gatoCollider is CapsuleCollider2D capsuleCollider)
        {
            capsuleCollider.size = new Vector2(capsuleCollider.size.x / 2, capsuleCollider.size.y / 2); // Reduz pela metade
        }

        // Ativar o Trail Renderer
        TrailRenderer trail = GetComponent<TrailRenderer>();
        if (trail != null)
        {
            trail.enabled = true;
        }

        transform.position = player.transform.position;

        StartCoroutine(DisableTrailAfterTime(trail));
        StartCoroutine(RestoreColliderSize(gatoCollider));
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        placaDeAtencao.SetActive(true);
        anim.SetBool("atkcurto", true);
        yield return new WaitForSeconds(1f); // Duracao da animacao
        anim.SetBool("atkcurto", false);
        anim.SetBool("attackingCurto", true);  
        AtkCurto();
        PlaySound(swordClip);
        placaDeAtencao.SetActive(false);
        yield return new WaitForSeconds(1f);
        anim.SetBool("attackingCurto", false);
        yield return new WaitForSeconds(1f); // Tempo de recarga do ataque
        anim.SetBool("atkLongo", true);
        yield return new WaitForSeconds(1f);
        AtkLongo();
        PlaySound(swordSlashClip);
        placaDeAtencao.SetActive(true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("atkLongo", false);
        anim.SetBool("attackingCurto", true);
        AtkCurto();
        PlaySound(swordSlashClip1);
        placaDeAtencao.SetActive(false);
        yield return new WaitForSeconds(1f);
        anim.SetBool("attackingCurto", false);
        isAttacking = false;
    }

    IEnumerator DisableTrailAfterTime(TrailRenderer trail)
    {
        yield return new WaitForSeconds(0.2f);
        if (trail != null)
        {
            trail.enabled = false;
        }
    }

    IEnumerator RestoreColliderSize(Collider2D gatoCollider)
    {
        yield return new WaitForSeconds(1f);

        if (gatoCollider is CapsuleCollider2D capsuleCollider)
        {
            capsuleCollider.size = new Vector2(capsuleCollider.size.x * 2, capsuleCollider.size.y * 2); // Restaura o tamanho original
        }
    }

    private void OnDrawGizmos()
    {
        if (pontoAtaque != null)
        {
            Gizmos.DrawWireSphere(this.pontoAtaque.position, this.raioAtaque);
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

    private void OnDestroy()
    {
        doorAnim.SetBool("close", false);
        doorAnim.SetBool("open", true);
        placaDeAtencao.SetActive(false);
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
