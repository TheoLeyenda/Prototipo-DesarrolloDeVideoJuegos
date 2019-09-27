using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Prototipo_2
{
    public class SelectedLevel : MonoBehaviour
    {
        // ESTE SCRIPT DEBE COMUNICAR AL STRUCT DEL GAME MANAGER LAS SELECCIONES DE LOS NIVELES
        private GameManager gm;
        public string nameNextScene;
        void Start()
        {
            if (GameManager.instanceGameManager != null)
            {
                gm = GameManager.instanceGameManager;
            }
            // SACAR ESTO LUEGO
            gm.structGameManager.gm_dataCombatPvP.level_selected = DataCombatPvP.Level_Selected.Programacion;
            SceneManager.LoadScene(nameNextScene);

        }

        void Update()
        {

        }
        // ESTE SCRIPT DEBE COMUNICAR AL STRUCT DEL GAME MANAGER LAS SELECCIONES DE LOS NIVELES
    }
}
