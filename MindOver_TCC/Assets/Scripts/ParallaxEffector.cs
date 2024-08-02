using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffector : MonoBehaviour
{
    public Camera cam; // Referência à câmera
    public Transform[] backgrounds; // Array de transform dos fundos
    public float[] parallaxScales; // Proporção de movimento do parallax para cada fundo
    public float smoothing = 1f; // Valor para suavizar o movimento do parallax

    private Vector3 previousCamPos; // Posição da câmera na frame anterior

    void Start()
    {
        // A posição inicial da câmera
        previousCamPos = cam.transform.position;

        // Inicializa o array de parallaxScales com base na quantidade de backgrounds
        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1; // Define a escala de parallax baseada na posição Z dos fundos
        }
    }

    void Update()
    {
        // Para cada fundo
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // O parallax é o oposto do movimento da câmera multiplicado pela escala de parallax
            float parallax = (previousCamPos.x - cam.transform.position.x) * parallaxScales[i];

            // Define uma posição alvo X que é a posição atual mais o parallax
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            // Cria a nova posição com o backgroundTargetPosX
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // Suaviza a transição entre a posição atual e a posição alvo
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        // Define a previousCamPos para a posição atual da câmera no final do frame
        previousCamPos = cam.transform.position;
    }
}

