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
            //Debug.Log(ActualSprite);
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
            else if(ActualSprite != SpriteActual.RecibirDanio && ActualSprite != SpriteActual.ContraAtaque)
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
            }
            CheckActualSprite();

        }
        public void CheckDeleyRecibirDanio()
        {
            if (delaySpriteRecibirDanio > 0)
            {
                delaySpriteRecibirDanio = delaySpriteRecibirDanio - Time.deltaTime;
            }
            else if (delaySpriteRecibirDanio <= 0)
            {
                delaySpriteRecibirDanio = auxDelaySpriteRecibirDanio;
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
                                //animator.Play("nombre animacion");
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
                                //animator.enabled = false;
                                spriteRenderer.sprite = CheckListSprite("RecibirDanio-Defensivo");
                                break;
                            case SpriteActual.ContraAtaque:
                                animator.enabled = false;
                                spriteRenderer.sprite = CheckListSprite("ContraAtaque-Defensivo");
                                break;
                            default:
                                //ActualSprite = SpriteActual.Parado;
                                //spriteRenderer.sprite = CheckListSprite("Parado-Defensivo");
                                break;
                        }
                        break;
                    case EnumsEnemy.TiposDeEnemigo.Balanceado:
                        switch (ActualSprite)
                        {
                            case SpriteActual.Agachado:
                                animator.enabled = false;
                                spriteRenderer.sprite = CheckListSprite("Agachado-Balanceado");
                                break;
                            case SpriteActual.AgachadoDefensa:
                                animator.enabled = false;
                                spriteRenderer.sprite = CheckListSprite("AgachadoDefensa-Balanceado");
                                break;
                            case SpriteActual.MoverAdelante:
                                animator.enabled = false;
                                spriteRenderer.sprite = CheckListSprite("MoverseAdelante-Balanceado");
                                break;
                            case SpriteActual.MoverAtras:
                                animator.enabled = false;
                                spriteRenderer.sprite = CheckListSprite("MoverseAtras-Balanceado");
                                break;
                            case SpriteActual.RecibirDanio:
                                animator.enabled = false;
                                spriteRenderer.sprite = CheckListSprite("RecibirDanio-Balanceado");
                                break;
                            case SpriteActual.Parado:
                                animator.enabled = false;
                                spriteRenderer.sprite = CheckListSprite("Parado-Balanceado");
                                break;
                            case SpriteActual.ParadoDefensa:
                                animator.enabled = false;
                                spriteRenderer.sprite = CheckListSprite("ParadoDefensa-Balanceado");
                                break;
                            case SpriteActual.Salto:
                                animator.enabled = false;
                                spriteRenderer.sprite = CheckListSprite("Salto-Balanceado");
                                break;
                            case SpriteActual.SaltoDefensa:
                                animator.enabled = false;
                                spriteRenderer.sprite = CheckListSprite("SaltoDefensa-Balanceado");
                                break;
                            case SpriteActual.AgachadoAtaque:
                                animator.enabled = true;
                                break;
                            case SpriteActual.ParadoAtaque:
                                animator.enabled = true;
                                break;
                            case SpriteActual.SaltoAtaque:
                                animator.enabled = true;
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
                    //Debug.Log("ENTRE");
                    break;
            }
        }
    }
}
