using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterDeath : MonoBehaviour
{
    CharacterHealth characterHealth;

    void Start()
    {
        characterHealth = GetComponent<CharacterHealth>();
    }

    void Update()
    {
        if(characterHealth.health <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Espinhos"))
        {
            SceneManager.LoadScene(0);
        }
    }
}
