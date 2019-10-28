using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2
{
    public class BoxColliderController : MonoBehaviour
    {
        public Player player;
        public Enemy enemy;
        public bool inEnemy;
        public bool inPlayer;
        public bool ZonaContraAtaque;
        private BoxCollider2D boxCollider2D;
        // Start is called before the first frame update
        public enum StateBoxCollider
        {
            Defendido,
            Normal,
        }
        public enum TypeCollider
        {
            ColliderSalto,
            ColliderParado,
            ColliderAgachado,
        }
        private void OnDisable()
        {
            boxCollider2D = GetComponent<BoxCollider2D>();
        }
        void Awake()
        {
            boxCollider2D = GetComponent<BoxCollider2D>();
        }
        /*void Update()
        {

        }*/
        //public TypeCollider tipoDeCollider;
        public StateBoxCollider state;
        public BoxCollider2D GetBoxCollider2D()
        {
            return boxCollider2D;
        }
        public void CollisionWhitProyectil(Collider2D collision)
        {
            Proyectil proyectil = collision.GetComponent<Proyectil>();
            if (proyectil.GetComponent<ProyectilInparable>() == null && proyectil.GetComponent<GranadaGaseosa>() == null)
            {
                if (player != null && !inEnemy && inPlayer)
                {
                    bool enableDamagePlayer = true;
                    if (state == StateBoxCollider.Normal)
                    {
                        if (proyectil.GetEnemy() != null)
                        {

                            if (proyectil.disparadorDelProyectil == Proyectil.DisparadorDelProyectil.Enemigo)
                            {
                                player.SetEnableCounterAttack(true);
                                if (player.delayCounterAttack > 0)
                                {
                                    player.delayCounterAttack = player.delayCounterAttack - Time.deltaTime;
                                    if (InputPlayerController.DeffenseButton_P1())
                                    {
                                        proyectil.gameObject.SetActive(false);
                                        player.Attack(Proyectil.DisparadorDelProyectil.Jugador1);
                                        player.delayCounterAttack = player.GetAuxDelayCounterAttack();
                                        proyectil.timeLife = 0;
                                        enableDamagePlayer = false;
                                    }
                                }
                                if (player.delayCounterAttack <= 0 && proyectil.timeLife <= 0 && enableDamagePlayer || !ZonaContraAtaque)
                                {
                                    if (proyectil.gameObject.activeSelf && gameObject.activeSelf && proyectil != null && proyectil.GetEnemy() != null)
                                    {
                                        proyectil.GetEnemy().SetXpActual(proyectil.GetEnemy().GetXpActual() + proyectil.GetEnemy().xpForHit);
                                        player.PD.lifePlayer = player.PD.lifePlayer - proyectil.damage;
                                    }
                                    player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                                    if (!ZonaContraAtaque)
                                    {
                                        proyectil.timeLife = 0;
                                        proyectil.gameObject.SetActive(false);
                                    }
                                }
                                else if (player.delayCounterAttack <= 0 && proyectil.timeLife > 0 && enableDamagePlayer || !ZonaContraAtaque)
                                {
                                    proyectil.GetEnemy().SetXpActual(proyectil.GetEnemy().GetXpActual() + proyectil.GetEnemy().xpForHit);
                                    player.delayCounterAttack = player.GetAuxDelayCounterAttack();
                                    player.SetEnableCounterAttack(false);
                                    player.PD.lifePlayer = player.PD.lifePlayer - proyectil.damage;
                                    player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                                    proyectil.timeLife = 0;
                                    proyectil.gameObject.SetActive(false);
                                }
                            }

                        }
                        if (proyectil.GetPlayer() != null)
                        {
                            if (proyectil.disparadorDelProyectil == Proyectil.DisparadorDelProyectil.Jugador1
                                || player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
                            {
                                //AUMENTO XP PARA EL ATAQUE ESPECIAL
                                if (proyectil.gameObject.activeSelf && gameObject.activeSelf && proyectil != null && proyectil.GetPlayer() != null)
                                {
                                    proyectil.GetPlayer().SetXpActual(proyectil.GetPlayer().GetXpActual() + proyectil.GetPlayer().xpForHit);
                                    proyectil.GetPlayer().PD.score = proyectil.GetPlayer().PD.score + proyectil.GetPlayer().PD.scoreForHit;
                                }
                                player.SetEnableCounterAttack(true);
                                if (player.delayCounterAttack > 0)
                                {
                                    player.delayCounterAttack = player.delayCounterAttack - Time.deltaTime;
                                    if (InputPlayerController.DeffenseButton_P2())
                                    {
                                        player.Attack(Proyectil.DisparadorDelProyectil.Jugador2);
                                        player.delayCounterAttack = player.GetAuxDelayCounterAttack();
                                        proyectil.timeLife = 0;
                                        enableDamagePlayer = false;
                                    }
                                }
                                if (player.delayCounterAttack <= 0 && proyectil.timeLife <= 0 && enableDamagePlayer)
                                {
                                    player.PD.lifePlayer = player.PD.lifePlayer - proyectil.damage;
                                    player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                                }
                                else if (player.delayCounterAttack <= 0 && proyectil.timeLife > 0 && enableDamagePlayer)
                                {
                                    player.delayCounterAttack = player.GetAuxDelayCounterAttack();
                                    player.SetEnableCounterAttack(false);
                                    player.PD.lifePlayer = player.PD.lifePlayer - proyectil.damage;
                                    player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                                    proyectil.timeLife = 0;
                                    proyectil.gameObject.SetActive(false);
                                }
                            }
                        }
                        if (proyectil.GetPlayer2() != null)
                        {
                            if (proyectil.disparadorDelProyectil == Proyectil.DisparadorDelProyectil.Jugador2 ||
                            player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1 ||
                            proyectil.GetPlayer2() != null)
                            {
                                //AUMENTO XP PARA EL ATAQUE ESPECIAL
                                if (proyectil.gameObject.activeSelf && gameObject.activeSelf && proyectil != null && proyectil.GetPlayer2() != null)
                                {
                                    proyectil.GetPlayer2().SetXpActual(proyectil.GetPlayer2().GetXpActual() + proyectil.GetPlayer2().xpForHit);
                                    proyectil.GetPlayer2().PD.score = proyectil.GetPlayer2().PD.score + proyectil.GetPlayer2().PD.scoreForHit;
                                }
                                player.SetEnableCounterAttack(true);
                                if (player.delayCounterAttack > 0)
                                {
                                    player.delayCounterAttack = player.delayCounterAttack - Time.deltaTime;
                                    if (InputPlayerController.DeffenseButton_P1())
                                    {
                                        player.Attack(Proyectil.DisparadorDelProyectil.Jugador1);
                                        player.delayCounterAttack = player.GetAuxDelayCounterAttack();
                                        proyectil.timeLife = 0;
                                        enableDamagePlayer = false;
                                    }
                                }
                                if (player.delayCounterAttack <= 0 && proyectil.timeLife <= 0 && enableDamagePlayer)
                                {
                                    player.PD.lifePlayer = player.PD.lifePlayer - proyectil.damage;
                                    player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                                }
                                else if (player.delayCounterAttack <= 0 && proyectil.timeLife > 0 && enableDamagePlayer)
                                {
                                    player.delayCounterAttack = player.GetAuxDelayCounterAttack();
                                    player.SetEnableCounterAttack(false);
                                    player.PD.lifePlayer = player.PD.lifePlayer - proyectil.damage;
                                    player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                                    proyectil.timeLife = 0;
                                    proyectil.gameObject.SetActive(false);
                                    proyectil.GetPoolObject().Recycle();
                                }
                            }
                        }
                    }
                    else if (state == StateBoxCollider.Defendido)
                    {
                        Player_PvP player_PvP = player.gameObject.GetComponent<Player_PvP>();
                        float realDamage;
                        if (proyectil.GetPlayer() != null)
                        {
                            if (proyectil.disparadorDelProyectil == Proyectil.DisparadorDelProyectil.Jugador1
                                || player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
                            {
                                //AUMENTO XP PARA EL ATAQUE ESPECIAL
                                //SI SE DEFIENDE CARGA LA MITAD DE LA BARRA DEL ATAQUE ESPECIAL.
                                if (proyectil.gameObject.activeSelf && gameObject.activeSelf && proyectil != null && proyectil.GetPlayer() != null)
                                {
                                    proyectil.GetPlayer().SetXpActual(proyectil.GetPlayer().GetXpActual() + (proyectil.GetPlayer().xpForHit / 2));
                                    proyectil.GetPlayer().PD.score = proyectil.GetPlayer().PD.score + (proyectil.GetPlayer().PD.scoreForHit / 2);
                                }
                                realDamage = proyectil.damage - player.pointsDeffence;
                                player.PD.lifePlayer = player.PD.lifePlayer - realDamage;
                                proyectil.timeLife = 0;
                                proyectil.damage = proyectil.GetAuxDamage();
                                proyectil.GetPoolObject().Recycle();
                                proyectil.gameObject.SetActive(false);
                            }
                        }
                        if (player_PvP != null)
                        {
                            //Debug.Log(proyectil.GetEnemy());
                            if (proyectil.GetPlayer2() != null)
                            {
                                if (proyectil.disparadorDelProyectil == Proyectil.DisparadorDelProyectil.Jugador2
                                || player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
                                {
                                    //AUMENTO XP PARA EL ATAQUE ESPECIAL
                                    //SI SE DEFIENDE CARGA LA MITAD DE LA BARRA DEL ATAQUE ESPECIAL.
                                    if (proyectil.gameObject.activeSelf && gameObject.activeSelf && proyectil != null && proyectil.GetPlayer2() != null)
                                    {
                                        proyectil.GetPlayer2().SetXpActual(proyectil.GetPlayer2().GetXpActual() + (proyectil.GetPlayer2().xpForHit / 2));
                                        proyectil.GetPlayer2().PD.score = proyectil.GetPlayer2().PD.score + (proyectil.GetPlayer2().PD.scoreForHit / 2);
                                    }
                                    realDamage = proyectil.damage - player.pointsDeffence;
                                    player.PD.lifePlayer = player.PD.lifePlayer - realDamage;
                                    proyectil.timeLife = 0;
                                    proyectil.damage = proyectil.GetAuxDamage();
                                    proyectil.GetPoolObject().Recycle();
                                    proyectil.gameObject.SetActive(false);
                                }
                            }
                            if (player_PvP.playerSelected == Player_PvP.PlayerSelected.Defensivo)
                            {
                                switch (player_PvP.playerState)
                                {
                                    case Player_PvP.State.Defendido:
                                        player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.ContraAtaqueParado;
                                        if (player_PvP.playerActual == Player_PvP.Player.player1)
                                        {
                                            player.Attack(Proyectil.DisparadorDelProyectil.Jugador1);
                                        }
                                        else if (player_PvP.playerActual == Player_PvP.Player.player2)
                                        {
                                            player.Attack(Proyectil.DisparadorDelProyectil.Jugador2);
                                        }
                                        realDamage = proyectil.damage - player.pointsDeffence;
                                        player.PD.lifePlayer = player.PD.lifePlayer - realDamage;
                                        proyectil.timeLife = 0;
                                        proyectil.damage = proyectil.GetAuxDamage();
                                        proyectil.GetPoolObject().Recycle();
                                        proyectil.gameObject.SetActive(false);
                                        break;
                                    default:
                                        if (proyectil.GetPlayer() != null)
                                        {
                                            if (proyectil.disparadorDelProyectil == Proyectil.DisparadorDelProyectil.Jugador1
                                                || player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
                                            {
                                                //AUMENTO XP PARA EL ATAQUE ESPECIAL
                                                //SI SE DEFIENDE CARGA LA MITAD DE LA BARRA DEL ATAQUE ESPECIAL.
                                                if (proyectil.gameObject.activeSelf && gameObject.activeSelf && proyectil != null && proyectil.GetPlayer() != null)
                                                {
                                                    proyectil.GetPlayer().SetXpActual(proyectil.GetPlayer().GetXpActual() + (proyectil.GetPlayer().xpForHit / 2));
                                                    proyectil.GetPlayer().PD.score = proyectil.GetPlayer().PD.score + (proyectil.GetPlayer().PD.scoreForHit / 2);
                                                }
                                            }
                                        }
                                        else if (proyectil.GetPlayer2() != null)
                                        {
                                            if (proyectil.disparadorDelProyectil == Proyectil.DisparadorDelProyectil.Jugador2
                                            || player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
                                            {
                                                //AUMENTO XP PARA EL ATAQUE ESPECIAL
                                                //SI SE DEFIENDE CARGA LA MITAD DE LA BARRA DEL ATAQUE ESPECIAL.
                                                if (proyectil.gameObject.activeSelf && gameObject.activeSelf && proyectil != null && proyectil.GetPlayer2() != null)
                                                {
                                                    proyectil.GetPlayer2().SetXpActual(proyectil.GetPlayer2().GetXpActual() + (proyectil.GetPlayer2().xpForHit / 2));
                                                    proyectil.GetPlayer2().PD.score = proyectil.GetPlayer2().PD.score + (proyectil.GetPlayer2().PD.scoreForHit / 2);
                                                }
                                            }
                                        }
                                        realDamage = proyectil.damage - player.pointsDeffence;
                                        player.PD.lifePlayer = player.PD.lifePlayer - realDamage;
                                        proyectil.timeLife = 0;
                                        proyectil.damage = proyectil.GetAuxDamage();
                                        proyectil.GetPoolObject().Recycle();
                                        proyectil.gameObject.SetActive(false);
                                        break;
                                }
                            }
                            //Debug.Log(proyectil.GetEnemy());
                            if (proyectil.GetEnemy() != null)
                            {
                                //Debug.Log("A LA DEFENSA");
                                proyectil.GetEnemy().SetXpActual(proyectil.GetEnemy().GetXpActual() + (proyectil.GetEnemy().xpForHit / 2));
                                realDamage = proyectil.damage - player.pointsDeffence;
                                player.PD.lifePlayer = player.PD.lifePlayer - realDamage;
                                proyectil.timeLife = 0;
                                proyectil.damage = proyectil.GetAuxDamage();
                                proyectil.GetPoolObject().Recycle();
                                proyectil.gameObject.SetActive(false);
                            }
                        }
                    }
                }
                else if (enemy != null && !inPlayer && inEnemy)
                {
                    bool enableDamagePlayer = true;
                    //Debug.Log(disparadorDelProyectil);
                    if (state == StateBoxCollider.Normal)
                    {
                        if (proyectil.disparadorDelProyectil == Proyectil.DisparadorDelProyectil.Jugador1)
                        {
                            if (enemy.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoveToPointCombat && enemy.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoveToPointDeath)
                            {
                                enemy.spriteEnemy.ActualSprite = SpriteEnemy.SpriteActual.RecibirDanio;
                                enemy.life = enemy.life - proyectil.damage;

                                //AUMENTO XP PARA EL ATAQUE ESPECIAL
                                if (proyectil.gameObject.activeSelf && gameObject.activeSelf && proyectil != null && proyectil.GetPlayer() != null)
                                {
                                    proyectil.GetPlayer().SetXpActual(proyectil.GetPlayer().GetXpActual() + proyectil.GetPlayer().xpForHit);
                                    proyectil.GetPlayer().PD.score = proyectil.GetPlayer().PD.score + proyectil.GetPlayer().PD.scoreForHit;
                                }
                                proyectil.timeLife = 0;
                                proyectil.gameObject.SetActive(false);
                                proyectil.GetPoolObject().Recycle();
                            }
                           

                        }
                    }
                    else if (state == StateBoxCollider.Defendido)
                    {
                        if (proyectil.disparadorDelProyectil == Proyectil.DisparadorDelProyectil.Jugador1)
                        {
                            //AUMENTO XP PARA EL ATAQUE ESPECIAL
                            //SI SE DEFIENDE CARGA LA MITAD DE LA BARRA DEL ATAQUE ESPECIAL.
                            if (proyectil.gameObject.activeSelf && gameObject.activeSelf && proyectil != null && proyectil.GetPlayer() != null)
                            {
                                proyectil.GetPlayer().SetXpActual(proyectil.GetPlayer().GetXpActual() + (proyectil.GetPlayer().xpForHit / 2));
                                proyectil.GetPlayer().PD.score = proyectil.GetPlayer().PD.score + (proyectil.GetPlayer().PD.scoreForHit / 2);
                            }
                            if (enemy.enumsEnemy.typeEnemy != EnumsEnemy.TiposDeEnemigo.Defensivo)
                            {
                                float realDamage = proyectil.damage - enemy.pointsDeffence;
                                enemy.life = enemy.life - realDamage;
                            }
                            else
                            {
                                if (enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.DefensaEnElLugar)
                                {
                                    //MECANICA DEFENSIVA DEL ENEMIGO DEFENSIVO//
                                    if (enemy.GetComponent<Defensivo>() != null)
                                    {
                                        Defensivo enemyDeffended = enemy.GetComponent<Defensivo>();
                                        if (enemyDeffended.GetStateDeffence() == Defensivo.StateDeffence.CounterAttackDeffense)
                                        {
                                            enemy.spriteEnemy.ActualSprite = SpriteEnemy.SpriteActual.ContraAtaque;
                                            //enemy.Attack(false, false, true, cuadrilla);
                                            enemy.Attack(false, false, true, proyectil);
                                            enemyDeffended.SetStateDeffense(Defensivo.StateDeffence.Nulo);
                                            enemyDeffended.delayStateCounterAttackDeffense = 0;
                                            enemyDeffended.delayStateDeffense = enemyDeffended.GetAuxDelayStateDeffense();
                                            enemyDeffended.delayVulnerable = enemyDeffended.GetAuxDelayVulnerable();
                                            if (enemy.damageCounterAttack)
                                            {
                                                float realDamage = proyectil.damage - enemy.pointsDeffence;
                                                enemy.life = enemy.life - realDamage;
                                            }
                                        }
                                        else if (enemyDeffended.GetStateDeffence() == Defensivo.StateDeffence.NormalDeffense)
                                        {
                                            if (enemy.damageCounterAttack)
                                            {
                                                float realDamage = proyectil.damage - enemy.pointsDeffence;
                                                enemy.life = enemy.life - realDamage;
                                            }
                                        }
                                    }
                                    //-----------------------------------------//
                                }
                                else
                                {
                                    enemy.life = enemy.life - proyectil.damage;
                                }
                            }
                            proyectil.timeLife = 0;
                            proyectil.GetPoolObject().Recycle();
                            proyectil.gameObject.SetActive(false);

                        }
                    }
                }
            }
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (gameObject.activeSelf)
            {
                switch (collision.tag)
                {
                    case "Proyectil":
                        //Debug.Log("COLICIONE");
                        CollisionWhitProyectil(collision);
                        break;
                }
            }
        }
    }
}
