using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Prototipo_2 {
    public class StructsEnemys : MonoBehaviour
    {
        public struct DataEnemy
        {
            public int CantCasillasOcupadas_X;
            public int CantCasillasOcupadas_Y;
            public int columnaActual;
        }
        [System.Serializable]
        public struct ParticleMovement
        {
            public float timeLifeParticleJamp;
            public float auxTimeLifeParticleJamp;
            public GameObject particleJump;
        }
        public DataEnemy dataEnemy;
        public GameObject ruta;
        public GameObject rutaAgachado;
        public int countRoot;
        public int initialRoot;
        public ParticleMovement particleMovement;
        private void Update()
        {
            CheckParticleJumpActivate();
        }
        public void CheckParticleJumpActivate()
        {
            if (particleMovement.particleJump != null)
            {
                if (particleMovement.timeLifeParticleJamp > 0 && particleMovement.particleJump.activeSelf)
                {
                    particleMovement.timeLifeParticleJamp = particleMovement.timeLifeParticleJamp - Time.deltaTime;
                }
                else if (particleMovement.timeLifeParticleJamp <= 0)
                {
                    particleMovement.timeLifeParticleJamp = particleMovement.auxTimeLifeParticleJamp;
                    particleMovement.particleJump.SetActive(false);
                }
            }
        }
    }
}
