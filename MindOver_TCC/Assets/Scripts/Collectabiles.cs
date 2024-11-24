using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectabiles : MonoBehaviour
{
    public CollectableManager cm;
    private bool isCollected = false;

    private void Start()
    {
        cm = FindAnyObjectByType<CollectableManager>();
    }

    private void OnTriggerEnter2D(Collider2D outro)
    {
        if (!isCollected && outro.gameObject.CompareTag("Player"))
        {
            isCollected = true;
            cm.colCount--;
            Destroy(this.gameObject);
        }
    }
}
