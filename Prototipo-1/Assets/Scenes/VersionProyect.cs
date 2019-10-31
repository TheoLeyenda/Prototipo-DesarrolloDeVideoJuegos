using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VersionProyect : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI textoDeVersion;

    void Start()
    {
        textoDeVersion.text = "v" + Application.version;
    }

}
