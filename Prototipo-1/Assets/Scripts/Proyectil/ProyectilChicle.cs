using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilChicle : Proyectil
{
    // Start is called before the first frame update
    public float speedRotation;
    public float timeEffectStuned;
    void Start()
    {

        tipoDeProyectil = Proyectil.typeProyectil.AtaqueEspecial;
        //soundgenerate = false;
        ShootForward();
        timeLife = auxTimeLife;
        if (GameManager.instanceGameManager != null)
        {
            gm = GameManager.instanceGameManager;
        }
    }
    public override void ShootForward()
    {
        if (trailRenderer != null)
        {
            trailRenderer.enabled = true;
        }
        rg2D.AddForce(-transform.right * speed, ForceMode2D.Force);
    }
    // Update is called once per frame
    void Update()
    {
        /*if (eventWise == null)
        {
            eventWise = GameObject.Find("EventWise").GetComponent<EventWise>();
        }
        if (eventWise != null && !soundgenerate)
        {
            soundgenerate = true;
            Sonido();
        }*/
        CheckTimeLife();
        transform.Rotate(new Vector3(0, 0, speedRotation));
    }
    private void OnDisable()
    {
        timeLife = auxTimeLife;
    }
    private void OnEnable()
    {
        animator.enabled = true;
    }
    public void DisableObject()
    {
        timeLife = 0;
        if (poolObject != null)
        {
            poolObject.Recycle();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    protected void CheckCollision(Collider2D collision, Player PlayerDisparador)
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
                        boxColliderController.enemy.enumsEnemy.SetStateEnemy(EnumsEnemy.EstadoEnemigo.Atrapado);
                        boxColliderController.enemy.timeStuned = timeEffectStuned;
                        boxColliderController.enemy.life = boxColliderController.enemy.life - damage;
                        animator.Play(nameAnimationHit);
                        rg2D.velocity = Vector2.zero;
                    }
                }
            }
            if (boxColliderController.player != null)
            {

                if (disparadorDelProyectil == DisparadorDelProyectil.Enemigo)
                {
                    boxColliderController.player.PD.lifePlayer = boxColliderController.player.PD.lifePlayer - damage;
                    boxColliderController.player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                    boxColliderController.player.enumsPlayers.estadoJugador = EnumsPlayers.EstadoJugador.Atrapado;
                    boxColliderController.player.timeStuned = timeEffectStuned;
                    animator.Play(nameAnimationHit);
                    rg2D.velocity = Vector2.zero;
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
                        boxColliderController.player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                        boxColliderController.player.enumsPlayers.estadoJugador = EnumsPlayers.EstadoJugador.Atrapado;
                        boxColliderController.player.timeStuned = timeEffectStuned;
                        animator.Play(nameAnimationHit);
                        rg2D.velocity = Vector2.zero;
                    }
                }
            }

        }
    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCollision(collision, PLAYER1);
        CheckCollision(collision, PLAYER2);
    }
}
