using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public Animator cannonAnim;
    public Animator cobraAnim;
    public float triggerFrame = 10f; // Frame onde a cobra deve começar a voar
    public float flyHeight = 5f; // Altura que a cobra deve alcançar
    public float flySpeed = 3f; // Velocidade do voo da cobra

    private bool hasTriggered = false;

    void Update()
    {
        // Checa o frame atual da animação do canhão
        float currentFrame = cannonAnim.GetCurrentAnimatorStateInfo(0).normalizedTime * cannonAnim.GetCurrentAnimatorStateInfo(0).length * 30; // Assume 30 fps

        if(currentFrame >= triggerFrame && !hasTriggered)
        {
            StartCoroutine(SnakeFlight());
            hasTriggered = true;
        }

        IEnumerator SnakeFlight()
        {
            // Inicializa a animação de voo
            cobraAnim.SetTrigger("Fly");

            // Move a cobra para cima
            float elapsedTime = 0;
            Vector3 startPosition = transform.position;
            Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y + flyHeight, transform.position.z);

            while(elapsedTime < flyHeight / flySpeed)
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime * flySpeed) / flyHeight);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPosition;

            // Troca para animação de queda
            cobraAnim.SetTrigger("Fall");
        }
    }
}
