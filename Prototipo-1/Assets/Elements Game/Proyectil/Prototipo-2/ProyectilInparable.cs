using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2
{
    public class ProyectilInparable : Proyectil
    {
        public List<Sprite> propsProyectilImparable;
        public float speedRotation;
        public SpriteRenderer spriteRenderer;
        private EventWise eventWise;
        // Start is called before the first frame update
        void Start()
        {
            soundgenerate = false;
            ShootForward();
            timeLife = auxTimeLife;
            if (GameManager.instanceGameManager != null)
            {
                gm = GameManager.instanceGameManager;
            }
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
            eventWise.StartEvent("patear_pelota");
        }
        private void Update()
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
            transform.Rotate(new Vector3(0, 0,speedRotation));
        }

        // Update is called once per frame
        private void OnTriggerStay2D(Collider2D collision)
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
                            boxColliderController.enemy.spriteEnemy.ActualSprite = SpriteEnemy.SpriteActual.RecibirDanio;
                            boxColliderController.enemy.life = boxColliderController.enemy.life - damage;
                            //AUMENTO XP PARA EL ATAQUE ESPECIAL
                            PLAYER1.SetXpActual(PLAYER1.GetXpActual() + PLAYER1.xpForHit);
                        }
                    }
                }
                if (boxColliderController.player != null)
                {

                    if (disparadorDelProyectil == DisparadorDelProyectil.Enemigo)
                    {
                        boxColliderController.player.PD.lifePlayer = boxColliderController.player.PD.lifePlayer - damage;
                        boxColliderController.player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                    }
                    if (PLAYER1 != null)
                    {
                        if (disparadorDelProyectil == DisparadorDelProyectil.Jugador1
                            || boxColliderController.player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
                        {
                            //AUMENTO XP PARA EL ATAQUE ESPECIAL
                            PLAYER1.SetXpActual(PLAYER1.GetXpActual() + PLAYER1.xpForHit);
                            boxColliderController.player.SetEnableCounterAttack(true);

                            boxColliderController.player.PD.lifePlayer = boxColliderController.player.PD.lifePlayer - damage;
                            boxColliderController.player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                                
                        }
                    }
                    if (PLAYER2 != null)
                    {
                        if (disparadorDelProyectil == DisparadorDelProyectil.Jugador2 ||
                        boxColliderController.player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1 ||
                        PLAYER2 != null)
                        {
                            //AUMENTO XP PARA EL ATAQUE ESPECIAL
                            PLAYER2.SetXpActual(PLAYER2.GetXpActual() + PLAYER2.xpForHit);
                            boxColliderController.player.SetEnableCounterAttack(true);

                            boxColliderController.player.PD.lifePlayer = boxColliderController.player.PD.lifePlayer - damage;
                            boxColliderController.player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                               
                        }
                    }
                }
            }
        }
    }
}
