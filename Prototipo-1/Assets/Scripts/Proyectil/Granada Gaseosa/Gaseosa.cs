using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gaseosa : MonoBehaviour
{
    public float damage;
    public float timeLife;
    public float auxTimeLife;
    public SpriteRenderer spriteRenderer;
    public Proyectil.DisparadorDelProyectil disparadorDelProyectil;
    public Animator animator;

    private void OnEnable()
    {
        timeLife = auxTimeLife;
        Enemy.OnDie += DisableObjectForDeadCurrentEnemy;
    }
    private void OnDisable()
    {
        Enemy.OnDie -= DisableObjectForDeadCurrentEnemy;
    }

    private void DisableObjectForDeadCurrentEnemy(Enemy currentEnemy)
    {
        timeLife = 0;
    }

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
            if (player.PD.Blindaje <= 0)
            {
                player.PD.lifePlayer = player.PD.lifePlayer - damage;
            }
            else
            {
                player.PD.Blindaje = player.PD.Blindaje - damage / 2;
            }
        }
        if (collision.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy == null)
            {
                return;
            }
            if (enemy.Blindaje <= 0)
            {
                enemy.life = enemy.life - (damage / 2);
            }
            else
            {
                enemy.Blindaje = enemy.Blindaje - (damage / 2) / 2;
            }
            
        }
    }
    public void PlaySegundaAnimacion()
    {
        animator.Play("FinalAnimacion");
    }
}

