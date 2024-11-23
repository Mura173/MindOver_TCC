using System.Collections;
using UnityEngine;

public class A_BossFogo : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;
    private bool isFacingRight = false;
    private bool canMove = false;

    public GameObject foguinho;
    public Transform instantiatePoint;
    public float speed;

    public int damage;
    public CharacterHealth playerHealth;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        CheckPlayerPosition();

        // Inicia o ataque ao pressionar a tecla "L"
        if (Input.GetKeyDown(KeyCode.L))
        {
            Attacking();
        }

        // Se permitido, movimenta o BossFogo na dire��o do jogador
        if (canMove)
        {
            MoveTowardsPlayer();
        }
    }

    public void Attacking()
    {
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1f);  // Aguarda antes de come�ar o ataque

        for (int i = 0; i < 3; i++)
        {
            Instantiate(foguinho, instantiatePoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1.5f); // Aguarda entre as inst�ncias
        }

        yield return new WaitForSeconds(2f); // Pausa ap�s instanciar os foguinhos

        canMove = true; // Permite que o BossFogo comece a se mover
    }

    void MoveTowardsPlayer()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    void CheckPlayerPosition()
    {
        // Ajusta a orienta��o do sprite (virado para a dire��o do jogador)
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
