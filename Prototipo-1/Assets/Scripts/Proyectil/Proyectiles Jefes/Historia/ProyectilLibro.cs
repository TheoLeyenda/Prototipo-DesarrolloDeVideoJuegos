using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilLibro : Proyectil
{
    // Start is called before the first frame update
    [Header("Config ProyectilLibro")]
    public List<Sprite> spriteLibros;
    private int IdLibro;
    public bool librosColoridos;
    public BoxCollider2D boxCollider2D;
    private void OnEnable()
    {
        OnProyectilLibro();
        boxCollider2D.enabled = true;
        if (trailRenderer != null)
        {
            trailRenderer.enabled = true;
        }
    }
    private void OnDisable()
    {
        if (trailRenderer != null)
        {
            trailRenderer.enabled = false;
        }
        if (trailRenderer != null)
        {
            trailRenderer.Clear();
        }
        spriteRenderer.color = Color.white;
        boxCollider2D.enabled = true; 
    }
    public void OnProyectilLibro()
    {
        if (librosColoridos)
        {
            float R = Random.Range(5, 256);
            float G = Random.Range(5, 256);
            float B = Random.Range(5, 256);
            IdLibro = Random.Range(0, spriteLibros.Count);

            spriteRenderer.sprite = spriteLibros[IdLibro];
            spriteRenderer.color = new Color(R, G, B);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "limite") 
        {
            boxCollider2D.enabled = false;
            trailRenderer.Clear();
            poolObject.Recycle();
            gameObject.SetActive(false);
        }
        if (collision.tag == "BoxColliderController")
        {
            Player p = null;
            BoxColliderController boxColliderController = collision.GetComponent<BoxColliderController>();
            if (boxColliderController.player == null)
            {
                return;
            }
            else 
            {
                p = boxColliderController.player;
            }
            if (boxColliderController.state != BoxColliderController.StateBoxCollider.Defendido)
            {
                p.PD.lifePlayer = boxColliderController.player.PD.lifePlayer - damage;
                p.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
            }
            else 
            {
                p.barraDeEscudo.SubstractPorcentageBar(p.barraDeEscudo.substractForHit);
            }
            boxCollider2D.enabled = false;
            trailRenderer.Clear();
            AnimationHit();
        }
    }
}
