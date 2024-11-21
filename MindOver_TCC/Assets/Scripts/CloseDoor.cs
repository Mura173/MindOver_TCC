using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    public Animator doorAnim;

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
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
