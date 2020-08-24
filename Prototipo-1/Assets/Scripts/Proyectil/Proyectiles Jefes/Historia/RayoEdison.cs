using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class RayoEdison : MonoBehaviour
{
    public Animator animator;
    public int damage;
    public Pool pool;
    protected PoolObject poolObject;
    public float timeLife;
    public float auxTimeLife;
    public bool DestroyForTime;
    public void RayoEdisonAnimation()
    {
        animator.Play("RayoEdison");
    }
    public void Update()
    {
        if (DestroyForTime)
        {
            if (timeLife > 0)
            {
                timeLife = timeLife - Time.deltaTime;
            }
            else if (timeLife <= 0)
            {
                timeLife = auxTimeLife;
                DisableMe();
            }
        }
    }
    public void DisableMe()
    {
        Debug.Log("ENTRE AL DESTRUIR RAYO");
        if (pool != null)
        {
            pool.Recycle(gameObject);
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BoxColliderController")
        {
            BoxColliderController boxColliderController = collision.GetComponent<BoxColliderController>();
            Player p = boxColliderController.player;
            if (p == null)
            {
                return;
            }
            if (p.PD.Blindaje <= 0)
            {
                p.PD.lifePlayer = p.PD.lifePlayer - damage;
            }
            else
            {
                p.PD.Blindaje = p.PD.Blindaje - damage / 2;
            }
            p.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
        }
    }
}
