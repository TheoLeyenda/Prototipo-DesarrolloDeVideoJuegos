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
            if (player.enumsPlayers.movimiento != EnumsPlayers.Movimiento.Saltar && Input.GetKey(player.ButtonDeffence))
            {
                ActualSprite = SpriteActual.ParadoDefensa;
                Debug.Log("CAMBIE EL SPRITE");
            }
            if(player.enumsPlayers.movimiento != EnumsPlayers.Movimiento.Saltar && !Input.GetKey(player.ButtonDeffence))
            {
                ActualSprite = SpriteActual.Parado;
            }
            CheckActualSprite();
            //if(Input.GetKey())
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
                    Debug.Log("CAMBIE EL SPRITE");
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
