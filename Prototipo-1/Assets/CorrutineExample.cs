using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrutineExample : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(MyCorrutine());
    }

    public IEnumerator MyCorrutine()
    {
        for (int i = 0; i < 10; i++)
        {
            transform.position += Vector3.forward * 3.0f;
            yield return new WaitForSeconds(1.0f);
        }
    }
}
