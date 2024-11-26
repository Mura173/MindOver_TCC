using UnityEngine;

public class MusicLooper : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip musicClip;
    public float loopStartTime;
    public float loopEndTime;

    void Start()
    {
        // Inicia a m�sica
        audioSource.clip = musicClip;
        audioSource.loop = true;  // Define o loop infinito
        audioSource.Play();
    }

    void Update()
    {
        if (audioSource.time >= loopEndTime)
        {
            // Se o tempo da m�sica alcan�ar o fim do loop, reinicie o �udio no ponto de loop
            audioSource.time = loopStartTime;
        }
    }
}
