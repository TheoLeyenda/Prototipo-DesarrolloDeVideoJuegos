using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public Image imagenButton;
    public Text textoButton;
    public float timeEnable;
    public float timeDisable;
    public float auxTimeEnable;
    public float auxTimeDisable;
    public float speedAviable;
    public float speedDisable;
    private float alpha;
    private float auxAlpha;
    void Start()
    {
        imagenButton.canvasRenderer.SetAlpha(0);
        textoButton.canvasRenderer.SetAlpha(0);
        alpha = 1;
        auxAlpha = alpha;
        timeDisable = 0;
    }
    private void OnEnable()
    {
        imagenButton.canvasRenderer.SetAlpha(0);
        textoButton.canvasRenderer.SetAlpha(0);
        timeDisable = 0;
        alpha = 0;
        timeEnable = auxTimeEnable;
    }
    // Update is called once per frame
    void Update()
    {
        CheckLife();
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
        Debug.Log(timeDisable);
        if (timeDisable > 0)
        {
            Desaparecer();
        }
    }
    public void Desaparecer()
    {
        if (alpha > 0)
        {
            alpha = alpha - Time.deltaTime*speedDisable;
        }
        imagenButton.canvasRenderer.SetAlpha(alpha);
        textoButton.canvasRenderer.SetAlpha(alpha);
        if (alpha <= 0)
        {
            alpha = auxAlpha;
            gameObject.SetActive(false);
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
}
