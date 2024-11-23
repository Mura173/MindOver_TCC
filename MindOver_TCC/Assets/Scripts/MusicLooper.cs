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
        audioSource.loop = false;
        audioSource.Play();
    }

    void Update()
    {
        if (audioSource.time >= loopStartTime && !audioSource.isPlaying)
        {
            // Reinicia o áudio no ponto de loop
            audioSource.time = loopStartTime;
            audioSource.Play();
        }

        // Se você tiver um tempo final para o loop, use esse controle
        if (audioSource.time >= loopEndTime)
        {
            audioSource.time = loopStartTime;  // Reinicia o loop
        }
    }
}
