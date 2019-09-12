using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Prototipo_1
{

    public class GameManagerCharactersController : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameManager gm;
        [HideInInspector]
        public Player player1;
        [HideInInspector]
        public Player player2;
        [HideInInspector]
        public List<Enemy> enemiesActivate;
        [HideInInspector]
        public Player.Movimiento movimientoJugador1;
        [HideInInspector]
        public Player.EstadoJugador estadoJugador1;
        [HideInInspector]
        public Enemy.EstadoEnemigo estadoEnemigo;
        [HideInInspector]
        public Enemy.Movimiento movimientoEnemigo;
        private GameObject[] arrayGameObjectsScenes;
        void Start()
        {
            enemiesActivate = new List<Enemy>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void ResetCharacters()
        {
            if (player1 != null)
            {
                player1.RestartPlayer();
            }
            if (player2 != null)
            {
                player2.RestartPlayer();
            }
            for (int i = 0; i < enemiesActivate.Count; i++)
            {
                if (enemiesActivate[i] != null)
                {
                    enemiesActivate[i].ResetEnemy();
                }
            }
        }
        public void CheckCharactersInScene()
        {
            arrayGameObjectsScenes = SceneManager.GetActiveScene().GetRootGameObjects();
            enemiesActivate.Clear();
            for (int i = 0; i < arrayGameObjectsScenes.Length; i++)
            {
                if (arrayGameObjectsScenes[i].tag == "Enemy")
                {
                    if (arrayGameObjectsScenes[i].activeSelf)
                    {
                        enemiesActivate.Add(arrayGameObjectsScenes[i].GetComponent<Enemy>());
                    }
                }
                if (arrayGameObjectsScenes[i].tag == "Player")
                {
                    if (arrayGameObjectsScenes[i].activeSelf)
                    {
                        player1 = arrayGameObjectsScenes[i].GetComponent<Player>();
                    }
                }
            }
        }
        public void CheckCharcaters()
        {

            if (gm.MultiPlayer)
            {
                return;
            }
            if (gm.SiglePlayer)
            {

                CheckCharactersInScene();
                if (movimientoEnemigo == Enemy.Movimiento.AtacarCabeza && movimientoJugador1 == Player.Movimiento.AtacarCabeza)
                {
                    //EVENTO CUANDO EL ENEMIGO Y EL JUGADOR ATACAN AL MISMO OBJETIVO
                    if (gm.enumsGameManager.specialEvent == EnumsGameManager.EventoEspecial.Nulo)
                    {
                        //InitPushEvent();
                        gm.enumsGameManager.specialEvent = EnumsGameManager.EventoEspecial.CartelClash;
                        gm.pushEventManager.InitPushEvent();

                    }
                }
                else if (movimientoEnemigo == Enemy.Movimiento.AtacarTorso && movimientoJugador1 == Player.Movimiento.AtacarTorso)
                {
                    //EVENTO CUANDO EL ENEMIGO Y EL JUGADOR ATACAN AL MISMO OBJETIVO
                    if (gm.enumsGameManager.specialEvent == EnumsGameManager.EventoEspecial.Nulo)
                    {
                        gm.enumsGameManager.specialEvent = EnumsGameManager.EventoEspecial.CartelClash;
                        gm.pushEventManager.InitPushEvent();
                    }
                }
                else if (movimientoEnemigo == Enemy.Movimiento.AtacarPies && movimientoJugador1 == Player.Movimiento.AtacarPies)
                {
                    //EVENTO CUANDO EL ENEMIGO Y EL JUGADOR ATACAN AL MISMO OBJETIVO
                    if (gm.enumsGameManager.specialEvent == EnumsGameManager.EventoEspecial.Nulo)
                    {
                        gm.enumsGameManager.specialEvent = EnumsGameManager.EventoEspecial.CartelClash;
                        gm.pushEventManager.InitPushEvent();
                    }
                }
                else if (movimientoEnemigo == Enemy.Movimiento.Saltar && movimientoJugador1 == Player.Movimiento.AtacarPies)
                {
                    player1.Attack(Player.Objetivo.Piernas, false);
                    for (int i = 0; i < enemiesActivate.Count; i++)
                    {
                        if (enemiesActivate[i].typeEnemy == Enemy.TiposDeEnemigo.Balanceado)
                        {
                            enemiesActivate[i].Jump();
                            enemiesActivate[i].CounterAttack(false);
                            gm.enumsGameManager.specialEvent = EnumsGameManager.EventoEspecial.ContraAtaque;
                        }
                    }

                }
                else if (movimientoEnemigo == Enemy.Movimiento.Agacharse && movimientoJugador1 == Player.Movimiento.AtacarCabeza)
                {
                    player1.Attack(Player.Objetivo.Cabeza, false);
                    for (int i = 0; i < enemiesActivate.Count; i++)
                    {
                        if (enemiesActivate[i].typeEnemy == Enemy.TiposDeEnemigo.Balanceado)
                        {
                            enemiesActivate[i].Duck();
                            enemiesActivate[i].CounterAttack(false);
                            gm.enumsGameManager.specialEvent = EnumsGameManager.EventoEspecial.ContraAtaque;
                        }
                    }

                }
                else if (movimientoJugador1 == Player.Movimiento.Agacharse && movimientoEnemigo == Enemy.Movimiento.AtacarCabeza)
                {
                    for (int i = 0; i < enemiesActivate.Count; i++)
                    {
                        enemiesActivate[i].Attack(Enemy.Objetivo.Cabeza, false);
                    }
                    player1.Duck();
                    player1.CounterAttack(false);
                    gm.enumsGameManager.specialEvent = EnumsGameManager.EventoEspecial.ContraAtaque;
                }
                else if (movimientoJugador1 == Player.Movimiento.Saltar && movimientoEnemigo == Enemy.Movimiento.AtacarPies)
                {
                    for (int i = 0; i < enemiesActivate.Count; i++)
                    {
                        enemiesActivate[i].Attack(Enemy.Objetivo.Piernas, false);
                    }
                    player1.Jump();
                    player1.CounterAttack(false);
                    gm.enumsGameManager.specialEvent = EnumsGameManager.EventoEspecial.ContraAtaque;
                }
                else if (gm.enumsGameManager.specialEvent == EnumsGameManager.EventoEspecial.Nulo)
                {
                    switch (movimientoJugador1)
                    {
                        case Player.Movimiento.AtacarCabeza:
                            player1.Attack(Player.Objetivo.Cabeza, false);
                            break;
                        case Player.Movimiento.AtacarTorso:
                            player1.Attack(Player.Objetivo.Torso, false);
                            break;
                        case Player.Movimiento.AtacarPies:
                            player1.Attack(Player.Objetivo.Piernas, false);
                            break;
                        case Player.Movimiento.DefenderCabeza:
                            player1.Deffense(Player.Objetivo.Cabeza);
                            break;
                        case Player.Movimiento.DefenderTorsoPies:
                            player1.Deffense(Player.Objetivo.Cuerpo);
                            break;
                        case Player.Movimiento.Saltar:
                            player1.Jump();
                            break;
                        case Player.Movimiento.Agacharse:
                            player1.Duck();
                            break;
                    }
                    for (int i = 0; i < enemiesActivate.Count; i++)
                    {
                        if (enemiesActivate != null)
                        {
                            switch (movimientoEnemigo)
                            {
                                case Enemy.Movimiento.AtacarCabeza:
                                    enemiesActivate[i].Attack(Enemy.Objetivo.Cabeza, false);
                                    break;
                                case Enemy.Movimiento.AtacarTorso:
                                    enemiesActivate[i].Attack(Enemy.Objetivo.Torso, false);
                                    break;
                                case Enemy.Movimiento.AtacarPies:
                                    enemiesActivate[i].Attack(Enemy.Objetivo.Piernas, false);
                                    break;
                                case Enemy.Movimiento.DefenderCabeza:
                                    enemiesActivate[i].Deffense(Enemy.Objetivo.Cabeza);
                                    break;
                                case Enemy.Movimiento.DefenderTorso:
                                    enemiesActivate[i].Deffense(Enemy.Objetivo.Torso);
                                    break;
                                case Enemy.Movimiento.DefenderPies:
                                    enemiesActivate[i].Deffense(Enemy.Objetivo.Piernas);
                                    break;
                                case Enemy.Movimiento.DefenderTorsoPies:
                                    enemiesActivate[i].Deffense(Enemy.Objetivo.Cuerpo);
                                    break;
                                case Enemy.Movimiento.Saltar:
                                    enemiesActivate[i].Jump();
                                    break;
                                case Enemy.Movimiento.Agacharse:
                                    enemiesActivate[i].Duck();
                                    break;
                            }
                        }
                    }
                }
                switch (gm.enumsGameManager.specialEvent)
                {
                    case EnumsGameManager.EventoEspecial.CartelClash:
                        gm.pushEventManager.textClashEvent.gameObject.SetActive(true);
                        gm.pushEventManager.ActivateCartelClash();
                        break;
                    case EnumsGameManager.EventoEspecial.PushButtonEvent:
                        gm.pushEventManager.CheckEventPushButton(gm.pushEventManager.TypePushEvent);
                        break;
                    case EnumsGameManager.EventoEspecial.ContraAtaque:
                        gm.enumsGameManager.specialEvent = EnumsGameManager.EventoEspecial.Nulo;
                        break;
                }
            }

        }

        public void DisableUICharacters()
        {
            player1.BARRA_DE_VIDA.SetActive(false);
            player1.PanelMovement.SetActive(false);
            player1.PanelAttack.SetActive(false);
            player1.PanelDeffense.SetActive(false);
            player1.PanelDodge.SetActive(false);
            player1.PanelDeLogos.SetActive(false);
            for (int i = 0; i < enemiesActivate.Count; i++)
            {
                enemiesActivate[i].PanelDeLogos.SetActive(false);
                enemiesActivate[i].BARRA_DE_VIDA.SetActive(false);
            }
        }
        public void ActivateButtonPlayer1()
        {
            if (player1 != null)
            {
                player1.ActivateButtonsUI();
            }
        }
        public void DisableButtonPlayer1()
        {
            if (player1 != null)
            {
                player1.DisableButtonsUI();
            }
        }
        public void ActivateUICharacters()
        {
            player1.BARRA_DE_VIDA.SetActive(true);
            player1.PanelDeLogos.SetActive(true);
            for (int i = 0; i < enemiesActivate.Count; i++)
            {
                enemiesActivate[i].PanelDeLogos.SetActive(true);
                enemiesActivate[i].BARRA_DE_VIDA.SetActive(true);
            }
        }
        public void CheckTimeAttackCharacters()
        {
            if (gm.timeSelectionAttack > 0)
            {
                gm.timeSelectionAttack = gm.timeSelectionAttack - Time.deltaTime;
                gm.TimeClockOfAttack.fillAmount = gm.timeSelectionAttack / gm.auxTimeSelectionAttack;
            }
            else if (gm.timeSelectionAttack <= 0)
            {
                gm.GetFSM().SendEvent((int)EnumsGameManager.GameEvents.JugadasElejidas);
            }
        }
        public void AnswerPlayers()
        {
            gm.TimeClockOfAttack.gameObject.SetActive(false);
            gm.TextTimeStart.gameObject.SetActive(false);
            CheckCharcaters();
            if (gm.enumsGameManager.specialEvent == EnumsGameManager.EventoEspecial.Nulo)
            {
                gm.GetFSM().SendEvent((int)EnumsGameManager.GameEvents.TiempoFuera);
            }
        }

    }
}
