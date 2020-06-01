using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoxColliderController : MonoBehaviour
{
    public Player player;
    public Enemy enemy;
    public bool inEnemy;
    public bool inPlayer;
    public bool ZonaContraAtaque;
    private BoxCollider2D boxCollider2D;
    public EventWise eventWise;
    private GameManager gm;
    private bool enableCounterAttack;
    private float delayEnableCounterAttack = 0.05f;
    private float auxDelayEnableCounterAttack = 0.05f;
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
    private void Update()
    {
        if (delayEnableCounterAttack > 0 && !enableCounterAttack)
        {
            delayEnableCounterAttack = delayEnableCounterAttack - Time.deltaTime;
        }
        if (delayEnableCounterAttack <= 0)
        {
            delayEnableCounterAttack = auxDelayEnableCounterAttack;
            enableCounterAttack = true;
        }
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
        enableCounterAttack = true;
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
    public void CollisionWhitProyectil(Collider2D collision, Player PlayerDisparador, Proyectil proyectil, Proyectil.DisparadorDelProyectil PlayerCounterAttack, bool notProyectilParabola, bool notProyectilGaseosa)
    {
        if (player != null && PlayerDisparador != player && !inEnemy && inPlayer)
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
                            if (InputPlayerController.GetInputButtonDown(player.inputDeffenseButton) && player.barraDeEscudo.GetEnableDeffence() && !player.barraDeEscudo.nededBarMaxPorcentage && enableCounterAttack)
                            {
                                proyectil.gameObject.SetActive(false);
                                player.Attack(PlayerCounterAttack);
                                proyectil.timeLife = 0;
                                proyectil.GetPoolObject().Recycle();
                                enableCounterAttack = false;
                            }
                        }
                        if (player.delayCounterAttack <= 0 && proyectil.timeLife <= 0 && enableDamagePlayer || (!ZonaContraAtaque || (proyectil.colisionPlayer && notProyectilParabola)))
                        {
                            if (proyectil.gameObject.activeSelf && gameObject.activeSelf && proyectil != null && proyectil.GetEnemy() != null)
                            {
                                if (Proyectil.typeProyectil.AtaqueEspecial != proyectil.tipoDeProyectil)
                                {
                                    Debug.Log("ENTRE");
                                    proyectil.GetEnemy().SetXpActual(proyectil.GetEnemy().GetXpActual() + proyectil.GetEnemy().xpForHit);
                                }
                                player.PD.lifePlayer = player.PD.lifePlayer - proyectil.damage;
                            }
                            player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                            if (!ZonaContraAtaque)
                            {
                                eventWise.StartEvent("golpear_p1");
                                proyectil.AnimationHit();
                            }
                            if (proyectil.colisionPlayer)
                            {
                                eventWise.StartEvent("golpear_p1");
                                proyectil.AnimationHit();
                            }
                        }
                        else if (player.delayCounterAttack <= 0 && proyectil.timeLife > 0 && enableDamagePlayer || (!ZonaContraAtaque || (proyectil.colisionPlayer && notProyectilParabola)))
                        {
                            if (Proyectil.typeProyectil.AtaqueEspecial != proyectil.tipoDeProyectil)
                            {
                                proyectil.GetEnemy().SetXpActual(proyectil.GetEnemy().GetXpActual() + proyectil.GetEnemy().xpForHit);
                            }
                            player.SetEnableCounterAttack(false);
                            player.PD.lifePlayer = player.PD.lifePlayer - proyectil.damage;
                            player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                            eventWise.StartEvent("golpear_p1");
                            proyectil.AnimationHit();
                        }
                    }

                }
                if (PlayerDisparador != null)
                {
                    //AUMENTO XP PARA EL ATAQUE ESPECIAL       
                    player.SetEnableCounterAttack(true);
                    if (player.delayCounterAttack > 0)
                    {
                        if (InputPlayerController.GetInputButtonDown(player.inputDeffenseButton) && player.barraDeEscudo.GetEnableDeffence() && !player.barraDeEscudo.nededBarMaxPorcentage && enableCounterAttack)
                        {
                            player.Attack(PlayerCounterAttack);
                            enableDamagePlayer = false;
                            proyectil.timeLife = 0;
                            proyectil.GetPoolObject().Recycle();
                            enableCounterAttack = false;
                        }
                    }
                    if (player.delayCounterAttack <= 0 && proyectil.timeLife <= 0 && enableDamagePlayer && notProyectilParabola || (!ZonaContraAtaque || (proyectil.colisionPlayer && notProyectilParabola)))
                    {

                        if (proyectil.gameObject.activeSelf && gameObject.activeSelf && proyectil != null && PlayerDisparador != null)
                        {
                            player.PD.lifePlayer = player.PD.lifePlayer - proyectil.damage;
                            player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                            if (Proyectil.typeProyectil.AtaqueEspecial != proyectil.tipoDeProyectil)
                            {
                                PlayerDisparador.SetXpActual(PlayerDisparador.GetXpActual() + PlayerDisparador.xpForHit);
                            }
                            if (gm.structGameManager.gm_dataCombatPvP.pointsForHit)
                            {
                                PlayerDisparador.PD.score = PlayerDisparador.PD.score + PlayerDisparador.PD.scoreForHit;
                            }
                            eventWise.StartEvent("golpear_p1");
                            proyectil.AnimationHit();
                        }

                    }
                    else if (player.delayCounterAttack <= 0 && proyectil.timeLife > 0 && enableDamagePlayer && notProyectilParabola || (!ZonaContraAtaque || (proyectil.colisionPlayer && notProyectilParabola)))
                    {

                        if (proyectil.gameObject.activeSelf && gameObject.activeSelf && proyectil != null && PlayerDisparador != null)
                        {
                            player.SetEnableCounterAttack(false);
                            player.PD.lifePlayer = player.PD.lifePlayer - proyectil.damage;
                            player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                            if (Proyectil.typeProyectil.AtaqueEspecial != proyectil.tipoDeProyectil)
                            {
                                PlayerDisparador.SetXpActual(PlayerDisparador.GetXpActual() + PlayerDisparador.xpForHit);
                            }
                            if (gm.structGameManager.gm_dataCombatPvP.pointsForHit)
                            {
                                PlayerDisparador.PD.score = PlayerDisparador.PD.score + PlayerDisparador.PD.scoreForHit;
                            }
                            eventWise.StartEvent("golpear_p1");
                            proyectil.AnimationHit();
                        }

                    }
                }
            }
            else if (state == StateBoxCollider.Defendido)
            {
                Player_PvP player_PvP = player.gameObject.GetComponent<Player_PvP>();
                float realDamage;
                if (PlayerDisparador != null)
                {
                    //AUMENTO XP PARA EL ATAQUE ESPECIAL
                    //SI SE DEFIENDE CARGA LA MITAD DE LA BARRA DEL ATAQUE ESPECIAL.
                    if (proyectil.gameObject.activeSelf && gameObject.activeSelf && proyectil != null && PlayerDisparador != null)
                    {
                        if (Proyectil.typeProyectil.AtaqueEspecial != proyectil.tipoDeProyectil)
                        {
                            PlayerDisparador.SetXpActual(PlayerDisparador.GetXpActual() + (PlayerDisparador.xpForHit / 2));
                        }
                        if (gm.structGameManager.gm_dataCombatPvP.pointsForHit)
                        {
                            PlayerDisparador.PD.score = PlayerDisparador.PD.score + (PlayerDisparador.PD.scoreForHit / 2);
                        }
                    }
                    proyectil.damage = proyectil.GetAuxDamage();
                    player.barraDeEscudo.SubstractPorcentageBar(player.barraDeEscudo.substractForHit);
                    eventWise.StartEvent("jugador_1_bloquear");
                    proyectil.AnimationHit();
                }
                if (player_PvP != null)
                {
                    if (player_PvP.playerSelected == Player_PvP.PlayerSelected.Defensivo)
                    {
                        switch (player_PvP.playerState)
                        {
                            case Player_PvP.State.Defendido:
                                if (player.barraDeEscudo != null)
                                {
                                    if (player_PvP.stateDeffence == Player_PvP.StateDeffence.CounterAttackDeffense && enableCounterAttack)
                                    {
                                        player.enableAttack = true;
                                        player.Attack(PlayerCounterAttack);
                                        player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.ContraAtaque;
                                        //BORRAR LINEA DE ABAJO (enableCounterAttack = false) SI PREFERIMOS QUE AL DEFENDER TIRE DOS PROYECTILES EN VEZ DE UNO
                                        enableCounterAttack = false;
                                    }
                                    proyectil.damage = proyectil.GetAuxDamage();
                                    player.barraDeEscudo.SubstractPorcentageBar(player.barraDeEscudo.substractForHit);
                                }
                                proyectil.GetPoolObject().Recycle();
                                eventWise.StartEvent("jugador_1_bloquear");
                                break;
                            default:
                                if (PlayerDisparador != null)
                                {
                                    //AUMENTO XP PARA EL ATAQUE ESPECIAL
                                    //SI SE DEFIENDE CARGA LA MITAD DE LA BARRA DEL ATAQUE ESPECIAL.
                                    if (proyectil.gameObject.activeSelf && gameObject.activeSelf && proyectil != null && PlayerDisparador != null)
                                    {
                                        if (Proyectil.typeProyectil.AtaqueEspecial != proyectil.tipoDeProyectil)
                                        {
                                            PlayerDisparador.SetXpActual(PlayerDisparador.GetXpActual() + (PlayerDisparador.xpForHit / 2));
                                        }
                                        if (gm.structGameManager.gm_dataCombatPvP.pointsForHit)
                                        {
                                            PlayerDisparador.PD.score = PlayerDisparador.PD.score + (PlayerDisparador.PD.scoreForHit / 2);
                                        }
                                    }
                                }
                                proyectil.damage = proyectil.GetAuxDamage();
                                player.barraDeEscudo.SubstractPorcentageBar(player.barraDeEscudo.substractForHit);
                                eventWise.StartEvent("jugador_1_bloquear");
                                proyectil.AnimationHit();
                                break;
                        }
                    }
                }
                if (proyectil.GetEnemy() != null)
                {
                    if (Proyectil.typeProyectil.AtaqueEspecial != proyectil.tipoDeProyectil)
                    {
                        proyectil.GetEnemy().SetXpActual(proyectil.GetEnemy().GetXpActual() + (proyectil.GetEnemy().xpForHit / 2));
                    }
                    proyectil.damage = proyectil.GetAuxDamage();
                    player.barraDeEscudo.SubstractPorcentageBar(player.barraDeEscudo.substractForHit);
                    eventWise.StartEvent("jugador_1_bloquear");
                    proyectil.AnimationHit();
                }
                
            }
        }
        else if (enemy != null && !inPlayer && inEnemy)
        {
            bool enableDamagePlayer = true;
            if (state == StateBoxCollider.Normal)
            {
                if (PlayerDisparador != null)
                {
                    if (enemy.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoveToPointCombat && enemy.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoveToPointDeath)
                    {
                        enemy.life = enemy.life - proyectil.damage;

                        //AUMENTO XP PARA EL ATAQUE ESPECIAL
                        if (proyectil.gameObject.activeSelf && gameObject.activeSelf && proyectil != null && PlayerDisparador != null)
                        {
                            if (Proyectil.typeProyectil.AtaqueEspecial != proyectil.tipoDeProyectil)
                            {
                                PlayerDisparador.SetXpActual(PlayerDisparador.GetXpActual() + PlayerDisparador.xpForHit);
                            }
                            if (gm.structGameManager.gm_dataCombatPvP.pointsForHit)
                            {
                                PlayerDisparador.PD.score = PlayerDisparador.PD.score + PlayerDisparador.PD.scoreForHit;
                            }
                        }
                        eventWise.StartEvent("golpear_p1");
                        proyectil.AnimationHit();
                    }
                }
            }
            else if (state == StateBoxCollider.Defendido)
            {
                if (PlayerDisparador != null)
                {
                    //AUMENTO XP PARA EL ATAQUE ESPECIAL
                    //SI SE DEFIENDE CARGA LA MITAD DE LA BARRA DEL ATAQUE ESPECIAL.
                    if (proyectil.gameObject.activeSelf && gameObject.activeSelf && proyectil != null && PlayerDisparador != null)
                    {
                        if (Proyectil.typeProyectil.AtaqueEspecial != proyectil.tipoDeProyectil)
                        {
                            PlayerDisparador.SetXpActual(PlayerDisparador.GetXpActual() + (PlayerDisparador.xpForHit / 2));
                        }
                        if (gm.structGameManager.gm_dataCombatPvP.pointsForHit)
                        {
                            PlayerDisparador.PD.score = PlayerDisparador.PD.score + (PlayerDisparador.PD.scoreForHit / 2);
                        }
                    }

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
                                enemy.Attack(false, false, true, proyectil);
                                enemyDeffended.SetStateDeffense(Defensivo.StateDeffence.Nulo);
                                enemyDeffended.delayStateCounterAttackDeffense = 0;
                                enemyDeffended.delayStateDeffense = enemyDeffended.GetAuxDelayStateDeffense();
                                enemyDeffended.delayVulnerable = enemyDeffended.GetAuxDelayVulnerable();
                            }
                        }
                        //-----------------------------------------//
                    }
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
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (gameObject.activeSelf)
        {
            switch (collision.tag)
            {
                case "Proyectil":
                    Proyectil proyectil = collision.GetComponent<Proyectil>();
                    bool notProyectilParabola = proyectil.GetComponent<ProyectilParabola>() == null;
                    bool notProyectilGaseosa = proyectil.GetComponent<GranadaGaseosa>() == null;
                    CollisionWhitProyectil(collision, proyectil.GetPlayer(), proyectil, Proyectil.DisparadorDelProyectil.Jugador2, notProyectilParabola, notProyectilGaseosa);
                    CollisionWhitProyectil(collision, proyectil.GetPlayer2(), proyectil, Proyectil.DisparadorDelProyectil.Jugador1, notProyectilParabola, notProyectilGaseosa);
                    break;
            }
        }
    }
}
