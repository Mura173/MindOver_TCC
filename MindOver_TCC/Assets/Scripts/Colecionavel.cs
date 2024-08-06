using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colecionavel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D item)
    {
        if (item.gameObject.CompareTag("Colecionavel"))
        {
            Destroy(item.gameObject);
        }
    }
}
