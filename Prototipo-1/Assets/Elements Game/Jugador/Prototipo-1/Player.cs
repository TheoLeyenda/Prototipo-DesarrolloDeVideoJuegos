﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Prototipo_1
{
    public class Player : MonoBehaviour
    {
        public enum Objetivo
        {
            Cuerpo,
            Cabeza,
            Torso,
            Piernas,
            Count,
        }
        public enum Movimiento
        {
            Nulo,
            AtacarCabeza,
            AtacarTorso,
            AtacarPies,
            DefenderCabeza,
            DefenderTorsoPies,
            Saltar,
            Agacharse,
            ContraAtaque,
            Count,
        }
        public enum EstadoJugador
        {
            vivo,
            muerto,
            Count,
        }
        //El player elije su movimiento.
        //El enemigo tira un random de movimiento.
        //dependiendo de la accion que aparezca un escudo que el player se mueva o que se dispare un objeto
        //que pueda elejir donde disparar(arriba, medio, abajo);
        // Start is called before the first frame update
        //Cada enemigo se especializa en alguna accion (Esquivar,Ataque o Defensa)
        public GameObject PanelDeLogos;
        public GameObject BARRA_DE_VIDA;
        public GameObject generadorProyectiles;
        private Objetivo _objetivo;
        private Movimiento _movimiento;
        private EstadoJugador _estado;
        private Animator animator;
        public float life;
        public float maxLife;
        public Image ImageHP;
        public SpriteRenderer mySelfSprite;
        public Pool poolObjectAttack;
        public Sprite SpriteBlanco;
        public Image imagenAccion;
        public Image imagenMovimiento;
        public Sprite SpriteMovimientoAtaque;
        public Sprite SpriteMovimientoDefensa;
        public Sprite SpriteMovimientoEsquive;
        public Sprite SpriteAtaqueCabeza;
        public Sprite SpriteAtaqueTorso;
        public Sprite SpriteAtaquePies;
        public Sprite SpriteDefensaCabeza;
        public Sprite SpriteDefensaCuerpo;
        public Sprite SpriteSalto;
        public Sprite SpriteAgacharse;
        public Button Button_DefenseHead;
        public GameObject PanelMovement;
        public GameObject PanelAttack;
        public GameObject PanelDeffense;
        public GameObject PanelDodge;
        public BoxCollider2D ShildHead;
        public BoxCollider2D ShildBoody;
        public BoxCollider2D BoxColliderHead;
        public BoxCollider2D BoxColliderChest;
        public BoxCollider2D BoxColliderLegs;
        public float timeButtonActivate;
        public float SpeedJump;
        private GameManager gm;
        private Rigidbody2D rg2D;
        public bool AviableDefenseHead;
        private float MinRangeRandom = 0;
        private float MaxRangeRandomMovement = 120;
        private float MaxRangeRandomTargertAttack = 3;
        private float MaxRangeRandomDodge = 2;
        private bool IaModeActivate;
        private const float MaxRangeRandomMovementOption1 = 40;
        private const float MaxRangeRandomMovementOption2 = 40;
        private float TiempoDisparoAutomatico = 0.01f;
        private Vector3 PosicionGeneracionBalaRelativa = new Vector3(-3f, -3.2f, 0);
        private int countClickButtonDefence;
        void Start()
        {
            countClickButtonDefence = 0;
            IaModeActivate = false;
            _movimiento = Movimiento.Nulo;
            _estado = EstadoJugador.vivo;
            DisableShild();
            if (GameManager.instanceGameManager != null)
            {
                gm = GameManager.instanceGameManager;
            }
            rg2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            CheckDead();
            CheckLifeBar();
            if (gm.GetGameState() == EnumsGameManager.GameState.EnComienzo)
            {
                if (_movimiento == Movimiento.Nulo && gm.timeSelectionAttack < TiempoDisparoAutomatico)
                {
                    IaMode();
                    gm.SetRespuestaJugador1(_movimiento);
                    gm.timeSelectionAttack = 0;
                }
                else if (_movimiento != Movimiento.Nulo && gm.timeSelectionAttack < TiempoDisparoAutomatico)
                {
                    gm.timeSelectionAttack = -1;
                }
                //Debug.Log((Movimiento)_movimiento);
                gm.SetRespuestaJugador1(_movimiento);

            }
        }
        public void IaMode()
        {
            float option = Random.Range(MinRangeRandom, MaxRangeRandomMovement);
            //Debug.Log(option);
            if (option <= MaxRangeRandomMovementOption1)
            {
                imagenMovimiento.sprite = SpriteMovimientoAtaque;
                option = Random.Range(MinRangeRandom, MaxRangeRandomTargertAttack);
                switch ((int)option)
                {
                    case 0:
                        imagenAccion.sprite = SpriteAtaqueCabeza;
                        _movimiento = Movimiento.AtacarCabeza;
                        break;
                    case 1:
                        imagenAccion.sprite = SpriteAtaqueTorso;
                        _movimiento = Movimiento.AtacarTorso;
                        break;
                    case 2:
                        imagenAccion.sprite = SpriteAtaquePies;
                        _movimiento = Movimiento.AtacarPies;
                        break;
                }
            }
            else if (option > MaxRangeRandomMovementOption1 && option <= MaxRangeRandomMovementOption2)
            {
                imagenMovimiento.sprite = SpriteMovimientoDefensa;
                imagenAccion.sprite = SpriteDefensaCuerpo;
                _movimiento = Movimiento.DefenderTorsoPies;
            }
            else if (option > MaxRangeRandomMovementOption2)
            {
                imagenMovimiento.sprite = SpriteMovimientoEsquive;
                option = Random.Range(MinRangeRandom, MaxRangeRandomDodge);
                switch ((int)option)
                {
                    case 0:
                        imagenAccion.sprite = SpriteAgacharse;
                        _movimiento = Movimiento.Agacharse;
                        break;
                    case 1:
                        imagenAccion.sprite = SpriteSalto;
                        _movimiento = Movimiento.Saltar;
                        break;
                }
            }
        }
        public void Back()
        {
            if (gm.timeSelectionAttack > 0)
            {
                PanelMovement.SetActive(true);
                PanelDodge.SetActive(false);
                PanelDeffense.SetActive(false);
                PanelAttack.SetActive(false);
                ShildHead.gameObject.SetActive(false);
                ShildBoody.gameObject.SetActive(false);
                imagenAccion.sprite = SpriteBlanco;
                imagenMovimiento.sprite = SpriteBlanco;
                _movimiento = Movimiento.Nulo;
            }

        }
        public void CheckLifeBar()
        {
            if (life <= maxLife)
            {
                ImageHP.fillAmount = life / maxLife;
            }
            else if (life > maxLife)
            {
                life = maxLife;
            }
            else if (life < 0)
            {
                life = 0;
            }
        }
        public void CheckDead()
        {
            if (life <= 0)
            {
                _estado = EstadoJugador.muerto;
                gm.SetEstadoJugador1(_estado);
                mySelfSprite.enabled = false;
                gm.ResetGameManager();
                gm.GameOver();
                gm.ResetRoundCombat(true);
            }
        }
        public void AttackButton()
        {
            countClickButtonDefence = 0;
            if (gm.GetGameState() == EnumsGameManager.GameState.EnComienzo)
            {
                imagenMovimiento.sprite = SpriteMovimientoAtaque;

                PanelDeffense.SetActive(false);
                PanelDodge.SetActive(false);
                PanelAttack.SetActive(true);
            }

        }
        public void DisableShild()
        {
            ShildBoody.gameObject.SetActive(false);
            ShildHead.gameObject.SetActive(false);
        }
        public void ActivateShild()
        {
            ShildBoody.gameObject.SetActive(true);
            ShildHead.gameObject.SetActive(true);
        }
        public void Attack(Objetivo ob, bool doubleDamage)
        {
            if (Time.timeScale > 0)
            {
                if (poolObjectAttack.count > 0)
                {

                    DisableShild();
                    GameObject go = poolObjectAttack.GetObject();
                    Proyectil proyectil = go.GetComponent<Proyectil>();
                    proyectil.SetDobleDamage(doubleDamage);
                    if (doubleDamage)
                    {
                        proyectil.damage = proyectil.damage * 2;
                    }
                    go.transform.position = generadorProyectiles.transform.localPosition;
                    go.transform.position = go.transform.position + PosicionGeneracionBalaRelativa;
                    proyectil.On();
                    switch (ob)
                    {
                        case Objetivo.Cabeza:
                            proyectil.ShootForwardUp();
                            break;
                        case Objetivo.Torso:
                            proyectil.ShootForward();
                            break;
                        case Objetivo.Piernas:
                            proyectil.ShootForwardDown();
                            break;
                    }

                }
            }
        }
        public void DefenseButton()
        {
            countClickButtonDefence++;
            PanelAttack.SetActive(false);
            PanelDodge.SetActive(false);
            switch (countClickButtonDefence)
            {
                case 1:
                    imagenMovimiento.sprite = SpriteMovimientoDefensa;
                    break;
                case 2:
                    imagenAccion.sprite = SpriteDefensaCuerpo;
                    break;
            }
            if (gm.GetGameState() == EnumsGameManager.GameState.EnComienzo && countClickButtonDefence >= 2)
            {
                if (AviableDefenseHead)
                {
                    Button_DefenseHead.gameObject.SetActive(true);
                }
                EstadoMovimiento_DefenderTorsoPies();
            }

        }

        public void Deffense(Objetivo ob)
        {
            countClickButtonDefence = 0;
            if (Time.timeScale > 0)
            {
                if (AviableDefenseHead)
                {
                    if (ob == Objetivo.Cabeza)
                    {
                        ShildHead.gameObject.SetActive(true);
                    }
                }
                else
                {
                    if (ob == Objetivo.Cuerpo)
                    {
                        ShildBoody.gameObject.SetActive(true);
                    }
                }
            }
        }
        public void Listo()
        {
            if (gm.GetGameState() == EnumsGameManager.GameState.EnComienzo)
            {
                if (gm.ActiveTime)
                {
                    gm.timeSelectionAttack = TiempoDisparoAutomatico;
                }
                else if (!gm.ActiveTime)
                {
                    gm.timeSelectionAttack = 0;
                }
            }
        }
        public void DisableButtonsUI()
        {
            PanelAttack.SetActive(false);
            PanelDeffense.SetActive(false);
            PanelDodge.SetActive(false);
            PanelMovement.SetActive(false);
        }
        public void ActivateButtonsUI()
        {
            PanelMovement.SetActive(true);
        }
        public void RestartPlayer()
        {
            imagenAccion.sprite = SpriteBlanco;
            imagenMovimiento.sprite = SpriteBlanco;
            Button_DefenseHead.gameObject.SetActive(false);
            PanelAttack.SetActive(false);
            PanelDeffense.SetActive(false);
            PanelDodge.SetActive(false);
            PanelMovement.SetActive(true);
            ShildHead.gameObject.SetActive(false);
            ShildBoody.gameObject.SetActive(false);
            BoxColliderHead.enabled = true;
            BoxColliderChest.enabled = true;
            BoxColliderLegs.enabled = true;
            _movimiento = Movimiento.Nulo;
        }
        public void DodgeButton()
        {
            countClickButtonDefence = 0;
            if (gm.GetGameState() == EnumsGameManager.GameState.EnComienzo)
            {
                imagenMovimiento.sprite = SpriteMovimientoEsquive;
                PanelAttack.SetActive(false);
                PanelDeffense.SetActive(false);
                PanelDodge.SetActive(true);
            }
        }
        public void Jump()
        {
            Debug.Log("Animacion De Salto");
            animator.Play("Animacion SaltoJugador");
        }
        public void Duck()
        {
            Debug.Log("Animacion De Agacharse");
            BoxColliderHead.enabled = false;
            BoxColliderChest.enabled = true;
            BoxColliderLegs.enabled = true;
        }
        public void CounterAttack(bool DoubleDamage)
        {
            ShildHead.gameObject.SetActive(false);
            ShildBoody.gameObject.SetActive(false);
            imagenMovimiento.sprite = SpriteMovimientoAtaque;
            if (poolObjectAttack.count > 0)
            {

                //Debug.Log("Objetivo: " + objetivoElejir);
                switch (_movimiento)
                {
                    case Movimiento.Agacharse:
                        //ATACAR A LA CABEZA
                        //Attack(Objetivo.Cabeza);
                        imagenAccion.sprite = SpriteAtaqueCabeza;
                        Attack(Objetivo.Cabeza, DoubleDamage);
                        break;
                    case Movimiento.Saltar:
                        //ATACAR A LOS PIES
                        //Attack(Objetivo.Piernas);
                        imagenAccion.sprite = SpriteAtaquePies;
                        Attack(Objetivo.Piernas, DoubleDamage);
                        break;
                    case Movimiento.ContraAtaque:
                        imagenMovimiento.sprite = SpriteMovimientoAtaque;
                        float option = Random.Range(MinRangeRandom, MaxRangeRandomTargertAttack);
                        switch ((int)option)
                        {
                            case 0:
                                imagenAccion.sprite = SpriteAtaqueCabeza;
                                Attack(Objetivo.Cabeza, DoubleDamage);
                                break;
                            case 1:
                                imagenAccion.sprite = SpriteAtaqueTorso;
                                Attack(Objetivo.Torso, DoubleDamage);
                                break;
                            case 2:
                                imagenAccion.sprite = SpriteAtaquePies;
                                Attack(Objetivo.Piernas, DoubleDamage);
                                break;
                        }
                        break;

                }
            }
        }
        public void EstadoMovimiento_AtacarCabeza()
        {
            if (gm.GetGameState() == EnumsGameManager.GameState.EnComienzo && gm.timeSelectionAttack > 1)
            {
                imagenAccion.sprite = SpriteAtaqueCabeza;
                _movimiento = Movimiento.AtacarCabeza;
                if (!gm.ActiveTime)
                {
                    gm.SetRespuestaJugador1(_movimiento);
                    Listo();
                }
            }
        }
        public void EstadoMovimiento_AtacarTorso()
        {
            if (gm.GetGameState() == EnumsGameManager.GameState.EnComienzo && gm.timeSelectionAttack > 1)
            {
                imagenAccion.sprite = SpriteAtaqueTorso;
                _movimiento = Movimiento.AtacarTorso;
                if (!gm.ActiveTime)
                {
                    gm.SetRespuestaJugador1(_movimiento);
                    Listo();
                }
            }
        }
        public void EstadoMovimiento_AtacarPies()
        {
            if (gm.GetGameState() == EnumsGameManager.GameState.EnComienzo && gm.timeSelectionAttack > 1)
            {
                imagenAccion.sprite = SpriteAtaquePies;
                _movimiento = Movimiento.AtacarPies;
                if (!gm.ActiveTime)
                {
                    gm.SetRespuestaJugador1(_movimiento);
                    Listo();
                }
            }
        }
        public void EstadoMovimiento_DefenderCabeza()
        {
            if (gm.GetGameState() == EnumsGameManager.GameState.EnComienzo && gm.timeSelectionAttack > 1)
            {
                imagenAccion.sprite = SpriteDefensaCabeza;
                _movimiento = Movimiento.DefenderCabeza;
                if (!gm.ActiveTime)
                {
                    gm.SetRespuestaJugador1(_movimiento);
                    Listo();
                }
            }
        }
        public void EstadoMovimiento_DefenderTorsoPies()
        {
            if (gm.GetGameState() == EnumsGameManager.GameState.EnComienzo && gm.timeSelectionAttack > 1)
            {
                imagenAccion.sprite = SpriteDefensaCuerpo;
                _movimiento = Movimiento.DefenderTorsoPies;
                if (!gm.ActiveTime)
                {
                    gm.SetRespuestaJugador1(_movimiento);
                    Listo();
                }
            }
        }
        public void EstadoMovimiento_Saltar()
        {
            if (gm.GetGameState() == EnumsGameManager.GameState.EnComienzo && gm.timeSelectionAttack > 1)
            {
                imagenAccion.sprite = SpriteSalto;
                _movimiento = Movimiento.Saltar;
                if (!gm.ActiveTime)
                {
                    gm.SetRespuestaJugador1(_movimiento);
                    Listo();
                }
            }
        }
        public void EstadoMovimiento_Agacharse()
        {
            if (gm.GetGameState() == EnumsGameManager.GameState.EnComienzo && gm.timeSelectionAttack > 1)
            {
                imagenAccion.sprite = SpriteAgacharse;
                _movimiento = Movimiento.Agacharse;
                if (!gm.ActiveTime)
                {
                    gm.SetRespuestaJugador1(_movimiento);
                    Listo();
                }
            }
        }
        public void EstadoMovimiento_ContraAtaque()
        {
            _movimiento = Movimiento.ContraAtaque;
        }
        public void EstadoJugador_vivo()
        {
            _estado = EstadoJugador.vivo;
        }
        public void EstadoJugador_muerto()
        {
            _estado = EstadoJugador.muerto;
        }
    }
}