using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    // Start is called before the first frame update
    public float distanceOfMove;
    public void MoveLeft()
    {
        transform.position = transform.position + new Vector3(-distanceOfMove, 0, 0);
    }
    public void MoveRight()
    {
        transform.position = transform.position + new Vector3(distanceOfMove, 0, 0);
    }
    public void MoveUp()
    {
        transform.position = transform.position + new Vector3(0, distanceOfMove, 0);
    }

    public void MoveDown()
    {
        transform.position = transform.position + new Vector3(0, -distanceOfMove, 0);
    }
}
