using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoCobra : MonoBehaviour
{
    public float speed;
    public float distance;
    public bool isRight = true;

    private Rigidbody2D rb;

    Animator anim;

    EnemyDamageTaken life;

    public Transform groundCheck;

    public Character ch;

    void Start()
    {
        life = GetComponent<EnemyDamageTaken>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Posicao de origem, direcao e distancia
        RaycastHit2D ground = Physics2D.Raycast(groundCheck.position, Vector2.down, distance);

        if(ground.collider == false)
        {
            if (isRight == true)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                isRight = true;
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        // Reduz a vida do inimigo
        life.life -= damageAmount;       

        StartCoroutine(EnemyGetHurt());

        // Opcional: Reiniciar o parâmetro DamageTaken após um tempo
        StartCoroutine(ResetDamageTakenBool());
    }

    private IEnumerator ResetDamageTakenBool()
    {
        // Espera o tempo da animação de dano (ajuste o tempo conforme necessário)
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator EnemyGetHurt()
    {
        GetComponent<Animator>().SetLayerWeight(1, 1);
        yield return new WaitForSeconds(3);
        GetComponent<Animator>().SetLayerWeight(1, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ch.kbCount = ch.kbTime;

            if (other.transform.position.x <= transform.position.x)
            {
                ch.isKnockRight = true;
            }
            else if (other.transform.position.x > transform.position.x)
            {
                ch.isKnockRight = false;
            }
        }
    }
}
