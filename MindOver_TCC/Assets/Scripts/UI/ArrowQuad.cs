using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowQuad : MonoBehaviour
{
    public GameObject z;
    public Animator zAnim;

    public GameObject sugada;
    private bool podeSugar;

    private LevelLoader levelLoader;

    private void Start()
    {
        levelLoader = FindAnyObjectByType<LevelLoader>();

        z.SetActive(false);
        sugada.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Appear());

        Sugada();
    }

    void Sugada()
    {
        if (Input.GetKey(KeyCode.Z) && podeSugar == true)
        {
            z.SetActive(false);
            sugada.SetActive(true);
            StartCoroutine(NextScene());
        }
    }

    IEnumerator Appear()
    {
        yield return new WaitForSeconds(16);
        z.SetActive(true);
        zAnim.SetBool("go", true);
        podeSugar = true;
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(1);
        levelLoader.LoadNextLevel();
    }
}
