using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private GameObject letterE;
    private GameObject door;

    public SpriteRenderer doorSprite;
    private bool estanaPorta;
    public bool portaAberta;

    public Sprite spriteClosed;
    public Sprite spriteOpened;

    public SceneManagement sceneManagement;

    private AudioSource audioSource;
    public AudioClip nextLevelClip;
    void Awake()
    {
        letterE = GameObject.Find("letterE");
        door = GameObject.FindGameObjectWithTag("Door");

        audioSource = GetComponent<AudioSource>();

        letterE.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && estanaPorta == true)
        {
            StartCoroutine(NextLevel());
        }

        if (estanaPorta == false)
        {
            doorSprite.sprite = spriteClosed;
        }

        if (portaAberta == true)
        {
            doorSprite.sprite = spriteOpened;
        }
    }

    IEnumerator NextLevel()
    {
        audioSource.PlayOneShot(nextLevelClip);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneManagement.sceneIndex + 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Door") && portaAberta == true)
        {
            letterE.SetActive(true);
            estanaPorta = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            letterE.SetActive(false);
            estanaPorta = false;
        }
    }
}
