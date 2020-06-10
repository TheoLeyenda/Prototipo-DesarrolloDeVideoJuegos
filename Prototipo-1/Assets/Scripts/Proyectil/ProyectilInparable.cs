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
            if (boxColliderController.enemy == null && boxColliderController.player == null || boxColliderController.enemy != null && boxColliderController.player != null)
            {
                return;
            }

            if (boxColliderController.enemy != null)
            {
                if (disparadorDelProyectil == DisparadorDelProyectil.Jugador1)
                {
                    if (boxColliderController.enemy.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoveToPointCombat && boxColliderController.enemy.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoveToPointDeath)
                    {
                        if (applySpriteRecibirDanio)
                        {
                            boxColliderController.enemy.spriteEnemy.ActualSprite = SpriteEnemy.SpriteActual.RecibirDanio;
                        }
                        boxColliderController.enemy.life = boxColliderController.enemy.life - damage;
                    }
                }
            }
            if (boxColliderController.player != null)
            {

                if (disparadorDelProyectil == DisparadorDelProyectil.Enemigo)
                {
                    boxColliderController.player.PD.lifePlayer = boxColliderController.player.PD.lifePlayer - damage;
                    if (applySpriteRecibirDanio)
                    {
                        boxColliderController.player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
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
                        boxColliderController.player.SetEnableCounterAttack(true);

                        boxColliderController.player.PD.lifePlayer = boxColliderController.player.PD.lifePlayer - damage;
                        if (applySpriteRecibirDanio)
                        {
                            boxColliderController.player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
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
