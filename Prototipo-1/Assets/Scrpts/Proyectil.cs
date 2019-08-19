using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float timeLife;
    public float auxTimeLife;
    public Rigidbody2D rg2D;
    public Transform vectorForward;
    public Transform vectorForwardUp;
    public Transform vectorForwardDown;
    public Pool pool;
    private PoolObject poolObject;

    private void OnEnable()
    {
        timeLife = auxTimeLife;
    }
    private void Update()
    {
        CheckTimeLife();
    }
    public void On()
    {
        poolObject = GetComponent<PoolObject>();
        rg2D.velocity = Vector2.zero;
        rg2D.angularVelocity = 0;
        timeLife = auxTimeLife;
    }
    public void CheckTimeLife() {
        if (timeLife > 0)
        {
            timeLife = timeLife - Time.deltaTime;
        }
        else if (timeLife <= 0) {
            poolObject.Recycle();
        }
    }
    public void ShootForward() {

        rg2D.AddForce(transform.right*speed,ForceMode2D.Force);
    }
    public void ShootForwardUp() {
        rg2D.AddRelativeForce(vectorForwardUp.right*speed);
    }
    public void ShootForwardDown() {
        rg2D.AddRelativeForce(vectorForwardDown.right*speed, ForceMode2D.Force);
    }
    public PoolObject GetPoolObject()
    {
        return poolObject;
    }


}
