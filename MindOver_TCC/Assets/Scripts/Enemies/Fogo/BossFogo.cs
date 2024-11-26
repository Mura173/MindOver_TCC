using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

public class BossFogo : MonoBehaviour
{
    public GameObject foguinho;
    public Transform instantiatePoint;
    public ParticleSystem smoke;

    private int maxHeight = 10;
    public int height;
    public float scaleFactor;
    public float speed;

    public int damage;
    public CharacterHealth playerHealth;
    public GameObject placadeAtencao;

    private Rigidbody2D rb;
    private GameObject player;
    private bool isFacingRight = true;

    [SerializeField]
    private bool canMove = false;

    private Animator anim;

    private Door doorScript;

    private AudioSource audioSource;

    public AudioSource audioSourceEnemyDamage;

    public AudioClip instantiateClip, runClip, deathClip;

    private MusicLooper musicLooper;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        doorScript = FindAnyObjectByType<Door>();

        musicLooper = FindAnyObjectByType<MusicLooper>();

        audioSource = GetComponent<AudioSource>();

        height = maxHeight;

        placadeAtencao.SetActive(false);
    }

    void Update()
    {
        CheckPlayerPosition();

        if (height <= 2)
        {
            Instantiate(smoke, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void Attack()
    {
        StartCoroutine(Spawn());
    }

    void Move()
    {
        if (canMove)
        {
            StartCoroutine(ShowAttention());
            anim.SetBool("walking", true);
            anim.SetBool("idle", false);
            rb.velocity = new Vector2(speed, rb.velocity.y);
            PlaySound(runClip);
            StartCoroutine(Spawn());
        }
    }

    public IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("idle", true);
        anim.SetBool("walking", false);

        for (int i = 0; i < 2; i++)
        {
            PlaySound(instantiateClip);

            // Verifica se o jogador está à direita ou à esquerda do BossFogo
            float direction = (player.transform.position.x > transform.position.x) ? 1f : -1f;

            speed = Mathf.Abs(speed) * direction;

            // Instancia o foguinho e passa a direcao para a velocidade
            GameObject foguinhoInstanciado = Instantiate(foguinho, instantiatePoint.transform.position, Quaternion.identity);

            MinionFogo foguinhoScript = foguinhoInstanciado.GetComponent<MinionFogo>();
            if (foguinhoScript != null)
            {
                foguinhoScript.speed *= -direction;
            }

            yield return new WaitForSeconds(1f);
            height--;
            DiminuirObjeto();
        }

        yield return new WaitForSeconds(2.5f);
        canMove = true;
        Move();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 10)
        {
            speed = -speed;
        }
    }

    IEnumerator ShowAttention()
    {
        placadeAtencao.SetActive(true);
        yield return new WaitForSeconds(2f);
        placadeAtencao.SetActive(false);
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

    private void OnDestroy()
    {
        audioSourceEnemyDamage.PlayOneShot(deathClip);
        musicLooper.audioSource.Stop();
        doorScript.portaAberta = true;
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
