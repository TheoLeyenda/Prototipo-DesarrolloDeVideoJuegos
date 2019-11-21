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
        public string nameNextScene;
        public int maxRounds;
        public int minRounds;
        [Header("Configuracion De Rondas")]
        public TextMeshProUGUI textCountRounds;
        public Button ButtonAddRounds;
        public Button ButtonSubstractRounds;
        [Header("Configuracion De Puntos Por Golpe")]
        public GameObject ConfiguracionPuntosPorGolpe;
        public Button ButtonYesPointForHit;
        public Button ButtonNoPointForHit;
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

        // Update is called once per frame
        public void StartGame()
        {
            //Debug.Log(gm.structGameManager.gm_dataCombatPvP.modoElegido);
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
                ButtonAddRounds.interactable = true;
            }
            else
            {
                ButtonAddRounds.interactable = false;
            }

            if (gm.structGameManager.gm_dataCombatPvP.countRounds > minRounds)
            {
                ButtonSubstractRounds.interactable = true;
            }
            else
            {
                ButtonSubstractRounds.interactable = false;
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
            ButtonNoPointForHit.interactable = true;
            ButtonYesPointForHit.interactable = false;
            gm.structGameManager.gm_dataCombatPvP.pointsForHit = true;
            textYesOrNot.text = "Si";
        }
        public void NoPointForHit()
        {
            ButtonNoPointForHit.interactable = false;
            ButtonYesPointForHit.interactable = true;
            gm.structGameManager.gm_dataCombatPvP.pointsForHit = false;
            textYesOrNot.text = "No";
        }
    }
}
