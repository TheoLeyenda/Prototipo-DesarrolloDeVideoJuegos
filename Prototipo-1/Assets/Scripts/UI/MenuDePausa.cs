using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuDePausa : MonoBehaviour
{
    public enum MenuActual
    {
        MenuPausa,
        MenuOpciones,
        MenuControles,
    }
    public EventWise eventwise;
    public GameObject MenuPausa;
    public GameObject MenuOpciones;
    public GameObject MenuControles;
    public LevelManager levelManager;
    private GameManager gm;
    private GameData gd;
    private MenuActual menuActual;
    private bool soundPause = false;
    [SerializeField] private bool useLoadingScene = true;
    // Start is called before the first frame update
    void Start()
    {
        menuActual = MenuActual.MenuPausa;
        if (GameManager.instanceGameManager != null)
        {
            gm = GameManager.instanceGameManager;
        }
        gd = GameData.instaceGameData;
        MenuPausa.SetActive(false);
    }
    private void OnEnable()
    {
        OnMenuPausa();
    }
    // Update is called once per frame
    void Update()
    {
        if (levelManager != null)
        {
            if (!levelManager.GetInDialog())
            {
                CheckInPause();
            }
        }
        else
        {
            CheckInPause();
        }
    }
    public void SoundDespausar()
    {
        eventwise.StartEvent("despausar");
    }
    public void Reanudar()
    {
        Time.timeScale = 1;
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        eventwise.StartEvent("despausar");
        eventwise.StartEvent("seleccionar");
        soundPause = false;
    }
    public void ReiniciarNivel()
    {
        DisableMenues();
        gm.playerData_P1.score = gm.playerData_P1.auxScore;
        eventwise.StartEvent("despausar");
        eventwise.StartEvent("seleccionar");
        eventwise.StartEvent("reiniciar");
        gm.resetMusic = true;
        if (gd != null)
        {
            gd.LoadAuxData();
        }
        if (gm.playerData_P1.score == gm.playerData_P1.auxScore)
        {
            if (levelManager != null)
            {
                gm.restartLevel = true;
                if (gm.restartLevel)
                {
                    if (useLoadingScene)
                    {
                        LevelLoader.nextLevel = SceneManager.GetActiveScene().name;
                        SceneManager.LoadScene("LoadScene");
                    }
                    else
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
            else
            {
                if (useLoadingScene)
                {
                    LevelLoader.nextLevel = SceneManager.GetActiveScene().name;
                    SceneManager.LoadScene("LoadScene");
                }
                else
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
    public void CheckInPause()
    {
        if (menuActual == MenuActual.MenuPausa)
        {
            if (Time.timeScale == 1)
            {
                MenuPausa.gameObject.SetActive(false);
            }
            else if (Time.timeScale == 0)
            {
                MenuPausa.gameObject.SetActive(true);
                if (!soundPause)
                {
                    eventwise.StartEvent("pausa");
                    soundPause = true;
                }
            }
        }
    }
    public void DisableMenues()
    {
        eventwise.StartEvent("seleccionar");
        Time.timeScale = 1;
        MenuPausa.SetActive(false);
        MenuOpciones.SetActive(false);
        MenuControles.SetActive(false);
    }
    public void OnMenuPausa()
    {
        eventwise.StartEvent("seleccionar");
        MenuPausa.SetActive(true);
        MenuOpciones.SetActive(false);
        MenuControles.SetActive(false);
        menuActual = MenuActual.MenuPausa;
    }
    public void OnMenuOpciones()
    {
        eventwise.StartEvent("seleccionar");
        MenuPausa.SetActive(false);
        MenuOpciones.SetActive(true);
        MenuControles.SetActive(false);
        menuActual = MenuActual.MenuOpciones;
    }
    public void OnMenuControles()
    {
        eventwise.StartEvent("seleccionar");
        MenuPausa.SetActive(false);
        MenuOpciones.SetActive(false);
        MenuControles.SetActive(true);
        menuActual = MenuActual.MenuControles;
    }
}
