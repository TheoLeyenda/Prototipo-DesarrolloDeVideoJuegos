using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Rigidbody2D rg2D;
    public Vector2 vectorForwardUp;
    public Vector2 vectorForwardDown;
    void Start()
    {
    }

    public void ShootForward() {

        rg2D.AddRelativeForce(transform.right*speed,ForceMode2D.Force);
    }
    public void ShootForwardUp() {
        rg2D.AddRelativeForce(vectorForwardUp, ForceMode2D.Force);
    }
    public void ShootForwardDown() {
        rg2D.AddRelativeForce(vectorForwardDown, ForceMode2D.Force);
    }
}
