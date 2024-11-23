using System.Collections;
using UnityEngine;

public class BossFogo : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;
    private bool isFacingRight = true;

    public GameObject foguinho;
    public Transform instantiatePoint;
    public ParticleSystem smoke;

    private int maxHeight = 10;
    public int height;
    public float scaleFactor;

    public int damage;
    public CharacterHealth playerHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        height = maxHeight;
    }

    void Update()
    {
        CheckPlayerPosition();

        // Inicia o ataque ao pressionar a tecla "L"
        if (Input.GetKeyDown(KeyCode.L))
        {
            Attacking();
        }
    }

    public void Attacking()
    {
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 3; i++)
        {
            Instantiate(foguinho, instantiatePoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
            height--;
            DiminuirObjeto();
        }
    }

    private void DiminuirObjeto()
    {
        Vector3 currentScale = transform.localScale;

        // Calcula a diminuição proporcional, mantendo o sinal de X
        float newScaleX = Mathf.Abs(currentScale.x) - scaleFactor;  // Usando o valor absoluto para diminuir
        float newScaleY = currentScale.y - scaleFactor;

        // Garante que a escala não vá abaixo de 0.1
        newScaleX = Mathf.Max(newScaleX, 0.1f);
        newScaleY = Mathf.Max(newScaleY, 0.1f);

        // Aplica o sinal original no eixo X
        if (currentScale.x < 0)
        {
            newScaleX = -newScaleX;  // Reaplica o sinal negativo no eixo X
        }

        // Atualiza a escala do objeto
        transform.localScale = new Vector3(newScaleX, newScaleY, currentScale.z);
    }

    public void AumentarObjeto()
    {
        Vector3 currentScale = transform.localScale;

        // Calcula a diminuição proporcional, mantendo o sinal de X
        float newScaleX = Mathf.Abs(currentScale.x) + scaleFactor;  // Usando o valor absoluto para diminuir
        float newScaleY = currentScale.y + scaleFactor;

        // Garante que a escala não vá abaixo de 0.1
        newScaleX = Mathf.Max(newScaleX, 0.1f);
        newScaleY = Mathf.Max(newScaleY, 0.1f);

        // Aplica o sinal original no eixo X
        if (currentScale.x < 0)
        {
            newScaleX = -newScaleX;  // Reaplica o sinal negativo no eixo X
        }

        // Atualiza a escala do objeto
        transform.localScale = new Vector3(newScaleX, newScaleY, currentScale.z);
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


        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
