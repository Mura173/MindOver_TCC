using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
    public float speed;
    public float distance;

    private float startX;
    private float lastXPosition;

    private bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;
        lastXPosition = startX;
    }

    // Update is called once per frame
    void Update()
    {
        // Seno para alterar o movimento
        float xPosition = startX + Mathf.Sin(Time.time * speed) * distance;
        transform.position = new Vector2(xPosition, transform.position.y);

        if (xPosition > lastXPosition && !isFacingRight)
        {
            FlipSprite();
        }

        else if (xPosition < lastXPosition && isFacingRight)
        {
            FlipSprite();
        }

        lastXPosition = xPosition;
    }

    void FlipSprite()
    {
        isFacingRight = !isFacingRight;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
