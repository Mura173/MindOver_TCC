using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CobraChefe : MonoBehaviour
{
    public GameObject bullets;
    public Transform bulletPos;

    private float timer;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < 15)
        {
            timer += Time.deltaTime;

            if (timer > 4)
            {
                timer = 0;
                Shoot();
            }
        }
        
    }

    void Shoot()
    {
        Instantiate(bullets, bulletPos.position, Quaternion.Euler(0f, 0f, 90f));
    }
}
