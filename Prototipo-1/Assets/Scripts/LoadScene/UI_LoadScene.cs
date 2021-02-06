using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_LoadScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textTitulo;
    [SerializeField] TextMeshProUGUI textEscenaCargando;
    [SerializeField] TextMeshProUGUI textPorcentage;
    [SerializeField] Image loadImage;
    private GameData gd;
    public Loading loadingReference;
    void Start()
    {
        loadImage.fillAmount = 0;
        string nameLoadLevel = LevelLoader.nextLevel;
        gd = GameData.instaceGameData;

        switch (gd.gd)
        {
            case GameData.GameMode.History:
                textTitulo.text = "HISTORIA";
                textEscenaCargando.text = nameLoadLevel;
                break;
            case GameData.GameMode.PvP:
                textTitulo.text = "MULTIJUGADOR";
                if (nameLoadLevel == "PvP") textEscenaCargando.text = "Combate";
                else if (nameLoadLevel == "TiroAlBlanco") textEscenaCargando.text = "Tiro al Blanco";
                break;
            case GameData.GameMode.Survival:
                textTitulo.text = "SUPERVIVENCIA";
                textEscenaCargando.text = nameLoadLevel;
                break;
        }
    }
    void Update()
    {
        CheckLoadImage();
    }
    public void CheckLoadImage()
    {
        if (loadingReference.porcentageLoad < loadingReference.maxPorcentage)
            loadImage.fillAmount = loadingReference.porcentageLoad / loadingReference.maxPorcentage;
        if (loadingReference.porcentageLoad >= 100)
            loadingReference.porcentageLoad = 99;
        textPorcentage.text = Mathf.Round(loadingReference.porcentageLoad) + "%";
    }
}
