using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CobraBulletScripts : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float force;

    public int damage;
    public CharacterHealth playerHealth;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = FindObjectOfType<CharacterHealth>();

        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = MathF.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 10)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage(damage);

            Destroy(this.gameObject);
        }
    }
}
