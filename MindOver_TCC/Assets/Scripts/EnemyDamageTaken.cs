using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageTaken : MonoBehaviour
{
    public int life;
    public ParticleSystem smoke;
    public void ReceberDano()
    {
        this.life--;

        if (this.life == 0)
        {
            Instantiate(smoke, transform.position, Quaternion.identity);
            GameObject.Destroy(this.gameObject);
        }
    }
}
