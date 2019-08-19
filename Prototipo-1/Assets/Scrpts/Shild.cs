using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shild : MonoBehaviour
{
    // Start is called before the first frame update
    public Enemy enemy;
    public Player Player;
    public bool InEnemy;
    public bool InPlayer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ENTRE");
        if (collision.tag == "Proyectil")
        {
            Proyectil proyect = collision.GetComponent<Proyectil>();
            
            if (InEnemy)
            {
                if (enemy.typeEnemy == Enemy.Categoria.Defensivo)
                {
                    Debug.Log("CONTRA ATAQUE");
                    enemy.CounterAttack();
                }
            }
            if (proyect != null)
            {
                proyect.GetPoolObject().Recycle();
            }
        }

    }
}
