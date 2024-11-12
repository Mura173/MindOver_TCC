using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuArrow : MonoBehaviour
{
    public Image arrow;
    public GameObject buttonPanel;

    private bool isPlay;
    private bool isInStart;

    public Animator panelButtonAnim, textAnim, bgAnim, arrowAnim;

    public float xAnchUp, yAnchUp;
    public float xAnchDown, yAnchDown;

    private LevelLoader levelLoader;

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

            if (Input.GetKey(KeyCode.Z) && isPlay == true)
            {
                levelLoader.LoadNextLevel();
            }

            if (Input.GetKey(KeyCode.Z) && isPlay == false)
            {
                Application.Quit();
            }
        }       
    }

    void MoveArrow()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            arrow.rectTransform.anchoredPosition = new Vector2(yAnchDown, xAnchDown);
            isPlay = false;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            arrow.rectTransform.anchoredPosition = new Vector2(yAnchUp, xAnchUp);

            isPlay = true;
        }
    }

    void StartMenu()
    {
        if (isInStart == true)
        {
            if (Input.GetKey(KeyCode.Z))
            {
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
}
