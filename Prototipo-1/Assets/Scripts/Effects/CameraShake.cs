using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private bool inShake;
    public GameObject Camera;
    private void Start()
    {
        inShake = false;
        if (Camera == null)
        {
            Camera = GameObject.Find("Main Camera");
        }
    }
    public bool GetInShake()
    {
        return inShake;
    }
    public IEnumerator Shake(float duration, float magnitude)
    {
        if (Camera != null)
        {
            Vector3 originalPos = Camera.transform.localPosition;

            float elapsed = 0.0f;
            inShake = true;
            while (elapsed < duration)
            {
                float x = Random.Range(-1, 1) * magnitude;
                float y = Random.Range(-1, 1) * magnitude;

                Camera.transform.localPosition = new Vector3(x, y, originalPos.z);

                elapsed = elapsed + Time.deltaTime;

                yield return null;
            }
            inShake = false;
            Camera.transform.localPosition = originalPos;
        }
        else 
        {
            inShake = false;
        }
    }
}
