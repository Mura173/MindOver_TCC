using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CobraAmarela : MonoBehaviour
{
    public float radius = 5.0f;  // Raio do c�rculo
    public float speed = 2.0f;   // Velocidade de rota��o

    private Vector2 centerPosition;  // Posi��o central do c�rculo
    private float angle;             // �ngulo atual da rota��o

    void Start()
    {
        // Define a posi��o central como a posi��o inicial do objeto
        centerPosition = transform.position;
    }

    void Update()
    {
        // Incrementa o �ngulo ao longo do tempo
        angle += speed * Time.deltaTime;

        // Calcula a nova posi��o da cobra
        float x = centerPosition.x + Mathf.Cos(angle) * radius;
        float y = centerPosition.y + Mathf.Sin(angle) * radius;

        // Atualiza a posi��o da cobra
        transform.position = new Vector2(x, y);
    }
}
