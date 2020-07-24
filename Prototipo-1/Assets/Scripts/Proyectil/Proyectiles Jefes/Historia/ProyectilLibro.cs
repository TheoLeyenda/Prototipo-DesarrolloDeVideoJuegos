using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilLibro : Proyectil
{
    // Start is called before the first frame update
    [Header("Config ProyectilLibro")]
    public List<Sprite> spriteLibros;
    private int IdLibro;
    public bool librosColoridos;
    private void OnEnable()
    {
        OnProyectilLibro();
    }
    private void OnDisable()
    {
        spriteRenderer.color = Color.white;
    }
    public void OnProyectilLibro()
    {
        if (librosColoridos)
        {
            float R = Random.Range(5, 256);
            float G = Random.Range(5, 256);
            float B = Random.Range(5, 256);
            IdLibro = Random.Range(0, spriteLibros.Count);

            spriteRenderer.sprite = spriteLibros[IdLibro];
            spriteRenderer.color = new Color(R, G, B);
        }
        
    }
}
