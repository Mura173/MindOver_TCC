using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colecionavel : MonoBehaviour
{
    public CollectableManager cm;
    private void OnTriggerEnter2D(Collider2D item)
    {
        if (item.gameObject.CompareTag("Colecionavel"))
        {
            cm.colCount++;
            Destroy(item.gameObject);
        }
    }
}
