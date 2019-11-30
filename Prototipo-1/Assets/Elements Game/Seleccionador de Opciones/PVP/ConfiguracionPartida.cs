using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

namespace Prototipo_2
{
    public class ConfiguracionPartida : MonoBehaviour
    {
        private GameManager gm;
        public Color colorDisable;
        public string nameNextScene;
        public int maxRounds;
        public int minRounds;
        [Header("Configuracion De Rondas")]
        public TextMeshProUGUI textCountRounds;
        public TextMeshProUGUI textButtonAddRonds;
        public TextMeshProUGUI textButtonSubstractRounds;
        [Header("Configuracion De Puntos Por Golpe")]
        public GameObject ConfiguracionPuntosPorGolpe;
        public TextMeshProUGUI textButtonYesPointForHit;
        public TextMeshProUGUI textButtonNoPointForHit;
        public TextMeshProUGUI textYesOrNot;
        // Start is called before the first frame update

        void Start()
        {
            if (GameManager.instanceGameManager != null)
            {
                gm = GameManager.instanceGameManager;
                gm.structGameManager.gm_dataCombatPvP.countRounds = minRounds;
                gm.structGameManager.gm_dataCombatPvP.countRoundsWiningP1 = 0;
                gm.structGameManager.gm_dataCombatPvP.countRoundsWiningP2 = 0;
                gm.structGameManager.gm_dataCombatPvP.pointsForHit = true;
                if (gm.structGameManager.gm_dataCombatPvP.modoElegido != StructGameManager.ModoPvPElegido.TiroAlBlanco)
                {
                    ConfiguracionPuntosPorGolpe.SetActive(false);
                }
                if (gm.structGameManager.gm_dataCombatPvP.pointsForHit)
                {
                    YesPointForHit();
                }
                else
                {
                    NoPointForHit();
                }
            }
        }

        public void StartGame()
        {
            if (gm.structGameManager.gm_dataCombatPvP.modoElegido == StructGameManager.ModoPvPElegido.PvP)
            {
                SceneManager.LoadScene("PvP");
            }
            else if (gm.structGameManager.gm_dataCombatPvP.modoElegido == StructGameManager.ModoPvPElegido.TiroAlBlanco)
            {
                SceneManager.LoadScene("TiroAlBlanco");
            }
        }
        private void Update()
        {
            if (gm != null)
            {
                CheckButtonActivate();
            }

        }
        public void CheckButtonActivate()
        {
            if (gm.structGameManager.gm_dataCombatPvP.countRounds < maxRounds)
            {
                textButtonAddRonds.color = Color.white;
            }
            else
            {
                textButtonAddRonds.color = colorDisable;
            }

            if (gm.structGameManager.gm_dataCombatPvP.countRounds > minRounds)
            {
                textButtonSubstractRounds.color = Color.white;
            }
            else
            {
                textButtonSubstractRounds.color = colorDisable;
            }
        }
        public void AddRounds()
        {
            if (gm.structGameManager.gm_dataCombatPvP.countRounds < maxRounds)
            {
                gm.structGameManager.gm_dataCombatPvP.countRounds++;
                textCountRounds.text = "" + gm.structGameManager.gm_dataCombatPvP.countRounds;
            }
        }
        public void SubstractRounds()
        {
            if (gm.structGameManager.gm_dataCombatPvP.countRounds > minRounds)
            {
                gm.structGameManager.gm_dataCombatPvP.countRounds--;
                textCountRounds.text = "" + gm.structGameManager.gm_dataCombatPvP.countRounds;
            }
        }
        public void YesPointForHit()
        {
            textButtonNoPointForHit.color = Color.white;
            textButtonYesPointForHit.color = colorDisable;
            gm.structGameManager.gm_dataCombatPvP.pointsForHit = true;
            textYesOrNot.text = "Si";
        }
        public void NoPointForHit()
        {
            textButtonNoPointForHit.color = colorDisable;
            textButtonYesPointForHit.color = Color.white;
            gm.structGameManager.gm_dataCombatPvP.pointsForHit = false;
            textYesOrNot.text = "No";
        }
    }
}
