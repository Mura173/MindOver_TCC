using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class BossFogo : MonoBehaviour
{
    public GameObject foguinhoL;
    public Transform instantiatePoint;

    private GameObject player;

    private bool isFacingRight = false;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerPosition();

        if (Input.GetKeyDown(KeyCode.L))
        {
            Attacking();
        }
    }

    public void Attacking()
    {
        StartCoroutine(Attack());
    }

    public IEnumerator Attack()
    {
        Instantiate(foguinhoL, instantiatePoint.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(5);
        Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void CheckPlayerPosition()
    {
        if (player.transform.position.x < transform.position.x && isFacingRight)
        {
            FlipSprite();
        }
        else if (player.transform.position.x > transform.position.x && !isFacingRight)
        {
            FlipSprite();
        }
    }

    void FlipSprite()
    {
        isFacingRight = !isFacingRight;

        // Inverte a escala no eixo X para flipar o sprite
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
