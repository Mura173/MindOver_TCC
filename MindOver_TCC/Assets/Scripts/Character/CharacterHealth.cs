using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int health;

    public Image[] hearts;

    public Sprite fullHeart;

    public Sprite emptyHeart;

    private Character character;

    private GameObject audioSourceObject;
    private AudioSource audioSource;
    public AudioClip healClip;

    // Start is called before the first frame update
    void Start()
    {
        audioSourceObject = GameObject.Find("AudioManagerCollectabiles");
        audioSource = audioSourceObject.GetComponent<AudioSource>();
        health = maxHealth;
        character = GetComponent<Character>();

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        foreach(Image img in hearts)
        {
            img.sprite = emptyHeart;
        }

        for(int i = 0; i < health; i++)
        {
            hearts[i].sprite = fullHeart;
        }

        if(health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void TakeDamage(int damage)
    {
        character.hasTakenDamage = true;
        character.isKnockedBack = true;
        health -= damage;

        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            StartCoroutine(GetHurt());
        }
    }

    IEnumerator GetHurt()
    {
        Physics2D.IgnoreLayerCollision(6, 7);
        GetComponent<Animator>().SetLayerWeight(1, 1);
        yield return new WaitForSeconds(3);
        GetComponent<Animator>().SetLayerWeight(1, 0);
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Espinhos"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coracao"))
        {
            health = maxHealth;
            PlaySound(healClip);
            Destroy(other.gameObject);
        }
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
