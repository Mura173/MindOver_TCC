using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuArrow : MonoBehaviour
{
    public Image arrow;
    public GameObject buttonPanel;

    private bool isPlay, isQuit;
    private bool isInStart;

    public Animator panelButtonAnim, textAnim, bgAnim, arrowAnim;

    public float xAnchUp, yAnchUp;
    public float xAnchDown, yAnchDown;

    private LevelLoader levelLoader;

    // Audio
    public AudioSource audioSource;
    public AudioClip initialEffect, quitEffect, startingEffect;

    private void Start()
    {
        levelLoader = FindAnyObjectByType<LevelLoader>();
        isInStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        StartMenu();

        if (isInStart == false)
        {
            MoveArrow();

            if (Input.GetKeyDown(KeyCode.Z) && isPlay == true)
            {
                // audioSource.clip = initialEffect;
                PlaySound(initialEffect);
                levelLoader.LoadNextLevel();
            }

            if (Input.GetKeyDown(KeyCode.Z) && isQuit == true)
            {
                // audioSource.clip = quitEffect;
                PlaySound(quitEffect);
                Application.Quit();
            }
        }       
    }

    void MoveArrow()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            arrow.rectTransform.anchoredPosition = new Vector2(yAnchDown, xAnchDown);
            isPlay = false;
            isQuit = true;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            arrow.rectTransform.anchoredPosition = new Vector2(yAnchUp, xAnchUp);
            isPlay = true;
            isQuit = false;
        }
    }

    void StartMenu()
    {
        if (isInStart == true)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                audioSource.clip = initialEffect;
                PlaySound(initialEffect);
                textAnim.SetBool("apertouZ", true);
                bgAnim.SetBool("apertouZ", true);
                isInStart = false;
                StartCoroutine(SetPanelActive());
            }          
        }
    }

    IEnumerator SetPanelActive()
    {       
        yield return new WaitForSeconds(1);
        buttonPanel.SetActive(true);
        StartCoroutine(ArrowGo());
    }

    IEnumerator ArrowGo()
    {
        yield return new WaitForSeconds(0.5f);
        arrowAnim.SetBool("go", true);
        isPlay = true;
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
