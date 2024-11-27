using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Load : MonoBehaviour
{
    public UnityEngine.UI.Slider progressBar;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    // Update is called once per frame
    private IEnumerator LoadSceneAsync()
    {
        yield return new WaitForSeconds(2f);

        // Obt�m o �ndice da cena atual
        int cenaAtualIndex = SceneManager.GetActiveScene().buildIndex;

        // Obt�m o �ndice da pr�xima cena
        int proximaCenaIndex = cenaAtualIndex + 1;

        // Verifica se h� uma pr�xima cena
        if (proximaCenaIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Come�a o carregamento ass�ncrono da pr�xima cena
            AsyncOperation operation = SceneManager.LoadSceneAsync(proximaCenaIndex);

            // Impede que a cena seja ativada at� que o carregamento tenha terminado
            operation.allowSceneActivation = false;

            // Enquanto a cena n�o estiver completamente carregada...
            while (!operation.isDone)
            {
                // Atualiza a barra de progresso
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                progressBar.value = progress;

                // Quando a cena estiver carregada, permite a ativa��o
                if (operation.progress >= 0.9f)
                {
                    operation.allowSceneActivation = true;
                }

                yield return null; // Espera o pr�ximo frame
            }
        }
    }
}
