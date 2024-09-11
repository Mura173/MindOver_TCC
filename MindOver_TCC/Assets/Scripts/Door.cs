using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private GameObject letterE;
    private GameObject door;

    private bool estanaPorta;

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
            SceneManager.LoadScene(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Door"))
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
