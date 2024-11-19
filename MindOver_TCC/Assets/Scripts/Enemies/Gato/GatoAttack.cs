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
    public int damage;
    public CharacterHealth playerHealth;

    private bool isFacingRight = true;

    private GameObject placaDeAtencao;

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

    IEnumerator Attack()
    {
        isAttacking = true;
        placaDeAtencao.SetActive(true);
        anim.SetBool("atkcurto", true);
        yield return new WaitForSeconds(1f); // Duracao da animacao
        anim.SetBool("atkcurto", false);
        anim.SetBool("attackingCurto", true);  
        AtkCurto();
        placaDeAtencao.SetActive(false);
        yield return new WaitForSeconds(1f);
        anim.SetBool("attackingCurto", false);
        yield return new WaitForSeconds(3f); // Tempo de recarga do ataque
        isAttacking = false;
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
}
