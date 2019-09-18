using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2 {
    public class SpritePlayer : MonoBehaviour
    {
        public Player player;
        public SpriteRenderer spriteRenderer;
        public Sprite SpriteAttackIdle;
        public Sprite SpriteDeffenceIdle;
        public Sprite SpriteAttackJumping;
        public Sprite SpriteIdle;
        public Sprite SpriteJump;
        public Sprite SpriteDuck;
        public Sprite SpriteMoveBack;
        public Sprite SpriteMoveForward;
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
            CheckSprite();
        }
        public void CheckSprite()
        {
            if (!Input.anyKeyDown)
            {
                spriteRenderer.sprite = SpriteIdle;
            }
            if (Input.GetKeyDown(player.ButtonAttack) && player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar && Input.GetKey(KeyCode.UpArrow))
            {
                Debug.Log("ENTRE");
                spriteRenderer.sprite = SpriteAttackJumping;
            }
            else if (Input.GetKey(player.ButtonAttack))
            {
                spriteRenderer.sprite = SpriteAttackIdle;
            }
            else if (Input.GetKey(player.ButtonDeffence))
            {
                spriteRenderer.sprite = SpriteDeffenceIdle;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo
                || player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAtras)
            {
                spriteRenderer.sprite = SpriteMoveBack;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo ||
                player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAdelante)
            {
                spriteRenderer.sprite = SpriteMoveForward;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo
                || player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar)
            {
                spriteRenderer.sprite = SpriteJump;
            }
            else if (Input.GetKey(KeyCode.DownArrow) && player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo)
            {
                spriteRenderer.sprite = SpriteDuck;
            }
            //if(Input.GetKey())
        }
    }
}
