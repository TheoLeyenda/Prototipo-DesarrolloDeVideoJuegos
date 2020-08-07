using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnatomiaPunch : MonoBehaviour
{
    public Animator animator;
    public int damage;
    public Pool pool;
    protected PoolObject poolObject;
    public void PunchAnimation() 
    {
        animator.Play("AnatomiaPunch");
    }
    public void DisableMe() 
    {
        //Debug.Log("ENTRE AL DESTRUIR PUÑO");
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
            if (boxColliderController.player == null)
            {
                return;
            }
            boxColliderController.player.PD.lifePlayer = boxColliderController.player.PD.lifePlayer - damage;
            boxColliderController.player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
        }
    }
}
