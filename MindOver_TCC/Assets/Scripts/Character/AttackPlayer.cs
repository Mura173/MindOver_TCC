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

    // Update is called once per frame
    void Update()
    {
        if (canAttack && Input.GetKeyDown(KeyCode.Z))
        {
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
        yield return new WaitForSeconds(1);
        canAttack = true;
    }
}
