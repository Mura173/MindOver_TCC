using UnityEngine;

public class MusicLooper : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip musicClip;
    public float loopStartTime;
    public float loopEndTime;

    void Start()
    {
        // Inicia a música
        audioSource.clip = musicClip;
        audioSource.loop = true;  // Define o loop infinito
        audioSource.Play();
    }

    void Update()
    {
        if (audioSource.time >= loopEndTime)
        {
            // Se o tempo da música alcançar o fim do loop, reinicie o áudio no ponto de loop
            audioSource.time = loopStartTime;
        }
    }
}
