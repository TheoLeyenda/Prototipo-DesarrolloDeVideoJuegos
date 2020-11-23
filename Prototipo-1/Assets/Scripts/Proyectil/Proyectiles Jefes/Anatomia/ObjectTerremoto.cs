using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class ObjectTerremoto : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage;
    public float timeLife;
    public bool disableByTime = false;
    public Animator animator;
    public string nameAnimaton;
    private void OnEnable()
    {
        animator.Play(nameAnimaton);
    }
    private void Update()
    {
        if (disableByTime)
        {
            if (timeLife > 0)
            {
                timeLife = timeLife - Time.deltaTime;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
    public void DisableMe()
    {
        gameObject.SetActive(false);
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
            p.eventWise.StartEvent("golpear_p1");
            p.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
        }
    }
}
