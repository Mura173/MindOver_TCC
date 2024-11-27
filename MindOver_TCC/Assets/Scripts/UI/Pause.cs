using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Pause : MonoBehaviour
{
    private GameObject pause;
    public PostProcessVolume ppVolume;
    private bool isPaused;

    private AudioSource audioSource;
    public AudioClip pauseClip;

    private void Start()
    {
        pause = GameObject.Find("Pause");
        ppVolume.enabled = false;
        pause.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            audioSource.PlayOneShot(pauseClip);

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
