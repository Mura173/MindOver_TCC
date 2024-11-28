using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CobraChefe : MonoBehaviour
{
    public GameObject bullets;
    public Transform bulletPos;

    private float timer;
    private GameObject player;

    private bool isFacingRight;

    public AudioSource audioSource;
    public AudioClip shootingClip, tookDamage;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerPosition();

        timer += Time.deltaTime;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < 15)
        {
            timer += Time.deltaTime;

            if (timer > 4)
            {
                timer = 0;
                Shoot();
            }
        }
        
    }

    void Shoot()
    {
        Instantiate(bullets, bulletPos.position, Quaternion.Euler(0f, 0f, 90f));
        PlaySound(shootingClip);
    }

    void CheckPlayerPosition()
    {
        if (player.transform.position.x < transform.position.x && isFacingRight)
        {
            FlipSprite();
        }
        else if (player.transform.position.x > transform.position.x && !isFacingRight)
        {
            FlipSprite();
        }
    }

    void FlipSprite()
    {
        isFacingRight = !isFacingRight;

        // Inverte a escala no eixo X para flipar o sprite
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
