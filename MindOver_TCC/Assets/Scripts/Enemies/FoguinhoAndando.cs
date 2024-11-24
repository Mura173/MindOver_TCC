using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class FoguinhoAndando : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float speed = 5f;

    private bool isRight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = speed > 0 ? Vector2.right : Vector2.left;
        transform.Translate(direction * Mathf.Abs(speed) * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("CollidersFoguinho") && !isRight)
        {
            speed = -speed;
            isRight = true;
            FlipSprite();
        }

        else if (other.gameObject.CompareTag("CollidersFoguinho") && isRight)
        {
            speed = -speed;
            isRight = false;
            FlipSprite();
        }

        if (other.gameObject.CompareTag("Foguinho"))
        {
            Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
        }
    }

    void FlipSprite()
    {
        isRight = !isRight;
        transform.rotation = Quaternion.Euler(0, isRight ? 0 : 180, 0);
    }
}
