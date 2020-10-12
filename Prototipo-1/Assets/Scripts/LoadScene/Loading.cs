using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Loading : MonoBehaviour
{
    [SerializeField] private float speedLoad;

    [SerializeField]private bool usePanelTransitions = true;
    [SerializeField]private Transitions panelTransitions;
    public float porcentageLoad = 0;
    [HideInInspector]public float maxPorcentage = 99;
    string nameloadLevel = LevelLoader.nextLevel;
    bool loadScene = false;
    public float minPorcentageChackLoadScene = 70;
    private void Update()
    {
       CheckLoad();
    }

    public void CheckLoad()
    {
        if (porcentageLoad < maxPorcentage)
        {
            porcentageLoad = porcentageLoad + Time.deltaTime * speedLoad;
            if (!usePanelTransitions && !loadScene && porcentageLoad >= minPorcentageChackLoadScene + 1)
            {
                loadScene = true;
                SceneManager.LoadSceneAsync(nameloadLevel);
            }
        }
        else
        {
            porcentageLoad = maxPorcentage;
            if (!loadScene && usePanelTransitions)
            {
                loadScene = true;
                panelTransitions.LoadScene(nameloadLevel);
            }
        }
    }
}
