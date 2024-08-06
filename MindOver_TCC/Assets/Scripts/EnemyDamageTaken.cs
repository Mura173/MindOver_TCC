using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageTaken : MonoBehaviour
{
    [SerializeField]
    private int life;

    public void ReceberDano()
    {
        this.life--;

        if (this.life == 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
