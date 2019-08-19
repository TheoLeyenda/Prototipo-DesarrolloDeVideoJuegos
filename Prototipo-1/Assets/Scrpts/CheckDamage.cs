using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDamage : MonoBehaviour
{
    // Start is called before the first frame update
    public Enemy enemy;
    public bool InEnemy;
    public Player player;
    public bool InPlayer;

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Proyectil")
        {
            //ACA PROGRAMAR QUE LE BAJE VIDA TANTO AL ENEMIGO COMO AL JUGADOR DEPENDIENDO DE LOS BOOLEANOS YA DEFINIDOS
            Proyectil proyect = collision.GetComponent<Proyectil>();
            if (proyect != null)
            {
                proyect.GetPoolObject().Recycle();
            }
        }
    }
}
