using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParCont2 : MonoBehaviour
{
    // Referência à transformação da câmera principal
    Transform cam;
    // Posição inicial da câmera
    Vector3 camStartPos;
    // Posição anterior da câmera
    Vector3 prevCamPos;
    // Vetor de distância entre a posição anterior e atual da câmera
    Vector3 distance;

    // Array de GameObjects que representam os backgrounds
    GameObject[] backgrounds;
    // Array de materiais dos backgrounds
    Material[] mat;
    // Array de velocidades dos backgrounds
    float[] backSpeed;

    // Variável para armazenar a maior distância de um background em relação à câmera
    float farthestBack;

    // Controle da velocidade do parallax, ajustável no editor entre 0.01 e 0.05
    [Range(0.01f, 0.05f)]
    public float parallaxSpeed;

    // Método chamado ao iniciar o script
    void Start()
    {
        // Inicializa a referência da câmera principal
        cam = Camera.main.transform;
        // Armazena a posição inicial da câmera
        camStartPos = cam.position;
        // Armazena a posição anterior da câmera como a inicial
        prevCamPos = camStartPos;

        // Obtém a contagem de filhos (backgrounds) do transform atual
        int backCount = transform.childCount;
        // Inicializa os arrays com o tamanho correspondente ao número de backgrounds
        mat = new Material[backCount];
        backSpeed = new float[backCount];
        backgrounds = new GameObject[backCount];

        // Preenche os arrays de backgrounds e materiais
        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            mat[i] = backgrounds[i].GetComponent<Renderer>().material;

            // Configura o modo de repetição da textura para lidar com a repetição da textura
            mat[i].mainTexture.wrapMode = TextureWrapMode.Repeat;
        }
        // Calcula a velocidade de cada background
        BackSpeedCalculate(backCount);
    }

    // Método para calcular a velocidade do parallax de cada background
    void BackSpeedCalculate(int backCount)
    {
        // Encontra o background mais distante
        for (int i = 0; i < backCount; i++)
        {
            float distanceToCam = backgrounds[i].transform.position.z - cam.position.z;
            if (distanceToCam > farthestBack)
            {
                farthestBack = distanceToCam;
            }
        }

        // Calcula a velocidade de parallax para cada background
        for (int i = 0; i < backCount; i++)
        {
            backSpeed[i] = 1 - (backgrounds[i].transform.position.z - cam.position.z) / farthestBack;
        }
    }

    // Método chamado após todas as atualizações do frame terem sido processadas
    private void LateUpdate()
    {
        // Calcula a distância que a câmera se moveu em relação à posição anterior
        distance = cam.position - prevCamPos;

        // Atualiza a posição do transform atual para seguir a câmera, mantendo a posição Z original
        transform.position = new Vector3(cam.position.x, cam.position.y, transform.position.z);

        // Atualiza o offset da textura dos materiais dos backgrounds para criar o efeito de parallax
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed;
            Vector2 offset = new Vector2(distance.x, distance.y) * speed;
            mat[i].SetTextureOffset("_MainTex", mat[i].GetTextureOffset("_MainTex") + offset);
        }

        // Atualiza a posição anterior da câmera
        prevCamPos = cam.position;
    }
}

