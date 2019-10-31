using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2
{
    public class Gaseosa : MonoBehaviour
    {
        // Start is called before the first frame update
        public float damage;
        public float timeLife;
        private float auxTimeLife;
        public SpriteRenderer spriteRenderer;
        public Proyectil.DisparadorDelProyectil disparadorDelProyectil;

        private void Update()
        {
            CheckTimeLife();
        }
        public void CheckTimeLife()
        {
            if (timeLife > 0)
            {
                timeLife = timeLife - Time.deltaTime;
            }
            else if (timeLife <= 0)
            {
                timeLife = auxTimeLife;
                gameObject.SetActive(false);
            }
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                Player player = collision.GetComponent<Player>();
                if (player == null)
                {
                    return;
                }
                player.PD.lifePlayer = player.PD.lifePlayer - damage;
            }
            if (collision.tag == "Enemy")
            {
                Enemy enemy = collision.GetComponent<Enemy>();
                if (enemy == null)
                {
                    return;
                }
                enemy.life = enemy.life - (damage/2);
            }
        }
    }
}
