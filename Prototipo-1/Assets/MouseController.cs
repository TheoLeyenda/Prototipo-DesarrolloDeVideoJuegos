using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    void Start()
    {
        LockedAndInvisibleCursor();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            LockedAndInvisibleCursor();
        }
    }
    public void LockedAndInvisibleCursor()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }
}
