using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botao : MonoBehaviour
{
    private bool apertado;
    private SpriteRenderer sR;
    public Sprite apertadoSprite;

    public Animator doorAnim;

    // Start is called before the first frame update
    void Start()
    {
        sR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (apertado)
        {
            sR.sprite = apertadoSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            apertado = true;
            doorAnim.SetBool("open", true);
        }
    }
}
