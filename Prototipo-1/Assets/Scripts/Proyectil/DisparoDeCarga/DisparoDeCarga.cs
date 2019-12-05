using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisparoDeCarga : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeLife;
    public float auxTimeLife;
    public float damage;
    // Update is called once per frame
    void Update()
    {
        CheckTimeLife();
    }
        
    public void CheckTimeLife()
    {
        if (timeLife > 0)
        {
            timeLife = timeLife - Time.deltaTime;
        }
        else if (timeLife <= 0)
        {
            timeLife = auxTimeLife;
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "BoxColliderController")
        {
            BoxColliderController boxColliderController = collision.GetComponent<BoxColliderController>();
            if (boxColliderController.player == null)
            {
                return;
            }
            boxColliderController.player.PD.lifePlayer = boxColliderController.player.PD.lifePlayer - damage;
        }
            
    }
}
