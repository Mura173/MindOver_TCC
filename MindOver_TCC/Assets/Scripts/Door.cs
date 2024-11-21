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

    void Awake()
    {
        letterE = GameObject.Find("letterE");
        door = GameObject.FindGameObjectWithTag("Door");

        letterE.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && estanaPorta == true)
        {
            SceneManager.LoadScene(3);
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
