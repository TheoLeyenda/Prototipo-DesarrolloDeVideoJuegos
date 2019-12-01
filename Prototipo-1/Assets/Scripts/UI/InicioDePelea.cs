using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Prototipo_2
{
    public class InicioDePelea : MonoBehaviour
    {
        public TextMeshProUGUI textRondaActual;
        public TextMeshProUGUI textInicioDeRonda;
        private bool OneEjecution;
        private GameManager gm;
        [HideInInspector]
        public bool DisableAllText;
        private void Start()
        {
            DisableAllText = false;
            OneEjecution = true;
            textRondaActual.gameObject.SetActive(false);
            textInicioDeRonda.gameObject.SetActive(false);
        }
        private void Update()
        {
            TextsActivate();
        }
        public void TextsActivate()
        {
            if (GameManager.instanceGameManager != null && OneEjecution)
            {
                gm = GameManager.instanceGameManager;
                textRondaActual.gameObject.SetActive(true);
                textRondaActual.text = "ROUND " + (gm.structGameManager.gm_dataCombatPvP.rondaActual + 1);
                textInicioDeRonda.text = "FIGHT";
                OneEjecution = false;
            }
            if (!OneEjecution && !textRondaActual.gameObject.activeSelf && !DisableAllText)
            {
                textInicioDeRonda.gameObject.SetActive(true);
                DisableAllText = true;
            }
        }
    }
}
