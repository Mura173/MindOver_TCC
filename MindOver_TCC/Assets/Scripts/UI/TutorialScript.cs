using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public Animator anim;
    private LevelLoader levelLoader;

    public AudioSource audioSource;
    public AudioClip nextSceneClip;

    private void Start()
    {
        levelLoader = FindAnyObjectByType<LevelLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("go", true);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            audioSource.PlayOneShot(nextSceneClip);
            levelLoader.LoadNextLevel();
        }
    }
}
