using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBossFogo : MonoBehaviour
{
    private BossFogo bossFogo;
    public Animator doorAnim;

    // Start is called before the first frame update
    void Start()
    {
        bossFogo = FindAnyObjectByType<BossFogo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            doorAnim.SetBool("open", false);
            doorAnim.SetBool("close", true);
            bossFogo.Attack();
            Destroy(gameObject);
        }
    }
}
