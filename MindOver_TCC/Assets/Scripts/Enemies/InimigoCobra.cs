using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoCobra : MonoBehaviour
{
    public float speed;
    public float distance;
    public bool isRight = true;

    Animator anim;

    EnemyDamageTaken life;

    public Transform groundCheck;

    void Start()
    {
        life = GetComponent<EnemyDamageTaken>();
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

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

        // Reseta o parâmetro DamageTaken
        anim.SetBool("DamageTaken", false);
    }

    IEnumerator EnemyGetHurt()
    {
        GetComponent<Animator>().SetLayerWeight(1, 1);
        yield return new WaitForSeconds(3);
        GetComponent<Animator>().SetLayerWeight(1, 0);
    }
}
