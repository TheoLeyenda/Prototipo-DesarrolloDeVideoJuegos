using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2
{
    public class ShildEnemy : MonoBehaviour
    {
        // Start is called before the first frame update
        public Enemy enemy;
        public bool damageCounterAttack;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Proyectil")
            {
                Proyectil proyect = collision.GetComponent<Proyectil>();
                if (proyect.disparadorDelProyectil == Proyectil.DisparadorDelProyectil.Jugador)
                {
                    if (enemy.enumsEnemy.typeEnemy == EnumsEnemy.TiposDeEnemigo.Defensivo)
                    {
                        Debug.Log("CONTRA ATAQUE");
                        if (damageCounterAttack)
                        {
                            float realDamage = proyect.damage - enemy.pointsDeffence;
                            enemy.life = enemy.life - realDamage;
                        }
                        enemy.CounterAttack(true);
                    }
                    if (proyect != null)
                    {
                        proyect.GetPoolObject().Recycle();
                    }
                }
                
            }

        }
    }
}
