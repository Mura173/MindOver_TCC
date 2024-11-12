using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuArrow : MonoBehaviour
{
    public Image arrow;
    public GameObject buttonPanel;

    private bool isPlay = true;
    private bool isInStart;

    public Animator panelButtonAnim, textAnim, bgAnim, arrowAnim;

    public float xAnch, yAnch;

    private void Start()
    {
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
                // SceneManager.LoadScene(1);
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
            arrow.rectTransform.anchoredPosition = new Vector2(-170, -930);
            isPlay = false;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            arrow.rectTransform.anchoredPosition = new Vector2(yAnch, xAnch);

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
    }
}
