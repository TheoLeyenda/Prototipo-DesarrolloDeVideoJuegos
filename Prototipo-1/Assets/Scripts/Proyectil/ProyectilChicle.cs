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
                        boxColliderController.enemy.life = boxColliderController.enemy.life - damage;
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

                    }
                }
            }
        }
    }
    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        CheckCollision(collision, PLAYER1);
        CheckCollision(collision, PLAYER2);
    }
}
