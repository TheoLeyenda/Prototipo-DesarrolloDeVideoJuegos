using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProyectilInparable : Proyectil
{
    public List<Sprite> propsProyectilImparable;
    public float speedRotation;

    private GameData gd;

    private EventWise eventWise;

    public virtual void Start()
    {
        gd = GameData.instaceGameData;
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
        int random = Random.Range(0, propsProyectilImparable.Count);
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = propsProyectilImparable[random];
        }
    }
    public override void Sonido()
    {
        if(gd.initScene)
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
                    if (e.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoveToPointCombat && e.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoveToPointDeath)
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
                    if (boxColliderController != PlayerDisparador.boxColliderAgachado
                        && boxColliderController != PlayerDisparador.boxColliderParado
                        && boxColliderController != PlayerDisparador.boxColliderPiernas
                        && boxColliderController != PlayerDisparador.boxColliderSaltando
                        && boxColliderController != PlayerDisparador.boxColliderSprite)
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
    private void OnTriggerStay2D(Collider2D collision)
    {
        CheckCollision(collision,PLAYER1,true);
        CheckCollision(collision,PLAYER2,true);
    }
    
}
