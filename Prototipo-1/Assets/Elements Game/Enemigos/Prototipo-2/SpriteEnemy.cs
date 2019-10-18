using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2
{
    public class SpriteEnemy : MonoBehaviour
    {
        [HideInInspector]
        public Animator animator;
        public Enemy enemy;
        public SpriteRenderer spriteRenderer;

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
            ContraAtaque,
            AnimacionAtaque,
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
            auxDelaySpriteRecibirDanio = delaySpriteRecibirDanio;
            auxDelaySpriteContraAtaque = delaySpriteContraAtaque;
            ActualSprite = SpriteActual.Parado;
            animator = GetComponent<Animator>();
        }
        private void OnEnable()
        {
            auxDelaySpriteRecibirDanio = delaySpriteRecibirDanio;
            auxDelaySpriteContraAtaque = delaySpriteContraAtaque;
            ActualSprite = SpriteActual.Parado;
        }
        public void Update()
        {
            CheckEnumSprite();
        }
        public void CheckEnumSprite()
        {
            if (enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Saltar && enemy.enumsEnemy.typeEnemy != EnumsEnemy.TiposDeEnemigo.Defensivo)
            {
                ActualSprite = SpriteActual.Salto;
            }
            else if (enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque && enemy.enumsEnemy.typeEnemy != EnumsEnemy.TiposDeEnemigo.Defensivo)
            {
                ActualSprite = SpriteActual.SaltoAtaque;
            }
            else if (enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoDefensa && enemy.enumsEnemy.typeEnemy != EnumsEnemy.TiposDeEnemigo.Defensivo)
            {
                ActualSprite = SpriteActual.SaltoDefensa;
            }
            else if (enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.MoverAdelante)
            {
                ActualSprite = SpriteActual.MoverAdelante;
            }
            else if (enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.MoverAtras)
            {
                ActualSprite = SpriteActual.MoverAtras;
            }
            else if (enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.DefensaEnElLugar)
            {
                ActualSprite = SpriteActual.ParadoDefensa;
            }
            else if (enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtacarEnElLugar)
            {
                ActualSprite = SpriteActual.ParadoAtaque;
            }
            else if (enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AgacheDefensa && enemy.enumsEnemy.typeEnemy != EnumsEnemy.TiposDeEnemigo.Defensivo)
            {
                ActualSprite = SpriteActual.AgachadoDefensa;
            }
            else if (enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AgacharseAtaque && enemy.enumsEnemy.typeEnemy != EnumsEnemy.TiposDeEnemigo.Defensivo)
            {
                ActualSprite = SpriteActual.AgachadoAtaque;
            }
            else if (enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Agacharse && enemy.enumsEnemy.typeEnemy != EnumsEnemy.TiposDeEnemigo.Defensivo)
            {
                ActualSprite = SpriteActual.Agachado;
            }
            else if (ActualSprite != SpriteActual.RecibirDanio && ActualSprite != SpriteActual.ContraAtaque)
            {
                ActualSprite = SpriteActual.Parado;
            }
            //Debug.Log("Movimiento:" + enemy.enumsEnemy.GetMovement());
            //Debug.Log("Actual Sprite:"+ActualSprite);
            CheckActualSprite();

        }
        
        public void CheckActualSprite()
        {
                if (enemy.enumsEnemy.typeEnemy != EnumsEnemy.TiposDeEnemigo.Jefe)
                {
                    switch (enemy.enumsEnemy.typeEnemy) {
                    case EnumsEnemy.TiposDeEnemigo.Defensivo:
                        switch (ActualSprite)
                        {
                            case SpriteActual.Parado:
                                animator.enabled = false;
                                spriteRenderer.sprite = CheckListSprite("Parado-Defensivo");
                                break;
                            case SpriteActual.ParadoDefensa:
                                animator.enabled = false;
                                spriteRenderer.sprite = CheckListSprite("ParadoDefensa-Defensivo");
                                break;
                            case SpriteActual.ParadoAtaque:
                                animator.enabled = true;
                                PlayAnimation("Ataque enemigo defensivo");
                                break;
                            case SpriteActual.MoverAdelante:
                                animator.enabled = false;
                                spriteRenderer.sprite = CheckListSprite("MoverAdelante-Defensivo");
                                break;
                            case SpriteActual.MoverAtras:
                                animator.enabled = false;
                                spriteRenderer.sprite = CheckListSprite("MoverAtras-Defensivo");
                                break;
                            case SpriteActual.RecibirDanio:
                                animator.enabled = false;
                                spriteRenderer.sprite = CheckListSprite("RecibirDanio-Defensivo");
                                break;
                            case SpriteActual.ContraAtaque:
                                animator.enabled = false;
                                spriteRenderer.sprite = CheckListSprite("ContraAtaque-Defensivo");
                                break;
                            default:
                                break;
                        }
                        break;
                    case EnumsEnemy.TiposDeEnemigo.Balanceado:
                        switch (ActualSprite)
                        {
                            case SpriteActual.Agachado:
                                PlayAnimation("Agachado balanceado");
                                break;
                            case SpriteActual.AgachadoDefensa:
                                PlayAnimation("Agachado Defensa balanceado");
                                break;
                            case SpriteActual.MoverAdelante:
                                PlayAnimation("Moverse Adelante balanceado");
                                break;
                            case SpriteActual.MoverAtras:
                                PlayAnimation("Moverse Atras balanceado");
                                break;
                            case SpriteActual.RecibirDanio:
                                PlayAnimation("Recibir Danio balanceado");
                                break;
                            case SpriteActual.Parado:
                                PlayAnimation("Parado balanceado");
                                break;
                            case SpriteActual.ParadoDefensa:
                                PlayAnimation("Parado Defensa balanceado");
                                break;
                            case SpriteActual.Salto:
                                PlayAnimation("Salto balanceado");
                                break;
                            case SpriteActual.SaltoDefensa:
                                PlayAnimation("Salto Defensa balanceado");
                                break;
                            case SpriteActual.AgachadoAtaque:
                                break;
                            case SpriteActual.ParadoAtaque:
                                break;
                            case SpriteActual.SaltoAtaque:
                                break;
                        }
                        break;
                    case EnumsEnemy.TiposDeEnemigo.Agresivo:
                        switch (ActualSprite)
                        {
                            case SpriteActual.MoverAdelante:
                                PlayAnimation("Moverse Adelante enemigo agresivo");
                                break;
                            case SpriteActual.MoverAtras:
                                PlayAnimation("Moverse Atras enemigo agresivo");
                                break;
                            case SpriteActual.RecibirDanio:
                                PlayAnimation("Recibir Danio enemigo agresivo");
                                break;
                            case SpriteActual.Parado:
                                PlayAnimation("Parado enemigo agresivo");
                                break;
                            case SpriteActual.SaltoDefensa:
                                PlayAnimation("Salto Defensa enemigo agresivo");
                                break;
                            case SpriteActual.ParadoDefensa:
                                PlayAnimation("Parado Defensa enemigo agresivo");
                                break;
                            case SpriteActual.Salto:
                                PlayAnimation("Salto enemigo agresivo");
                                break;
                            case SpriteActual.ParadoAtaque:
                                break;
                            case SpriteActual.SaltoAtaque:
                                break;
                        }
                        break;
                }
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
        private void OnTriggerStay2D(Collider2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Cuadrilla":
                    Cuadrilla cuadrilla = collision.GetComponent<Cuadrilla>();
                    if (cuadrilla.posicionCuadrilla != Cuadrilla.PosicionCuadrilla.CuadrillaBajaCentral
                            && cuadrilla.posicionCuadrilla != Cuadrilla.PosicionCuadrilla.CuadrillaBajaDerecha
                            && cuadrilla.posicionCuadrilla != Cuadrilla.PosicionCuadrilla.CuadrillaBajaIzquierda || !cuadrilla.enemy.GetIsJamping())
                    {
                        cuadrilla.stateCuadrilla = Cuadrilla.StateCuadrilla.Ocupado;
                    }
                    break;
            }
        }
        public void PlayAnimation(string nameAnimation)
        {
            animator.Play(nameAnimation);
        }
        public void AttackJampEnemy()
        {
            enemy.delayAttack = enemy.delayAttackJumping;
            enemy.Attack(true, false, false);
        }
        public void AttackEnemy()
        {
            enemy.delayAttack = enemy.GetAuxDelayAttack();
            enemy.Attack(false, false, false);
        }
        public void RestartDelayAttackEnemy()
        {
            if (enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtacarEnElLugar && !enemy.GetIsJamping() 
                && enemy.SpeedJump >= enemy.GetAuxSpeedJamp() 
                || enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AgacharseAtaque 
                && enemy.SpeedJump >= enemy.GetAuxSpeedJamp())
            {
                enemy.delayAttack = enemy.GetAuxDelayAttack();
            }
            else if(enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque
                || enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Nulo)
            {
                enemy.delayAttack = enemy.delayAttackJumping;
            }
        }

    }
}
