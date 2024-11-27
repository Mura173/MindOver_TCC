using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MabelRunning : MonoBehaviour
{
    public float speed;

    [SerializeField]
    private LevelLoader levelLoader;

    // Start is called before the first frame update
    void Start()
    {
        levelLoader = FindAnyObjectByType<LevelLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime, 0);
        StartCoroutine(Finish());
    }

    IEnumerator Finish()
    {
        yield return new WaitForSeconds(43f);
        levelLoader.Restart();
    }
}
