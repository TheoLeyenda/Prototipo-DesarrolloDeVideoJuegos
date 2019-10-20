using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2
{
    public class StructsPlayer : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Awake()
        {
            player = GetComponent<Player>();
        }
        public struct DataPlayer
        {
            public int CantCasillasOcupadas_X;
            public int CantCasillasOcupadas_Y;
            public int CantCasillasOcupadasAgachado;
            public int CantCasillasOcupadasParado;
            public int columnaActual;
        }
        
        [System.Serializable]
        public struct DataSpecialAttack
        {
            public Pool poolProyectil;
            public Pool poolProyectilParabola;
            public Pool poolProyectilImparable;
            public GameObject DisparoDeCarga;
            public Pool poolGranadaGaseosa;
        }
        public Player player;
        public DataSpecialAttack dataAttack;
        public GameObject ruta;
        public GameObject rutaAgachado;
        public DataPlayer dataPlayer;
        public void Update()
        {
            CheckSpecialAttackDisparoDeCarga();
        }
        public void CheckSpecialAttackDisparoDeCarga()
        {
            if (dataAttack.DisparoDeCarga.activeSelf && player.enumsPlayers.specialAttackEquipped == EnumsPlayers.SpecialAttackEquipped.DisparoDeCarga)
            {
                player.spritePlayerActual.GetAnimator().SetBool("FinalAtaqueEspecial", false);
            }
            else
            {
                player.spritePlayerActual.GetAnimator().SetBool("FinalAtaqueEspecial", true);
            }
        }
    }
}
