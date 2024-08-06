using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CobraAmarela : MonoBehaviour
{
    public float radius = 5.0f;  // Raio do círculo
    public float speed = 2.0f;   // Velocidade de rotação

    private Vector2 centerPosition;  // Posição central do círculo
    private float angle;             // Ângulo atual da rotação

    void Start()
    {
        // Define a posição central como a posição inicial do objeto
        centerPosition = transform.position;
    }

    void Update()
    {
        // Incrementa o ângulo ao longo do tempo
        angle += speed * Time.deltaTime;

        // Calcula a nova posição da cobra
        float x = centerPosition.x + Mathf.Cos(angle) * radius;
        float y = centerPosition.y + Mathf.Sin(angle) * radius;

        // Atualiza a posição da cobra
        transform.position = new Vector2(x, y);
    }
}
