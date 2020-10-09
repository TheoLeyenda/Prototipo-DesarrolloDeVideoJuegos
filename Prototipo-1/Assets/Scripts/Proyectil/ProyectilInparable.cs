using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProyectilInparable : Proyectil
{
    public List<Sprite> propsProyectilImparable;
    public float speedRotation;
    //public SpriteRenderer spriteRenderer;
    private EventWise eventWise;
    // Start is called before the first frame update
    public virtual void Start()
    {
        soundgenerate = false;
        ShootForward();
        timeLife = auxTimeLife;
        if (GameManager.instanceGameManager != null)
        {
            gm = GameManager.instanceGameManager;
        }
    }
    private void OnDisable()
    {
        soundgenerate = false;
        rg2D.velocity = Vector3.zero;
        rg2D.angularVelocity = 0f;
    }
    private void OnEnable()
    {
        timeLife = auxTimeLife;
        //tipoProyectil = Proyectil.typeProyectil.AtaqueEspecial;
        int random = Random.Range(0, propsProyectilImparable.Count);
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = propsProyectilImparable[random];
        }
    }
    public override void Sonido()
    {
        eventWise.StartEvent("patear_pelota");
    }
    public virtual void Update()
    {
        if (eventWise == null)
        {
            eventWise = GameObject.Find("EventWise").GetComponent<EventWise>();
        }
        if (eventWise != null && !soundgenerate)
        {
            soundgenerate = true;
            Sonido();
        }
        CheckTimeLife();
        if (rg2D.velocity.x != 0)
        {
            transform.Rotate(new Vector3(0, 0, speedRotation));
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    protected void CheckCollision(Collider2D collision,Player PlayerDisparador, bool applySpriteRecibirDanio)
    {

        if (collision.tag == "BoxColliderController")
        {
            BoxColliderController boxColliderController = collision.GetComponent<BoxColliderController>();
            Enemy e = boxColliderController.enemy;
            Player p = boxColliderController.player;
            if (e == null && p == null || e != null && p != null)
            {
                return;
            }

            if (e != null)
            {
                if (disparadorDelProyectil == DisparadorDelProyectil.Jugador1)
                {
                    if (e.enumsEnemy.movimiento != EnumsCharacter.Movimiento.MoveToPointCombat && e.enumsEnemy.movimiento != EnumsCharacter.Movimiento.MoveToPointDeath)
                    {
                        if (applySpriteRecibirDanio)
                        {
                            e.spriteEnemy.ActualSprite = SpriteEnemy.SpriteActual.RecibirDanio;
                        }
                        if (e.Blindaje <= 0)
                        {
                            e.life = e.life - damage;
                        }
                        else
                        {
                            e.Blindaje = e.Blindaje - damage / 2;
                        }
                    }
                }
            }
            if (p != null)
            {

                if (disparadorDelProyectil == DisparadorDelProyectil.Enemigo)
                {
                    if (p.PD.Blindaje <= 0)
                    {
                        p.PD.lifePlayer = p.PD.lifePlayer - damage;
                    }
                    else
                    {
                        p.PD.Blindaje = p.PD.Blindaje - damage / 2;
                    }
                    if (applySpriteRecibirDanio)
                    {
                        p.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                    }
                }
                if (PlayerDisparador != null)
                {
                    if (boxColliderController != PlayerDisparador.boxColliderControllerAgachado
                        && boxColliderController != PlayerDisparador.boxColliderControllerParado
                        && boxColliderController != PlayerDisparador.boxColliderControllerPiernas
                        && boxColliderController != PlayerDisparador.boxColliderControllerSaltando
                        && boxColliderController != PlayerDisparador.boxColliderControllerSprite)
                    {
                        p.SetEnableCounterAttack(true);
                        if (p.PD.Blindaje <= 0)
                        {
                            p.PD.lifePlayer = p.PD.lifePlayer - damage;
                        }
                        else
                        {
                            p.PD.Blindaje = p.PD.Blindaje - damage / 2;
                        }
                        if (applySpriteRecibirDanio)
                        {
                            p.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                        }
                    }
                }
            }
        }
    }
    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        CheckCollision(collision,PLAYER1,true);
        CheckCollision(collision,PLAYER2,true);
    }
    
}
