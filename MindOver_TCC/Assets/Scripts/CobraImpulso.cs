using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CobraImpulso : MonoBehaviour
{
    Rigidbody2D rb;
    public float launchForce = 10f;
    public float launchAngle = 90f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Launch();
    }

    void Launch()
    {
        // Converter �ngulo em radianos
        float angleInRadians = launchAngle * Mathf.Deg2Rad;

        // Calcula os componentes x e y da for�a
        float xForce = Mathf.Cos(angleInRadians) * launchAngle;
        float yForce = Mathf.Sin(angleInRadians) * launchAngle;

        // Cria o vetor de for�a com a magnitude especificada
        Vector2 force = new Vector2(xForce, yForce).normalized * launchForce;

        // Aplica a for�a
        rb.AddForce(force, ForceMode2D.Impulse);
    }
}
