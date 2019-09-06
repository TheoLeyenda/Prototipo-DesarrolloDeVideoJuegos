using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public Image imagenButton;
    public Text textoButton;
    public Image panelAparicion;
    public ButtonEvent nextButton;
    [Header("Tiempo Boton Habilitado")]
    public float timeEnable;
    public float auxTimeEnable;
    [Header("Tiempo Boton Deshabilitado")]
    public float timeDisable;
    public float auxTimeDisable;
    [Header("Velocidad de Aparicion y Desaparicion")]
    public float speedAviable;
    public float speedDisable;
    private bool preseed;
    private float alpha;
    private float auxAlpha;
    [HideInInspector]
    public bool disappear;
    private float border = 5f;
    private GameManager gm;

    void Start()
    {
        if (GameManager.instanceGameManager != null)
        {
            gm = GameManager.instanceGameManager;
        }
        preseed = false;
        imagenButton.canvasRenderer.SetAlpha(0);
        textoButton.canvasRenderer.SetAlpha(0);
        alpha = 1;
        auxAlpha = alpha;
        timeDisable = 0;
    }
    private void OnEnable()
    {
        imagenButton.color = Color.white;
        disappear = false;
        preseed = false;
        imagenButton.canvasRenderer.SetAlpha(0);
        textoButton.canvasRenderer.SetAlpha(0);
        SetRandomPosition(-panelAparicion.rectTransform.sizeDelta.x/ border, panelAparicion.rectTransform.sizeDelta.x/ border, -panelAparicion.rectTransform.sizeDelta.y/ border, panelAparicion.rectTransform.sizeDelta.y/ border);
        timeDisable = 0;
        alpha = 0;
        timeEnable = auxTimeEnable;
    }
    // Update is called once per frame
    void Update()
    {
        //if (!preseed)
        //{
            CheckLife();
        //}
        //else
        //{
            
        //}
    }
    public void CheckLife()
    {
        if (timeEnable > 0)
        {
            Aparecer();
            
            timeEnable = timeEnable - Time.deltaTime;
            
        }
        if (timeEnable <= 0)
        {
            timeDisable = auxTimeDisable;
        }
        if (timeDisable > 0)
        {
            Desaparecer();
        }
    }
    public void Desaparecer()
    {
        if (nextButton != null)
        {
            if (nextButton.gameObject.activeSelf == false)
            {
                nextButton.gameObject.SetActive(true);
            }
        }
        if (alpha > 0)
        {
            alpha = alpha - Time.deltaTime*speedDisable;
        }
        imagenButton.canvasRenderer.SetAlpha(alpha);
        textoButton.canvasRenderer.SetAlpha(alpha);
        if (alpha <= 0)
        {
            alpha = auxAlpha;
            disappear = true;
           //gameObject.SetActive(false);
        }
    }
    public void Aparecer()
    {
        if (alpha < 1)
        {
            alpha = alpha + Time.deltaTime*speedAviable;
        }

        imagenButton.canvasRenderer.SetAlpha(alpha);
        textoButton.canvasRenderer.SetAlpha(alpha);
    }
    public void SetRandomPosition(float min_X, float max_X, float max_Y, float min_Y)
    {
        transform.localPosition = new Vector2(Random.Range(min_X, max_X), Random.Range(min_Y, max_Y));
    }
    public bool GetPressed()
    {
        return preseed;
    }
    public void ActivePressed()
    {
        imagenButton.color = Color.green;
        preseed = true;
    }
}
