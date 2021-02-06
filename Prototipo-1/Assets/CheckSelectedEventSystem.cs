using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckSelectedEventSystem : MonoBehaviour
{

    public GameObject newFirstGo;
    void Update()
    {
        if (EventSystem.current != null)
        {
            if (!EventSystem.current.currentSelectedGameObject)
            {
                EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
            }
        }
    }
    public void NewObjectCurrentFirst()
    {
        EventSystem.current.SetSelectedGameObject(newFirstGo);
    }
}
