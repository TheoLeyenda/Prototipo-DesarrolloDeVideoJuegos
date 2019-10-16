using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2 {
    public class SpritePlayer : MonoBehaviour
    {
        public Player player;
        public SpriteRenderer spriteRenderer;
        private Animator animator;
        [System.Serializable]
        public class ElementsSprites
        {
            public Sprite sprite;
            public string name;

        }
        public enum SpriteActual
        {
            SaltoAtaque,
            SaltoDefensa,
            Salto,
            ParadoAtaque,
            ParadoDefensa,
            Parado,
            RecibirDanio,
            MoverAtras,
            MoverAdelante,
            AgachadoAtaque,
            AgachadoDefensa,
            Agachado,
            AnimacionAtaque,
            AtaqueEspecial,
            ContraAtaque,
            Count,
        }
        public List<ElementsSprites> Sprites;
        public SpriteActual ActualSprite;
        public float delaySpriteRecibirDanio;
        private float auxDelaySpriteRecibirDanio;
        public float delaySpriteContraAtaque;
        private float auxDelaySpriteContraAtaque;
        private void Start()
        {
            animator = GetComponent<Animator>();
            ActualSprite = SpriteActual.Parado;
            auxDelaySpriteRecibirDanio = delaySpriteRecibirDanio;
            auxDelaySpriteContraAtaque = delaySpriteContraAtaque;
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Cuadrilla":
                    Cuadrilla cuadrilla = collision.GetComponent<Cuadrilla>();
                    if (cuadrilla.stateCuadrilla != Cuadrilla.StateCuadrilla.Defendido)
                    {
                        if (cuadrilla.posicionCuadrilla != Cuadrilla.PosicionCuadrilla.CuadrillaBajaCentral
                            && cuadrilla.posicionCuadrilla != Cuadrilla.PosicionCuadrilla.CuadrillaBajaDerecha
                            && cuadrilla.posicionCuadrilla != Cuadrilla.PosicionCuadrilla.CuadrillaBajaIzquierda || !cuadrilla.player.GetIsJumping())
                        {
                            cuadrilla.stateCuadrilla = Cuadrilla.StateCuadrilla.Ocupado;
                        }
                    }
                    //Debug.Log("ENTRE");
                    break;
            }
        }
        public void Update()
        {
            CheckActualSprite();
        }
        public float GetAuxDelaySpriteRecibirDanio()
        {
            return auxDelaySpriteRecibirDanio;
        }
        public void CheckDeleyRecibirDanio()
        {
            if (delaySpriteRecibirDanio > 0)
            {
                delaySpriteRecibirDanio = delaySpriteRecibirDanio - Time.deltaTime;
            }
            else if(delaySpriteRecibirDanio <= 0)
            {
                ActualSprite = SpriteActual.Parado;
            }
        }
        public void CheckDeleyContraAtaque()
        {
            if (delaySpriteContraAtaque > 0)
            {
                delaySpriteContraAtaque = delaySpriteContraAtaque - Time.deltaTime;
            }
            else if (delaySpriteContraAtaque <= 0)
            {
                delaySpriteContraAtaque = auxDelaySpriteContraAtaque;
                ActualSprite = SpriteActual.Parado;
            }
        }
        public void CheckActualSprite()
        {
            if (ActualSprite == SpriteActual.RecibirDanio || ActualSprite == SpriteActual.ContraAtaque)
            {
                if (ActualSprite == SpriteActual.RecibirDanio)
                {
                    CheckDeleyRecibirDanio();
                }
                if (ActualSprite == SpriteActual.ContraAtaque)
                {
                    CheckDeleyContraAtaque();
                }
            }
            switch (ActualSprite)
            {
                case SpriteActual.Parado:
                    //animator.enabled = false;
                    spriteRenderer.sprite = CheckListSprite("Parado");
                    break;
                case SpriteActual.ParadoDefensa:
                    //animator.enabled = false;
                    spriteRenderer.sprite = CheckListSprite("ParadoDefensa");
                    break;
                case SpriteActual.Salto:
                    //animator.enabled = false;
                    spriteRenderer.sprite = CheckListSprite("Salto");
                    break;
                case SpriteActual.SaltoDefensa:
                    //animator.enabled = false;
                    spriteRenderer.sprite = CheckListSprite("SaltoDefensa");
                    break;
                case SpriteActual.MoverAdelante:
                    //animator.enabled = false;
                    spriteRenderer.sprite = CheckListSprite("MoverAdelante");
                    break;
                case SpriteActual.MoverAtras:
                    //animator.enabled = false;
                    spriteRenderer.sprite = CheckListSprite("MoverAtras");
                    break;
                case SpriteActual.RecibirDanio:
                    //animator.enabled = false;
                    spriteRenderer.sprite = CheckListSprite("RecibirDanio");
                    break;
                case SpriteActual.Agachado:
                    //animator.enabled = false;
                    spriteRenderer.sprite = CheckListSprite("Agachado");
                    break;
                case SpriteActual.AgachadoDefensa:
                    //animator.enabled = false;
                    spriteRenderer.sprite = CheckListSprite("AgachadoDefensa");
                    break;
                case SpriteActual.ContraAtaque:
                    //animator.enabled = false;
                    spriteRenderer.sprite = CheckListSprite("ContraAtaque");
                    break;
                case SpriteActual.ParadoAtaque:
                    break;
                case SpriteActual.AgachadoAtaque:
                    break;
                case SpriteActual.SaltoAtaque:
                    break;
            }
        }
        public Sprite CheckListSprite(string nameSprite)
        {
            for (int i = 0; i < Sprites.Count; i++)
            {
                if (nameSprite == Sprites[i].name)
                {
                    return Sprites[i].sprite;
                }
            }
            return null;
        }
        public void PlayAnimation(string nameAnimation)
        {
            animator.Play(nameAnimation);
        }
        public void PlayerAttack()
        {
            if (player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
            {
                player.Attack(Proyectil.DisparadorDelProyectil.Jugador1);
            }
            else if(player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
            {
                player.Attack(Proyectil.DisparadorDelProyectil.Jugador2);
            }
        }
        public void PlayerAttackDown()
        {
            if (player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
            {
                player.AttackDown(Proyectil.DisparadorDelProyectil.Jugador1);
            }
            else if (player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
            {
                player.AttackDown(Proyectil.DisparadorDelProyectil.Jugador2);
            }
        }
    }
    
}
