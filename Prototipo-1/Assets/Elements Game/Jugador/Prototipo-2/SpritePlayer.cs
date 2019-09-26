using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2 {
    public class SpritePlayer : MonoBehaviour
    {
        public Player player;
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
            AnimacionAtaque,
            Count,
        }
        public List<ElementsSprites> Sprites;
        public SpriteActual ActualSprite;
        public float delaySpriteRecibirDanio;
        private float auxDelaySpriteRecibirDanio;
        private void Start()
        {
            ActualSprite = SpriteActual.Parado;
            auxDelaySpriteRecibirDanio = delaySpriteRecibirDanio;
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
        public void CheckActualSprite()
        {
            switch (ActualSprite)
            {
                case SpriteActual.Parado:
                    spriteRenderer.sprite = CheckListSprite("Parado");
                    break;
                case SpriteActual.ParadoDefensa:
                    spriteRenderer.sprite = CheckListSprite("ParadoDefensa");
                    break;
                case SpriteActual.ParadoAtaque:
                    spriteRenderer.sprite = CheckListSprite("ParadoAtaque");
                    break;
                case SpriteActual.Salto:
                    spriteRenderer.sprite = CheckListSprite("Salto");
                    break;
                case SpriteActual.SaltoAtaque:
                    spriteRenderer.sprite = CheckListSprite("SaltoAtaque");
                    break;
                case SpriteActual.SaltoDefensa:
                    spriteRenderer.sprite = CheckListSprite("SaltoDefensa");
                    break;
                case SpriteActual.MoverAdelante:
                    spriteRenderer.sprite = CheckListSprite("MoverAdelante");
                    break;
                case SpriteActual.MoverAtras:
                    spriteRenderer.sprite = CheckListSprite("MoverAtras");
                    break;
                case SpriteActual.RecibirDanio:
                    spriteRenderer.sprite = CheckListSprite("RecibirDanio");
                    break;
                case SpriteActual.Agachado:
                    spriteRenderer.sprite = CheckListSprite("Agachado");
                    break;
                case SpriteActual.AgachadoAtaque:
                    spriteRenderer.sprite = CheckListSprite("AgachadoAtaque");
                    break;
                case SpriteActual.AgachadoDefensa:
                    spriteRenderer.sprite = CheckListSprite("AgachadoDefensa");
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
    }
}
