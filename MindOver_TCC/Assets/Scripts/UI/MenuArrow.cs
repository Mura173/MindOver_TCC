using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuArrow : MonoBehaviour
{
    public Image arrow;

    private bool isPlay = true;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveArrow();

        if (Input.GetKey(KeyCode.Z) && isPlay == true)
        {
            SceneManager.LoadScene(1);
        }

        if (Input.GetKey(KeyCode.Z) && isPlay == false)
        {
            Application.Quit();
        }
    }

    void MoveArrow()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            arrow.rectTransform.anchoredPosition = new Vector2(40, -130);

            isPlay = false;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            arrow.rectTransform.anchoredPosition = new Vector2(0, 0);

            isPlay = true;
        }
    }
}
