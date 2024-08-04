using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CobraImpulso : MonoBehaviour
{
    public Rigidbody2D rb;
    public float launchForce = 10f;
    public float launchAngle = 90f;

    private Animator anim;

    void Start()
    {
        Launch();
    }

    public void Launch()
    {
        // anim.SetBool("Voando", true);

        // Converter ângulo em radianos
        float angleInRadians = launchAngle * Mathf.Deg2Rad;

        // Calcula os componentes x e y da força
        float xForce = Mathf.Cos(angleInRadians) * launchAngle;
        float yForce = Mathf.Sin(angleInRadians) * launchAngle;

        // Cria o vetor de força com a magnitude especificada
        Vector2 force = new Vector2(xForce, yForce).normalized * launchForce;

        // Aplica a força
        rb.AddForce(force, ForceMode2D.Impulse);
    }
}
