using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Pause : MonoBehaviour
{
    private GameObject pause;
    public PostProcessVolume ppVolume;
    private bool isPaused;

    private void Start()
    {
        pause = GameObject.Find("Pause");
        ppVolume.enabled = false;
        pause.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                pause.SetActive(true);
                ppVolume.enabled = !ppVolume.enabled;
                isPaused = true;
                Time.timeScale = 0f;
            }
            else
            {
                pause.SetActive(false);
                ppVolume.enabled = !ppVolume.enabled;
                isPaused = false;
                Time.timeScale = 1f;
            }
        }
    }
}
