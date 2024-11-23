using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingBall : MonoBehaviour
{
    public float swingForce;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.AddForce(new Vector2(-1, 0) * swingForce);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
