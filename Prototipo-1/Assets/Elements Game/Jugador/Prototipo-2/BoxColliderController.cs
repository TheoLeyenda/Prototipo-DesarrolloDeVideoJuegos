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
        public EventWise eventWise;
        //public bool pointsForHit;
        private GameManager gm;
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
        private void Start()
        {
            GameObject go = GameObject.Find("EventWise");
            eventWise = go.GetComponent<EventWise>();
            if (GameManager.instanceGameManager != null)
            {
                gm = GameManager.instanceGameManager;
            }
        }
        public StateBoxCollider state;
        public BoxCollider2D GetBoxCollider2D()
        {
            return boxCollider2D;
        }
        public void CollisionWhitProyectil(Collider2D collision)
        {
            Proyectil proyectil = collision.GetComponent<Proyectil>();
            bool notProyectilParabola = proyectil.GetComponent<ProyectilParabola>() == null;
            //Debug.Log(proyectil);
            //Debug.Log(proyectil.disparadorDelProyectil);
            if (proyectil.GetComponent<ProyectilInparable>() == null && proyectil.GetComponent<GranadaGaseosa>() == null && proyectil != null)
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
                                    if (InputPlayerController.DeffenseButton_P1() && player.barraDeEscudo.GetEnableDeffence() && !player.barraDeEscudo.nededBarMaxPorcentage)
                                    {
                                        proyectil.gameObject.SetActive(false);
                                        player.Attack(Proyectil.DisparadorDelProyectil.Jugador1);
                                        player.delayCounterAttack = player.GetAuxDelayCounterAttack();
                                        proyectil.timeLife = 0;
                                        proyectil.GetPoolObject().Recycle();
                                        //enableDamagePlayer = false;
                                        //proyectil.AnimationHit();
                                    }
                                }
                                if (player.delayCounterAttack <= 0 && proyectil.timeLife <= 0 && enableDamagePlayer && notProyectilParabola || !ZonaContraAtaque || proyectil.colisionPlayer)
                                {
                                    if (proyectil.gameObject.activeSelf && gameObject.activeSelf && proyectil != null && proyectil.GetEnemy() != null)
                                    {
                                        proyectil.GetEnemy().SetXpActual(proyectil.GetEnemy().GetXpActual() + proyectil.GetEnemy().xpForHit);
                                        player.PD.lifePlayer = player.PD.lifePlayer - proyectil.damage;
                                    }
                                    player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                                    if (!ZonaContraAtaque)
                                    {
                                        //proyectil.timeLife = 0;
                                        //proyectil.gameObject.SetActive(false);
                                        eventWise.StartEvent("golpear_p1");
                                        proyectil.AnimationHit();
                                    }
                                    if (proyectil.colisionPlayer)
                                    {
                                        eventWise.StartEvent("golpear_p1");
                                        proyectil.AnimationHit();
                                    }
                                }
                                else if (player.delayCounterAttack <= 0 && proyectil.timeLife > 0 && enableDamagePlayer && notProyectilParabola || !ZonaContraAtaque || proyectil.colisionPlayer)
                                {
                                    proyectil.GetEnemy().SetXpActual(proyectil.GetEnemy().GetXpActual() + proyectil.GetEnemy().xpForHit);
                                    player.delayCounterAttack = player.GetAuxDelayCounterAttack();
                                    player.SetEnableCounterAttack(false);
                                    player.PD.lifePlayer = player.PD.lifePlayer - proyectil.damage;
                                    player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                                    //proyectil.timeLife = 0;
                                    //proyectil.gameObject.SetActive(false);
                                    eventWise.StartEvent("golpear_p1");
                                    proyectil.AnimationHit();
                                }
                            }

                        }
                        if (proyectil.GetPlayer() != null)
                        {
                            if (player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
                            {
                                //AUMENTO XP PARA EL ATAQUE ESPECIAL
                                
                                player.SetEnableCounterAttack(true);
                                if (player.delayCounterAttack > 0)
                                {
                                    player.delayCounterAttack = player.delayCounterAttack - Time.deltaTime;
                                    
                                    if (InputPlayerController.DeffenseButton_P2() && player.barraDeEscudo.GetEnableDeffence() && !player.barraDeEscudo.nededBarMaxPorcentage)
                                    {
                                        player.Attack(Proyectil.DisparadorDelProyectil.Jugador2);
                                        player.delayCounterAttack = player.GetAuxDelayCounterAttack();
                                        enableDamagePlayer = false;
                                        proyectil.timeLife = 0;
                                        proyectil.GetPoolObject().Recycle();
                                        //proyectil.AnimationHit();
                                    }
                                }
                                if (player.delayCounterAttack <= 0 && proyectil.timeLife <= 0 && enableDamagePlayer && notProyectilParabola || !ZonaContraAtaque || proyectil.colisionPlayer)
                                {
                                    
                                    if (proyectil.gameObject.activeSelf && gameObject.activeSelf && proyectil != null && proyectil.GetPlayer() != null)
                                    {
                                        player.PD.lifePlayer = player.PD.lifePlayer - proyectil.damage;
                                        player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                                        proyectil.GetPlayer().SetXpActual(proyectil.GetPlayer().GetXpActual() + proyectil.GetPlayer().xpForHit);
                                        if (gm.structGameManager.gm_dataCombatPvP.pointsForHit)
                                        {
                                            proyectil.GetPlayer().PD.score = proyectil.GetPlayer().PD.score + proyectil.GetPlayer().PD.scoreForHit;
                                        }
                                        eventWise.StartEvent("golpear_p1");
                                        proyectil.AnimationHit();
                                    }
                                    
                                }
                                else if (player.delayCounterAttack <= 0 && proyectil.timeLife > 0 && enableDamagePlayer && notProyectilParabola || !ZonaContraAtaque || proyectil.colisionPlayer)
                                {
                                    
                                    //proyectil.timeLife = 0;
                                    //proyectil.gameObject.SetActive(false);
                                    if (proyectil.gameObject.activeSelf && gameObject.activeSelf && proyectil != null && proyectil.GetPlayer2() != null)
                                    {
                                        player.delayCounterAttack = player.GetAuxDelayCounterAttack();
                                        player.SetEnableCounterAttack(false);
                                        player.PD.lifePlayer = player.PD.lifePlayer - proyectil.damage;
                                        player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                                        proyectil.GetPlayer2().SetXpActual(proyectil.GetPlayer2().GetXpActual() + proyectil.GetPlayer2().xpForHit);
                                        if (gm.structGameManager.gm_dataCombatPvP.pointsForHit)
                                        {
                                            proyectil.GetPlayer2().PD.score = proyectil.GetPlayer2().PD.score + proyectil.GetPlayer2().PD.scoreForHit;
                                        }
                                        eventWise.StartEvent("golpear_p1");
                                        proyectil.AnimationHit();
                                    }
                                    
                                }
                            }
                        }
                        if (proyectil.GetPlayer2() != null)
                        {
                            if (player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
                            {
                                //AUMENTO XP PARA EL ATAQUE ESPECIAL
                                
                                player.SetEnableCounterAttack(true);
                                if (player.delayCounterAttack > 0)
                                {
                                    player.delayCounterAttack = player.delayCounterAttack - Time.deltaTime;
                                    if (InputPlayerController.DeffenseButton_P1() && player.barraDeEscudo.GetEnableDeffence() && !player.barraDeEscudo.nededBarMaxPorcentage)
                                    {
                                        player.Attack(Proyectil.DisparadorDelProyectil.Jugador1);
                                        player.delayCounterAttack = player.GetAuxDelayCounterAttack();
                                        enableDamagePlayer = false;
                                        proyectil.timeLife = 0;
                                        proyectil.GetPoolObject().Recycle();
                                        //proyectil.AnimationHit();
                                    }
                                }
                                if ((player.delayCounterAttack <= 0 && proyectil.timeLife <= 0 && enableDamagePlayer) && notProyectilParabola || !ZonaContraAtaque || proyectil.colisionPlayer)
                                {
                                    
                                    if (proyectil.gameObject.activeSelf && gameObject.activeSelf && proyectil != null && proyectil.GetPlayer2() != null)
                                    {
                                        player.PD.lifePlayer = player.PD.lifePlayer - proyectil.damage;
                                        player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                                        proyectil.GetPlayer2().SetXpActual(proyectil.GetPlayer2().GetXpActual() + proyectil.GetPlayer2().xpForHit);
                                        if (gm.structGameManager.gm_dataCombatPvP.pointsForHit)
                                        {
                                            proyectil.GetPlayer2().PD.score = proyectil.GetPlayer2().PD.score + proyectil.GetPlayer2().PD.scoreForHit;
                                        }
                                        eventWise.StartEvent("golpear_p1");
                                        proyectil.AnimationHit();
                                    }
                                    
                                }
                                else if ((player.delayCounterAttack <= 0 && proyectil.timeLife > 0 && enableDamagePlayer) && notProyectilParabola || !ZonaContraAtaque || proyectil.colisionPlayer)
                                {
                                    
                                    if (proyectil.gameObject.activeSelf && gameObject.activeSelf && proyectil != null && proyectil.GetPlayer2() != null)
                                    {
                                        player.delayCounterAttack = player.GetAuxDelayCounterAttack();
                                        player.SetEnableCounterAttack(false);
                                        player.PD.lifePlayer = player.PD.lifePlayer - proyectil.damage;
                                        player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                                        proyectil.GetPlayer2().SetXpActual(proyectil.GetPlayer2().GetXpActual() + proyectil.GetPlayer2().xpForHit);
                                        if (gm.structGameManager.gm_dataCombatPvP.pointsForHit)
                                        {
                                            proyectil.GetPlayer2().PD.score = proyectil.GetPlayer2().PD.score + proyectil.GetPlayer2().PD.scoreForHit;
                                        }
                                        eventWise.StartEvent("golpear_p1");
                                        proyectil.AnimationHit();
                                    }
                                    //proyectil.timeLife = 0;
                                    //proyectil.gameObject.SetActive(false);
                                    //proyectil.GetPoolObject().Recycle();

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
                            if (player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
                            {
                                //AUMENTO XP PARA EL ATAQUE ESPECIAL
                                //SI SE DEFIENDE CARGA LA MITAD DE LA BARRA DEL ATAQUE ESPECIAL.
                                if (proyectil.gameObject.activeSelf && gameObject.activeSelf && proyectil != null && proyectil.GetPlayer() != null)
                                {
                                    proyectil.GetPlayer().SetXpActual(proyectil.GetPlayer().GetXpActual() + (proyectil.GetPlayer().xpForHit / 2));
                                    if (gm.structGameManager.gm_dataCombatPvP.pointsForHit)
                                    {
                                        proyectil.GetPlayer().PD.score = proyectil.GetPlayer().PD.score + (proyectil.GetPlayer().PD.scoreForHit / 2);
                                    }
                                }
                                proyectil.damage = proyectil.GetAuxDamage();
                                player.barraDeEscudo.SubstractPorcentageBar(player.barraDeEscudo.substractForHit);
                                //proyectil.timeLife = 0;
                                //proyectil.GetPoolObject().Recycle();
                                //proyectil.gameObject.SetActive(false);
                                eventWise.StartEvent("jugador_1_bloquear");
                                proyectil.AnimationHit();
                            }
                        }
                        if (player_PvP != null)
                        {
                            //Debug.Log(proyectil.GetEnemy());
                            if (proyectil.GetPlayer2() != null)
                            {
                                if (player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
                                {
                                    //AUMENTO XP PARA EL ATAQUE ESPECIAL
                                    //SI SE DEFIENDE CARGA LA MITAD DE LA BARRA DEL ATAQUE ESPECIAL.
                                    if (proyectil.gameObject.activeSelf && gameObject.activeSelf && proyectil != null && proyectil.GetPlayer2() != null)
                                    {
                                        proyectil.GetPlayer2().SetXpActual(proyectil.GetPlayer2().GetXpActual() + (proyectil.GetPlayer2().xpForHit / 2));
                                        if (gm.structGameManager.gm_dataCombatPvP.pointsForHit)
                                        {
                                            proyectil.GetPlayer2().PD.score = proyectil.GetPlayer2().PD.score + (proyectil.GetPlayer2().PD.scoreForHit / 2);
                                        }
                                    }
                                    proyectil.damage = proyectil.GetAuxDamage();
                                    player.barraDeEscudo.SubstractPorcentageBar(player.barraDeEscudo.substractForHit);
                                    //proyectil.timeLife = 0;
                                    //proyectil.GetPoolObject().Recycle();
                                    //proyectil.gameObject.SetActive(false);
                                    eventWise.StartEvent("jugador_1_bloquear");
                                    proyectil.AnimationHit();
                                }
                            }
                            if (player_PvP.playerSelected == Player_PvP.PlayerSelected.Defensivo)
                            {
                                switch (player_PvP.playerState)
                                {
                                    case Player_PvP.State.Defendido:
                                        if (player.barraDeEscudo != null)
                                        {
                                            if (player_PvP.playerActual == Player_PvP.Player.player1)
                                            {
                                                if (player_PvP.stateDeffence == Player_PvP.StateDeffence.CounterAttackDeffense)
                                                {
                                                    // SI ANDA MAL EL DISPARO COMENTAR LA LINEA DE ABAJO (player.enableAttack = true;)
                                                    player.enableAttack = true;
                                                    player.Attack(Proyectil.DisparadorDelProyectil.Jugador1);
                                                    player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.ContraAtaque;
                                                }
                                            }
                                            else if (player_PvP.playerActual == Player_PvP.Player.player2)
                                            {
                                                if (player_PvP.stateDeffence == Player_PvP.StateDeffence.CounterAttackDeffense)
                                                {
                                                    // SI ANDA MAL EL DISPARO COMENTAR LA LINEA DE ABAJO (player.enableAttack = true;)
                                                    player.enableAttack = true;
                                                    player.Attack(Proyectil.DisparadorDelProyectil.Jugador2);
                                                    player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.ContraAtaque;
                                                }
                                            }
                                            proyectil.damage = proyectil.GetAuxDamage();
                                            player.barraDeEscudo.SubstractPorcentageBar(player.barraDeEscudo.substractForHit);
                                        }
                                        //proyectil.GetPoolObject().Recycle();
                                        //proyectil.gameObject.SetActive(false);
                                        //proyectil.timeLife = 0;
                                        proyectil.GetPoolObject().Recycle();
                                        eventWise.StartEvent("jugador_1_bloquear");
                                        //proyectil.AnimationHit();
                                        break;
                                    default:
                                        if (proyectil.GetPlayer() != null)
                                        {
                                            if (player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
                                            {
                                                //AUMENTO XP PARA EL ATAQUE ESPECIAL
                                                //SI SE DEFIENDE CARGA LA MITAD DE LA BARRA DEL ATAQUE ESPECIAL.
                                                if (proyectil.gameObject.activeSelf && gameObject.activeSelf && proyectil != null && proyectil.GetPlayer() != null)
                                                {
                                                    proyectil.GetPlayer().SetXpActual(proyectil.GetPlayer().GetXpActual() + (proyectil.GetPlayer().xpForHit / 2));
                                                    if (gm.structGameManager.gm_dataCombatPvP.pointsForHit)
                                                    {
                                                        proyectil.GetPlayer().PD.score = proyectil.GetPlayer().PD.score + (proyectil.GetPlayer().PD.scoreForHit / 2);
                                                    }
                                                }
                                            }
                                        }
                                        else if (proyectil.GetPlayer2() != null)
                                        {
                                            if (player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
                                            {
                                                //AUMENTO XP PARA EL ATAQUE ESPECIAL
                                                //SI SE DEFIENDE CARGA LA MITAD DE LA BARRA DEL ATAQUE ESPECIAL.
                                                if (proyectil.gameObject.activeSelf && gameObject.activeSelf && proyectil != null && proyectil.GetPlayer2() != null)
                                                {
                                                    proyectil.GetPlayer2().SetXpActual(proyectil.GetPlayer2().GetXpActual() + (proyectil.GetPlayer2().xpForHit / 2));
                                                    if (gm.structGameManager.gm_dataCombatPvP.pointsForHit)
                                                    {
                                                        proyectil.GetPlayer2().PD.score = proyectil.GetPlayer2().PD.score + (proyectil.GetPlayer2().PD.scoreForHit / 2);
                                                    }
                                                }
                                            }
                                        }
                                        proyectil.damage = proyectil.GetAuxDamage();
                                        player.barraDeEscudo.SubstractPorcentageBar(player.barraDeEscudo.substractForHit);
                                        //proyectil.timeLife = 0;
                                        //proyectil.GetPoolObject().Recycle();
                                        //proyectil.gameObject.SetActive(false);
                                        eventWise.StartEvent("jugador_1_bloquear");
                                        proyectil.AnimationHit();
                                        break;
                                }
                            }
                            //Debug.Log(proyectil.GetEnemy());
                            if (proyectil.GetEnemy() != null)
                            {
                                //Debug.Log("A LA DEFENSA");
                                proyectil.GetEnemy().SetXpActual(proyectil.GetEnemy().GetXpActual() + (proyectil.GetEnemy().xpForHit / 2));
                                proyectil.damage = proyectil.GetAuxDamage();
                                player.barraDeEscudo.SubstractPorcentageBar(player.barraDeEscudo.substractForHit);
                                //proyectil.timeLife = 0;
                                //proyectil.GetPoolObject().Recycle();
                                //proyectil.gameObject.SetActive(false);
                                eventWise.StartEvent("jugador_1_bloquear");
                                proyectil.AnimationHit();
                            }
                        }
                    }
                }
                else if (enemy != null && !inPlayer && inEnemy)
                {
                    bool enableDamagePlayer = true;
                    //Debug.Log(state);
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
                                    if (gm.structGameManager.gm_dataCombatPvP.pointsForHit)
                                    {
                                        proyectil.GetPlayer().PD.score = proyectil.GetPlayer().PD.score + proyectil.GetPlayer().PD.scoreForHit;
                                    }
                                }
                                //proyectil.timeLife = 0;
                                //proyectil.gameObject.SetActive(false);
                                //proyectil.GetPoolObject().Recycle();
                                eventWise.StartEvent("golpear_p1");
                                proyectil.AnimationHit();
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
                                if (gm.structGameManager.gm_dataCombatPvP.pointsForHit)
                                {
                                    proyectil.GetPlayer().PD.score = proyectil.GetPlayer().PD.score + (proyectil.GetPlayer().PD.scoreForHit / 2);
                                }
                            }

                            //Debug.Log(enemy.enumsEnemy.GetMovement());
                            if (enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.DefensaEnElLugar 
                                || enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AgacheDefensa
                                || enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoDefensa)
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
                                    }
                                }
                                //-----------------------------------------//
                            }
                            //proyectil.timeLife = 0;
                            //proyectil.GetPoolObject().Recycle();
                            //proyectil.gameObject.SetActive(false);
                            if (enemy.barraDeEscudo != null)
                            {
                                if (enemy.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.AtaqueEspecial
                                    && enemy.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.AtaqueEspecialAgachado
                                    && enemy.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.AtaqueEspecialSalto)
                                {
                                    enemy.barraDeEscudo.SubstractPorcentageBar(enemy.barraDeEscudo.substractForHit);
                                }
                            }
                            eventWise.StartEvent("jugador_1_bloquear");
                            proyectil.AnimationHit();
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
