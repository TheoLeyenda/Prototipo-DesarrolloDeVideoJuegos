using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2
{
    public class SpriteEnemy : MonoBehaviour
    {
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
        }
        private void OnEnable()
        {
            auxDelaySpriteRecibirDanio = delaySpriteRecibirDanio;
            auxDelaySpriteContraAtaque = delaySpriteContraAtaque;
        }
        public void Update()
        {
            CheckEnumSprite();
        }
        public void CheckEnumSprite()
        {
            if (ActualSprite == SpriteActual.RecibirDanio || ActualSprite == SpriteActual.ContraAtaque)
            {
                CheckDeleyRecibirDanio();
                CheckDeleyContraAtaque();
            }
            else
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
                    delaySpriteRecibirDanio = auxDelaySpriteRecibirDanio;
                    delaySpriteContraAtaque = auxDelaySpriteContraAtaque;
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
                                spriteRenderer.sprite = CheckListSprite("Parado-Defensivo");
                                break;
                            case SpriteActual.ParadoDefensa:
                                spriteRenderer.sprite = CheckListSprite("ParadoDefensa-Defensivo");
                                break;
                            case SpriteActual.ParadoAtaque:
                                spriteRenderer.sprite = CheckListSprite("ParadoAtaque-Defensivo");
                                if (spriteRenderer.sprite == null)
                                {
                                    spriteRenderer.sprite = CheckListSprite("Parado-Defensivo");
                                }
                                break;
                            case SpriteActual.MoverAdelante:
                                spriteRenderer.sprite = CheckListSprite("MoverAdelante-Defensivo");
                                break;
                            case SpriteActual.MoverAtras:
                                spriteRenderer.sprite = CheckListSprite("MoverAtras-Defensivo");
                                break;
                            case SpriteActual.RecibirDanio:
                                spriteRenderer.sprite = CheckListSprite("RecibirDanio-Defensivo");
                                break;
                            case SpriteActual.ContraAtaque:
                                spriteRenderer.sprite = CheckListSprite("ContraAtaque-Defensivo");
                                break;
                            default:
                                ActualSprite = SpriteActual.Parado;
                                spriteRenderer.sprite = CheckListSprite("Parado-Defensivo");
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
                    cuadrilla.stateCuadrilla = Cuadrilla.StateCuadrilla.Ocupado;
                    //Debug.Log("ENTRE");
                    break;
            }
        }
    }
}
