using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]
    public bool disappear;
    public Image imageButton;
    public Text textoButton;
    public Image panelAparicion;
    public ButtonEvent nextButton;
    public float inclination;
    [Header("Tiempo Boton Habilitado")]
    public float timeEnable;
    public float auxTimeEnable;
    [Header("Tiempo Boton Deshabilitado")]
    public float timeDisable;
    public float auxTimeDisable;
    [Header("Velocidad de Aparicion y Desaparicion")]
    public float speedAviable;
    public float speedDisable;
    [Header("Velocidad de Movimiento")]
    public float SpeedMovement;
    private bool preseed;
    private float alpha;
    private float auxAlpha;
    private float border = 5f;
    private GameManager gm;
    private int typePattern;
    private int esquinaElejida;
    void Start()
    {
        if (GameManager.instanceGameManager != null)
        {
            gm = GameManager.instanceGameManager;
        }
        preseed = false;
        imageButton.canvasRenderer.SetAlpha(0);
        textoButton.canvasRenderer.SetAlpha(0);
        alpha = 1;
        auxAlpha = alpha;
        timeDisable = 0;
    }
    private void OnEnable()
    {
        imageButton.color = Color.white;
        disappear = false;
        preseed = false;
        imageButton.canvasRenderer.SetAlpha(0);
        textoButton.canvasRenderer.SetAlpha(0);
        alpha = 0;
        switch (typePattern) {

            case 0:
                SetRandomPosition(-panelAparicion.rectTransform.sizeDelta.x / border, panelAparicion.rectTransform.sizeDelta.x / border, -panelAparicion.rectTransform.sizeDelta.y / border, panelAparicion.rectTransform.sizeDelta.y / border);
                break;
            case 1:
                SetRandomPosition(-panelAparicion.rectTransform.sizeDelta.x / border, panelAparicion.rectTransform.sizeDelta.x / border, -panelAparicion.rectTransform.sizeDelta.y / border, panelAparicion.rectTransform.sizeDelta.y / border);
                break;
            case 2:
                int spawnPoints = 8;
                esquinaElejida = Random.Range(0, spawnPoints);
                //esquinaElejida = 7;
                float restarPosicion = 80;
                float sumarPosicion = 80;
                float fragmentArea = 2.5f;
                float altura = Random.Range(-panelAparicion.rectTransform.sizeDelta.y / 2, panelAparicion.rectTransform.sizeDelta.y / 2);
                float ancho = Random.Range(-panelAparicion.rectTransform.sizeDelta.x / 2, panelAparicion.rectTransform.sizeDelta.x / 2);
                switch (esquinaElejida)
                {
                    case 0:
                        
                        //Debug.Log(altura);
                        if (altura > panelAparicion.rectTransform.sizeDelta.y / fragmentArea)
                        {
                            transform.localPosition = new Vector2(panelAparicion.rectTransform.sizeDelta.x / 2 + imageButton.rectTransform.sizeDelta.x / 2, altura - restarPosicion);
                        }
                        else
                        {
                            transform.localPosition = new Vector2(panelAparicion.rectTransform.sizeDelta.x / 2 + imageButton.rectTransform.sizeDelta.x / 2, altura + sumarPosicion);
                        }
                        break;
                    case 1:
                        if (altura > panelAparicion.rectTransform.sizeDelta.y / fragmentArea)
                        {
                            Debug.Log("TE VAS MUY PARA ARRIBA");
                            transform.localPosition = new Vector2(-panelAparicion.rectTransform.sizeDelta.x / 2, altura - restarPosicion);
                        }
                        else
                        {
                            transform.localPosition = new Vector2(-panelAparicion.rectTransform.sizeDelta.x / 2, altura + sumarPosicion);
                        }
                        break;
                    case 2:
                        if (ancho > panelAparicion.rectTransform.sizeDelta.x / fragmentArea)
                        {
                            transform.localPosition = new Vector2(ancho - restarPosicion, panelAparicion.rectTransform.sizeDelta.y/2);
                        }
                        else
                        {
                            transform.localPosition = new Vector2(ancho + sumarPosicion, panelAparicion.rectTransform.sizeDelta.y/2);
                        }
                        break;
                    case 3:
                        if (ancho > panelAparicion.rectTransform.sizeDelta.x / fragmentArea)
                        {
                            transform.localPosition = new Vector2(ancho - restarPosicion, -panelAparicion.rectTransform.sizeDelta.y / 2);
                        }
                        else
                        {
                            transform.localPosition = new Vector2(ancho + sumarPosicion, -panelAparicion.rectTransform.sizeDelta.y / 2);
                        }
                        break;
                    case 4:
                        transform.localPosition = new Vector2(-panelAparicion.rectTransform.sizeDelta.x / 2, panelAparicion.rectTransform.sizeDelta.y/2);
                        break;
                    case 5:
                        transform.localPosition = new Vector2(-panelAparicion.rectTransform.sizeDelta.x / 2, -panelAparicion.rectTransform.sizeDelta.y / 2);
                        break;
                    case 6:
                        transform.localPosition = new Vector2(panelAparicion.rectTransform.sizeDelta.x / 2, panelAparicion.rectTransform.sizeDelta.y / 2);
                        break;
                    case 7:
                        transform.localPosition = new Vector2(panelAparicion.rectTransform.sizeDelta.x / 2, -panelAparicion.rectTransform.sizeDelta.y / 2);
                        break;
                }
                break;
        }
        timeDisable = 0;
        timeEnable = auxTimeEnable;
    }
    // Update is called once per frame
    void Update()
    {
        switch (typePattern) {
            case 0:
                //Debug.Log("ENTRE 0");
                CheckLife();
                break;
            case 1:
                //Debug.Log("ENTRE 1");
                if (!preseed)
                {
                    Aparecer();
                }
                else
                {
                    Desaparecer();
                    if (alpha <= 0)
                    {
                        gameObject.SetActive(false);
                    }
                }
                break;
            case 2:
                //Debug.Log("ENTRE 2");
                switch (esquinaElejida)
                {
                    case 0:
                        transform.Translate(Vector3.left * SpeedMovement);
                        //Debug.Log(-panelAparicion.rectTransform.sizeDelta.x / 2);
                        if (transform.localPosition.x < -panelAparicion.rectTransform.sizeDelta.x / 2)
                        {
                            Desaparecer();
                        }
                        else
                        {
                            Aparecer();
                        }
                        break;
                    case 1:
                        transform.Translate(Vector3.right * SpeedMovement);
                        //Debug.Log(-panelAparicion.rectTransform.sizeDelta.x / 2);
                        if (transform.localPosition.x > panelAparicion.rectTransform.sizeDelta.x / 2)
                        {
                            Desaparecer();
                        }
                        else
                        {
                            Aparecer();
                        }
                        break;
                    case 2:
                        transform.Translate(Vector3.down * SpeedMovement);
                        //Debug.Log(-panelAparicion.rectTransform.sizeDelta.x / 2);
                        if (transform.localPosition.y < -panelAparicion.rectTransform.sizeDelta.y / 2)
                        {
                            Desaparecer();
                        }
                        else
                        {
                            Aparecer();
                        }
                        break;
                    case 3:
                        transform.Translate(Vector3.up * SpeedMovement);
                        if (transform.localPosition.y > panelAparicion.rectTransform.sizeDelta.y / 2)
                        {
                            Desaparecer();
                        }
                        else
                        {
                            Aparecer();
                        }
                        break;
                    case 4:
                        Vector3 direction = Vector3.down + Vector3.right*inclination;
                        transform.Translate(direction * SpeedMovement);
                        if (transform.localPosition.y < -panelAparicion.rectTransform.sizeDelta.y / 2 || transform.localPosition.x > panelAparicion.rectTransform.sizeDelta.x / 2)
                        {
                            Desaparecer();
                        }
                        else
                        {
                            Aparecer();
                        }
                        break;
                    case 5:
                        direction = Vector3.up + Vector3.right * inclination;
                        transform.Translate(direction * SpeedMovement);
                        if (transform.localPosition.y > panelAparicion.rectTransform.sizeDelta.y / 2 || transform.localPosition.x > panelAparicion.rectTransform.sizeDelta.x / 2)
                        {
                            Desaparecer();
                        }
                        else
                        {
                            Aparecer();
                        }
                        break;
                    case 6:
                        direction = Vector3.down + Vector3.left * inclination;
                        transform.Translate(direction * SpeedMovement);
                        if (transform.localPosition.y < -panelAparicion.rectTransform.sizeDelta.y / 2 || transform.localPosition.x < -panelAparicion.rectTransform.sizeDelta.x / 2)
                        {
                            Desaparecer();
                        }
                        else
                        {
                            Aparecer();
                        }
                        break;
                    case 7:
                        direction = Vector3.up + Vector3.left * inclination;
                        transform.Translate(direction * SpeedMovement);
                        if (transform.localPosition.y > panelAparicion.rectTransform.sizeDelta.y / 2 || transform.localPosition.x < -panelAparicion.rectTransform.sizeDelta.x / 2)
                        {
                            Desaparecer();
                        }
                        else
                        {
                            Aparecer();
                        }
                        break;
                }
                break;
        }
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
        if (nextButton != null && typePattern != 1)
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
        imageButton.canvasRenderer.SetAlpha(alpha);
        textoButton.canvasRenderer.SetAlpha(alpha);
        if (alpha <= 0)
        {
            disappear = true;
        }
    }
    public void Aparecer()
    {
        if (alpha < 1)
        {
            alpha = alpha + Time.deltaTime*speedAviable;
        }
        imageButton.canvasRenderer.SetAlpha(alpha);
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
        gm.pushEventManager.ObjectivePushs++;
        if (typePattern == 1)
        {
            gm.pushEventManager.TextBotonesPrecionados.text = gm.pushEventManager.ObjectivePushs + "/" + gm.pushEventManager.buttonsEvents.Count;
        }
        else
        {
            gm.pushEventManager.TextBotonesPrecionados.text = gm.pushEventManager.ObjectivePushs + "/" + gm.pushEventManager.GetCantButtonUse();
        }
        imageButton.color = Color.green;
        preseed = true;
    }
    public void SetTypePattern(int _pattern)
    {
        typePattern = _pattern;
    }
    public void Move()
    {
        
    }
}
