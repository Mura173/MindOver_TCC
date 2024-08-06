using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonActivate : MonoBehaviour
{
    public GameObject snake;
    public GameObject cannon;
    public GameObject snakeSpawner;

    public Animator snakeAnim;
    public Animator cannonAnim;

    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cannonAnim.SetBool("Estourado", true);
            Instantiate(snake, cannon.transform);
        }
    }
}
