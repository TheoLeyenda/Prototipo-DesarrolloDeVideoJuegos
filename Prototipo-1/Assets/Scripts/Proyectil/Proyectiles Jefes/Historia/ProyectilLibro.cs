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
    public float delayCounterAttack = 0.1f;
    private float auxDelayCounterAttack = 0.1f;

    private void Start()
    {
        auxDelayCounterAttack = delayCounterAttack;
    }
    private void OnEnable()
    {
        OnProyectilLibro();
        boxCollider2D.enabled = true;
        if (trailRenderer != null)
        {
            trailRenderer.enabled = true;
        }
        delayCounterAttack = auxDelayCounterAttack;
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
    private void OnTriggerStay2D(Collider2D collision)
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
            bool enableDamagePlayer = true;
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
            delayCounterAttack = delayCounterAttack - Time.deltaTime;
            //p.delayCounterAttack = p.delayCounterAttack - Time.deltaTime;
            if (boxColliderController.state != BoxColliderController.StateBoxCollider.Defendido)
            {
                if (p.delayCounterAttack > 0)
                {
                    if (InputPlayerController.GetInputButtonDown(p.inputDeffenseButton) && p.barraDeEscudo.GetEnableDeffence() && !p.barraDeEscudo.nededBarMaxPorcentage)
                    {
                        gameObject.SetActive(false);
                        p.Attack(DisparadorDelProyectil.Jugador1);
                        timeLife = 0;
                        GetPoolObject().Recycle();
                        p.delayCounterAttack = p.GetAuxDelayCounterAttack();
                        enableDamagePlayer = false;
                        delayCounterAttack = auxDelayCounterAttack;
                    }
                }
                if (((p.delayCounterAttack <= 0 && timeLife <= 0 || !boxColliderController.ZonaContraAtaque)
                    || (p.delayCounterAttack <= 0 && timeLife > 0  || !boxColliderController.ZonaContraAtaque)) 
                    && enableDamagePlayer && delayCounterAttack <= 0)
                {
                    if (p.PD.Blindaje <= 0)
                    {
                        p.PD.lifePlayer = p.PD.lifePlayer - damage;
                    }
                    else
                    {
                        p.PD.Blindaje = p.PD.Blindaje - damage / 2;
                    }
                    p.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                    boxCollider2D.enabled = false;
                    trailRenderer.Clear();
                    AnimationHit();
                    p.delayCounterAttack = p.GetAuxDelayCounterAttack();
                    delayCounterAttack = auxDelayCounterAttack;
                }

            }
            else
            {
                p.barraDeEscudo.SubstractPorcentageBar(p.barraDeEscudo.substractForHit);
                boxCollider2D.enabled = false;
                trailRenderer.Clear();
                AnimationHit();
                p.delayCounterAttack = p.GetAuxDelayCounterAttack();
                delayCounterAttack = auxDelayCounterAttack;
            }
           
        }
        
    }
}
