using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoCobra : MonoBehaviour
{
    public float speed;
    public float distance;
    public bool isRight = true;

    public Transform groundCheck;
    
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Posicao de origem, direcao e distancia
        RaycastHit2D ground = Physics2D.Raycast(groundCheck.position, Vector2.down, distance);

        if(ground.collider == false)
        {
            if (isRight == true)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                isRight = true;
            }
        }
    }
}
