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

        // Obtém o índice da cena atual
        int cenaAtualIndex = SceneManager.GetActiveScene().buildIndex;

        // Obtém o índice da próxima cena
        int proximaCenaIndex = cenaAtualIndex + 1;

        // Verifica se há uma próxima cena
        if (proximaCenaIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Começa o carregamento assíncrono da próxima cena
            AsyncOperation operation = SceneManager.LoadSceneAsync(proximaCenaIndex);

            // Impede que a cena seja ativada até que o carregamento tenha terminado
            operation.allowSceneActivation = false;

            // Enquanto a cena não estiver completamente carregada...
            while (!operation.isDone)
            {
                // Atualiza a barra de progresso
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                progressBar.value = progress;

                // Quando a cena estiver carregada, permite a ativação
                if (operation.progress >= 0.9f)
                {
                    operation.allowSceneActivation = true;
                }

                yield return null; // Espera o próximo frame
            }
        }
    }
}
