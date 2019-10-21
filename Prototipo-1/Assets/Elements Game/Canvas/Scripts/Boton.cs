using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace Prototipo_2
{
    public class Boton : MonoBehaviour
    {
        // Start is called before the first frame update
        public string nameLoadScene;
        public Color[] colores;
        public Image buttonImage;
        public Button button;
        public bool selected = false;
        private int selectedColor = 1;
        private int deselectedColor = 0;
        void Start()
        {
            buttonImage = GetComponent<Image>();
            buttonImage.color = colores[deselectedColor];
        }
        private void FixedUpdate()
        {
            CheckSelectedButton();
        }
        // Update is called once per frame
        void Update()
        {

        }
        public void CheckSelectedButton()
        {
            if (selected)
            {
                buttonImage.enabled = true;
            }
            else
            {
                buttonImage.enabled = false;
            }
        }
    }
}
