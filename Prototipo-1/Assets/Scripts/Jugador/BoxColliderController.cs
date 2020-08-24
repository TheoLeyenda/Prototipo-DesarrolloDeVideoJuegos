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
    [HideInInspector]
    public bool enableCounterAttack;
    [HideInInspector]
    public float delayEnableCounterAttack = 0.05f;
    [HideInInspector]
    public float auxDelayEnableCounterAttack = 0.05f;
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
    public void CollisionWhitProyectil(Collider2D collision, Player PlayerDisparador, Proyectil proyectil, Proyectil.DisparadorDelProyectil PlayerCounterAttack, bool notProyectilParabola, bool notProyectilGaseosa, bool enableAnimationHit)
    {
        if (player != null && PlayerDisparador != player && !inEnemy && inPlayer)
        {
            
            bool enableDamagePlayer = true;
            if (state == StateBoxCollider.Normal)
            {
                //Debug.Log("ENTRE");
                if (proyectil.GetEnemy() != null)
                {
                    Enemy enemy = proyectil.GetEnemy();
                    //Debug.Log("ENTRE");
                    if (proyectil.disparadorDelProyectil == Proyectil.DisparadorDelProyectil.Enemigo)
                    {
                        player.SetEnableCounterAttack(true);
                        if (player.delayCounterAttack > 0)
                        {
                            //Debug.Log("ENTRE");
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
                            //Debug.Log("ENTRE");
                            if (proyectil.gameObject.activeSelf && gameObject.activeSelf && proyectil != null && enemy != null)
                            {
                                if (Proyectil.typeProyectil.AtaqueEspecial != proyectil.tipoDeProyectil
                                    && enemy.EnableChargerSpecialAttackForHit)
                                {
                                    enemy.SetXpActual(enemy.GetXpActual() + enemy.xpForHit);
                                }

                                if (player.PD.Blindaje <= 0)
                                {
                                    player.PD.lifePlayer = player.PD.lifePlayer - proyectil.damage;
                                }
                                else
                                {
                                    player.PD.Blindaje = player.PD.Blindaje - proyectil.damage / 2;
                                }
                            }
                            if (proyectil.GetComponent<ProyectilLimo>() == null)
                            {
                                player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                            }
                            if (!ZonaContraAtaque)
                            {
                                eventWise.StartEvent("golpear_p1");
                                if (enableAnimationHit)
                                {
                                    proyectil.AnimationHit();
                                }
                            }
                            if (proyectil.colisionPlayer)
                            {
                                eventWise.StartEvent("golpear_p1");
                                if (enableAnimationHit)
                                {
                                    proyectil.AnimationHit();
                                }
                            }
                        }
                        else if (player.delayCounterAttack <= 0 && proyectil.timeLife > 0 && enableDamagePlayer || (!ZonaContraAtaque || (proyectil.colisionPlayer && notProyectilParabola)))
                        {
                            //Debug.Log("ENTRE");
                            if (Proyectil.typeProyectil.AtaqueEspecial != proyectil.tipoDeProyectil
                                && enemy.EnableChargerSpecialAttackForHit)
                            {
                                enemy.SetXpActual(enemy.GetXpActual() + enemy.xpForHit);
                            }
                            player.SetEnableCounterAttack(false);
                            if (player.PD.Blindaje <= 0)
                            {
                                player.PD.lifePlayer = player.PD.lifePlayer - proyectil.damage;
                            }
                            else
                            {
                                player.PD.Blindaje = player.PD.Blindaje - proyectil.damage / 2;
                            }
                            if (proyectil.GetComponent<ProyectilLimo>() == null)
                            {
                                player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                            }
                            eventWise.StartEvent("golpear_p1");
                            if (enableAnimationHit)
                            {
                                proyectil.AnimationHit();
                            }
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
                            if (player.PD.Blindaje <= 0)
                            {
                                player.PD.lifePlayer = player.PD.lifePlayer - proyectil.damage;
                            }
                            else
                            {
                                player.PD.Blindaje = player.PD.Blindaje - proyectil.damage / 2;
                            }

                            if (proyectil.GetComponent<ProyectilLimo>() == null)
                            {
                                player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                            }
                            if (Proyectil.typeProyectil.AtaqueEspecial != proyectil.tipoDeProyectil)
                            {
                                PlayerDisparador.SetXpActual(PlayerDisparador.GetXpActual() + PlayerDisparador.xpForHit);
                            }
                            if (gm.structGameManager.gm_dataCombatPvP.pointsForHit)
                            {
                                PlayerDisparador.PD.score = PlayerDisparador.PD.score + PlayerDisparador.PD.scoreForHit;
                            }
                            eventWise.StartEvent("golpear_p1");
                            if (enableAnimationHit)
                            {
                                proyectil.AnimationHit();
                            }
                        }

                    }
                    else if (player.delayCounterAttack <= 0 && proyectil.timeLife > 0 && enableDamagePlayer && notProyectilParabola || (!ZonaContraAtaque || (proyectil.colisionPlayer && notProyectilParabola)))
                    {

                        if (proyectil.gameObject.activeSelf && gameObject.activeSelf && proyectil != null && PlayerDisparador != null)
                        {
                            player.SetEnableCounterAttack(false);
                            if (player.PD.Blindaje <= 0)
                            {
                                player.PD.lifePlayer = player.PD.lifePlayer - proyectil.damage;
                            }
                            else
                            {
                                player.PD.Blindaje = player.PD.Blindaje - proyectil.damage / 2;
                            }
                            if (proyectil.GetComponent<ProyectilLimo>() == null)
                            {
                                player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                            }
                            if (Proyectil.typeProyectil.AtaqueEspecial != proyectil.tipoDeProyectil)
                            {
                                PlayerDisparador.SetXpActual(PlayerDisparador.GetXpActual() + PlayerDisparador.xpForHit);
                            }
                            if (gm.structGameManager.gm_dataCombatPvP.pointsForHit)
                            {
                                PlayerDisparador.PD.score = PlayerDisparador.PD.score + PlayerDisparador.PD.scoreForHit;
                            }
                            eventWise.StartEvent("golpear_p1");
                            if (enableAnimationHit)
                            {
                                proyectil.AnimationHit();
                            }
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
                    if (enableAnimationHit)
                    {
                        proyectil.AnimationHit();
                    }
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
                                if (enableAnimationHit)
                                {
                                    proyectil.AnimationHit();
                                }
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
                    if (enableAnimationHit)
                    {
                        proyectil.AnimationHit();
                    }
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
                        if (enemy.Blindaje <= 0)
                        {
                            enemy.life = enemy.life - proyectil.damage;
                        }
                        else
                        {
                            enemy.Blindaje = enemy.Blindaje - proyectil.damage / 2;
                        }

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
                        if (enableAnimationHit)
                        {
                            proyectil.AnimationHit();
                        }
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
                    if (enableAnimationHit)
                    {
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
                    Proyectil proyectil = collision.GetComponent<Proyectil>();
                    bool notProyectilParabola = proyectil.GetComponent<ProyectilParabola>() == null;
                    bool notProyectilGaseosa = proyectil.GetComponent<GranadaGaseosa>() == null;
                    bool enableAnimationHit = proyectil.GetComponent<ProyectilInparable>() == null;
                    CollisionWhitProyectil(collision, proyectil.GetPlayer(), proyectil, Proyectil.DisparadorDelProyectil.Jugador2, notProyectilParabola, notProyectilGaseosa, enableAnimationHit);
                    CollisionWhitProyectil(collision, proyectil.GetPlayer2(), proyectil, Proyectil.DisparadorDelProyectil.Jugador1, notProyectilParabola, notProyectilGaseosa, enableAnimationHit);
                    break;
            }
        }
    }
}
