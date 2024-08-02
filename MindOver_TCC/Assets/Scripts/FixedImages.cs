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
            // Obtém a posição atual do fundo
            Vector3 newPosition = background[i].transform.position;

            // Atualiza a posição x e y do fundo para acompanhar a câmera
            newPosition.x = cam.transform.position.x;
            newPosition.y = cam.transform.position.y;

            // Aplica a nova posição ao fundo
            background[i].transform.position = newPosition;
        }

    }
}
