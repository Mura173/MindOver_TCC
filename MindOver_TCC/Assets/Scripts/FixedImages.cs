using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedImages : MonoBehaviour
{
    public Camera cam;
    public GameObject[] background;

    void Update()
    {
        for(int i = 0; i < background.Length; i++)
        {
            // Obt�m a posi��o atual do fundo
            Vector3 newPosition = background[i].transform.position;

            // Atualiza a posi��o x e y do fundo para acompanhar a c�mera
            newPosition.x = cam.transform.position.x;
            newPosition.y = cam.transform.position.y;

            // Aplica a nova posi��o ao fundo
            background[i].transform.position = newPosition;
        }

    }
}
