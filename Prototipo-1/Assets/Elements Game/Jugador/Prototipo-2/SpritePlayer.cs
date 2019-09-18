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
                        cuadrilla.stateCuadrilla = Cuadrilla.StateCuadrilla.Ocupado;
                    }
                    //Debug.Log("ENTRE");
                    break;
            }
        }
        public void Update()
        {
            CheckEnumSprite();
        }
        public void CheckEnumSprite()
        {
            if (ActualSprite == SpriteActual.RecibirDanio)
            {
                CheckDeleyRecibirDanio();
            }
            else
            {
                if (player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar)
                {
                    ActualSprite = SpriteActual.Salto;
                }
                if (player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar && Input.GetKey(player.ButtonAttack))
                {
                    ActualSprite = SpriteActual.SaltoAtaque;
                }
                if (player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar && Input.GetKey(player.ButtonDeffence))
                {
                    ActualSprite = SpriteActual.SaltoDefensa;
                }
                if (player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAdelante)
                {
                    ActualSprite = SpriteActual.MoverAdelante;
                }
                if (player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAtras)
                {
                    ActualSprite = SpriteActual.MoverAtras;
                }
                if (player.enumsPlayers.movimiento != EnumsPlayers.Movimiento.Saltar && Input.GetKey(player.ButtonDeffence))
                {
                    ActualSprite = SpriteActual.ParadoDefensa;
                }
                if (player.enumsPlayers.movimiento != EnumsPlayers.Movimiento.Saltar && Input.GetKey(player.ButtonAttack))
                {
                    ActualSprite = SpriteActual.ParadoAtaque;
                }
                if ((ActualSprite != SpriteActual.RecibirDanio && player.enumsPlayers.movimiento != EnumsPlayers.Movimiento.Saltar && !Input.GetKey(player.ButtonDeffence) && !Input.GetKey(player.ButtonAttack)
                    && player.enumsPlayers.movimiento != EnumsPlayers.Movimiento.MoverAdelante && player.enumsPlayers.movimiento != EnumsPlayers.Movimiento.MoverAtras))
                {
                    ActualSprite = SpriteActual.Parado;
                    delaySpriteRecibirDanio = auxDelaySpriteRecibirDanio;
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
                    Debug.Log("ENTRE");
                    spriteRenderer.sprite = CheckListSprite("RecibirDanio");
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
