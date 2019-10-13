using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2
{
    public class Gaseosa : MonoBehaviour
    {
        // Start is called before the first frame update
        public float damage;
        public float timeLife;
        private float auxTimeLife;
        public SpriteRenderer spriteRenderer;

        private void OnEnable()
        {
            timeLife = auxTimeLife;
        }

        public void CheckTimeLife()
        {
            if (timeLife > 0)
            {
                timeLife = timeLife - Time.deltaTime;
            }
            else if (timeLife <= 0)
            {
                gameObject.SetActive(false);
            }
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            
        }
    }
}
