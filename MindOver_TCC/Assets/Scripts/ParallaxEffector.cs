using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffector : MonoBehaviour
{
    public Camera cam; // Refer�ncia � c�mera
    public Transform[] backgrounds; // Array de transform dos fundos
    public float[] parallaxScales; // Propor��o de movimento do parallax para cada fundo
    public float smoothing = 1f; // Valor para suavizar o movimento do parallax

    private Vector3 previousCamPos; // Posi��o da c�mera na frame anterior

    void Start()
    {
        // A posi��o inicial da c�mera
        previousCamPos = cam.transform.position;

        // Inicializa o array de parallaxScales com base na quantidade de backgrounds
        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1; // Define a escala de parallax baseada na posi��o Z dos fundos
        }
    }

    void Update()
    {
        // Para cada fundo
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // O parallax � o oposto do movimento da c�mera multiplicado pela escala de parallax
            float parallax = (previousCamPos.x - cam.transform.position.x) * parallaxScales[i];

            // Define uma posi��o alvo X que � a posi��o atual mais o parallax
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            // Cria a nova posi��o com o backgroundTargetPosX
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // Suaviza a transi��o entre a posi��o atual e a posi��o alvo
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        // Define a previousCamPos para a posi��o atual da c�mera no final do frame
        previousCamPos = cam.transform.position;
    }
}

