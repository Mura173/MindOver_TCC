using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    public Animator doorAnim;
    public BossFogo bossFogo;

    private AudioSource audioSource;
    public AudioClip closeClip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DestroyDoor());
        }
    }

    IEnumerator DestroyDoor()
    {
        doorAnim.SetBool("open", false);
        doorAnim.SetBool("close", true);
        audioSource.PlayOneShot(closeClip);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
