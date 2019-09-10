using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShildEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public Enemy enemy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Proyectil")
        {
            Proyectil proyect = collision.GetComponent<Proyectil>();
            if (enemy.typeEnemy == Enemy.TiposDeEnemigo.Defensivo)
            {
                Debug.Log("CONTRA ATAQUE");
                enemy.CounterAttack(false);
            }
            if (proyect != null)
            {
                proyect.GetPoolObject().Recycle();
            }
        }

    }
}
