using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform pontoAtaque;

    [SerializeField]
    private float raioAtaque;

    [SerializeField]
    private bool canAttack = true;

    [SerializeField]
    private LayerMask layersAtaque;

    public ParticleSystem particle;

    public AudioSource audioSource;
    public AudioClip[] clips;

    private float hitstopDuration = 0.1f;

    // Update is called once per frame
    void Update()
    {
        if (canAttack && Input.GetKeyDown(KeyCode.Z))
        {
            PlayRandomSound();
            StartCoroutine(Attack());
        }
    }

    // Desenhar coisas na Unity
    private void OnDrawGizmos()
    {
        if (pontoAtaque != null)
        {
            Gizmos.DrawWireSphere(this.pontoAtaque.position, this.raioAtaque);
        }        
    }

    private void Atacar()
    {
        // Verifica se existe um colisor na regiao do raio
        Collider2D colliderInimigo = Physics2D.OverlapCircle(this.pontoAtaque.position, this.raioAtaque, this.layersAtaque);

        if (colliderInimigo != null)
        {
            // Causar dano no inimigo
            EnemyDamageTaken inimigo = colliderInimigo.GetComponent<EnemyDamageTaken>();

            if (inimigo != null)
            {
                StartCoroutine(Hitstop());
                Instantiate(particle, pontoAtaque.position, particle.transform.rotation);
                Vector2 knockbackDirection = (colliderInimigo.transform.position - this.pontoAtaque.position).normalized;
                inimigo.ReceberDano(knockbackDirection);
            }
        }
    }

    IEnumerator Attack()
    {
        Atacar();
        canAttack = false;
        yield return new WaitForSeconds(0.7f);
        canAttack = true;
    }

    private IEnumerator Hitstop()
    {
        float originalTimeScale = Time.timeScale; // Salva o TimeScale original
        Time.timeScale = 0f; // Pausa o tempo
        yield return new WaitForSecondsRealtime(hitstopDuration); // Espera em tempo real
        Time.timeScale = originalTimeScale; // Restaura o TimeScale
    }

    void PlayRandomSound()
    {
        int randomIndex = Random.Range(0, clips.Length);
        audioSource.PlayOneShot(clips[randomIndex], 1f);
    }
}
